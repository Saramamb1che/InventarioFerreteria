Imports MySqlConnector
Imports System.Net.Mail
Imports System.Text.RegularExpressions

Public Class FrmProveedores
    Private ReadOnly repositorio As New ProveedorRepositorio()
    Private idSeleccionado As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub AlCargar(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarLista()
        Nuevo()
    End Sub

    Private Sub CargarLista()
        Try
            dgvProveedores.DataSource = repositorio.Listar(txtBuscar.Text)
            dgvProveedores.Columns("IdProveedor").Visible = False
            dgvProveedores.Columns("Ruc").HeaderText = "RUC"
            dgvProveedores.Columns("Telefono").HeaderText = "Teléfono"
            dgvProveedores.Columns("Nombre").FillWeight = 160
            dgvProveedores.ClearSelection()
            lblEstado.Text = $"{dgvProveedores.Rows.Count} proveedor(es)"
        Catch ex As MySqlException
            MostrarError(ex)
        End Try
    End Sub

    Private Sub Nuevo()
        idSeleccionado = 0
        txtNombre.Clear()
        txtRuc.Clear()
        txtTelefono.Clear()
        txtCorreo.Clear()
        errValidacion.Clear()
        btnAgregar.Enabled = True
        btnActualizar.Enabled = False
        btnEliminar.Enabled = False
        dgvProveedores.ClearSelection()
    End Sub

    Private Function Validar() As Boolean
        errValidacion.Clear()
        Dim correcto = True
        If String.IsNullOrWhiteSpace(txtNombre.Text) Then
            errValidacion.SetError(txtNombre, "Escriba el nombre.")
            correcto = False
        End If
        If String.IsNullOrWhiteSpace(txtRuc.Text) Then
            errValidacion.SetError(txtRuc, "Escriba el RUC.")
            correcto = False
        End If
        If Not Regex.IsMatch(txtTelefono.Text, "^[0-9]{8}$") Then
            errValidacion.SetError(txtTelefono, "El teléfono debe tener 8 dígitos.")
            correcto = False
        End If
        Dim direccion As MailAddress = Nothing
        If Not MailAddress.TryCreate(txtCorreo.Text.Trim(), direccion) OrElse
            direccion.Address <> txtCorreo.Text.Trim() Then
            errValidacion.SetError(txtCorreo, "Escriba un correo válido.")
            correcto = False
        End If
        Return correcto
    End Function

    Private Function LeerFormulario() As Proveedor
        Return New Proveedor With {.IdProveedor = idSeleccionado,
            .Nombre = txtNombre.Text.Trim(), .Ruc = txtRuc.Text.Trim(),
            .Telefono = txtTelefono.Text.Trim(), .Correo = txtCorreo.Text.Trim()}
    End Function

    Private Sub Seleccionar(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProveedores.CellClick
        If e.RowIndex < 0 Then Return
        Dim p = DirectCast(dgvProveedores.Rows(e.RowIndex).DataBoundItem, Proveedor)
        idSeleccionado = p.IdProveedor
        txtNombre.Text = p.Nombre
        txtRuc.Text = p.Ruc
        txtTelefono.Text = p.Telefono
        txtCorreo.Text = p.Correo
        errValidacion.Clear()
        btnAgregar.Enabled = False
        btnActualizar.Enabled = True
        btnEliminar.Enabled = True
    End Sub

    Private Sub Agregar(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If Not Validar() Then Return
        Try
            Dim id = repositorio.Insertar(LeerFormulario())
            CargarLista()
            Nuevo()
            lblEstado.Text = $"Proveedor agregado con ID {id}."
        Catch ex As MySqlException
            MostrarError(ex)
        End Try
    End Sub

    Private Sub Actualizar(sender As Object, e As EventArgs) Handles btnActualizar.Click
        If idSeleccionado = 0 OrElse Not Validar() Then Return
        Try
            Dim filas = repositorio.Actualizar(LeerFormulario())
            CargarLista()
            Nuevo()
            lblEstado.Text = If(filas = 0, "El proveedor ya no existe.", "Proveedor actualizado.")
        Catch ex As MySqlException
            MostrarError(ex)
        End Try
    End Sub

    Private Sub Eliminar(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If idSeleccionado = 0 Then Return
        If MessageBox.Show($"¿Eliminar a «{txtNombre.Text}»?", "Eliminar proveedor",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then Return
        Try
            Dim filas = repositorio.Eliminar(idSeleccionado)
            CargarLista()
            Nuevo()
            lblEstado.Text = If(filas = 0, "El proveedor ya no existe.", "Proveedor eliminado.")
        Catch ex As MySqlException
            MostrarError(ex)
        End Try
    End Sub

    Private Sub Buscar(sender As Object, e As EventArgs) Handles btnBuscar.Click
        CargarLista()
        Nuevo()
    End Sub

    Private Sub BuscarConEnter(sender As Object, e As KeyEventArgs) Handles txtBuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnBuscar.PerformClick()
        End If
    End Sub

    Private Sub Limpiar(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Nuevo()
        txtNombre.Focus()
    End Sub

    Private Sub MostrarError(ex As MySqlException)
        Dim mensaje As String
        Select Case ex.Number
            Case 1062 : mensaje = "Ya existe un proveedor con ese RUC."
            Case 1451 : mensaje = "Este proveedor tiene productos asignados. Reasígnelos antes de eliminarlo."
            Case 1146 : mensaje = "Falta preparar la tabla de proveedores con el script 04."
            Case Else : mensaje = "No se pudo completar la operación: " & ex.Message
        End Select
        lblEstado.Text = "Operación no realizada."
        MessageBox.Show(mensaje, "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub
End Class
