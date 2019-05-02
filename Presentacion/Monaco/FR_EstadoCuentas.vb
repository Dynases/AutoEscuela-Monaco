Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class FR_EstadoCuentas
    'gb_FacturaIncluirICE

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _prCargarComboLibreria(cbServicio, 6, 1) ''Libreria Vehiculo=1  TamVehiculo=4
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()
        Me.Text = "REPORTE DE ESTADOS DE CUENTAS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        _IniciarComponentes()
        If (CType(cbServicio.DataSource, DataTable).Rows.Count > 0) Then
            cbServicio.SelectedIndex = 0
        End If
        cbServicio.Visible = False
        lbmodulo.Visible = False
    End Sub
    Public Sub _IniciarComponentes()
        tbCliente.ReadOnly = True
        tbCliente.Visible = False
        tbCuentas.Visible = False
        lbcliente.Visible = False
        lbCuentas.Visible = False
        CheckTodosCuenta.CheckValue = True
        CheckUnaCuenta.Visible = False
        CheckTodosCuenta.Visible = False

    End Sub
    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)
        If (swCreditoCliente.Value = True) Then
            _dt = L_prReporteCreditoGeneral(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))
        Else
            If (CheckTodosCuenta.Checked And tbCodigoCliente.Text.Length > 0) Then
                _dt = L_prReporteCreditoClienteTodosCuentas(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbCodigoCliente.Text)
            End If
            If (CheckUnaCuenta.Checked And tbCodigoCliente.Text.Length > 0 And tbcodCuenta.Text.Length > 0) Then
                _dt = L_prReporteCreditoClienteUnaCuentas(tbcodCuenta.Text)
            End If

        End If
    End Sub

    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_HisotorialCobrosVentasCredito
            objrep.SetDataSource(_dt)

            Dim ParEmp1 As String = ""
            Dim ParEmp2 As String = ""
            Dim ParEmp3 As String = ""
            Dim ParEmp4 As String = ""

            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.SetParameterValue("usuario", L_Usuario)
            objrep.SetParameterValue("fechaI", fechaI)
            objrep.SetParameterValue("fechaF", fechaF)
            objrep.SetParameterValue("Empresa", ParEmp1)
            objrep.SetParameterValue("Empresa1", ParEmp2)
            objrep.SetParameterValue("Empresa2", ParEmp3)
            objrep.SetParameterValue("Empresa3", ParEmp4)
            MReportViewer.ReportSource = objrep
            MReportViewer.Show()
            MReportViewer.BringToFront()
        Else
            ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
                                       My.Resources.INFORMATION, 2000,
                                       eToastGlowColor.Blue,
                                       eToastPosition.BottomLeft)
            MReportViewer.ReportSource = Nothing
        End If
    End Sub
    'Private Sub _prCargarReporte2()
    '    Dim _dt As New DataTable
    '    _prInterpretarDatos2(_dt)
    '    If (_dt.Rows.Count > 0) Then
    '        Dim dt2 As DataTable = L_fnNameReporte()
    '        Dim ParEmp1 As String = ""
    '        Dim ParEmp2 As String = ""
    '        Dim ParEmp3 As String = ""
    '        Dim ParEmp4 As String = ""
    '        If (dt2.Rows.Count > 0) Then
    '            ParEmp1 = dt2.Rows(0).Item("Empresa1").ToString
    '            ParEmp2 = dt2.Rows(0).Item("Empresa2").ToString
    '            ParEmp3 = dt2.Rows(0).Item("Empresa3").ToString
    '            ParEmp4 = dt2.Rows(0).Item("Empresa4").ToString
    '        End If

    '        Dim objrep As New KardexClienteRes
    '        objrep.SetDataSource(_dt)
    '        Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
    '        Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
    '        'objrep.SetParameterValue("usuario", L_Usuario)
    '        objrep.SetParameterValue("FechaDel", fechaI)
    '        objrep.SetParameterValue("FechaAl", fechaF)
    '        objrep.SetParameterValue("Empresa", ParEmp1)
    '        objrep.SetParameterValue("Empresa1", ParEmp2)
    '        objrep.SetParameterValue("Empresa2", ParEmp3)
    '        objrep.SetParameterValue("Empresa3", ParEmp4)
    '        MReportViewer.ReportSource = objrep
    '        MReportViewer.Show()
    '        MReportViewer.BringToFront()
    '    Else
    '        ToastNotification.Show(Me, "NO HAY DATOS PARA LOS PARAMETROS SELECCIONADOS..!!!",
    '                                   My.Resources.INFORMATION, 2000,
    '                                   eToastGlowColor.Blue,
    '                                   eToastPosition.BottomLeft)
    '        MReportViewer.ReportSource = Nothing
    '    End If
    'End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If swdetresum.Value = True Then
            _prCargarReporte()
            'Else
            '    _prCargarReporte2()
        End If
    End Sub

    Private Sub FR_EstadoCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub



    Private Sub CheckUnaALmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckUnaCuenta.CheckValueChanged
        If (CheckUnaCuenta.Checked) Then
            CheckTodosCuenta.CheckValue = False
            tbCuentas.Enabled = True
            tbCuentas.BackColor = Color.White
            tbCuentas.Focus()
            tbCuentas.ReadOnly = False


        End If
    End Sub

    Private Sub CheckTodosAlmacen_CheckValueChanged(sender As Object, e As EventArgs) Handles CheckTodosCuenta.CheckValueChanged
        If (CheckTodosCuenta.Checked) Then
            CheckUnaCuenta.CheckValue = False
            tbCuentas.Enabled = True
            tbCuentas.BackColor = Color.Gainsboro
            tbCuentas.ReadOnly = True


        End If
    End Sub




    Private Sub tbVendedor_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown

        If (swCreditoCliente.Value = False) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim codigo As Integer = -1

                If (cbServicio.SelectedIndex < 0) Then

                    ToastNotification.Show(Me, "Seleccione un Modulo por favor..!!!",
                                               My.Resources.INFORMATION, 2000,
                                               eToastGlowColor.Blue,
                                               eToastPosition.BottomLeft)
                    cbServicio.Focus()
                    Return

                End If
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

                    If (IsNothing(frmAyuda.filaSelect)) Then
                        tbCliente.Focus()
                        Return
                    End If
                    tbCodigoCliente.Text = frmAyuda.filaSelect.Cells("codigo").Value
                    tbCliente.Text = frmAyuda.filaSelect.Cells("nombre").Value
                    btnGenerar.Focus()

                End If
            End If





        End If
    End Sub

    Sub _prHabilitar()
        tbCliente.ReadOnly = True
        tbCliente.Visible = True
        lbmodulo.Visible = True
        cbServicio.Visible = True
        'tbCuentas.Visible = True
        lbcliente.Visible = True
        'lbCuentas.Visible = True
        'CheckTodosCuenta.CheckValue = True
        'CheckUnaCuenta.Visible = True
        'CheckTodosCuenta.Visible = True
        'tbCuentas.BackColor = Color.Gainsboro
        tbCodigoCliente.Clear()
        tbCliente.Clear()
        tbCliente.Focus()
        'tbcodCuenta.Clear()
        'tbCuentas.Clear()
    End Sub

    Private Sub swCreditoCliente_ValueChanged(sender As Object, e As EventArgs) Handles swCreditoCliente.ValueChanged
        If (swCreditoCliente.Value = True) Then
            _IniciarComponentes()
        Else
            _prHabilitar()
        End If
    End Sub

    Private Sub tbCuentas_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCuentas.KeyDown
        If (swCreditoCliente.Value = False And tbCodigoCliente.Text.Length > 0) Then
            If e.KeyData = Keys.Control + Keys.Enter Then
                Dim dt As DataTable
                dt = L_prReporteCreditoListarCuentasPorCliente(tbCodigoCliente.Text)
                Dim frmAyuda As Modelos.ModeloAyuda

                'numiVenta,numeroFactura, fechaVenta,  FechaVencCredito, totalVenta
                Dim listEstCeldas As New List(Of Modelos.Celda)
                listEstCeldas.Add(New Modelos.Celda("tcnumi,", False, "CODIGO", 100))
                listEstCeldas.Add(New Modelos.Celda("numiVenta,", True, "DOC VENTA", 100))
                listEstCeldas.Add(New Modelos.Celda("numeroFactura", False, "NRO FACTURA", 100))
                listEstCeldas.Add(New Modelos.Celda("fechaVenta", True, "FECHA VENTA", 130, "yyyy/MM/dd"))
                listEstCeldas.Add(New Modelos.Celda("FechaVencCredito", True, "VENC.CREDITO".ToUpper, 130, "yyyy/MM/dd"))
                listEstCeldas.Add(New Modelos.Celda("totalVenta", True, "TOTAL VENTA", 130, "0.00"))
                frmAyuda = New Modelos.ModeloAyuda(50, 250, dt, "SELECCIONE UN ALUMNO".ToUpper, listEstCeldas)
                frmAyuda.ShowDialog()


                If frmAyuda.seleccionado = True Then

                    If (IsNothing(frmAyuda.filaSelect)) Then
                        tbCliente.Focus()
                        Return
                    End If

                    tbCuentas.Text = IIf(Not IsDBNull(frmAyuda.filaSelect.Cells("numeroFactura").Value), "FACTURA:" + Str(frmAyuda.filaSelect.Cells("numeroFactura").Value), "") + "  DOC VENTA: " + Str(frmAyuda.filaSelect.Cells("numiVenta").Value)
                    tbcodCuenta.Text = frmAyuda.filaSelect.Cells("tcnumi").Value
                    btnGenerar.Focus()
                End If



            End If

        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()

    End Sub
End Class