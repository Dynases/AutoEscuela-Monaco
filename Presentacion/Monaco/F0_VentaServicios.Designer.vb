<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class F0_VentaServicios
    Inherits Modelos.ModeloF0

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim SuperTabColorTable2 As DevComponents.DotNetBar.Rendering.SuperTabColorTable = New DevComponents.DotNetBar.Rendering.SuperTabColorTable()
        Dim SuperTabLinearGradientColorTable2 As DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable = New DevComponents.DotNetBar.Rendering.SuperTabLinearGradientColorTable()
        Dim cbServicio_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F0_VentaServicios))
        Dim cbventa_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.MSuperTabControl = New DevComponents.DotNetBar.SuperTabControl()
        Me.MSuperTabControlPanel1 = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelContent = New System.Windows.Forms.Panel()
        Me.PanelButton = New System.Windows.Forms.Panel()
        Me.GpDetalle = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grDetalle = New Janus.Windows.GridEX.GridEX()
        Me.GpPanelServicio = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.grProducto = New Janus.Windows.GridEX.GridEX()
        Me.PanelDatosTop = New System.Windows.Forms.Panel()
        Me.PanelDatos = New System.Windows.Forms.Panel()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.cbServicio = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.tbObservacion = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.lboservacion = New DevComponents.DotNetBar.LabelX()
        Me.lbcredito = New DevComponents.DotNetBar.LabelX()
        Me.tbcredito = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX15 = New DevComponents.DotNetBar.LabelX()
        Me.LabelX14 = New DevComponents.DotNetBar.LabelX()
        Me.cbmoneda = New DevComponents.DotNetBar.Controls.SwitchButton()
        Me.cbventa = New Janus.Windows.GridEX.EditControls.MultiColumnCombo()
        Me.FechaVenta = New DevComponents.Editors.DateTimeAdv.DateTimeInput()
        Me.LabelX8 = New DevComponents.DotNetBar.LabelX()
        Me.tbCliente = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX1 = New DevComponents.DotNetBar.LabelX()
        Me.tbCodigo = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX6 = New DevComponents.DotNetBar.LabelX()
        Me.MSuperTabItem1 = New DevComponents.DotNetBar.SuperTabItem()
        Me.SuperBuscador = New DevComponents.DotNetBar.SuperTabControlPanel()
        Me.PanelBuscador = New System.Windows.Forms.Panel()
        Me.GroupPanel2 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.grVentas = New Janus.Windows.GridEX.GridEX()
        Me.SuperTabItemBuscador = New DevComponents.DotNetBar.SuperTabItem()
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperTabPrincipal.SuspendLayout()
        Me.SuperTabControlPanelRegistro.SuspendLayout()
        Me.PanelSuperior.SuspendLayout()
        Me.PanelInferior.SuspendLayout()
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelToolBar1.SuspendLayout()
        Me.PanelToolBar2.SuspendLayout()
        Me.PanelPrincipal.SuspendLayout()
        Me.PanelUsuario.SuspendLayout()
        Me.PanelNavegacion.SuspendLayout()
        Me.MPanelUserAct.SuspendLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MSuperTabControl.SuspendLayout()
        Me.MSuperTabControlPanel1.SuspendLayout()
        Me.PanelContent.SuspendLayout()
        Me.PanelButton.SuspendLayout()
        Me.GpDetalle.SuspendLayout()
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GpPanelServicio.SuspendLayout()
        CType(Me.grProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDatosTop.SuspendLayout()
        Me.PanelDatos.SuspendLayout()
        CType(Me.cbServicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbcredito, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbventa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FechaVenta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuperBuscador.SuspendLayout()
        Me.PanelBuscador.SuspendLayout()
        Me.GroupPanel2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SuperTabPrincipal
        '
        '
        '
        '
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.SuperTabPrincipal.ControlBox.MenuBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.Name = ""
        Me.SuperTabPrincipal.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.SuperTabPrincipal.ControlBox.MenuBox, Me.SuperTabPrincipal.ControlBox.CloseBox})
        Me.SuperTabPrincipal.Size = New System.Drawing.Size(1179, 853)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelBuscador, 0)
        Me.SuperTabPrincipal.Controls.SetChildIndex(Me.SuperTabControlPanelRegistro, 0)
        '
        'SuperTabControlPanelBuscador
        '
        Me.SuperTabControlPanelBuscador.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanelBuscador.Size = New System.Drawing.Size(1179, 662)
        '
        'SuperTabControlPanelRegistro
        '
        Me.SuperTabControlPanelRegistro.Location = New System.Drawing.Point(0, 28)
        Me.SuperTabControlPanelRegistro.Size = New System.Drawing.Size(1179, 825)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelSuperior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelInferior, 0)
        Me.SuperTabControlPanelRegistro.Controls.SetChildIndex(Me.PanelPrincipal, 0)
        '
        'PanelSuperior
        '
        Me.PanelSuperior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelSuperior.Style.BackColor1.Color = System.Drawing.Color.Yellow
        Me.PanelSuperior.Style.BackColor2.Color = System.Drawing.Color.Khaki
        Me.PanelSuperior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelSuperior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelSuperior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelSuperior.Style.GradientAngle = 90
        '
        'PanelInferior
        '
        Me.PanelInferior.Location = New System.Drawing.Point(0, 781)
        Me.PanelInferior.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelInferior.Style.BackColor1.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.BackColor2.Color = System.Drawing.Color.Gold
        Me.PanelInferior.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelInferior.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelInferior.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelInferior.Style.GradientAngle = 90
        '
        'BubbleBarUsuario
        '
        '
        '
        '
        Me.BubbleBarUsuario.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BackColor = System.Drawing.Color.Transparent
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderBottomWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderLeftWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderRightWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.BorderTopWidth = 1
        Me.BubbleBarUsuario.ButtonBackAreaStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingBottom = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingLeft = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingRight = 3
        Me.BubbleBarUsuario.ButtonBackAreaStyle.PaddingTop = 3
        Me.BubbleBarUsuario.MouseOverTabColors.BorderColor = System.Drawing.SystemColors.Highlight
        Me.BubbleBarUsuario.SelectedTabColors.BorderColor = System.Drawing.Color.Black
        '
        'btnSalir
        '
        '
        'btnGrabar
        '
        '
        'btnEliminar
        '
        '
        'btnModificar
        '
        '
        'btnNuevo
        '
        '
        'PanelPrincipal
        '
        Me.PanelPrincipal.Controls.Add(Me.MSuperTabControl)
        Me.PanelPrincipal.Size = New System.Drawing.Size(1179, 692)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.PanelUsuario, 0)
        Me.PanelPrincipal.Controls.SetChildIndex(Me.MSuperTabControl, 0)
        '
        'btnImprimir
        '
        '
        'btnUltimo
        '
        '
        'btnSiguiente
        '
        '
        'btnAnterior
        '
        '
        'btnPrimero
        '
        '
        'MRlAccion
        '
        '
        '
        '
        Me.MRlAccion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'MSuperTabControl
        '
        Me.MSuperTabControl.BackColor = System.Drawing.Color.White
        '
        '
        '
        '
        '
        '
        Me.MSuperTabControl.ControlBox.CloseBox.Name = ""
        '
        '
        '
        Me.MSuperTabControl.ControlBox.MenuBox.Name = ""
        Me.MSuperTabControl.ControlBox.Name = ""
        Me.MSuperTabControl.ControlBox.SubItems.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabControl.ControlBox.MenuBox, Me.MSuperTabControl.ControlBox.CloseBox})
        Me.MSuperTabControl.Controls.Add(Me.MSuperTabControlPanel1)
        Me.MSuperTabControl.Controls.Add(Me.SuperBuscador)
        Me.MSuperTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MSuperTabControl.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSuperTabControl.ForeColor = System.Drawing.Color.Black
        Me.MSuperTabControl.HorizontalText = False
        Me.MSuperTabControl.Location = New System.Drawing.Point(0, 0)
        Me.MSuperTabControl.Margin = New System.Windows.Forms.Padding(4)
        Me.MSuperTabControl.Name = "MSuperTabControl"
        Me.MSuperTabControl.ReorderTabsEnabled = True
        Me.MSuperTabControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MSuperTabControl.SelectedTabFont = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSuperTabControl.SelectedTabIndex = 0
        Me.MSuperTabControl.Size = New System.Drawing.Size(1179, 692)
        Me.MSuperTabControl.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Right
        Me.MSuperTabControl.TabFont = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSuperTabControl.TabIndex = 20
        Me.MSuperTabControl.Tabs.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.MSuperTabItem1, Me.SuperTabItemBuscador})
        SuperTabLinearGradientColorTable2.Colors = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))}
        SuperTabColorTable2.Background = SuperTabLinearGradientColorTable2
        Me.MSuperTabControl.TabStripColor = SuperTabColorTable2
        Me.MSuperTabControl.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.OfficeMobile2014
        Me.MSuperTabControl.Text = "REGISTRO"
        '
        'MSuperTabControlPanel1
        '
        Me.MSuperTabControlPanel1.Controls.Add(Me.PanelContent)
        Me.MSuperTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MSuperTabControlPanel1.Location = New System.Drawing.Point(0, 0)
        Me.MSuperTabControlPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.MSuperTabControlPanel1.Name = "MSuperTabControlPanel1"
        Me.MSuperTabControlPanel1.Size = New System.Drawing.Size(1142, 692)
        Me.MSuperTabControlPanel1.TabIndex = 1
        Me.MSuperTabControlPanel1.TabItem = Me.MSuperTabItem1
        '
        'PanelContent
        '
        Me.PanelContent.BackColor = System.Drawing.Color.White
        Me.PanelContent.Controls.Add(Me.PanelButton)
        Me.PanelContent.Controls.Add(Me.PanelDatosTop)
        Me.PanelContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContent.Location = New System.Drawing.Point(0, 0)
        Me.PanelContent.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelContent.Name = "PanelContent"
        Me.PanelContent.Size = New System.Drawing.Size(1142, 692)
        Me.PanelContent.TabIndex = 0
        '
        'PanelButton
        '
        Me.PanelButton.Controls.Add(Me.GpDetalle)
        Me.PanelButton.Controls.Add(Me.GpPanelServicio)
        Me.PanelButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelButton.Location = New System.Drawing.Point(0, 272)
        Me.PanelButton.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelButton.Name = "PanelButton"
        Me.PanelButton.Size = New System.Drawing.Size(1142, 420)
        Me.PanelButton.TabIndex = 38
        '
        'GpDetalle
        '
        Me.GpDetalle.CanvasColor = System.Drawing.SystemColors.Control
        Me.GpDetalle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GpDetalle.Controls.Add(Me.grDetalle)
        Me.GpDetalle.DisabledBackColor = System.Drawing.Color.Empty
        Me.GpDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GpDetalle.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpDetalle.Location = New System.Drawing.Point(0, 0)
        Me.GpDetalle.Margin = New System.Windows.Forms.Padding(4)
        Me.GpDetalle.Name = "GpDetalle"
        Me.GpDetalle.Padding = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.GpDetalle.Size = New System.Drawing.Size(1142, 146)
        '
        '
        '
        Me.GpDetalle.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GpDetalle.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GpDetalle.Style.BackColorGradientAngle = 90
        Me.GpDetalle.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpDetalle.Style.BorderBottomWidth = 1
        Me.GpDetalle.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.GpDetalle.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpDetalle.Style.BorderLeftWidth = 1
        Me.GpDetalle.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpDetalle.Style.BorderRightWidth = 1
        Me.GpDetalle.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpDetalle.Style.BorderTopWidth = 1
        Me.GpDetalle.Style.CornerDiameter = 0
        Me.GpDetalle.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GpDetalle.Style.Font = New System.Drawing.Font("Georgia", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpDetalle.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GpDetalle.Style.TextColor = System.Drawing.Color.White
        Me.GpDetalle.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GpDetalle.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GpDetalle.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GpDetalle.TabIndex = 0
        Me.GpDetalle.Text = "D E T A L L E"
        '
        'grDetalle
        '
        Me.grDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grDetalle.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grDetalle.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grDetalle.Location = New System.Drawing.Point(7, 6)
        Me.grDetalle.Margin = New System.Windows.Forms.Padding(4)
        Me.grDetalle.Name = "grDetalle"
        Me.grDetalle.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grDetalle.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grDetalle.Size = New System.Drawing.Size(1126, 108)
        Me.grDetalle.TabIndex = 0
        Me.grDetalle.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GpPanelServicio
        '
        Me.GpPanelServicio.CanvasColor = System.Drawing.SystemColors.Control
        Me.GpPanelServicio.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GpPanelServicio.Controls.Add(Me.grProducto)
        Me.GpPanelServicio.DisabledBackColor = System.Drawing.Color.Empty
        Me.GpPanelServicio.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GpPanelServicio.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpPanelServicio.Location = New System.Drawing.Point(0, 146)
        Me.GpPanelServicio.Margin = New System.Windows.Forms.Padding(4)
        Me.GpPanelServicio.Name = "GpPanelServicio"
        Me.GpPanelServicio.Padding = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.GpPanelServicio.Size = New System.Drawing.Size(1142, 274)
        '
        '
        '
        Me.GpPanelServicio.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GpPanelServicio.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GpPanelServicio.Style.BackColorGradientAngle = 90
        Me.GpPanelServicio.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpPanelServicio.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.GpPanelServicio.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpPanelServicio.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpPanelServicio.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GpPanelServicio.Style.CornerDiameter = 0
        Me.GpPanelServicio.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GpPanelServicio.Style.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpPanelServicio.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GpPanelServicio.Style.TextColor = System.Drawing.Color.White
        Me.GpPanelServicio.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GpPanelServicio.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GpPanelServicio.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GpPanelServicio.TabIndex = 0
        Me.GpPanelServicio.Text = "S E R V I C I O"
        Me.GpPanelServicio.Visible = False
        '
        'grProducto
        '
        Me.grProducto.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grProducto.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProducto.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grProducto.Location = New System.Drawing.Point(7, 6)
        Me.grProducto.Margin = New System.Windows.Forms.Padding(4)
        Me.grProducto.Name = "grProducto"
        Me.grProducto.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grProducto.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grProducto.Size = New System.Drawing.Size(1128, 242)
        Me.grProducto.TabIndex = 0
        Me.grProducto.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'PanelDatosTop
        '
        Me.PanelDatosTop.Controls.Add(Me.PanelDatos)
        Me.PanelDatosTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelDatosTop.Location = New System.Drawing.Point(0, 0)
        Me.PanelDatosTop.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelDatosTop.Name = "PanelDatosTop"
        Me.PanelDatosTop.Size = New System.Drawing.Size(1142, 272)
        Me.PanelDatosTop.TabIndex = 37
        '
        'PanelDatos
        '
        Me.PanelDatos.Controls.Add(Me.LabelX2)
        Me.PanelDatos.Controls.Add(Me.cbServicio)
        Me.PanelDatos.Controls.Add(Me.tbObservacion)
        Me.PanelDatos.Controls.Add(Me.lboservacion)
        Me.PanelDatos.Controls.Add(Me.lbcredito)
        Me.PanelDatos.Controls.Add(Me.tbcredito)
        Me.PanelDatos.Controls.Add(Me.LabelX15)
        Me.PanelDatos.Controls.Add(Me.LabelX14)
        Me.PanelDatos.Controls.Add(Me.cbmoneda)
        Me.PanelDatos.Controls.Add(Me.cbventa)
        Me.PanelDatos.Controls.Add(Me.FechaVenta)
        Me.PanelDatos.Controls.Add(Me.LabelX8)
        Me.PanelDatos.Controls.Add(Me.tbCliente)
        Me.PanelDatos.Controls.Add(Me.LabelX1)
        Me.PanelDatos.Controls.Add(Me.tbCodigo)
        Me.PanelDatos.Controls.Add(Me.LabelX6)
        Me.PanelDatos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDatos.Location = New System.Drawing.Point(0, 0)
        Me.PanelDatos.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelDatos.Name = "PanelDatos"
        Me.PanelDatos.Size = New System.Drawing.Size(1142, 272)
        Me.PanelDatos.TabIndex = 34
        '
        'LabelX2
        '
        Me.LabelX2.AutoSize = True
        Me.LabelX2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX2.Location = New System.Drawing.Point(48, 60)
        Me.LabelX2.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(110, 20)
        Me.LabelX2.TabIndex = 47
        Me.LabelX2.Text = "Tipo Servicio:"
        '
        'cbServicio
        '
        cbServicio_DesignTimeLayout.LayoutString = resources.GetString("cbServicio_DesignTimeLayout.LayoutString")
        Me.cbServicio.DesignTimeLayout = cbServicio_DesignTimeLayout
        Me.cbServicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbServicio.Location = New System.Drawing.Point(171, 56)
        Me.cbServicio.Margin = New System.Windows.Forms.Padding(4)
        Me.cbServicio.Name = "cbServicio"
        Me.cbServicio.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbServicio.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbServicio.SelectedIndex = -1
        Me.cbServicio.SelectedItem = Nothing
        Me.cbServicio.Size = New System.Drawing.Size(208, 26)
        Me.cbServicio.TabIndex = 0
        Me.cbServicio.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'tbObservacion
        '
        Me.tbObservacion.BackColor = System.Drawing.Color.White
        '
        '
        '
        Me.tbObservacion.Border.Class = "TextBoxBorder"
        Me.tbObservacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbObservacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbObservacion.Location = New System.Drawing.Point(171, 166)
        Me.tbObservacion.Margin = New System.Windows.Forms.Padding(4)
        Me.tbObservacion.Multiline = True
        Me.tbObservacion.Name = "tbObservacion"
        Me.tbObservacion.PreventEnterBeep = True
        Me.tbObservacion.Size = New System.Drawing.Size(351, 70)
        Me.tbObservacion.TabIndex = 3
        Me.tbObservacion.Visible = False
        '
        'lboservacion
        '
        Me.lboservacion.AutoSize = True
        Me.lboservacion.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lboservacion.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lboservacion.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lboservacion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lboservacion.Location = New System.Drawing.Point(48, 167)
        Me.lboservacion.Margin = New System.Windows.Forms.Padding(4)
        Me.lboservacion.Name = "lboservacion"
        Me.lboservacion.Size = New System.Drawing.Size(105, 20)
        Me.lboservacion.TabIndex = 45
        Me.lboservacion.Text = "Observacion:"
        Me.lboservacion.Visible = False
        '
        'lbcredito
        '
        Me.lbcredito.AutoSize = True
        Me.lbcredito.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.lbcredito.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lbcredito.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbcredito.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lbcredito.Location = New System.Drawing.Point(560, 91)
        Me.lbcredito.Margin = New System.Windows.Forms.Padding(4)
        Me.lbcredito.Name = "lbcredito"
        Me.lbcredito.Size = New System.Drawing.Size(96, 20)
        Me.lbcredito.TabIndex = 41
        Me.lbcredito.Text = "Fecha Venc:"
        Me.lbcredito.Visible = False
        '
        'tbcredito
        '
        '
        '
        '
        Me.tbcredito.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.tbcredito.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcredito.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.tbcredito.ButtonDropDown.Visible = True
        Me.tbcredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbcredito.IsPopupCalendarOpen = False
        Me.tbcredito.Location = New System.Drawing.Point(665, 92)
        Me.tbcredito.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.tbcredito.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcredito.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.tbcredito.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.tbcredito.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcredito.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.tbcredito.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.tbcredito.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.tbcredito.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.tbcredito.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.tbcredito.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbcredito.MonthCalendar.TodayButtonVisible = True
        Me.tbcredito.Name = "tbcredito"
        Me.tbcredito.Size = New System.Drawing.Size(160, 26)
        Me.tbcredito.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.tbcredito.TabIndex = 5
        Me.tbcredito.Visible = False
        '
        'LabelX15
        '
        Me.LabelX15.AutoSize = True
        Me.LabelX15.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX15.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX15.Location = New System.Drawing.Point(560, 60)
        Me.LabelX15.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX15.Name = "LabelX15"
        Me.LabelX15.Size = New System.Drawing.Size(92, 20)
        Me.LabelX15.TabIndex = 39
        Me.LabelX15.Text = "Tipo Venta:"
        '
        'LabelX14
        '
        Me.LabelX14.AutoSize = True
        Me.LabelX14.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX14.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX14.Location = New System.Drawing.Point(560, 140)
        Me.LabelX14.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX14.Name = "LabelX14"
        Me.LabelX14.Size = New System.Drawing.Size(71, 20)
        Me.LabelX14.TabIndex = 38
        Me.LabelX14.Text = "Moneda:"
        '
        'cbmoneda
        '
        '
        '
        '
        Me.cbmoneda.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.cbmoneda.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbmoneda.Location = New System.Drawing.Point(665, 139)
        Me.cbmoneda.Margin = New System.Windows.Forms.Padding(4)
        Me.cbmoneda.Name = "cbmoneda"
        Me.cbmoneda.OffBackColor = System.Drawing.Color.DodgerBlue
        Me.cbmoneda.OffText = "US"
        Me.cbmoneda.OffTextColor = System.Drawing.Color.White
        Me.cbmoneda.OnBackColor = System.Drawing.Color.Gold
        Me.cbmoneda.OnText = "BS"
        Me.cbmoneda.OnTextColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(59, Byte), Integer), CType(CType(66, Byte), Integer))
        Me.cbmoneda.Size = New System.Drawing.Size(189, 27)
        Me.cbmoneda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cbmoneda.TabIndex = 6
        Me.cbmoneda.Value = True
        Me.cbmoneda.ValueObject = "Y"
        '
        'cbventa
        '
        cbventa_DesignTimeLayout.LayoutString = resources.GetString("cbventa_DesignTimeLayout.LayoutString")
        Me.cbventa.DesignTimeLayout = cbventa_DesignTimeLayout
        Me.cbventa.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbventa.Location = New System.Drawing.Point(665, 57)
        Me.cbventa.Margin = New System.Windows.Forms.Padding(4)
        Me.cbventa.Name = "cbventa"
        Me.cbventa.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.cbventa.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.cbventa.SelectedIndex = -1
        Me.cbventa.SelectedItem = Nothing
        Me.cbventa.Size = New System.Drawing.Size(208, 26)
        Me.cbventa.TabIndex = 4
        Me.cbventa.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'FechaVenta
        '
        '
        '
        '
        Me.FechaVenta.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.FechaVenta.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaVenta.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown
        Me.FechaVenta.ButtonDropDown.Visible = True
        Me.FechaVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FechaVenta.IsPopupCalendarOpen = False
        Me.FechaVenta.Location = New System.Drawing.Point(171, 122)
        Me.FechaVenta.Margin = New System.Windows.Forms.Padding(4)
        '
        '
        '
        '
        '
        '
        Me.FechaVenta.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaVenta.MonthCalendar.CalendarDimensions = New System.Drawing.Size(1, 1)
        Me.FechaVenta.MonthCalendar.ClearButtonVisible = True
        '
        '
        '
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1
        Me.FechaVenta.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaVenta.MonthCalendar.DisplayMonth = New Date(2017, 2, 1, 0, 0, 0, 0)
        Me.FechaVenta.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday
        '
        '
        '
        Me.FechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.FechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90
        Me.FechaVenta.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.FechaVenta.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.FechaVenta.MonthCalendar.TodayButtonVisible = True
        Me.FechaVenta.Name = "FechaVenta"
        Me.FechaVenta.Size = New System.Drawing.Size(160, 26)
        Me.FechaVenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.FechaVenta.TabIndex = 2
        '
        'LabelX8
        '
        Me.LabelX8.AutoSize = True
        Me.LabelX8.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX8.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX8.Location = New System.Drawing.Point(48, 123)
        Me.LabelX8.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX8.Name = "LabelX8"
        Me.LabelX8.Size = New System.Drawing.Size(125, 20)
        Me.LabelX8.TabIndex = 9
        Me.LabelX8.Text = "Fecha de Venta:"
        '
        'tbCliente
        '
        '
        '
        '
        Me.tbCliente.Border.Class = "TextBoxBorder"
        Me.tbCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCliente.Location = New System.Drawing.Point(171, 88)
        Me.tbCliente.Margin = New System.Windows.Forms.Padding(4)
        Me.tbCliente.Name = "tbCliente"
        Me.tbCliente.PreventEnterBeep = True
        Me.tbCliente.Size = New System.Drawing.Size(356, 26)
        Me.tbCliente.TabIndex = 1
        '
        'LabelX1
        '
        Me.LabelX1.AutoSize = True
        Me.LabelX1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX1.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX1.Location = New System.Drawing.Point(48, 25)
        Me.LabelX1.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX1.Name = "LabelX1"
        Me.LabelX1.Size = New System.Drawing.Size(62, 20)
        Me.LabelX1.TabIndex = 0
        Me.LabelX1.Text = "Código:"
        '
        'tbCodigo
        '
        '
        '
        '
        Me.tbCodigo.Border.Class = "TextBoxBorder"
        Me.tbCodigo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.tbCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbCodigo.Location = New System.Drawing.Point(171, 22)
        Me.tbCodigo.Margin = New System.Windows.Forms.Padding(4)
        Me.tbCodigo.Name = "tbCodigo"
        Me.tbCodigo.PreventEnterBeep = True
        Me.tbCodigo.Size = New System.Drawing.Size(67, 26)
        Me.tbCodigo.TabIndex = 0
        Me.tbCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelX6
        '
        Me.LabelX6.AutoSize = True
        Me.LabelX6.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.LabelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX6.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.LabelX6.Location = New System.Drawing.Point(48, 90)
        Me.LabelX6.Margin = New System.Windows.Forms.Padding(4)
        Me.LabelX6.Name = "LabelX6"
        Me.LabelX6.Size = New System.Drawing.Size(70, 20)
        Me.LabelX6.TabIndex = 7
        Me.LabelX6.Text = "Alumno:"
        '
        'MSuperTabItem1
        '
        Me.MSuperTabItem1.AttachedControl = Me.MSuperTabControlPanel1
        Me.MSuperTabItem1.GlobalItem = False
        Me.MSuperTabItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far
        Me.MSuperTabItem1.Name = "MSuperTabItem1"
        Me.MSuperTabItem1.TabFont = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MSuperTabItem1.Text = "REGISTRO"
        Me.MSuperTabItem1.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Near
        '
        'SuperBuscador
        '
        Me.SuperBuscador.Controls.Add(Me.PanelBuscador)
        Me.SuperBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SuperBuscador.Location = New System.Drawing.Point(0, 0)
        Me.SuperBuscador.Name = "SuperBuscador"
        Me.SuperBuscador.Size = New System.Drawing.Size(1142, 692)
        Me.SuperBuscador.TabIndex = 0
        Me.SuperBuscador.TabItem = Me.SuperTabItemBuscador
        '
        'PanelBuscador
        '
        Me.PanelBuscador.BackColor = System.Drawing.Color.White
        Me.PanelBuscador.Controls.Add(Me.GroupPanel2)
        Me.PanelBuscador.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelBuscador.Location = New System.Drawing.Point(0, 0)
        Me.PanelBuscador.Name = "PanelBuscador"
        Me.PanelBuscador.Size = New System.Drawing.Size(1142, 692)
        Me.PanelBuscador.TabIndex = 0
        '
        'GroupPanel2
        '
        Me.GroupPanel2.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel2.Controls.Add(Me.Panel6)
        Me.GroupPanel2.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel2.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupPanel2.Name = "GroupPanel2"
        Me.GroupPanel2.Size = New System.Drawing.Size(1142, 692)
        '
        '
        '
        Me.GroupPanel2.Style.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BackColorGradientAngle = 90
        Me.GroupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderBottomWidth = 1
        Me.GroupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(72, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.GroupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderLeftWidth = 1
        Me.GroupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderRightWidth = 1
        Me.GroupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel2.Style.BorderTopWidth = 1
        Me.GroupPanel2.Style.CornerDiameter = 4
        Me.GroupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel2.Style.Font = New System.Drawing.Font("Georgia", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel2.Style.TextColor = System.Drawing.Color.White
        Me.GroupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel2.TabIndex = 4
        Me.GroupPanel2.Text = "BUSCADOR  VENTAS"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.grVentas)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1136, 665)
        Me.Panel6.TabIndex = 0
        '
        'grVentas
        '
        Me.grVentas.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grVentas.BackColor = System.Drawing.Color.GhostWhite
        Me.grVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grVentas.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None
        Me.grVentas.FocusStyle = Janus.Windows.GridEX.FocusStyle.Solid
        Me.grVentas.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.UseRowStyle
        Me.grVentas.HeaderFormatStyle.Font = New System.Drawing.Font("Georgia", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grVentas.Location = New System.Drawing.Point(0, 0)
        Me.grVentas.Margin = New System.Windows.Forms.Padding(4)
        Me.grVentas.Name = "grVentas"
        Me.grVentas.Office2007ColorScheme = Janus.Windows.GridEX.Office2007ColorScheme.Custom
        Me.grVentas.Office2007CustomColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.BackColor = System.Drawing.Color.DodgerBlue
        Me.grVentas.SelectedFormatStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grVentas.SelectedFormatStyle.ForeColor = System.Drawing.Color.White
        Me.grVentas.SelectOnExpand = False
        Me.grVentas.Size = New System.Drawing.Size(1136, 665)
        Me.grVentas.TabIndex = 0
        Me.grVentas.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SuperTabItemBuscador
        '
        Me.SuperTabItemBuscador.AttachedControl = Me.SuperBuscador
        Me.SuperTabItemBuscador.GlobalItem = False
        Me.SuperTabItemBuscador.Name = "SuperTabItemBuscador"
        Me.SuperTabItemBuscador.Text = "BUSCADOR"
        '
        'F0_VentaServicios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1179, 853)
        Me.Name = "F0_VentaServicios"
        Me.Text = "F0_VentaServicios"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Controls.SetChildIndex(Me.SuperTabPrincipal, 0)
        CType(Me.SuperTabPrincipal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperTabPrincipal.ResumeLayout(False)
        Me.SuperTabControlPanelRegistro.ResumeLayout(False)
        Me.PanelSuperior.ResumeLayout(False)
        Me.PanelInferior.ResumeLayout(False)
        CType(Me.BubbleBarUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelToolBar1.ResumeLayout(False)
        Me.PanelToolBar2.ResumeLayout(False)
        Me.PanelPrincipal.ResumeLayout(False)
        Me.PanelUsuario.ResumeLayout(False)
        Me.PanelUsuario.PerformLayout()
        Me.PanelNavegacion.ResumeLayout(False)
        Me.MPanelUserAct.ResumeLayout(False)
        Me.MPanelUserAct.PerformLayout()
        CType(Me.MEP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MSuperTabControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MSuperTabControl.ResumeLayout(False)
        Me.MSuperTabControlPanel1.ResumeLayout(False)
        Me.PanelContent.ResumeLayout(False)
        Me.PanelButton.ResumeLayout(False)
        Me.GpDetalle.ResumeLayout(False)
        CType(Me.grDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GpPanelServicio.ResumeLayout(False)
        CType(Me.grProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDatosTop.ResumeLayout(False)
        Me.PanelDatos.ResumeLayout(False)
        Me.PanelDatos.PerformLayout()
        CType(Me.cbServicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbcredito, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbventa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FechaVenta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SuperBuscador.ResumeLayout(False)
        Me.PanelBuscador.ResumeLayout(False)
        Me.GroupPanel2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        CType(Me.grVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Protected WithEvents MSuperTabControl As DevComponents.DotNetBar.SuperTabControl
    Protected WithEvents MSuperTabControlPanel1 As DevComponents.DotNetBar.SuperTabControlPanel
    Protected WithEvents PanelContent As Panel
    Protected WithEvents MSuperTabItem1 As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents SuperBuscador As DevComponents.DotNetBar.SuperTabControlPanel
    Protected WithEvents PanelBuscador As Panel
    Protected WithEvents SuperTabItemBuscador As DevComponents.DotNetBar.SuperTabItem
    Friend WithEvents PanelDatosTop As Panel
    Friend WithEvents PanelDatos As Panel
    Friend WithEvents tbObservacion As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents lboservacion As DevComponents.DotNetBar.LabelX
    Friend WithEvents lbcredito As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbcredito As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX15 As DevComponents.DotNetBar.LabelX
    Friend WithEvents LabelX14 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbmoneda As DevComponents.DotNetBar.Controls.SwitchButton
    Friend WithEvents cbventa As Janus.Windows.GridEX.EditControls.MultiColumnCombo
    Friend WithEvents FechaVenta As DevComponents.Editors.DateTimeAdv.DateTimeInput
    Friend WithEvents LabelX8 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCliente As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX1 As DevComponents.DotNetBar.LabelX
    Friend WithEvents tbCodigo As DevComponents.DotNetBar.Controls.TextBoxX
    Friend WithEvents LabelX6 As DevComponents.DotNetBar.LabelX
    Friend WithEvents PanelButton As Panel
    Friend WithEvents GpDetalle As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grDetalle As Janus.Windows.GridEX.GridEX
    Friend WithEvents GpPanelServicio As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents grProducto As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupPanel2 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents grVentas As Janus.Windows.GridEX.GridEX
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents cbServicio As Janus.Windows.GridEX.EditControls.MultiColumnCombo
End Class
