USE [DBDies_Monaco]
GO

/****** Object:  View [dbo].[Vr_HistorialAlumno]    Script Date: 11/07/2019 6:56:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Vr_HistorialAlumno]
AS
	
	SELECT 1 as numi,a.egalum, b.cbape,b.cbnom,  a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Clases Practicas Programadas' as concepto,
				   (select count(bb.ehnumi) from tce0061 bb where a.egnumi=bb.ehnumi and bb.ehest in (0,1) ) as cantidaddias,
					cast(a.egfact as nvarchar(20)) as fecha,
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =8967

	union
		
	SELECT 2 as numi,a.egalum, b.cbape,b.cbnom,
				   a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Clases Asistidas' as concepto,
				   (select count(bb.ehnumi) from tce0061 bb where a.egnumi=bb.ehnumi and bb.ehest=1 ) as cantidaddias,
				   (SELECT STUFF((SELECT ', ' + Cast(bb.ehfec as nvarchar(20))
					 from tce0061 bb where a.egnumi =bb.ehnumi and bb.ehest=1
					FOR XML PATH ('')),
				1,2, ''))as fecha,
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =8967

union
		
	SELECT 3 as numi, a.egalum, b.cbape,b.cbnom,
				   a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Permisos' as concepto,
				   (select count(bb.ehnumi) from tce0061 bb where a.egnumi=bb.ehnumi and bb.ehest=2 ) as cantidaddias,
				   (SELECT STUFF((SELECT ', ' + Cast(bb.ehfec as nvarchar(20))
					 from tce0061 bb where a.egnumi =bb.ehnumi and bb.ehest=2
					FOR XML PATH ('')),
				1,2, '')),
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =8967
union
		
	SELECT 4 as numi,a.egalum, b.cbape,b.cbnom,
				   a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Faltas' as concepto,
				   (select count(bb.ehnumi) from tce0061 bb where a.egnumi=bb.ehnumi and bb.ehest=-1 ) as cantidaddias,
				   (SELECT STUFF((SELECT ', ' + Cast(bb.ehfec as nvarchar(20))
					 from tce0061 bb where a.egnumi =bb.ehnumi and bb.ehest=-1
					FOR XML PATH ('')),
				1,2, '')) as fecha,
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =8967
union
		
	SELECT 5 as numi, a.egalum, b.cbape,b.cbnom,
				   a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Clases Teoricas' as concepto,
				   (SELECT count (aa.ekline)
			FROM TCE008 aa
			where aa.ekalum=b.cbnumi and aa.ektipo=1) as cantidaddias,
				   '' as fecha,
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =8967

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
      Begin ColumnWidths = 16
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
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vr_HistorialAlumno'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Vr_HistorialAlumno'
GO


