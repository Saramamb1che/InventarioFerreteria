using InventarioFerreteria;
using MySqlConnector;

internal static class Program
{
    private static void Exigir(bool condicion, string caso)
    {
        if (!condicion) throw new Exception(caso);
        Console.WriteLine("OK: " + caso);
    }

    private static void ErrorSql(int numero, Action accion)
    {
        try { accion(); }
        catch (MySqlException ex) when (ex.Number == numero)
        { Console.WriteLine("OK: restricción " + numero); return; }
        throw new Exception("Se esperaba el error " + numero);
    }

    [STAThread]
    private static int Main(string[] args)
    {
        if (args.Length != 1) { Console.Error.WriteLine("Indique la carpeta raíz del repositorio."); return 1; }
        var root = Path.GetFullPath(args[0]);
        var temporal = "test_ferreteria_" + Guid.NewGuid().ToString("N");
        using var cn = ConexionBD.ObtenerConexion();
        var creada = false;
        try
        {
            cn.Open();
            void Sql(string sql) { using var cmd = new MySqlCommand(sql, cn); cmd.ExecuteNonQuery(); }
            Sql($"CREATE DATABASE `{temporal}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci");
            creada = true;
            Sql($"USE `{temporal}`");
            var esquema = File.ReadAllText(Path.Combine(root, "database/01_esquema.sql"));
            var tablas = esquema[esquema.IndexOf("CREATE TABLE categorias", StringComparison.Ordinal)..];
            Sql(tablas);
            Sql(File.ReadAllText(Path.Combine(root, "database/03_datos_prueba.sql"))
                .Replace("USE ferreteria_db;", $"USE `{temporal}`;"));
            var builder = new MySqlConnectionStringBuilder(cn.ConnectionString) { Database = temporal };
            // La conexión abierta oculta la contraseña en ConnectionString.
            using (var original = ConexionBD.ObtenerConexion())
                builder = new MySqlConnectionStringBuilder(original.ConnectionString) { Database = temporal };
            Environment.SetEnvironmentVariable("FERRETERIA_CONEXION", builder.ConnectionString);
            var productos = new ProductoRepositorio();
            var proveedores = new ProveedorRepositorio();
            Exigir(new CategoriaRepositorio().Listar().Count == 6, "seis categorías");
            Exigir(productos.Listar().Rows.Count == 14, "catorce productos");
            Exigir(productos.Listar("cable").Rows.Count == 1, "búsqueda por nombre");
            Exigir(!proveedores.EstaInstalado(), "funciona antes de la migración de proveedores");
            var producto = new Producto { Codigo = "TEST-001", Nombre = "Llave D'Angelo", IdCategoria = 1,
                Unidad = "Unidad", Precio = 28.50m, Existencia = 4, Activo = false };
            producto.IdProducto = productos.Insertar(producto);
            Exigir(productos.ObtenerPorId(producto.IdProducto).Nombre == producto.Nombre, "alta y apóstrofos");
            Exigir(productos.Listar("TEST-001", true).Rows.Count == 0, "filtro solo activos");
            Exigir(productos.ExisteCodigo("TEST-001", 0), "detección de código repetido");
            Exigir(!productos.ExisteCodigo("TEST-001", producto.IdProducto), "edición sin falso duplicado");
            ErrorSql(1062, () => productos.Insertar(producto));
            producto.Precio = 0;
            ErrorSql(4025, () => productos.Actualizar(producto));
            producto.Precio = 123.45m;
            producto.Activo = true;
            Exigir(productos.Actualizar(producto) == 1, "actualizar producto");
            Exigir(productos.ObtenerPorId(producto.IdProducto).Precio == 123.45m, "precio decimal exacto");
            Sql(File.ReadAllText(Path.Combine(root, "database/04_proveedores.sql"))
                .Replace("USE ferreteria_db;", $"USE `{temporal}`;"));
            Exigir(proveedores.EstaInstalado(), "migración de proveedores");
            productos.UsaProveedores = true;
            var proveedor = new Proveedor { Nombre = "Distribuidor de prueba", Ruc = "PRUEBA-001",
                Telefono = "88887777", Correo = "prueba@example.com" };
            proveedor.IdProveedor = proveedores.Insertar(proveedor);
            Exigir(proveedores.Listar("PRUEBA-001").Count == 1, "alta y búsqueda de proveedor");
            ErrorSql(1062, () => proveedores.Insertar(proveedor));
            proveedor.Telefono = "123";
            ErrorSql(4025, () => proveedores.Actualizar(proveedor));
            proveedor.Telefono = "77776666";
            Exigir(proveedores.Actualizar(proveedor) == 1, "actualizar proveedor");
            producto.IdProveedor = proveedor.IdProveedor;
            productos.Actualizar(producto);
            Exigir(productos.ObtenerPorId(producto.IdProducto).IdProveedor == proveedor.IdProveedor,
                "producto asociado a proveedor");
            ErrorSql(1451, () => proveedores.Eliminar(proveedor.IdProveedor));
            Exigir(productos.Eliminar(producto.IdProducto) == 1, "eliminar producto de prueba");
            Exigir(proveedores.Eliminar(proveedor.IdProveedor) == 1, "eliminar proveedor sin productos");
            using var ventanaProductos = new FrmProductos();
            using var ventanaProveedores = new FrmProveedores();
            Exigir(ventanaProductos.Controls.Find("cboProveedor", true).Length == 1, "formulario de productos");
            Exigir(ventanaProveedores.Controls.Find("dgvProveedores", true).Length == 1, "formulario de proveedores");
            object Invocar(object instancia, string metodo, params object[] parametros) =>
                instancia.GetType().GetMethod(metodo,
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                    .Invoke(instancia, parametros)!;
            Invocar(ventanaProductos, "PrepararNuevo");
            Exigir(!(bool)Invocar(ventanaProductos, "ValidarFormulario"), "formulario rechaza producto vacío");
            Exigir(!(bool)Invocar(ventanaProductos, "SeleccionarFila", -1), "selección detecta producto fuera de la lista");
            Exigir(((NumericUpDown)ventanaProductos.Controls.Find("nudPrecio", true)[0]).Maximum == 99999999.99m,
                "precio admite el rango DECIMAL(10,2)");
            Exigir(!(bool)Invocar(ventanaProveedores, "Validar"), "formulario rechaza proveedor vacío");
            ((TextBox)ventanaProveedores.Controls.Find("txtNombre", true)[0]).Text = "Proveedor de prueba";
            ((TextBox)ventanaProveedores.Controls.Find("txtRuc", true)[0]).Text = "PRUEBA-VALIDACION";
            ((TextBox)ventanaProveedores.Controls.Find("txtTelefono", true)[0]).Text = "88887777";
            ((TextBox)ventanaProveedores.Controls.Find("txtCorreo", true)[0]).Text = "correo incorrecto";
            Exigir(!(bool)Invocar(ventanaProveedores, "Validar"), "formulario rechaza correo inválido");
            ((TextBox)ventanaProveedores.Controls.Find("txtCorreo", true)[0]).Text = "prueba@example.com";
            Exigir((bool)Invocar(ventanaProveedores, "Validar"), "formulario acepta proveedor válido");
            Exigir(productos.Listar().Rows.Count == 14, "datos originales de prueba conservados");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
        finally
        {
            Environment.SetEnvironmentVariable("FERRETERIA_CONEXION", null);
            if (creada)
            {
                using var borrar = new MySqlCommand($"DROP DATABASE `{temporal}`", cn);
                borrar.ExecuteNonQuery();
                Console.WriteLine("Base temporal eliminada.");
            }
        }
    }
}
