Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class Pr_HistorialAlumno

    Public _nameButton As String
    Public _tab As SuperTabItem
    Public _modulo As SideNavItem

    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _prCargarComboLibreria(cbServicio, 6, 1) ''Libreria Vehiculo=1  TamVehiculo=4
        If (CType(cbServicio.DataSource, DataTable).Rows.Count > 0) Then
            cbServicio.SelectedIndex = 0
        End If
        _PMIniciarTodo()
        Me.Text = "REPORTE DE HISTORIAL ALUMNOS"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub
    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _dt = L_prReporteHistorialAlumno(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbCodigoCliente.Text)
        If (_dt.Rows.Count > 0) Then

            Dim objrep As New R_HistorialAlumno
            objrep.SetDataSource(_dt)
            Dim dt2 As DataTable = L_prReporteHistorialAlumnoVentas(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"), tbCodigoCliente.Text)
            Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
            Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
            objrep.Subreports.Item("R_HistorialVentas.rpt").SetDataSource(dt2)
            objrep.SetParameterValue("RangoFecha", fechaI + " Al " + fechaF)
            objrep.SetParameterValue("usuario", L_Usuario)
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

    Private Sub Pr_HistorialAlumno_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub tbVendedor_KeyDown_1(sender As Object, e As KeyEventArgs) Handles tbCliente.KeyDown


        If e.KeyData = Keys.Control + Keys.Enter Then
                Dim codigo As Integer = -1

                If (cbServicio.SelectedIndex < 0) Then

                    ToastNotification.Show(Me, "Seleccione un Modulo por favor..!!!",
                                               My.Resources.INFORMATION, 2000,
                                               eToastGlowColor.Blue,
                                               eToastPosition.BottomLeft)

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


    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class