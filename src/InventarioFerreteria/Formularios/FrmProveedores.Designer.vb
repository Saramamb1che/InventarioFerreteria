<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProveedores
    Inherits Form
    Private components As System.ComponentModel.IContainer
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        errValidacion = New ErrorProvider(components)
        errValidacion.ContainerControl = Me
        errValidacion.BlinkStyle = ErrorBlinkStyle.NeverBlink
        SuspendLayout()
        lblNombre = New Label()
        lblNombre.Text = "Nombre:"
        lblNombre.Location = New Point(20, 55)
        lblNombre.AutoSize = True
        txtNombre = New TextBox()
        txtNombre.Name = "txtNombre"
        txtNombre.Location = New Point(110, 52)
        txtNombre.Size = New Size(220, 23)
        txtNombre.MaxLength = 100
        txtNombre.TabIndex = 0
        Controls.Add(lblNombre)
        Controls.Add(txtNombre)
        lblRuc = New Label()
        lblRuc.Text = "RUC:"
        lblRuc.Location = New Point(20, 110)
        lblRuc.AutoSize = True
        txtRuc = New TextBox()
        txtRuc.Name = "txtRuc"
        txtRuc.Location = New Point(110, 107)
        txtRuc.Size = New Size(220, 23)
        txtRuc.MaxLength = 20
        txtRuc.TabIndex = 1
        Controls.Add(lblRuc)
        Controls.Add(txtRuc)
        lblTelefono = New Label()
        lblTelefono.Text = "Teléfono:"
        lblTelefono.Location = New Point(20, 165)
        lblTelefono.AutoSize = True
        txtTelefono = New TextBox()
        txtTelefono.Name = "txtTelefono"
        txtTelefono.Location = New Point(110, 162)
        txtTelefono.Size = New Size(220, 23)
        txtTelefono.MaxLength = 8
        txtTelefono.TabIndex = 2
        Controls.Add(lblTelefono)
        Controls.Add(txtTelefono)
        lblCorreo = New Label()
        lblCorreo.Text = "Correo:"
        lblCorreo.Location = New Point(20, 220)
        lblCorreo.AutoSize = True
        txtCorreo = New TextBox()
        txtCorreo.Name = "txtCorreo"
        txtCorreo.Location = New Point(110, 217)
        txtCorreo.Size = New Size(220, 23)
        txtCorreo.MaxLength = 150
        txtCorreo.TabIndex = 3
        Controls.Add(lblCorreo)
        Controls.Add(txtCorreo)
        btnNuevo = New Button()
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Text = "&Nuevo"
        btnNuevo.Location = New Point(20, 290)
        btnNuevo.Size = New Size(150, 36)
        btnNuevo.TabIndex = 4
        Controls.Add(btnNuevo)
        btnAgregar = New Button()
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Text = "&Agregar"
        btnAgregar.Location = New Point(180, 290)
        btnAgregar.Size = New Size(150, 36)
        btnAgregar.TabIndex = 5
        Controls.Add(btnAgregar)
        btnActualizar = New Button()
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Text = "A&ctualizar"
        btnActualizar.Location = New Point(20, 340)
        btnActualizar.Size = New Size(150, 36)
        btnActualizar.TabIndex = 6
        Controls.Add(btnActualizar)
        btnEliminar = New Button()
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Text = "&Eliminar"
        btnEliminar.Location = New Point(180, 340)
        btnEliminar.Size = New Size(150, 36)
        btnEliminar.TabIndex = 7
        Controls.Add(btnEliminar)
        txtBuscar = New TextBox()
        txtBuscar.Name = "txtBuscar"
        txtBuscar.Location = New Point(360, 20)
        txtBuscar.Size = New Size(410, 23)
        txtBuscar.PlaceholderText = "Nombre o RUC"
        txtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        btnBuscar = New Button()
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Text = "&Buscar"
        btnBuscar.Location = New Point(785, 18)
        btnBuscar.Size = New Size(95, 27)
        btnBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        dgvProveedores = New DataGridView()
        dgvProveedores.Name = "dgvProveedores"
        dgvProveedores.Location = New Point(360, 60)
        dgvProveedores.Size = New Size(520, 340)
        dgvProveedores.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvProveedores.ReadOnly = True
        dgvProveedores.AllowUserToAddRows = False
        dgvProveedores.AllowUserToDeleteRows = False
        dgvProveedores.MultiSelect = False
        dgvProveedores.RowHeadersVisible = False
        dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvProveedores.BackgroundColor = SystemColors.Window
        lblEstado = New Label()
        lblEstado.AutoSize = True
        lblEstado.Location = New Point(20, 420)
        lblEstado.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblEstado.Text = "Listo"
        Controls.Add(txtBuscar)
        Controls.Add(btnBuscar)
        Controls.Add(dgvProveedores)
        Controls.Add(lblEstado)
        Name = "FrmProveedores"
        Text = "Ferretería Los Robles · Proveedores"
        Font = New Font("Segoe UI", 9.0F)
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 460)
        MinimumSize = New Size(916, 499)
        StartPosition = FormStartPosition.CenterParent
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents dgvProveedores As DataGridView
    Friend WithEvents lblEstado As Label
    Friend WithEvents errValidacion As ErrorProvider
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents lblNombre As Label
    Friend WithEvents txtRuc As TextBox
    Friend WithEvents lblRuc As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents lblTelefono As Label
    Friend WithEvents txtCorreo As TextBox
    Friend WithEvents lblCorreo As Label
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnEliminar As Button

End Class
