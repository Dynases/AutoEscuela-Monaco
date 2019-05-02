Imports Logica.AccesoLogica
Imports DevComponents.DotNetBar
Imports DevComponents.DotNetBar.Controls
Public Class Fr_Morosidad

    'gb_FacturaIncluirICE
    Public _nameButton As String
    Public _tab As SuperTabItem
    Dim Almacen As String = ""
    Dim Vendedor As String = ""
    Public _modulo As SideNavItem

    Public Sub _prIniciarTodo()
        'L_prAbrirConexion(gs_Ip, gs_UsuarioSql, gs_ClaveSql, gs_NombreBD)
        _PMIniciarTodo()
        Me.Text = "INFORME DE MOROSIDAD"
        MReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None

    End Sub

    Public Sub _prInterpretarDatos(ByRef _dt As DataTable)

        _dt = L_fnReporteMorosidadTodosAlmacenVendedor()
        Almacen = "TODOS ALMACENES"
        Vendedor = "TODOS VENDEDORES"

    End Sub

    Private Sub _prCargarReporte()
        Dim _dt As New DataTable
        _prInterpretarDatos(_dt)
        If (_dt.Rows.Count > 0) Then


            Dim objrep As New R_MorosidadCredito
                objrep.SetDataSource(_dt)
                objrep.SetParameterValue("usuario", L_Usuario)
                objrep.SetParameterValue("vendedor", Vendedor)
                objrep.SetParameterValue("almacen", Almacen)
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

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        _prCargarReporte()
    End Sub

    Private Sub Fr_Morosidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _prIniciarTodo()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()

    End Sub
End Class