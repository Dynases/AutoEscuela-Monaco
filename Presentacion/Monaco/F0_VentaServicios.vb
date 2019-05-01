Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Imports DevComponents.DotNetBar.SuperGrid
Imports Presentacion.F_ClienteNuevoServicio
Public Class F0_VentaServicios

#Region "Variables Globales"
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Public _nameButton As String
    Dim _CodCliente As Integer = 0
#End Region

#Region "METODOS PRIVADOS"
    Private Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.Text = "S E R V I C I O   V E N T A"
        _prCargarComboLibreria(cbServicio, 6, 1) ''Libreria Vehiculo=1  TamVehiculo=4
        _prCargarComboLibreria(cbventa, 14, 3) ''Libreria Contado, Credito,ATC
        _prAsignarPermisos()
        Dim blah As Bitmap = My.Resources.venta
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())
        Me.Icon = ico
        _prCargarStyle()

        _prCargarVenta()
        _prInhabiliitar()



        _prAsignarPermisos()
    End Sub

    Private Sub _prInhabiliitar()

        tbCodigo.ReadOnly = True
        tbCliente.ReadOnly = True
        tbObservacion.ReadOnly = True
        cbServicio.ReadOnly = True
        FechaVenta.IsInputReadOnly = True
        cbventa.ReadOnly = True
        tbcredito.IsInputReadOnly = True
        cbmoneda.IsReadOnly = True
        'Datos facturacion

        btnModificar.Enabled = True
        btnGrabar.Enabled = False
        btnNuevo.Enabled = True
        btnEliminar.Enabled = True

        grVentas.Enabled = True
        PanelNavegacion.Enabled = True




        If (GpPanelServicio.Visible = True) Then
            GpPanelServicio.Visible = False
        End If


        PanelInferior.Visible = True
    End Sub

    Private Sub _prhabilitar()

        grVentas.Enabled = False
        tbCodigo.ReadOnly = True

        tbObservacion.ReadOnly = False
        cbServicio.ReadOnly = False
        FechaVenta.IsInputReadOnly = False
        cbventa.ReadOnly = False
        tbcredito.IsInputReadOnly = False
        cbmoneda.IsReadOnly = False
        btnGrabar.Enabled = True


        If (GpPanelServicio.Visible = True) Then
            GpPanelServicio.Visible = False
        End If
        'If (tbCodigo.Text.Length > 0) Then
        '    cbSucursal.ReadOnly = True
        'Else

        '    If (gb_userTodasSuc = False And CType(cbSucursal.DataSource, DataTable).Rows.Count > 0) Then
        '        cbSucursal.ReadOnly = True
        '    Else

        '    End If


        'End If


    End Sub

    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbCliente.Clear()
        tbObservacion.Clear()
        FechaVenta.Value = Now.Date
        tbcredito.Value = Now.Date
        tbCliente.Clear()
        cbmoneda.Value = True
        tbObservacion.Clear()
        lbcredito.Visible = False
        _prCargarGridDetalle(-1)
        MSuperTabControl.SelectedTabIndex = 0
        Try
            CType(grDetalle.DataSource, DataTable).Rows.Clear()
        Catch ex As Exception

        End Try
        If (CType(cbventa.DataSource, DataTable).Rows.Count > 0) Then
            cbventa.SelectedIndex = 0
        End If
        If (CType(cbServicio.DataSource, DataTable).Rows.Count > 0) Then
            cbServicio.SelectedIndex = 0
        End If

    End Sub


    Public Sub _prMostrarRegistro(_N As Integer)
        '        venta.vcnumi , venta.vcsector, a.cedesc2 As sector, venta.vcalm, venta.vcfdoc, venta.vcclie, venta.vcfvcr, venta.vctipo, venta.vcest,
        'venta.vcobs, venta.vctotal, venta.vcmoneda

        With grVentas

            tbCodigo.Text = .GetValue("vcnumi")
            FechaVenta.Value = .GetValue("vcfdoc")
            cbServicio.Value = .GetValue("vcsector")
            cbmoneda.Value = .GetValue("vcmoneda")
            _CodCliente = .GetValue("vcclie")
            tbCliente.Text = .GetValue("alumno")
            tbObservacion.Text = .GetValue("vcobs")
            tbcredito.Value = .GetValue("vcfvcr")
            cbventa.Value = .GetValue("vctipo")


            'lbFecha.Text = CType(.GetValue("tafact"), Date).ToString("dd/MM/yyyy")
            'lbHora.Text = .GetValue("tahact").ToString
            'lbUsuario.Text = .GetValue("tauact").ToString

        End With
        'If (cbsector.Value = 1) Then
        '    btnModificar.Enabled = True
        'Else
        '    btnModificar.Enabled = False
        'End If
        _prCargarGridDetalle(tbCodigo.Text)


        'tbanular.Value = grVentas.GetValue("anulada")

        _prCalcularPrecioTotal()
        LblPaginacion.Text = Str(grVentas.Row + 1) + "/" + grVentas.RowCount.ToString

    End Sub

    Public Sub _prFiltrar()
        'cargo el buscador
        Dim _Mpos As Integer
        _prCargarVenta()
        If grVentas.RowCount > 0 Then
            _Mpos = 0
            grVentas.Row = _Mpos
        Else
            _Limpiar()
            LblPaginacion.Text = "0/0"
        End If
    End Sub

    Private Sub _prCargarVenta()

        Dim dt As New DataTable
        dt = L_prServicioVentaGeneralEscuela()
        grVentas.DataSource = dt
        grVentas.RetrieveStructure()
        grVentas.AlternatingColors = True
        '        venta.vcnumi , venta.vcsector, a.cedesc2 As sector, venta.vcalm, venta.vcfdoc, venta.vcclie, venta.vcfvcr, venta.vctipo, venta.vcest,
        'venta.vcobs, venta.vctotal, venta.vcmoneda
        With grVentas.RootTable.Columns("vcnumi")
            .Width = 90
            .Caption = "CODIGO"
            .Visible = True
            .TextAlignment = TextAlignment.Far


        End With
        With grVentas.RootTable.Columns("vcsector")
            .Width = 180
            .Visible = False
        End With

        With grVentas.RootTable.Columns("sector")
            .Width = 150
            .Visible = True
            .Caption = "TIPO SERVICIO"
        End With

        With grVentas.RootTable.Columns("alumno")
            .Width = 350
            .Visible = True
            .Caption = "NOMBRE ALUMNO"
        End With
        With grVentas.RootTable.Columns("vcalm")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("vcmoneda")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcfdoc")
            .Width = 90
            .Caption = "FECHA"
            .FormatString = "dd/MM/yyyy"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("vcclie")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcfvcr")
            .Width = 90
            .Visible = False
        End With

        'With grVentas.RootTable.Columns("tipo")
        '    .Width = 200
        '    .Caption = "TIPO VENTA"
        '    .Visible = True
        'End With

        With grVentas.RootTable.Columns("vctipo")
            .Width = 90
            .Visible = False
        End With
        With grVentas.RootTable.Columns("vcest")
            .Width = 90
            .Visible = False
        End With

        With grVentas.RootTable.Columns("vcobs")
            .Width = 120
            .Caption = "OBSERVACION"
            .Visible = True
        End With
        With grVentas.RootTable.Columns("vctotal")
            .Width = 150
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            .Visible = True
            .Caption = "TOTAL"
            .FormatString = "0.00"
        End With
        'With grVentas.RootTable.Columns("anulada")
        '    .Width = 90
        '    .Visible = False
        'End With

        With grVentas
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
            'diseño de la grilla

        End With

        If (dt.Rows.Count <= 0) Then
            _prCargarGridDetalle(-1)
        End If
    End Sub


    Private Sub _prCargarAyudaServicios()
        Dim dt As New DataTable

        If (cbServicio.SelectedIndex < 0) Then
            Return

        End If
        dt = L_prGeneralServicioEscuela(CType(grDetalle.DataSource, DataTable), cbServicio.Value)

        ''''janosssssssss''''''
        grProducto.DataSource = dt
        grProducto.RetrieveStructure()
        grProducto.AlternatingColors = True
        grProducto.RowFormatStyle.Font = New Font("arial", 10)
        'servicio.ednumi, servicio.eddesc, servicio.edprec 
        With grProducto.RootTable.Columns("ednumi")
            .Width = 180
            .TextAlignment = TextAlignment.Center
            .Caption = "CODIGO SERVICIO"
            .Visible = True
        End With


        With grProducto.RootTable.Columns("eddesc")
            .Width = 350
            .Visible = True
            .Caption = "SERVICIOS"

        End With

        With grProducto.RootTable.Columns("edprec")
            .Width = 180
            .Visible = True
            .Caption = "PRECIO"
            .FormatString = "0.00"
        End With


        With grProducto
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007
            .DefaultFilterRowComparison = FilterConditionOperator.Contains
            .FilterMode = FilterMode.Automatic
            .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
            .GroupByBoxVisible = False
        End With
        grProducto.RootTable.HeaderFormatStyle.FontBold = TriState.True

    End Sub
    Private Sub _prCargarComboLibreria(mCombo As Janus.Windows.GridEX.EditControls.MultiColumnCombo, cod1 As String, cod2 As String)
        Dim dt As New DataTable
        dt = L_prLibreriaClienteLGeneral(cod1, cod2)

        With mCombo
            .DropDownList.Columns.Clear()

            .DropDownList.Columns.Add("cenum").Width = 70
            .DropDownList.Columns("cenum").Caption = "COD"

            .DropDownList.Columns.Add("cedesc1").Width = 200
            .DropDownList.Columns("cedesc1").Caption = "DESCRIPCION"

            .ValueMember = "cenum"
            .DisplayMember = "cedesc1"
            .DataSource = dt
            .Refresh()
        End With
    End Sub
    Private Sub _prCargarGridDetalle(_vcnumi As Integer)
        Dim dt As New DataTable


        dt = L_prServicioVentaDetalleServiciosEscuela(_vcnumi)

        ''''janosssssssss''''''
        grDetalle.DataSource = dt
        grDetalle.RetrieveStructure()
        grDetalle.AlternatingColors = True
        grDetalle.RowFormatStyle.Font = New Font("arial", 10)

        '    detalle.vdnumi , detalle.vdvc2numi, detalle.vdserv, servicio.eddesc As servicio,
        'detalle.vdcmin , detalle.vdpbas, detalle.vdptot, detalle.vdporc, detalle.vddesc, detalle.vdtotdesc, detalle.vdobs, detalle.vdpcos,
        'detalle.vdptot2 
        With grDetalle.RootTable.Columns("vdnumi")
            .Width = 80
            .TextAlignment = TextAlignment.Center
            .Caption = "CODIGO"
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("vdvc2numi")
            .Width = 60
            .Visible = False


        End With
        With grDetalle.RootTable.Columns("vdserv")
            .Width = 120
            .Visible = True
            .Caption = "Cod Servicio"
        End With

        With grDetalle.RootTable.Columns("servicio")
            .Width = 350
            .Caption = "SERVICIO"
            .Visible = True
        End With
        '    detalle.vdnumi , detalle.vdvc2numi, detalle.vdserv, servicio.eddesc As servicio,
        'detalle.vdcmin , detalle.vdpbas, detalle.vdptot, detalle.vdporc, detalle.vddesc, detalle.vdtotdesc, detalle.vdobs, detalle.vdpcos,
        'detalle.vdptot2 


        With grDetalle.RootTable.Columns("vdpbas")
            .Caption = "PRECIO UNITARIO"
            .Width = 150
            .FormatString = "0.00"
            .TextAlignment = TextAlignment.Center
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("vdcmin")
            .Caption = "CANTIDAD"
            .FormatString = "0.00"
            .TextAlignment = TextAlignment.Center
            .Width = 100
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("vdptot")
            .Caption = "CANTIDAD"
            .FormatString = "0.00"
            .TextAlignment = TextAlignment.Center
            .Width = 100
            .Visible = True
        End With
        With grDetalle.RootTable.Columns("vdporc")
            .Width = 120
            .Caption = "% DESCUENTO"
            .TextAlignment = TextAlignment.Center
            .FormatString = "0.00"
            .Visible = True

        End With
        With grDetalle.RootTable.Columns("vddesc")
            .Caption = "MONTO DE DESCUENTO"
            .Width = 200
            .TextAlignment = TextAlignment.Center
            .FormatString = "0.00"
            .Visible = True
        End With
        '    detalle.vdnumi , detalle.vdvc2numi, detalle.vdserv, servicio.eddesc As servicio,
        'detalle.vdcmin , detalle.vdpbas, detalle.vdptot, detalle.vdporc, detalle.vddesc, detalle.vdtotdesc, detalle.vdobs, detalle.vdpcos,
        'detalle.vdptot2 

        With grDetalle.RootTable.Columns("vdtotdesc")
            .Width = 200
            .Caption = "PRECIO TOTAL"
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .Visible = True
            .AggregateFunction = AggregateFunction.Sum
            .CellStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        End With
        With grDetalle.RootTable.Columns("vdobs")
            .Width = 100
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("vdpcos")
            .Width = 100
            .Visible = False
        End With
        With grDetalle.RootTable.Columns("vdptot2")
            .Width = 100
            .Visible = False
        End With

        With grDetalle.RootTable.Columns("estado")
            .Width = 100
            .Visible = False
        End With

        With grDetalle
            .GroupByBoxVisible = False
            grDetalle.TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.DodgerBlue
            .TotalRowPosition = TotalRowPosition.BottomFixed

            .VisualStyle = VisualStyle.Office2007

        End With
        grDetalle.RootTable.HeaderFormatStyle.FontBold = TriState.True
        'grDetalle.RootTable.SortKeys.Add(
        '    "lclin", SortOrder.Ascending) ''Esta instruccion me orden las columnas




    End Sub


    Public Sub _prCargarStyle()
        GpDetalle.Style.BackColor = Color.FromArgb(13, 71, 161)
        GpDetalle.Style.BackColor2 = Color.FromArgb(13, 71, 161)
        GpDetalle.Style.TextColor = Color.White

    End Sub
    Private Sub _prAsignarPermisos()

        Dim dtRolUsu As DataTable = L_prRolDetalleGeneral(gi_userRol, _nameButton)

        Dim show As Boolean = dtRolUsu.Rows(0).Item("ycshow")
        Dim add As Boolean = dtRolUsu.Rows(0).Item("ycadd")
        Dim modif As Boolean = dtRolUsu.Rows(0).Item("ycmod")
        Dim del As Boolean = dtRolUsu.Rows(0).Item("ycdel")

        If add = False Then
            btnNuevo.Visible = False
        End If
        If modif = False Then
            btnModificar.Visible = False
        End If
        If del = False Then
            btnEliminar.Visible = False
        End If

    End Sub

    Public Function _fnActionNuevo() As Boolean
        Return tbCodigo.Text = String.Empty

    End Function

    Public Function _fnVisualizarRegistros() As Boolean
        Return tbObservacion.ReadOnly = True
    End Function

    Public Function _fnObtenerLinDetalle() As Integer
        Dim length As Integer = CType(grDetalle.DataSource, DataTable).Rows.Count
        Dim lin As Integer
        If (length > 0) Then
            lin = CType(grDetalle.DataSource, DataTable).Rows(length - 1).Item("vdnumi")
            Return lin

        Else
            Return 0
        End If

    End Function

    Public Sub _prAddFilaDetalle()
        '    detalle.vdnumi , detalle.vdvc2numi, detalle.vdserv, servicio.eddesc As servicio,
        'detalle.vdcmin , detalle.vdpbas, detalle.vdptot, detalle.vdporc, detalle.vddesc, detalle.vdtotdesc, detalle.vdobs, detalle.vdpcos,
        'detalle.vdptot2, 1 as estado

        Dim lin As Integer = _fnObtenerLinDetalle() + 1
        _prCalcularPrecioTotal()
        CType(grDetalle.DataSource, DataTable).Rows.Add(lin, 0, 0, "", 0, 0, 0, 0, 0, 0, "", 0, 0, 0)
    End Sub

    Public Function _fnValidarColumn(pos As Integer, row As Integer, _MostrarMensaje As Boolean) As Boolean
        If (CType(grDetalle.DataSource, DataTable).Rows(pos).Item("servicio") = String.Empty) Then
            grDetalle.Row = row
            grDetalle.Col = 3
            grDetalle.FocusCellFormatStyle.BackColor = Color.Red

            If (_MostrarMensaje = True) Then
                ToastNotification.Show(Me, "           Seleccione un Servicio o Producto!!             ", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If

            Return False
        End If


        Return True
    End Function

    Public Sub _prCalcularPrecioTotal()
        'tbTotal.Text = grDetalle.GetTotal(grDetalle.RootTable.Columns("lcptot"), AggregateFunction.Sum)
    End Sub


    Public Sub _prObtenerPosicionDetalle(ByRef pos As Integer, ByVal Lin As Integer)
        Dim length As Integer = CType(grDetalle.DataSource, DataTable).Rows.Count
        pos = -1
        For i As Integer = 0 To length Step 1
            Dim numi As Integer = CType(grDetalle.DataSource, DataTable).Rows(i).Item("vdnumi")
            If (numi = Lin) Then
                pos = i
                Return

            End If
        Next

    End Sub


    Private Sub P_PonerTotal(rowIndex As Integer)
        If (rowIndex < grDetalle.RowCount) Then


            grDetalle.Row = rowIndex
            Dim lin As Integer = grDetalle.GetValue("vdnumi")
            Dim pos As Integer = -1
            _prObtenerPosicionDetalle(pos, lin)
            Dim cant As Double = grDetalle.GetValue("vdcmin")

            Dim uni As Double = grDetalle.GetValue("vdpbas")

            If (pos >= 0) Then

                Dim porcdesc As Double = IIf(IsDBNull(grDetalle.GetValue("vdporc")), 0, grDetalle.GetValue("vdporc"))
                Dim TotalUnitario As Double = cant * uni
                Dim montodesc As Double = (TotalUnitario * (porcdesc / 100))
                'grDetalle.SetValue("lcmdes", montodesc)
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("lcmdes") = montodesc
                grDetalle.SetValue("vdtotdesc", (cant * uni) - montodesc)
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = ((cant * uni) - montodesc)
                Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                If (estado = 1) Then
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            End If
            _prCalcularPrecioTotal()
        End If

    End Sub
    Public Sub _prCambiarEstadoItemEliminar()
        For i As Integer = 0 To CType(grDetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (estado = 1 Or estado = 2) Then
                CType(grDetalle.DataSource, DataTable).Rows(i).Item("estado") = -1
            Else
                If (estado = 0) Then
                    CType(grDetalle.DataSource, DataTable).Rows.RemoveAt(i)

                End If
            End If
        Next
        grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
    End Sub

    Private Sub _prSalir()
        If btnGrabar.Enabled = True Then
            _prInhabiliitar()
            If grVentas.RowCount > 0 Then

                _prMostrarRegistro(0)

            End If
        Else
            '_modulo.Select()
            '_tab.Close()

            'BanderaServer = False
            Me.Close()

        End If
    End Sub
    Public Sub _PrimerRegistro()
        Dim _MPos As Integer
        If grVentas.RowCount > 0 Then
            _MPos = 0
            ''   _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub
    Private Sub F0_VentaServicios_Load(sender As Object, e As EventArgs)
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        _prSalir()
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim _pos As Integer = grVentas.Row
        If _pos < grVentas.RowCount - 1 And _pos >= 0 Then
            _pos = grVentas.Row + 1
            '' _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim _pos As Integer = grVentas.Row
        If grVentas.RowCount > 0 Then
            _pos = grVentas.RowCount - 1
            ''  _prMostrarRegistro(_pos)
            grVentas.Row = _pos
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim _MPos As Integer = grVentas.Row
        If _MPos > 0 And grVentas.RowCount > 0 Then
            _MPos = _MPos - 1
            ''  _prMostrarRegistro(_MPos)
            grVentas.Row = _MPos
        End If
    End Sub

    Private Sub btnPrimero_Click(sender As Object, e As EventArgs) Handles btnPrimero.Click
        _PrimerRegistro()
    End Sub

    Private Sub grVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles grVentas.KeyDown
        If e.KeyData = Keys.Enter Then
            MSuperTabControl.SelectedTabIndex = 0
            grDetalle.Focus()

        End If
    End Sub

    Private Sub tbCliente_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown
        If (Not _fnVisualizarRegistros() And tbObservacion.ReadOnly = False) Then


            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim codigo As Integer = -1

                'grabar horario
                Dim frmAyuda As Modelos.ModeloAyuda

                Dim dt As DataTable
                '' a.cbnumi as codigo, a.cbci as ci,concat (a.cbnom ,' ', a.cbape ) as nombre
                dt = L_prListarAlumnosVenta(cbServicio.Value)

                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("codigo", True, "CODIGO", 150))
                listEstCeldas.Add(New Modelos.Celda("ci", True, "CI", 120))
                listEstCeldas.Add(New Modelos.Celda("nombre", True, "NOMBRE COMPLETO", 350))

                frmAyuda = New Modelos.ModeloAyuda(50, 250, dt, "SELECCIONE UN ALUMNO".ToUpper, listEstCeldas)
                frmAyuda.ShowDialog()


                If frmAyuda.seleccionado = True Then
                    tbCliente.Text = frmAyuda.filaSelect.Cells("nombre").Value
                    _CodCliente = frmAyuda.filaSelect.Cells("codigo").Value
                    FechaVenta.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        MSuperTabControl.SelectedTabIndex = 0
        _Limpiar()
        _prhabilitar()

        btnNuevo.Enabled = False
        btnModificar.Enabled = False
        btnEliminar.Enabled = False
        btnGrabar.Enabled = True
        PanelNavegacion.Enabled = False

        'btnNuevo.Enabled = False
        'btnModificar.Enabled = False
        'btnEliminar.Enabled = False
        'GPanelProductos.Visible = False
        '_prhabilitar()

        '_Limpiar()

        CType(grDetalle.DataSource, DataTable).Rows.Clear()
        _prAddFilaDetalle()
        cbServicio.Focus()

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If (grVentas.RowCount > 0) Then
            'If (gb_FacturaEmite) Then
            '    If (Not P_fnValidarFacturaVigente()) Then
            'Dim img As Bitmap = New Bitmap(My.Resources.WARNING, 50, 50)

            'ToastNotification.Show(Me, "No se puede modificar la venta con codigo ".ToUpper + tbCodigo.Text + ", su factura esta anulada.".ToUpper,
            '                          img, 2000,
            '                          eToastGlowColor.Green,
            '                          eToastPosition.TopCenter)
            'Exit Sub
            'End If
            '    End If

            _prhabilitar()
            btnNuevo.Enabled = False
            btnModificar.Enabled = False
            btnEliminar.Enabled = False
            btnGrabar.Enabled = True

            PanelNavegacion.Enabled = False


        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim info As New TaskDialogInfo("¿esta seguro de eliminar el registro?".ToUpper, eTaskDialogIcon.Delete, "advertencia".ToUpper, "mensaje principal".ToUpper, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Blue)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            Dim res As Boolean = L_prServicioVentaBorrarAlumnos(tbCodigo.Text, mensajeError)
            If res Then

                Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)

                ToastNotification.Show(Me, "Código de Venta ".ToUpper + tbCodigo.Text + " eliminado con Exito.".ToUpper,
                                          img, 2000,
                                          eToastGlowColor.Green,
                                          eToastPosition.TopCenter)

                _prFiltrar()
            Else
                ToastNotification.Show(Me, mensajeError, My.Resources.WARNING, 8000, eToastGlowColor.Red, eToastPosition.TopCenter)
            End If
        End If

    End Sub

    Public Function _ValidarCampos() As Boolean


        If (_CodCliente <= 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un ALumno con Ctrl+Enter".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            tbCliente.Focus()
            Return False
        End If

        If (cbServicio.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un tipo de servicio".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbServicio.Focus()
            Return False
        End If
        If (cbventa.SelectedIndex < 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione un tipo de venta".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            cbventa.Focus()
            Return False
        End If
        If (grDetalle.RowCount = 0) Then
            Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
            ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return False
        End If

        If (grDetalle.RowCount > 0) Then
            grDetalle.Row = grDetalle.RowCount - 1
            If (grDetalle.GetValue("vdcmin") = 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                ToastNotification.Show(Me, "Por Favor Seleccione  un detalle de producto".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                Return False
            End If
        End If



        'codigo danny
        'validar detalle si todos los servicios son con factura



        Return True
    End Function
    Public Sub _GuardarNuevo()

        Dim numi As String = ""
        ' @vcnumi  ,@vcsector ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
        '@vcest ,@vcobs ,@vcdesc ,@vctotal ,@vcmoneda,@vcbanco
        Dim res As Boolean = L_fnGrabarVentaAlumno(numi, cbServicio.Value, 1, FechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente, tbcredito.Value.ToString("yyyy/MM/dd"), cbventa.Value, 1, tbObservacion.Text, 0, 0, CType(grDetalle.DataSource, DataTable), IIf(cbmoneda.Value = True, 1, 0), 0)
        If res Then

            ToastNotification.Show(Me, "Codigo de Servicio Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            '_prMesajeImprimi(tbCodigo.Text)
            _prImprimir(numi)
            _prCargarVenta()

            _Limpiar()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser insertado.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If

    End Sub

    Public Sub _prGuardarModificado()
        Dim res As Boolean = L_fnModificarVentaAlumno(tbCodigo.Text, cbServicio.Value, 1, FechaVenta.Value.ToString("yyyy/MM/dd"), _CodCliente, tbcredito.Value.ToString("yyyy/MM/dd"), cbventa.Value, 1, tbObservacion.Text, 0, 0, CType(grDetalle.DataSource, DataTable), IIf(cbmoneda.Value = True, 1, 0), 0)
        If res Then

            ToastNotification.Show(Me, "Codigo de Servicio Venta ".ToUpper + tbCodigo.Text + " Grabado con Exito.".ToUpper, My.Resources.GRABACION_EXITOSA, 5000, eToastGlowColor.Green, eToastPosition.TopCenter)
            _prImprimir(tbCodigo.Text)
            _prCargarVenta()

            _Limpiar()
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Venta no pudo ser insertado.".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        End If
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If _ValidarCampos() = False Then
            Exit Sub
        End If

        If (tbCodigo.Text = String.Empty) Then
            _GuardarNuevo()
        Else
            If (tbCodigo.Text <> String.Empty) Then
                _prGuardarModificado()
                ''    _prInhabiliitar()

            End If
        End If
    End Sub

    Private Sub grDetalle_KeyDown(sender As Object, e As KeyEventArgs) Handles grDetalle.KeyDown
        If (_fnVisualizarRegistros()) Then
            Return
        End If

        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grDetalle.Col
            f = grDetalle.Row

            If (grDetalle.Col = grDetalle.RootTable.Columns("vdcmin").Index) Then
                If (grDetalle.GetValue("servicio").ToString <> String.Empty) Then
                    _prAddFilaDetalle()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Servicio Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
            If (grDetalle.Col = grDetalle.RootTable.Columns("servicio").Index) Then
                If (grDetalle.GetValue("servicio").ToString <> String.Empty) Then
                    _prAddFilaDetalle()
                    _HabilitarProductos()
                Else
                    ToastNotification.Show(Me, "Seleccione un Servicio Por Favor", My.Resources.WARNING, 3000, eToastGlowColor.Red, eToastPosition.TopCenter)
                End If

            End If
salirIf:
        End If

        If (e.KeyData = Keys.Control + Keys.Enter And grDetalle.Row >= 0 And
            grDetalle.Col = grDetalle.RootTable.Columns("servicio").Index) Then
            Dim indexfil As Integer = grDetalle.Row
            Dim indexcol As Integer = grDetalle.Col
            _HabilitarProductos()

        End If
        If (e.KeyData = Keys.Escape And grDetalle.Row >= 0) Then
            _prEliminarFila()
        End If

    End Sub

    Private Sub _HabilitarProductos()
        GpPanelServicio.Visible = True

        PanelInferior.Visible = False
        _prCargarAyudaServicios()

        grProducto.Focus()
        grProducto.MoveTo(grProducto.FilterRow)
        grProducto.Col = 1
    End Sub
    Public Sub _prEliminarFila()
        If (grDetalle.Row >= 0) Then
            If (grDetalle.RowCount >= 2) Then
                Dim estado As Integer = grDetalle.GetValue("estado")
                Dim pos As Integer = -1
                Dim lin As Integer = grDetalle.GetValue("vdnumi")
                _fnObtenerFilaDetalle(pos, lin)
                If (estado = 0) Then
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = -2

                End If
                If (estado = 1) Then
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = -1
                End If
                grDetalle.RootTable.ApplyFilter(New Janus.Windows.GridEX.GridEXFilterCondition(grDetalle.RootTable.Columns("estado"), Janus.Windows.GridEX.ConditionOperator.GreaterThanOrEqualTo, 0))
                _prCalcularPrecioTotal()
                grDetalle.Select()
                grDetalle.Col = 5
                grDetalle.Row = grDetalle.RowCount - 1
            End If
        End If
    End Sub
    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(grDetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(grDetalle.DataSource, DataTable).Rows(i).Item("vdnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next
    End Sub

    Private Sub grDetalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles grDetalle.EditingCell
        If (Not _fnVisualizarRegistros()) Then

            If (e.Column.Index = grDetalle.RootTable.Columns("vdcmin").Index Or e.Column.Index = grDetalle.RootTable.Columns("vdpbas").Index) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If

        End If


    End Sub

    Private Sub grDetalle_Enter(sender As Object, e As EventArgs) Handles grDetalle.Enter
        grDetalle.Focus()
        If (grDetalle.RowCount <= 0) Then
            _prAddFilaDetalle()

        End If
        'grdetalle.Col = 0
        grDetalle.Row = 0
    End Sub

    Private Sub grDetalle_CellEdited(sender As Object, e As ColumnActionEventArgs) Handles grDetalle.CellEdited
        If (e.Column.Index = grDetalle.RootTable.Columns("vdcmin").Index) Then
            If (Not IsNumeric(grDetalle.GetValue("vdcmin")) Or grDetalle.GetValue("vdcmin").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grDetalle.GetValue("vdnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                grDetalle.SetValue("vdcmin", 1)
                Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdpbas")
                If (estado = 1) Then
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            Else
                If (grDetalle.GetValue("vdcmin") > 0) Then
                    Dim lin As Integer = grDetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdpbas") * grDetalle.GetValue("vdcmin")

                    If (estado = 1) Then
                        CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grDetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1
                    Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdpbas")
                    If (estado = 1) Then
                        CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If
                    grDetalle.SetValue("vdcmin", 1)
                End If
            End If
        End If


        If (e.Column.Index = grDetalle.RootTable.Columns("vdpbas").Index) Then
            If (Not IsNumeric(grDetalle.GetValue("vdpbas")) Or grDetalle.GetValue("vdpbas").ToString = String.Empty) Then

                'grDetalle.GetRow(rowIndex).Cells("cant").Value = 1
                '  grDetalle.CurrentRow.Cells.Item("cant").Value = 1
                Dim lin As Integer = grDetalle.GetValue("vdnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, lin)
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grDetalle.GetValue("vdptot2")

                grDetalle.SetValue("vdcmin", grDetalle.GetValue("vdptot2"))
                Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdptot2") * grDetalle.GetValue("vdcmin")

                CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grDetalle.GetValue("vdptot2") * grDetalle.GetValue("vdcmin")
                If (estado = 1) Then
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                End If
            Else
                If (grDetalle.GetValue("vdpbas") > 0) Then
                    Dim lin As Integer = grDetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")

                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdpbas") * grDetalle.GetValue("vdcmin")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grDetalle.GetValue("vdpbas") * grDetalle.GetValue("vdcmin")
                    If (estado = 1) Then
                        CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                Else
                    Dim lin As Integer = grDetalle.GetValue("vdnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, lin)
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grDetalle.GetValue("vdptot2")

                    grDetalle.SetValue("vdcmin", grDetalle.GetValue("vdptot2"))
                    Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grDetalle.GetValue("vdptot2") * grDetalle.GetValue("vdcmin")

                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grDetalle.GetValue("vdptot2") * grDetalle.GetValue("vdcmin")
                    If (estado = 1) Then
                        CType(grDetalle.DataSource, DataTable).Rows(pos).Item("estado") = 2
                    End If

                End If
            End If
        End If
        _prCalcularPrecioTotal()
    End Sub

    Private Sub grProducto_KeyDown(sender As Object, e As KeyEventArgs) Handles grProducto.KeyDown
        If (_fnVisualizarRegistros()) Then
            Return
        End If
        If (e.KeyData = Keys.Enter) Then
            Dim f, c As Integer
            c = grProducto.Col
            f = grProducto.Row
            If (f >= 0) Then


                grDetalle.Row = grDetalle.RowCount - 1
                If ((grDetalle.GetValue("vdserv") > 0)) Then
                    _prAddFilaDetalle()
                End If

                Dim pos As Integer = -1
                grDetalle.Row = grDetalle.RowCount - 1
                _fnObtenerFilaDetalle(pos, grDetalle.GetValue("vdnumi"))


                Dim existe As Boolean = _fnExisteProducto(grProducto.GetValue("ednumi"))
                If ((pos >= 0)) Then 'And (Not existe)
                    'servicio.ednumi, servicio.eddesc, servicio.edprec 
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdserv") = grProducto.GetValue("ednumi")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("servicio") = grProducto.GetValue("eddesc")


                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdpbas") = grProducto.GetValue("edprec")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdptot") = grProducto.GetValue("edprec")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdtotdesc") = grProducto.GetValue("edprec")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdptot2") = grProducto.GetValue("edprec")
                    CType(grDetalle.DataSource, DataTable).Rows(pos).Item("vdcmin") = 1

                    grProducto.RemoveFilters()
                    grProducto.Focus()
                    grProducto.MoveTo(grProducto.FilterRow)
                    grProducto.Col = 1

                Else
                    If (existe) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                        ToastNotification.Show(Me, "El Servicio ya existe en el detalle".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        grProducto.RemoveFilters()
                        grProducto.Focus()
                        grProducto.MoveTo(grProducto.FilterRow)
                        grProducto.Col = 1
                    End If
                End If
            End If
        End If
        If e.KeyData = Keys.Escape Then
            _DesHabilitarProductos()
            _prCalcularPrecioTotal()
        End If
    End Sub

    Public Function _fnExisteProducto(idserv As Integer) As Boolean
        For i As Integer = 0 To CType(grDetalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _idprod As Integer = CType(grDetalle.DataSource, DataTable).Rows(i).Item("vdserv")
            Dim estado As Integer = CType(grDetalle.DataSource, DataTable).Rows(i).Item("estado")
            If (_idprod = idserv And estado >= 0) Then

                Return True
            End If
        Next
        Return False
    End Function

    Private Sub _DesHabilitarProductos()
        If (GpPanelServicio.Visible = True) Then
            GpPanelServicio.Visible = False
            PanelInferior.Visible = True
            grDetalle.Select()
            grDetalle.Col = 4
            grDetalle.Row = grDetalle.RowCount - 1

        End If

    End Sub

    Private Sub F0_VentaServicios_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()

    End Sub

    Private Sub grVentas_SelectionChanged(sender As Object, e As EventArgs) Handles grVentas.SelectionChanged
        If (grVentas.RowCount >= 0 And grVentas.Row >= 0) Then

            _prMostrarRegistro(grVentas.Row)
        End If
    End Sub

    Private Sub cbventa_ValueChanged(sender As Object, e As EventArgs) Handles cbventa.ValueChanged
        If (cbventa.Value = 1) Then
            lbcredito.Visible = True
            tbcredito.Visible = True

        Else
            lbcredito.Visible = False
            tbcredito.Visible = False
        End If
    End Sub
    Private Sub _prImprimir(_numiVenta As Integer)
        Dim _dt As New DataTable
        _dt = L_prReporteServicioVentaClienteAlumno(_numiVenta)
        Dim form As New PR_ServiciosVenta
        form._dt = _dt
        form.pTitulo = "V E N T A S   D E   S E R V I C I O S"
        form.pTipo = 2
        form.Show()


    End Sub
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        _prImprimir(tbCodigo.Text)
    End Sub
#End Region



End Class