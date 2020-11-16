Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Imports Janus.Windows.GridEX
Imports System.IO
Public Class HorarioDisponibleAlumnos
    Dim RutaGlobal As String = gs_CarpetaRaiz
    Dim RutaTemporal As String = "C:\Temporal"
    Private Sub P_prInicio()
        'Abrir conexion dsds
        If (Not gb_ConexionAbierta) Then
            L_prAbrirConexion()
        End If

        Me.WindowState = FormWindowState.Maximized
        tbFecha.Value = Now.Date
    End Sub
    Private Sub MostrarMensajeError(mensaje As String)
        ToastNotification.Show(Me,
                               mensaje.ToUpper,
                               My.Resources.WARNING,
                               5000,
                               eToastGlowColor.Red,
                               eToastPosition.TopCenter)

    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim dt As DataTable = ArmarEstructura()
        '  filtrar(dt)
        If (dt.Rows.Count > 0) Then
            grDatos.DataSource = dt
            grDatos.RetrieveStructure()
            grDatos.AlternatingColors = True
            Dim numero As Integer = 0
            With grDatos.RootTable.Columns("Horario")
                .Caption = "Horario"
                .Width = 120
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Center
                .FormatString = ""
                .Visible = True
            End With
            Dim personal As DataTable = L_prPersonal()
            For Each fila As DataRow In personal.Rows
                With grDatos.RootTable.Columns("ACTUAL" + numero.ToString)
                    .Caption = "ACTUAL"
                    .Width = 120
                    .HeaderAlignment = TextAlignment.Center
                    .TextAlignment = TextAlignment.Center
                    .FormatString = ""
                    .Visible = True
                End With
                With grDatos.RootTable.Columns("SIGUIENTE" + numero.ToString)
                    .Caption = "SIGUIENTE"
                    .Width = 120
                    .HeaderAlignment = TextAlignment.Center
                    .TextAlignment = TextAlignment.Center
                    .FormatString = ""
                    .Visible = True
                End With
                With grDatos.RootTable.Columns("RESERVA" + numero.ToString)
                    .Caption = "RESERVA"
                    .Width = 120
                    .HeaderAlignment = TextAlignment.Center
                    .TextAlignment = TextAlignment.Center
                    .FormatString = ""
                    .Visible = True
                End With
            Next
            With grDatos
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
            End With
        Else
            If (Not IsNothing(grDatos) And Not IsNothing(grDatos.DataSource)) Then

                CType(grDatos.DataSource, DataTable).Rows.Clear()

            End If

            ToastNotification.Show(Me, "no hay datos para los parametros seleccionados..!!!",
                                    My.Resources.INFORMATION, 2000,
                                    eToastGlowColor.Blue,
                                    eToastPosition.TopCenter)
        End If
    End Sub
    Private Function ArmarEstructura() As DataTable
        Dim estructura As DataTable = New DataTable
        Dim fecha As DateTime = tbFecha.Value
        Dim personal As DataTable = L_prPersonal()
        Dim numero As Integer = 0
        Dim columnaEstructura As Integer = 0
        Dim filaEstructura As Integer = 0
        Dim numeroColumnaPersonal As Integer = 1
        Dim alumno As String = ""
        Dim claseId As String = 0
        Dim cantidadColumnasPorChofer = 3
        Dim reiniciar As Integer = 1
        estructura.Columns.Add("HORARIO")
        For Each fila As DataRow In personal.Rows
            estructura.Columns.Add("ACTUAL" + numero.ToString)
            estructura.Columns.Add("SIGUIENTE" + numero.ToString)
            estructura.Columns.Add("RESERVA" + numero.ToString)
            numero += 1
        Next

        Dim horario As DataTable = L_prHorario()
        For Each filaHorario As DataRow In horario.Rows
            estructura.Rows.Add(filaHorario.Item("Hora"))
            For i As Integer = filaEstructura To estructura.Rows.Count() - 1 Step 1
                columnaEstructura = reiniciar
                numeroColumnaPersonal = reiniciar
                For Each filaPersonal As DataRow In personal.Rows
                    fecha = tbFecha.Value
                    While columnaEstructura <= numeroColumnaPersonal * cantidadColumnasPorChofer
                        If estructura.Columns.Count() >= columnaEstructura Then
                            alumno = L_prDatosGenetalesPorAlumno(fecha,
                                                                filaPersonal.Item("IdPersonal"),
                                                                filaHorario.Item("Hora"),
                                                                claseId)
                            estructura(filaEstructura)(columnaEstructura) = alumno
                            columnaEstructura += 1
                            fecha = L_prFechaSiguiente(filaPersonal.Item("IdPersonal"),
                                                       filaHorario.Item("Hora"), claseId).Rows(0).Item(0)
                        End If
                    End While
                    numeroColumnaPersonal += 1
                Next
                filaEstructura += 1
            Next
        Next

        numeroColumnaPersonal = 0
        Return estructura
    End Function

    Private Sub HorarioDisponibleAlumnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ArmarEstructura()
    End Sub
End Class