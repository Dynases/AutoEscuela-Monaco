﻿Imports DevComponents.DotNetBar.Controls
Imports System.Threading
Imports System.Drawing.Text
Imports Logica.AccesoLogica
Imports Janus.Windows.GridEX
Imports DevComponents.DotNetBar
Imports System.IO
Public Class F0_PagoCliente
#Region "Variables Globales"
    Dim precio As DataTable
    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem
    Dim Bin As New MemoryStream
    Public _inicioDesde As Integer = 0
    Public _codigoCliente As Integer = 0
    Public _nombreCliene As String
    Public _tipoServicio As Integer
    Public formularioVenta As Integer = 1
#End Region

#Region "METODOS PRIVADOS"

    Private Sub _IniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        Me.WindowState = FormWindowState.Maximized
        _prAsignarPermisos()
        Me.Text = "PAGOS"
        _prCargarComboLibreria(cbServicio, 6, 1)
        _prCargarTablaPagos(-1, -1)
        tbCodigo.ReadOnly = True
        tbNombre.ReadOnly = True
        tbFechaVenta.Value = Now.Date
        tbNombre.Focus()
        If (CType(cbServicio.DataSource, DataTable).Rows.Count > 0) Then
            cbServicio.SelectedIndex = 0
        End If
        btnNuevo.Visible = False
        btnModificar.Visible = False
        If _inicioDesde = formularioVenta Then  'Inicia desde Venta
            cbServicio.Value = _tipoServicio
            tbCodigo.Text = _codigoCliente
            tbnrocod.Text = _codigoCliente
            tbNombre.Text = _nombreCliene
            _prCargarTablaPagos(_codigoCliente, cbServicio.Value)
        End If
    End Sub

    Private Sub _Limpiar()
        tbCodigo.Clear()
        tbnrocod.Clear()
        tbFechaVenta.Value = Now.Date
        tbNombre.Clear()
        tbTotalCobrado.Text = 0
        tbTotalCobrar.Text = 0
        tbSaldo.Text = 0
        tbNombre.Clear()
        If (CType(cbServicio.DataSource, DataTable).Rows.Count > 0) Then
            cbServicio.SelectedIndex = 0
        End If
        _prCargarTablaPagos(-1, -1)
        '_prAddDetalle()
        tbnrocod.Focus()



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
    Private Sub _prCargarTablaPagos(_numi As Integer, sector As Integer)

        Dim dt As New DataTable
        dt = L_fnObtenerLasVentasCreditoPorCliente(_numi, sector)
        '_prCargarIconDelete(dt)
        gr_detalle.DataSource = dt
        gr_detalle.RetrieveStructure()
        gr_detalle.AlternatingColors = True
        '      ' a.tcnumi, NroDoc,as factura,a.tctv1numi ,a.tcty4clie ,  cliente,a.tcty4vend, vendedor,a.tcfdoc
        ',a.tcfvencre,totalfactura, pendiente, PagoAc, Pagar
        'With gr_detalle.RootTable.Columns("factura")
        '    .Width = 100
        '    .Visible = False
        'End With
        With gr_detalle.RootTable.Columns("tctv1numi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcty4clie")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("cliente")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("pendiente2")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcty4vend")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("vendedor")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("pendiente2")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("tcnumi")
            .Width = 100
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("NroDoc")
            .Width = 150
            .Visible = True
            .TextAlignment = TextAlignment.Far
            .Caption = "Nro documento"
        End With
        With gr_detalle.RootTable.Columns("tcfdoc")
            .Caption = "Fecha Factura"
            .Width = 120
            .TextAlignment = TextAlignment.Center
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("tcfvencre")
            .Caption = "Fecha Vencimiento"
            .TextAlignment = TextAlignment.Center
            .Width = 160
            .Visible = False
        End With
        With gr_detalle.RootTable.Columns("totalfactura")
            .Caption = "Monto Total"
            .Width = 180
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With
        With gr_detalle.RootTable.Columns("pendiente")
            .Caption = "Saldo"
            .Width = 180
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("PagoAc")
            .Caption = "Total Pagado"
            .Width = 180
            .TextAlignment = TextAlignment.Far
            .FormatString = "0.00"
            .AggregateFunction = AggregateFunction.Sum
            .Visible = True
        End With

        With gr_detalle.RootTable.Columns("Pagar")
            .Width = 100
            .Visible = False
            .Caption = "Pagar!"
        End With



        With gr_detalle
            .GroupByBoxVisible = False
            'diseño de la grilla
            .VisualStyle = VisualStyle.Office2007


            .VisualStyle = VisualStyle.Office2007


            .RowHeaders = InheritableBoolean.True
            .TotalRow = InheritableBoolean.True
            .TotalRowFormatStyle.BackColor = Color.Gold
            .TotalRowPosition = TotalRowPosition.BottomFixed
        End With
        _prAplicarCondiccionJanus()
        _prCalcularTotal()
    End Sub
    Public Sub _prAplicarCondiccionJanus()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(gr_detalle.RootTable.Columns("pendiente"), ConditionOperator.Equal, 0)
        fc.FormatStyle.BackColor = Color.Green
        gr_detalle.RootTable.FormatConditions.Add(fc)
    End Sub


    Private Sub F0_Cobrar_Cliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _IniciarTodo()
    End Sub



    Private Sub Bt1Generar_Click(sender As Object, e As EventArgs) Handles Bt1Generar.Click
        If (tbCodigo.Text <> String.Empty) Then
            tbSaldo.Value = 0
            tbTotalCobrado.Value = 0
            tbTotalCobrar.Value = 0
            _prCargarTablaPagos(tbCodigo.Text, cbServicio.Value)
        End If
    End Sub

    Private Sub gr_detalle_EditingCell(sender As Object, e As EditingCellEventArgs) Handles gr_detalle.EditingCell


        If (e.Column.Index = gr_detalle.RootTable.Columns("Pagar").Index Or e.Column.Index = gr_detalle.RootTable.Columns("PagoAc").Index) Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub gr_detalle_CellValueChanged(sender As Object, e As ColumnActionEventArgs) Handles gr_detalle.CellValueChanged
        ''Dim rowIndex As Integer = gr_detalle.CurrentRow.RowIndex
        Dim rowIndex As Integer = gr_detalle.Row
        'Columna de Precio Venta
        Dim ob As Boolean = gr_detalle.GetValue("Pagar")


        If (e.Column.Index = gr_detalle.RootTable.Columns("Pagar").Index) Then

            ''''''''ÁQUI VERIFICO EL CHECK DE PAGAR TODO EL SALDO
            'If (ob = True) Then
            '    'pendiente, PagoAc, Pagar
            '    tbTotalCobrado.Value = tbTotalCobrado.Value + gr_detalle.GetValue("pendiente")
            '    tbSaldo.Value = tbSaldo.Value - gr_detalle.GetValue("pendiente")
            '    gr_detalle.SetValue("PagoAc", gr_detalle.GetValue("pendiente"))
            '    gr_detalle.SetValue("pendiente", 0)

            'Else
            '    tbTotalCobrado.Value = tbTotalCobrado.Value - gr_detalle.GetValue("PagoAc")
            '    tbSaldo.Value = tbSaldo.Value + gr_detalle.GetValue("PagoAc")
            '    gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente") + gr_detalle.GetValue("PagoAc"))
            '    gr_detalle.SetValue("PagoAc", 0)
            'End If
            '_prCalcularTotal()
        End If
        ''''''''''''''Aqui se valida por el monto del saldo '''''''''''''''


        If (e.Column.Index = gr_detalle.RootTable.Columns("PagoAc").Index) Then


            If (Not IsNumeric(gr_detalle.GetValue("PagoAc")) Or gr_detalle.GetValue("PagoAc").ToString = String.Empty Or IsDBNull(gr_detalle.GetValue("PagoAc"))) Then

                Dim lin As Integer = gr_detalle.GetValue("tcnumi")
                Dim pos As Integer = -1
                _fnObtenerFilaDetalle(pos, gr_detalle.GetValue("tcnumi"))
                CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente") = CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente2")
                CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("PagoAc") = 0
                CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pagar") = False

                gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente2"))
                gr_detalle.SetValue("pagar", False)
                gr_detalle.SetValue("PagoAc", 0)

                _prSumarTotales()
            Else
                If (gr_detalle.GetValue("PagoAc") >= 0 And gr_detalle.GetValue("PagoAc") <= gr_detalle.GetValue("pendiente2")) Then
                    Dim lin As Integer = gr_detalle.GetValue("tcnumi")
                    Dim pos As Integer = -1
                    _fnObtenerFilaDetalle(pos, gr_detalle.GetValue("tcnumi"))
                    CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente") = CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente2") - gr_detalle.GetValue("PagoAc")
                    CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("PagoAc") = gr_detalle.GetValue("PagoAc")
                    gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente2") - gr_detalle.GetValue("PagoAc"))

                    gr_detalle.SetValue("pagar", True)
                    CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pagar") = True

                    _prSumarTotales()
                Else
                    If (gr_detalle.GetValue("PagoAc") > gr_detalle.GetValue("pendiente2")) Then
                        Dim img As Bitmap = New Bitmap(My.Resources.Mensaje, 50, 50)
                        ToastNotification.Show(Me, "El monto a pagar es mayor al saldo: " + Str(gr_detalle.GetValue("pendiente2")), img, 5000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                        Dim lin As Integer = gr_detalle.GetValue("tcnumi")
                        Dim pos As Integer = -1
                        _fnObtenerFilaDetalle(pos, gr_detalle.GetValue("tcnumi"))
                        CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente") = CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pendiente2")
                        CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("PagoAc") = 0

                        'gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente") + gr_detalle.GetValue("PagoAc"))
                        gr_detalle.SetValue("pendiente", gr_detalle.GetValue("pendiente2"))
                        gr_detalle.SetValue("PagoAc", 0)
                        gr_detalle.SetValue("pagar", False)
                        CType(gr_detalle.DataSource, DataTable).Rows(pos).Item("pagar") = True
                        _prSumarTotales()
                    End If
                End If

            End If
        End If

    End Sub
    Public Sub _prSumarTotales()
        Dim dt As DataTable = CType(gr_detalle.DataSource, DataTable)
        Dim Cobrado As Double = 0
        Dim Saldo As Double = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            Cobrado = Cobrado + dt.Rows(i).Item("PagoAc")
            Saldo = Saldo + dt.Rows(i).Item("pendiente")
        Next
        tbTotalCobrado.Value = Cobrado
        tbSaldo.Value = Saldo

    End Sub

    Public Sub _fnObtenerFilaDetalle(ByRef pos As Integer, numi As Integer)
        For i As Integer = 0 To CType(gr_detalle.DataSource, DataTable).Rows.Count - 1 Step 1
            Dim _numi As Integer = CType(gr_detalle.DataSource, DataTable).Rows(i).Item("tcnumi")
            If (_numi = numi) Then
                pos = i
                Return
            End If
        Next

    End Sub
    Public Sub _prCalcularTotal()


        tbSaldo.Text = gr_detalle.GetTotal(gr_detalle.RootTable.Columns("pendiente"), AggregateFunction.Sum)
        tbTotalCobrar.Text = gr_detalle.GetTotal(gr_detalle.RootTable.Columns("PagoAc"), AggregateFunction.Sum) + gr_detalle.GetTotal(gr_detalle.RootTable.Columns("pendiente"), AggregateFunction.Sum)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Sub _prInterpretarDatosCobranza(ByRef dt As DataTable, ByRef bandera As Boolean)


        '       numidetalle, NroDoc, factura, numiCredito, numiCobranza, A.tctv1numi
        ',a.tcty4clie ,cliente,detalle.tdfechaPago, PagoAc, NumeroRecibo, DescBanco, banco, detalle.tdnrocheque,
        'img,estado,pendiente
        Dim Bin As New MemoryStream
        Dim img As New Bitmap(My.Resources.ELIMINAR, 28, 28)
        img.Save(Bin, Imaging.ImageFormat.Png)
        Dim dtcobro As DataTable = CType(gr_detalle.DataSource, DataTable)
        For i As Integer = 0 To dtcobro.Rows.Count - 1 Step 1
            Dim pago As Double = dtcobro.Rows(i).Item("PagoAc")
            Dim estado As Boolean = dtcobro.Rows(i).Item("Pagar")
            If (estado = True) Then
                '             td.tdtv12numi ,@tenumi ,td.tdnrodoc ,@newFecha ,td.tdmonto ,td.tdnrorecibo ,td.tdty3banco,
                'td.tdnrocheque, @newFecha  ,@newHora  ,@teuact

                '              a.tcnumi, NroDoc,as factura, a.tctv1numi, a.tcty4clie, cliente, a.tcty4vend, vendedor, a.tcfdoc
                ',a.tcfvencre,totalfactura, pendiente, PagoAc, Pagar
                If (pago > 0) Then
                    dt.Rows.Add(0, dtcobro.Rows(i).Item("tcnumi"), 0, dtcobro.Rows(i).Item("NroDoc"),
                                            Now.Date, pago, 0, dtcobro.Rows(i).Item("tcty4vend"), 0, Now.Date,
                                            "", "", Bin.ToArray, 0)
                    bandera = True
                End If

            End If

        Next
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        '_tenumi As String, _tefdoc As String, _tety4vend As Integer, _teobs As String, detalle As DataTable
        Dim numi As String = ""
        Dim img2 As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        If (tbCodigo.Text = String.Empty) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        If (CType(gr_detalle.DataSource, DataTable).Rows.Count <= 0) Then
            ToastNotification.Show(Me, "No existen datos validos".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return

        End If
        Dim dtCobro As DataTable = L_fnCobranzasObtenerLosPagos(-1)
        Dim bandera As Boolean = False

        _prInterpretarDatosCobranza(dtCobro, bandera)
        If (bandera = False) Then
            ToastNotification.Show(Me, "Seleccione un detalle de la lista de pendientes".ToUpper, img2, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            Return
        End If
        Dim res As Boolean = L_fnGrabarCobranza2(numi, tbFechaVenta.Value.ToString("yyyy/MM/dd"), 0, "", dtCobro)
        If res Then

            Dim img As Bitmap = New Bitmap(My.Resources.checked, 50, 50)
            ToastNotification.Show(Me, "El Pago Ha Sido ".ToUpper + " Grabado con Exito.".ToUpper,
                                      img, 2000,
                                      eToastGlowColor.Green,
                                      eToastPosition.TopCenter
                                      )


            _Limpiar()
            If _inicioDesde = formularioVenta Then
                Me.Close()
            End If
        Else
            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
            ToastNotification.Show(Me, "La Compra no pudo ser insertado".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)

        End If
    End Sub
    Private Sub tbnrocod_KeyDown(sender As Object, e As KeyEventArgs) Handles tbnrocod.KeyDown
        'If e.KeyData = Keys.Enter Then
        '    Dim dt As DataTable
        '    If (tbnrocod.Text = String.Empty) Then
        '        Return
        '    End If
        '    dt = L_fnListarClientesUno(tbnrocod.Text)
        '    If (Not IsDBNull(dt)) Then
        '        If (dt.Rows.Count > 0) Then
        '            tbCodigo.Text = dt.Rows(0).Item("ydnumi")
        '            tbnrocod.Text = dt.Rows(0).Item("ydcod")
        '            tbNombre.Text = dt.Rows(0).Item("yddesc")
        '            _prCargarTablaPagos(dt.Rows(0).Item("ydnumi"))
        '        Else
        '            Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '            ToastNotification.Show(Me, "Los Datos Del Cliente No Existe en el sistema".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '            tbCodigo.Clear()
        '            tbnrocod.Clear()
        '            tbNombre.Clear()

        '        End If
        '    Else
        '        Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
        '        ToastNotification.Show(Me, "Los Datos Del Cliente No Existe en el sistema".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
        '        tbCodigo.Clear()
        '        tbnrocod.Clear()
        '        tbNombre.Clear()
        '    End If
        'End If



    End Sub

    Private Sub tbnrocod_TextChanged(sender As Object, e As EventArgs) Handles tbnrocod.TextChanged
        If (tbnrocod.Text = String.Empty) Then
            tbCodigo.Clear()
            tbNombre.Clear()
            _prCargarTablaPagos(-1, -1)
        End If
    End Sub

    Private Sub tbNombre_KeyDown(sender As Object, e As KeyEventArgs) Handles tbNombre.KeyDown

        If e.KeyData = Keys.Control + Keys.Enter Then
            If (cbServicio.SelectedIndex < 0) Then
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "Seleccione un Modulo".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
                cbServicio.Focus()
                Return
            End If
            Dim dt As DataTable

            dt = L_prListarAlumnosVenta(cbServicio.Value)
            '              a.ydnumi, a.ydcod, a.yddesc, a.yddctnum, a.yddirec
            ',a.ydtelf1 ,a.ydfnac 

            Dim listEstCeldas As New List(Of Modelos.Celda)
            listEstCeldas.Add(New Modelos.Celda("codigo", True, "CODIGO", 150))
            listEstCeldas.Add(New Modelos.Celda("ci", True, "CI", 120))
            listEstCeldas.Add(New Modelos.Celda("nombre", True, "NOMBRE COMPLETO", 350))
            Dim ef = New Efecto
            ef.tipo = 3
            ef.dt = dt
            ef.SeleclCol = 2
            ef.listEstCeldas = listEstCeldas
            ef.alto = 50
            ef.ancho = 350
            ef.NameLabel = "ALUMNO :"
            ef.NamelColumna = "yddesc"
            ef.Context = "Seleccione Alumno".ToUpper
            ef.ShowDialog()
            Dim bandera As Boolean = False
            bandera = ef.band
            If (bandera = True) Then
                Try
                    Dim Row As Janus.Windows.GridEX.GridEXRow = ef.Row
                    tbCodigo.Text = Row.Cells("codigo").Value
                    tbnrocod.Text = Row.Cells("codigo").Value
                    tbNombre.Text = Row.Cells("nombre").Value
                    _prCargarTablaPagos(Row.Cells("codigo").Value, cbServicio.Value)
                Catch ex As Exception

                End Try
            Else
                tbCodigo.Clear()
                tbnrocod.Clear()
                tbNombre.Clear()
                Dim img As Bitmap = New Bitmap(My.Resources.cancel, 50, 50)
                ToastNotification.Show(Me, "Los Datos Del Alumno No Existe en el sistema".ToUpper, img, 2000, eToastGlowColor.Red, eToastPosition.BottomCenter)
            End If

        End If
    End Sub

#End Region
End Class