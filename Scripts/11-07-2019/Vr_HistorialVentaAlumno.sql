USE [DBDies_Monaco]
GO

/****** Object:  View [dbo].[Vr_HistorialVentaAlumno]    Script Date: 11/07/2019 6:56:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vr_HistorialVentaAlumno]
AS
select cliente.cbnumi   as numicliente,concat(cliente.cbnom ,' ',cliente.cbape  )  as cliente
,venta.vcnumi  as numiventa,venta.vctipo ,'Principal' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy')  as fechaventa,FORMAT (venta.vcfvcr , 'dd-MM-yyyy')  as fechacredito,(select sum (detalle .vdtotdesc ) from TV0021 as detalle where detalle .vdvc2numi =venta.vcnumi )  as venta,
(IIF(venta.vctipo =0,(select sum (detalle .vdtotdesc ) from TV0021 as detalle where detalle .vdvc2numi =venta.vcnumi ),
isnull((select Sum(Isnull(detallepago.tdmonto,0) ) 
from TV0012 as a inner join TV00121 detallepago on detallepago.tdtv12numi =a.tcnumi and a.tctv1numi =venta.vcnumi),0)))as Pagos,
( IIF (venta.vctipo =0,0,
(select sum (detalle .vdtotdesc ) from TV0021 as detalle where detalle .vdvc2numi =venta.vcnumi )-isnull((select Sum(Isnull(detallepago.tdmonto,0) ) 
from TV0012 as a inner join TV00121 detallepago on detallepago.tdtv12numi =a.tcnumi and a.tctv1numi =venta.vcnumi
group by a.tctotcre),0)))as saldo,
IIF (vctipo =0,0,DATEDIFF(DAY,venta.vcfdoc  , Getdate())) as mora,IIF(venta.vctipo =0,'NO','SI') as EsVentaCredito,
(SELECT STUFF((SELECT ', ' + Concat(servicio.eddesc ,' ',detalle.vdtotdesc ,' Bs' )
					 from TV0021 as detalle,TCE004 as servicio where detalle .vdvc2numi =venta.vcnumi  and servicio.ednumi =detalle .vdserv  
					FOR XML PATH ('')),
				1,2, ''))as detalle
from TV002  as venta
inner join TCE002 as cliente on 
 cliente .cbnumi  =venta.vcclie  
group by cliente .cbnumi  ,cliente .cbnom ,cliente .cbape ,venta.vcnumi ,venta .vcfdoc  ,venta.vcfvcr,venta.vctotal,venta.vctipo    

--select * from TV0012 as a Sum(Isnull(detallepago.tdmonto,0) ) 

--left join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi  (a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0)


--select * from TV002 


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vr_HistorialVentaAlumno'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vr_HistorialVentaAlumno'
GO


