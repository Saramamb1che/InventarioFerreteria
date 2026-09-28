Imports MySqlConnector
Public Class FrmProductos
    Private ReadOnly _productos As New ProductoRepositorio()
    Private ReadOnly _categorias As New CategoriaRepositorio()
    Private _idSeleccionado As Integer = 0
    Private ReadOnly _proveedores As New ProveedorRepositorio()

Private Sub FrmProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Dim mensaje As String = ""

    If Not ConexionBD.ProbarConexion(mensaje) Then
        MessageBox.Show("No fue posible conectar con MariaDB." & vbCrLf & mensaje,
                        "Sin conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        lblEstado.Text = "Sin conexión: revise el servicio MariaDB y ConexionBD.vb"
        grpDatos.Enabled = False
        grpAcciones.Enabled = False
        txtBuscar.Enabled = False
        btnBuscar.Enabled = False
        btnProveedores.Enabled = False
        chkSoloActivos.Enabled = False
        Return
    End If

    lblEstado.Text = mensaje
    Try
        _productos.UsaProveedores = _proveedores.EstaInstalado()
        cboProveedor.Enabled = _productos.UsaProveedores
        CargarProveedores()
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
    CargarCategorias()
    CargarProductos()
    PrepararNuevo()
End Sub
Private Sub CargarCategorias()
    Try
        cboCategoria.DisplayMember = "Nombre"        ' lo que ve el usuario
        cboCategoria.ValueMember = "IdCategoria"     ' lo que usa el programa
        cboCategoria.DataSource = _categorias.Listar()
        cboCategoria.SelectedIndex = -1
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub
Private Sub CargarProductos(Optional filtro As String = "")
    Try
        dgvProductos.DataSource = _productos.Listar(filtro, chkSoloActivos.Checked)
        FormatearGrid()
        dgvProductos.ClearSelection()
        lblTotal.Text = $"{dgvProductos.Rows.Count} producto(s)"
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub
Private Sub FormatearGrid()
    With dgvProductos
        .Columns("id_producto").Visible = False
        .Columns("codigo").HeaderText = "Código"
        .Columns("nombre").HeaderText = "Producto"
        .Columns("nombre").FillWeight = 220
        .Columns("categoria").HeaderText = "Categoría"
        .Columns("unidad").HeaderText = "Unidad"
        .Columns("precio").HeaderText = "Precio"
        .Columns("precio").DefaultCellStyle.Format = "'C$' #,##0.00"
        .Columns("precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("existencia").HeaderText = "Existencia"
        .Columns("existencia").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        .Columns("activo").HeaderText = "Activo"
    End With
End Sub

Private Sub dgvProductos_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
    Handles dgvProductos.CellClick

    If e.RowIndex < 0 Then Return                   ' clic en el encabezado

    Dim id As Integer = CInt(dgvProductos.Rows(e.RowIndex).Cells("id_producto").Value)

    Try
        Dim p As Producto = _productos.ObtenerPorId(id)
        If p Is Nothing Then
            lblEstado.Text = "Ese producto ya no existe; se recargó la lista."
            CargarProductos(txtBuscar.Text)
            PrepararNuevo()
            Return
        End If
        MostrarProducto(p)
        PrepararEdicion()
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub

Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
    CargarProductos(txtBuscar.Text)
    PrepararNuevo()
End Sub

Private Sub txtBuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBuscar.KeyDown
    If e.KeyCode = Keys.Enter Then
        e.SuppressKeyPress = True                   ' evita el "beep"
        btnBuscar.PerformClick()
    End If
End Sub

Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
    PrepararNuevo()
    txtCodigo.Focus()
End Sub

Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
    Try
        If Not ValidarFormulario() Then Return

        Dim nuevo As Producto = LeerFormulario()
        Dim idNuevo As Integer = _productos.Insertar(nuevo)

        CargarProductos(txtBuscar.Text)
        PrepararNuevo()
        lblEstado.Text = $"Agregado: «{nuevo.Nombre}» con ID {idNuevo}."
        txtCodigo.Focus()
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub

Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
    If _idSeleccionado = 0 Then Return

    Try
        If Not ValidarFormulario() Then Return

        Dim editado As Producto = LeerFormulario()
        Dim filas As Integer = _productos.Actualizar(editado)

        If filas = 0 Then
            CargarProductos(txtBuscar.Text)
            PrepararNuevo()
            lblEstado.Text = "El producto ya no existe; se recargó la lista."
            Return
        Else
            lblEstado.Text = $"Actualizado: «{editado.Nombre}»."
        End If

        CargarProductos(txtBuscar.Text)
        If Not SeleccionarFila(editado.IdProducto) Then PrepararNuevo()
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub

Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
    If _idSeleccionado = 0 Then Return

    Dim respuesta As DialogResult =
        MessageBox.Show($"¿Eliminar definitivamente «{txtNombre.Text}»?" & vbCrLf &
                        "Esta acción no se puede deshacer.",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2)

    If respuesta <> DialogResult.Yes Then Return

    Try
        Dim nombre As String = txtNombre.Text
        Dim filas = _productos.Eliminar(_idSeleccionado)
        CargarProductos(txtBuscar.Text)
        PrepararNuevo()
        lblEstado.Text = If(filas = 0, "El producto ya había sido eliminado.", $"Eliminado: «{nombre}».")
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub
Private Function LeerFormulario() As Producto
    Return New Producto With {
        .IdProducto = _idSeleccionado,
        .IdProveedor = If(cboProveedor.SelectedValue Is Nothing OrElse CInt(cboProveedor.SelectedValue) = 0,
            CType(Nothing, Integer?), CInt(cboProveedor.SelectedValue)),
        .Codigo = txtCodigo.Text.Trim(),
        .Nombre = txtNombre.Text.Trim(),
        .IdCategoria = CInt(cboCategoria.SelectedValue),
        .Unidad = cboUnidad.Text,
        .Precio = nudPrecio.Value,
        .Existencia = CInt(nudExistencia.Value),
        .Activo = chkActivo.Checked
    }
End Function
Private Sub MostrarProducto(p As Producto)
    _idSeleccionado = p.IdProducto
    lblIdValor.Text = p.IdProducto.ToString()
    txtCodigo.Text = p.Codigo
    txtNombre.Text = p.Nombre
    cboCategoria.SelectedValue = p.IdCategoria
    cboProveedor.SelectedValue = p.IdProveedor.GetValueOrDefault()
    cboUnidad.SelectedItem = p.Unidad
    nudPrecio.Value = p.Precio
    nudExistencia.Value = p.Existencia
    chkActivo.Checked = p.Activo
    errValidacion.Clear()
End Sub
Private Function ValidarFormulario() As Boolean
    errValidacion.Clear()
    Dim valido As Boolean = True

    If String.IsNullOrWhiteSpace(txtCodigo.Text) Then
        errValidacion.SetError(txtCodigo, "El código es obligatorio.")
        valido = False
    ElseIf _productos.ExisteCodigo(txtCodigo.Text.Trim(), _idSeleccionado) Then
        errValidacion.SetError(txtCodigo, "Otro producto ya usa este código.")
        valido = False
    End If

    If String.IsNullOrWhiteSpace(txtNombre.Text) Then
        errValidacion.SetError(txtNombre, "Escriba el nombre del producto.")
        valido = False
    End If

    If cboCategoria.SelectedIndex < 0 Then
        errValidacion.SetError(cboCategoria, "Seleccione una categoría.")
        valido = False
    End If

    If cboUnidad.SelectedIndex < 0 Then
        errValidacion.SetError(cboUnidad, "Seleccione la unidad de venta.")
        valido = False
    End If

    If nudPrecio.Value <= 0D Then
        errValidacion.SetError(nudPrecio, "El precio debe ser mayor que C$ 0.00.")
        valido = False
    End If

    If Not valido Then lblEstado.Text = "Revise los campos marcados en rojo."
    Return valido
End Function
Private Sub PrepararNuevo()
    _idSeleccionado = 0
    lblIdValor.Text = "(nuevo)"
    txtCodigo.Clear()
    txtNombre.Clear()
    cboCategoria.SelectedIndex = -1
    If cboProveedor.Items.Count > 0 Then cboProveedor.SelectedIndex = 0
    cboUnidad.SelectedIndex = 0
    nudPrecio.Value = 0D
    nudExistencia.Value = 0D
    chkActivo.Checked = True
    errValidacion.Clear()

    btnAgregar.Enabled = True
    btnActualizar.Enabled = False
    btnEliminar.Enabled = False
    dgvProductos.ClearSelection()
End Sub
Private Sub PrepararEdicion()
    btnAgregar.Enabled = False
    btnActualizar.Enabled = True
    btnEliminar.Enabled = True
End Sub
Private Function SeleccionarFila(id As Integer) As Boolean
    For Each fila As DataGridViewRow In dgvProductos.Rows
        If CInt(fila.Cells("id_producto").Value) = id Then
            fila.Selected = True
            dgvProductos.FirstDisplayedScrollingRowIndex = fila.Index
            Return True
        End If
    Next
    Return False
End Function
Private Sub MostrarErrorBD(ex As MySqlException)
    Dim texto As String

    Select Case ex.Number
        Case 1042 : texto = "No se encontró el servidor MariaDB. ¿Está iniciado el servicio?"
        Case 1045 : texto = "Usuario o contraseña de MariaDB incorrectos (ConexionBD.vb)."
        Case 1049 : texto = "La base ferreteria_db no existe. Ejecute database/01_esquema.sql."
        Case 1062 : texto = "Ya existe un producto con ese código."
        Case 1142 : texto = "El usuario ferre_app no tiene permiso para esa operación."
        Case 1451 : texto = "No se puede eliminar: otros registros dependen de este."
        Case 1452 : texto = "La categoría o el proveedor seleccionado ya no existe. Actualice la lista."
        Case 4025 : texto = "Un valor no cumple las reglas de la tabla (precio o existencia)."
        Case Else : texto = ex.Message
    End Select

    lblEstado.Text = $"Error {ex.Number}"
    MessageBox.Show(texto, $"Error de base de datos ({ex.Number})",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
End Sub
Private Sub CargarProveedores()
    If Not _productos.UsaProveedores Then Return
    Dim actual = If(cboProveedor.SelectedValue Is Nothing, 0, CInt(cboProveedor.SelectedValue))
    Dim lista = _proveedores.Listar()
    lista.Insert(0, New Proveedor With {.IdProveedor = 0, .Nombre = "(Sin proveedor)"})
    cboProveedor.DisplayMember = "Nombre"
    cboProveedor.ValueMember = "IdProveedor"
    cboProveedor.DataSource = lista
    cboProveedor.SelectedValue = If(lista.Any(Function(p) p.IdProveedor = actual), actual, 0)
End Sub

Private Sub AbrirProveedores(sender As Object, e As EventArgs) Handles btnProveedores.Click
    If Not _productos.UsaProveedores Then
        MessageBox.Show("Primero ejecute database/04_proveedores.sql en HeidiSQL y reinicie la aplicación.",
                        "Preparar proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Return
    End If
    Using formulario As New FrmProveedores()
        formulario.ShowDialog(Me)
    End Using
    Try
        CargarProveedores()
    Catch ex As MySqlException
        MostrarErrorBD(ex)
    End Try
End Sub

Private Sub FiltrarActivos(sender As Object, e As EventArgs) Handles chkSoloActivos.CheckedChanged
    CargarProductos(txtBuscar.Text)
    PrepararNuevo()
End Sub

Private Sub ColorearExistencias(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvProductos.CellFormatting
    If e.RowIndex < 0 OrElse Not dgvProductos.Columns.Contains("existencia") Then Return
    Dim fila = dgvProductos.Rows(e.RowIndex)
    If fila.Cells("existencia").Value Is Nothing OrElse IsDBNull(fila.Cells("existencia").Value) Then Return
    fila.DefaultCellStyle.BackColor = If(CInt(fila.Cells("existencia").Value) < 10, Color.MistyRose, Color.White)
End Sub
End Class
