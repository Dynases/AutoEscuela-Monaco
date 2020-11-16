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
    Public Sub aplicarCondicionJanues()
        Dim fc As GridEXFormatCondition
        fc = New GridEXFormatCondition(grDatos.RootTable.Columns("tipo"), ConditionOperator.Equal, 1)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.BackColor = Color.DarkGray
        grDatos.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDatos.RootTable.Columns("tipo"), ConditionOperator.Equal, 2)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.BackColor = Color.Silver
        grDatos.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDatos.RootTable.Columns("tipo"), ConditionOperator.Equal, 3)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.BackColor = Color.LightGray
        grDatos.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDatos.RootTable.Columns("tipo"), ConditionOperator.Equal, 4)
        fc.FormatStyle.FontBold = TriState.True
        fc.FormatStyle.BackColor = Color.Gainsboro
        grDatos.RootTable.FormatConditions.Add(fc)

        fc = New GridEXFormatCondition(grDatos.RootTable.Columns("tipo"), ConditionOperator.Equal, 5)
        'fc.FormatStyle.BackColor = Color.LightSlateGray

        grDatos.RootTable.FormatConditions.Add(fc)
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim dt As DataTable = ArmarEstructura()
        '  filtrar(dt)
        If (dt.Rows.Count > 0) Then
            grDatos.DataSource = dt
            grDatos.RetrieveStructure()
            grDatos.AlternatingColors = True

            With grDatos.RootTable.Columns("codigo")
                .Caption = "codigo"
                .Width = 120
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Center
                .FormatString = ""
                .Visible = True
            End With
            With grDatos.RootTable.Columns("descripcion")
                .Caption = "empresa/categoria/marca/atributo/producto"
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Near
                .FormatString = ""
                .Width = 500
                .Visible = True
            End With
            With grDatos.RootTable.Columns("cajas")
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Far
                .Caption = "cajas"
                .FormatString = "0.00"
                .Width = 160
                .Visible = True
            End With

            With grDatos.RootTable.Columns("importe")
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Far
                .Caption = "importe"
                .FormatString = "0.00"
                .Width = 160
                .Visible = True
            End With
            With grDatos.RootTable.Columns("nroclientes")
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Far
                .Caption = "nroclientes"
                .FormatString = "0.00"
                .Width = 160
                .Visible = True
            End With
            With grDatos.RootTable.Columns("porcentaje")
                .HeaderAlignment = TextAlignment.Center
                .TextAlignment = TextAlignment.Far
                .Caption = "porcentaje"
                .FormatString = "0.00"
                .Width = 160
                .Visible = True
            End With
            With grDatos.RootTable.Columns("tipo")
                .Visible = False
            End With
            With grDatos
                .DefaultFilterRowComparison = FilterConditionOperator.Contains
                .FilterMode = FilterMode.Automatic
                .FilterRowUpdateMode = FilterRowUpdateMode.WhenValueChanges
                .GroupByBoxVisible = False
                'diseño de la grilla
                .VisualStyle = VisualStyle.Office2007
            End With
            aplicarCondicionJanues()
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
    Private Shared Function ArmarEstructura() As DataTable
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
        ArmarEstructura()
    End Sub
End Class