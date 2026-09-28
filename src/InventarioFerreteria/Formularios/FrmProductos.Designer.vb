<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmProductos
    Inherits System.Windows.Forms.Form
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        cboProveedor = New ComboBox()
        lblProveedor = New Label()
        btnProveedores = New Button()
        chkSoloActivos = New CheckBox()
        lblTitulo = New Label()
        grpDatos = New GroupBox()
        lblId = New Label()
        lblIdValor = New Label()
        lblCodigo = New Label()
        txtCodigo = New TextBox()
        lblNombre = New Label()
        txtNombre = New TextBox()
        lblCategoria = New Label()
        cboCategoria = New ComboBox()
        lblUnidad = New Label()
        cboUnidad = New ComboBox()
        lblPrecio = New Label()
        nudPrecio = New NumericUpDown()
        lblExistencia = New Label()
        nudExistencia = New NumericUpDown()
        chkActivo = New CheckBox()
        grpAcciones = New GroupBox()
        btnNuevo = New Button()
        btnAgregar = New Button()
        btnActualizar = New Button()
        btnEliminar = New Button()
        lblBuscar = New Label()
        txtBuscar = New TextBox()
        btnBuscar = New Button()
        dgvProductos = New DataGridView()
        ssEstado = New StatusStrip()
        lblEstado = New ToolStripStatusLabel()
        lblTotal = New ToolStripStatusLabel()
        errValidacion = New ErrorProvider(components)
        ttAyuda = New ToolTip(components)
        grpDatos.SuspendLayout()
        CType(nudPrecio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(nudExistencia, System.ComponentModel.ISupportInitialize).BeginInit()
        grpAcciones.SuspendLayout()
        CType(dgvProductos, System.ComponentModel.ISupportInitialize).BeginInit()
        ssEstado.SuspendLayout()
        CType(errValidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        lblTitulo.Location = New Point(16, 12)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Text = "Inventario de productos"
        grpDatos.Controls.Add(lblId)
        grpDatos.Controls.Add(lblIdValor)
        grpDatos.Controls.Add(lblCodigo)
        grpDatos.Controls.Add(txtCodigo)
        grpDatos.Controls.Add(lblNombre)
        grpDatos.Controls.Add(txtNombre)
        grpDatos.Controls.Add(lblCategoria)
        grpDatos.Controls.Add(cboCategoria)
        grpDatos.Controls.Add(lblUnidad)
        grpDatos.Controls.Add(cboUnidad)
        grpDatos.Controls.Add(lblPrecio)
        grpDatos.Controls.Add(nudPrecio)
        grpDatos.Controls.Add(lblExistencia)
        grpDatos.Controls.Add(nudExistencia)
        grpDatos.Controls.Add(chkActivo)
        grpDatos.Location = New Point(16, 52)
        grpDatos.Name = "grpDatos"
        grpDatos.Size = New Size(340, 346)
        grpDatos.TabIndex = 0
        grpDatos.TabStop = False
        grpDatos.Text = "Datos del producto"
        lblId.AutoSize = True
        lblId.Location = New Point(16, 32)
        lblId.Name = "lblId"
        lblId.Text = "ID:"
        lblIdValor.AutoSize = True
        lblIdValor.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblIdValor.Location = New Point(120, 32)
        lblIdValor.Name = "lblIdValor"
        lblIdValor.Text = "(nuevo)"
        lblCodigo.AutoSize = True
        lblCodigo.Location = New Point(16, 66)
        lblCodigo.Name = "lblCodigo"
        lblCodigo.Text = "Código:"
        txtCodigo.CharacterCasing = CharacterCasing.Upper
        txtCodigo.Location = New Point(120, 63)
        txtCodigo.MaxLength = 15
        txtCodigo.Name = "txtCodigo"
        txtCodigo.Size = New Size(200, 23)
        txtCodigo.TabIndex = 1
        lblNombre.AutoSize = True
        lblNombre.Location = New Point(16, 100)
        lblNombre.Name = "lblNombre"
        lblNombre.Text = "Nombre:"
        txtNombre.Location = New Point(120, 97)
        txtNombre.MaxLength = 100
        txtNombre.Name = "txtNombre"
        txtNombre.Size = New Size(200, 23)
        txtNombre.TabIndex = 2
        lblCategoria.AutoSize = True
        lblCategoria.Location = New Point(16, 134)
        lblCategoria.Name = "lblCategoria"
        lblCategoria.Text = "Categoría:"
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategoria.Location = New Point(120, 131)
        cboCategoria.Name = "cboCategoria"
        cboCategoria.Size = New Size(200, 23)
        cboCategoria.TabIndex = 3
        lblUnidad.AutoSize = True
        lblUnidad.Location = New Point(16, 168)
        lblUnidad.Name = "lblUnidad"
        lblUnidad.Text = "Unidad:"
        cboUnidad.DropDownStyle = ComboBoxStyle.DropDownList
        cboUnidad.Items.AddRange(New Object() {"Unidad", "Libra", "Galón", "Metro", "Bolsa", "Caja", "Rollo"})
        cboUnidad.Location = New Point(120, 165)
        cboUnidad.Name = "cboUnidad"
        cboUnidad.Size = New Size(200, 23)
        cboUnidad.TabIndex = 4
        lblPrecio.AutoSize = True
        lblPrecio.Location = New Point(16, 202)
        lblPrecio.Name = "lblPrecio"
        lblPrecio.Text = "Precio (C$):"
        nudPrecio.DecimalPlaces = 2
        nudPrecio.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        nudPrecio.Location = New Point(120, 199)
        nudPrecio.Maximum = New Decimal(New Integer() {1410065407, 2, 0, 131072})
        nudPrecio.Name = "nudPrecio"
        nudPrecio.Size = New Size(130, 23)
        nudPrecio.TabIndex = 5
        nudPrecio.TextAlign = HorizontalAlignment.Right
        nudPrecio.ThousandsSeparator = True
        lblExistencia.AutoSize = True
        lblExistencia.Location = New Point(16, 236)
        lblExistencia.Name = "lblExistencia"
        lblExistencia.Text = "Existencia:"
        nudExistencia.Location = New Point(120, 233)
        nudExistencia.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
        nudExistencia.Name = "nudExistencia"
        nudExistencia.Size = New Size(130, 23)
        nudExistencia.TabIndex = 6
        nudExistencia.TextAlign = HorizontalAlignment.Right
        nudExistencia.ThousandsSeparator = True
        chkActivo.AutoSize = True
        chkActivo.Checked = True
        chkActivo.CheckState = CheckState.Checked
        chkActivo.Location = New Point(120, 268)
        chkActivo.Name = "chkActivo"
        chkActivo.TabIndex = 7
        chkActivo.Text = "Producto activo"
        grpAcciones.Controls.Add(btnNuevo)
        grpAcciones.Controls.Add(btnAgregar)
        grpAcciones.Controls.Add(btnActualizar)
        grpAcciones.Controls.Add(btnEliminar)
        grpAcciones.Location = New Point(16, 408)
        grpAcciones.Name = "grpAcciones"
        grpAcciones.Size = New Size(340, 124)
        grpAcciones.TabIndex = 1
        grpAcciones.TabStop = False
        grpAcciones.Text = "Operaciones"
        btnNuevo.Location = New Point(16, 28)
        btnNuevo.Name = "btnNuevo"
        btnNuevo.Size = New Size(148, 36)
        btnNuevo.TabIndex = 0
        btnNuevo.Text = "&Nuevo"
        ttAyuda.SetToolTip(btnNuevo, "Limpia el formulario para registrar otro producto")
        btnAgregar.Location = New Point(176, 28)
        btnAgregar.Name = "btnAgregar"
        btnAgregar.Size = New Size(148, 36)
        btnAgregar.TabIndex = 1
        btnAgregar.Text = "&Agregar"
        ttAyuda.SetToolTip(btnAgregar, "INSERT: guarda el producto nuevo")
        btnActualizar.Enabled = False
        btnActualizar.Location = New Point(16, 72)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(148, 36)
        btnActualizar.TabIndex = 2
        btnActualizar.Text = "A&ctualizar"
        ttAyuda.SetToolTip(btnActualizar, "UPDATE: guarda los cambios del producto seleccionado")
        btnEliminar.Enabled = False
        btnEliminar.Location = New Point(176, 72)
        btnEliminar.Name = "btnEliminar"
        btnEliminar.Size = New Size(148, 36)
        btnEliminar.TabIndex = 3
        btnEliminar.Text = "&Eliminar"
        ttAyuda.SetToolTip(btnEliminar, "DELETE: borra el producto seleccionado")
        lblBuscar.AutoSize = True
        lblBuscar.Location = New Point(376, 58)
        lblBuscar.Name = "lblBuscar"
        lblBuscar.Text = "Buscar:"
        txtBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtBuscar.Location = New Point(436, 55)
        txtBuscar.Name = "txtBuscar"
        txtBuscar.PlaceholderText = "Código o nombre del producto"
        txtBuscar.Size = New Size(420, 23)
        txtBuscar.TabIndex = 2
        btnBuscar.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnBuscar.Location = New Point(866, 53)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(118, 27)
        btnBuscar.TabIndex = 3
        btnBuscar.Text = "&Buscar"
        dgvProductos.AllowUserToAddRows = False
        dgvProductos.AllowUserToDeleteRows = False
        dgvProductos.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvProductos.BackgroundColor = SystemColors.Window
        dgvProductos.Location = New Point(376, 122)
        dgvProductos.MultiSelect = False
        dgvProductos.Name = "dgvProductos"
        dgvProductos.ReadOnly = True
        dgvProductos.RowHeadersVisible = False
        dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvProductos.Size = New Size(708, 410)
        dgvProductos.TabIndex = 4
        ssEstado.Items.AddRange(New ToolStripItem() {lblEstado, lblTotal})
        ssEstado.Location = New Point(0, 508)
        ssEstado.Name = "ssEstado"
        ssEstado.Size = New Size(1000, 22)
        lblEstado.Name = "lblEstado"
        lblEstado.Spring = True
        lblEstado.Text = "Listo"
        lblEstado.TextAlign = ContentAlignment.MiddleLeft
        lblTotal.Name = "lblTotal"
        lblTotal.Text = "0 producto(s)"
        errValidacion.BlinkStyle = ErrorBlinkStyle.NeverBlink
        errValidacion.ContainerControl = Me
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1100, 580)
        Controls.Add(lblTitulo)
        Controls.Add(grpDatos)
        Controls.Add(grpAcciones)
        Controls.Add(lblBuscar)
        Controls.Add(txtBuscar)
        Controls.Add(btnBuscar)
        Controls.Add(dgvProductos)
        Controls.Add(ssEstado)
        Font = New Font("Segoe UI", 9.0F)
        MinimumSize = New Size(1116, 619)
        Name = "FrmProductos"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Ferretería Los Robles · Inventario"
                lblProveedor.Text = "Proveedor:"
        lblProveedor.Location = New Point(16, 305)
        lblProveedor.AutoSize = True
        cboProveedor.Name = "cboProveedor"
        cboProveedor.Location = New Point(120, 301)
        cboProveedor.Size = New Size(200, 23)
        cboProveedor.DropDownStyle = ComboBoxStyle.DropDownList
        cboProveedor.TabIndex = 8
        grpDatos.Controls.Add(lblProveedor)
        grpDatos.Controls.Add(cboProveedor)
        btnProveedores.Name = "btnProveedores"
        btnProveedores.Text = "Proveedores"
        btnProveedores.Location = New Point(940, 13)
        btnProveedores.Size = New Size(144, 30)
        btnProveedores.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Controls.Add(btnProveedores)
        chkSoloActivos.Name = "chkSoloActivos"
        chkSoloActivos.Text = "Solo activos"
        chkSoloActivos.AutoSize = True
        chkSoloActivos.Location = New Point(376, 92)
        Controls.Add(chkSoloActivos)
        grpDatos.ResumeLayout(False)
        grpDatos.PerformLayout()
        CType(nudPrecio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(nudExistencia, System.ComponentModel.ISupportInitialize).EndInit()
        grpAcciones.ResumeLayout(False)
        CType(dgvProductos, System.ComponentModel.ISupportInitialize).EndInit()
        ssEstado.ResumeLayout(False)
        ssEstado.PerformLayout()
        CType(errValidacion, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents grpDatos As GroupBox
    Friend WithEvents lblId As Label
    Friend WithEvents lblIdValor As Label
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtCodigo As TextBox
    Friend WithEvents lblNombre As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents lblCategoria As Label
    Friend WithEvents cboCategoria As ComboBox
    Friend WithEvents lblUnidad As Label
    Friend WithEvents cboUnidad As ComboBox
    Friend WithEvents lblPrecio As Label
    Friend WithEvents nudPrecio As NumericUpDown
    Friend WithEvents lblExistencia As Label
    Friend WithEvents nudExistencia As NumericUpDown
    Friend WithEvents chkActivo As CheckBox
    Friend WithEvents grpAcciones As GroupBox
    Friend WithEvents btnNuevo As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents btnEliminar As Button
    Friend WithEvents lblBuscar As Label
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents dgvProductos As DataGridView
    Friend WithEvents ssEstado As StatusStrip
    Friend WithEvents lblEstado As ToolStripStatusLabel
    Friend WithEvents lblTotal As ToolStripStatusLabel
    Friend WithEvents errValidacion As ErrorProvider
    Friend WithEvents ttAyuda As ToolTip
    Friend WithEvents cboProveedor As ComboBox
    Friend WithEvents lblProveedor As Label
    Friend WithEvents btnProveedores As Button
    Friend WithEvents chkSoloActivos As CheckBox
End Class
