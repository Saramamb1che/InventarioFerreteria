Imports System.Data
Imports MySqlConnector
Public Class ProductoRepositorio
    Public Property UsaProveedores As Boolean
    Public Function Listar(Optional filtro As String = "", Optional soloActivos As Boolean = False) As DataTable
        Const sql As String =
            "SELECT p.id_producto, p.codigo, p.nombre, c.nombre AS categoria, " &
            "       p.unidad, p.precio, p.existencia, p.activo " &
            "FROM productos p " &
            "INNER JOIN categorias c ON c.id_categoria = p.id_categoria " &
            "WHERE (p.codigo LIKE @filtro OR p.nombre LIKE @filtro) " &
            "AND (@soloActivos = 0 OR p.activo = 1) " &
            "ORDER BY p.nombre;"

        Dim tabla As New DataTable("productos")

        Using cn As MySqlConnection = ObtenerConexion(),
              da As New MySqlDataAdapter(sql, cn)
            da.SelectCommand.Parameters.AddWithValue("@filtro", $"%{filtro.Trim()}%")
            da.SelectCommand.Parameters.AddWithValue("@soloActivos", soloActivos)
            da.Fill(tabla)        ' Fill abre y cierra la conexión por sí solo
        End Using

        Return tabla
    End Function
    Public Function ObtenerPorId(id As Integer) As Producto
        Dim sql As String =
            "SELECT " & If(UsaProveedores, "id_proveedor, ", "NULL AS id_proveedor, ") &
            "id_producto, codigo, nombre, id_categoria, unidad, " &
            "       precio, existencia, activo " &
            "FROM productos WHERE id_producto = @id;"

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()

            Using dr As MySqlDataReader = cmd.ExecuteReader()
                If Not dr.Read() Then Return Nothing   ' no existe

                Return New Producto With {
                    .IdProducto = dr.GetInt32("id_producto"),
                    .IdProveedor = If(dr.IsDBNull(dr.GetOrdinal("id_proveedor")),
                        CType(Nothing, Integer?), dr.GetInt32("id_proveedor")),
                    .Codigo = dr.GetString("codigo"),
                    .Nombre = dr.GetString("nombre"),
                    .IdCategoria = dr.GetInt32("id_categoria"),
                    .Unidad = dr.GetString("unidad"),
                    .Precio = dr.GetDecimal("precio"),
                    .Existencia = dr.GetInt32("existencia"),
                    .Activo = dr.GetBoolean("activo")
                }
            End Using
        End Using
    End Function
    Public Function ExisteCodigo(codigo As String, idExcluir As Integer) As Boolean
        Const sql As String =
            "SELECT COUNT(*) FROM productos " &
            "WHERE codigo = @codigo AND id_producto <> @id;"

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.Parameters.AddWithValue("@id", idExcluir)
            cn.Open()
            Return Convert.ToInt64(cmd.ExecuteScalar()) > 0
        End Using
    End Function
    Public Function Insertar(p As Producto) As Integer
        Dim sql As String =
            "INSERT INTO productos " &
            "  (codigo, nombre, id_categoria, unidad, precio, existencia, activo" &
            If(UsaProveedores, ", id_proveedor", "") & ") " &
            "VALUES " &
            "  (@codigo, @nombre, @idCategoria, @unidad, @precio, @existencia, @activo" &
            If(UsaProveedores, ", @idProveedor", "") & ");"

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            AgregarParametros(cmd, p)
            cn.Open()
            cmd.ExecuteNonQuery()
            Return CInt(cmd.LastInsertedId)
        End Using
    End Function
    Public Function Actualizar(p As Producto) As Integer
        Dim sql As String =
            "UPDATE productos SET " &
            "   codigo = @codigo, nombre = @nombre, id_categoria = @idCategoria, " &
            "   unidad = @unidad, precio = @precio, existencia = @existencia, " &
            "   activo = @activo " & If(UsaProveedores, ", id_proveedor = @idProveedor ", "") &
            "WHERE id_producto = @id;"

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            AgregarParametros(cmd, p)
            cmd.Parameters.AddWithValue("@id", p.IdProducto)
            cn.Open()
            Return cmd.ExecuteNonQuery()
        End Using
    End Function
    Public Function Eliminar(id As Integer) As Integer
        Const sql As String = "DELETE FROM productos WHERE id_producto = @id;"

        Using cn As MySqlConnection = ObtenerConexion(),
              cmd As New MySqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@id", id)
            cn.Open()
            Return cmd.ExecuteNonQuery()
        End Using
    End Function
    Private Sub AgregarParametros(cmd As MySqlCommand, p As Producto)
        If UsaProveedores Then cmd.Parameters.AddWithValue("@idProveedor", If(p.IdProveedor.HasValue, CType(p.IdProveedor.Value, Object), DBNull.Value))
        cmd.Parameters.AddWithValue("@codigo", p.Codigo)
        cmd.Parameters.AddWithValue("@nombre", p.Nombre)
        cmd.Parameters.AddWithValue("@idCategoria", p.IdCategoria)
        cmd.Parameters.AddWithValue("@unidad", p.Unidad)
        cmd.Parameters.AddWithValue("@precio", p.Precio)
        cmd.Parameters.AddWithValue("@existencia", p.Existencia)
        cmd.Parameters.AddWithValue("@activo", p.Activo)
    End Sub

End Class
