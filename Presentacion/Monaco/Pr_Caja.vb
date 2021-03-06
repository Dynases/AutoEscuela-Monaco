﻿Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Public Class Pr_Caja
    Public Sub _prIniciarTodo()
        L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        tbFechaI.Value = Now.Date
        tbFechaF.Value = Now.Date
        _PMIniciarTodo()

        Me.Text = "REPORTE CAJA DETALLADO"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable, ByRef _dt1 As DataTable)


        _dt = L_prVentasCajaDetalleTodosSucursal(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

        _dt1 = L_prVentasCajaTipoVentaTodosSucursal(tbFechaI.Value.ToString("yyyy/MM/dd"), tbFechaF.Value.ToString("yyyy/MM/dd"))

    End Sub

    Private Sub _prCargarReporte()
        Try
            Dim _dt As New DataTable
            Dim dt1 As DataTable
            Dim _total As Decimal = 0
            _prInterpretarDatos(_dt, dt1)
            'Obtiene el saldo todal (Venta + credito) - gasto
            If Not dt1.Rows.Count = 0 Then
                For Each fila As DataRow In dt1.Rows
                    If fila.Item("yccod3") = 4 Then
                        _total = _total - fila.Item("total")
                    Else
                        _total = _total + fila.Item("total")
                    End If
                Next
            End If

            If (_dt.Rows.Count > 0) Then
                Dim objrep As New R_VentaCaja
                objrep.Subreports.Item("R_TipoVenta.rpt").SetDataSource(dt1)
                objrep.SetDataSource(_dt)
                Dim fechaI As String = tbFechaI.Value.ToString("dd/MM/yyyy")
                Dim fechaF As String = tbFechaF.Value.ToString("dd/MM/yyyy")
                objrep.SetParameterValue("usuario", L_Usuario)
                objrep.SetParameterValue("fechaI", fechaI)
                objrep.SetParameterValue("fechaF", fechaF)
                objrep.SetParameterValue("_total", _total)
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

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        End Try
    End Sub
    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub Pr_Caja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()

    End Sub
End Class