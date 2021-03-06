USE [DBDies_Monaco]
GO
/****** Object:  StoredProcedure [dbo].[sp_dg_TCE002]    Script Date: 16/06/2019 15:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[sp_dg_TCE002](@tipo int,@cbnumi int=-1,@cbci nvarchar(50)='',@cbnom nvarchar(50)='',@cbape nvarchar(50)='',
								    @cbdirec nvarchar(150)='',@cbtelef1 nvarchar(20)='',@cbtelef2 nvarchar(20)='',@cbemail nvarchar(50)='',
									@cbfnac date=null,@cbfing date=null,@cblnac int=-1,@cbeciv int =-1,@cbprof int=-1,@cbtipo int=-1,
									@cbest int=-1,@cbfot nvarchar(50)='',@cbobs nvarchar(300)='',@cbnumiSoc int=-1,@cbparent int=-1,@cbmen int=-1,
									@cbtutci nvarchar(50)='',@cbtutnom nvarchar(150)='',@cbsuc int=-1,@cbnrogr nvarchar(30)='',@cbnfact nvarchar(50)='',
									@auxFecha as date=null,@cbuact nvarchar(10)='')
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()
	
	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 
			DELETE FROM TCE002 WHERE cbnumi=@cbnumi
			SELECT @cbnumi AS newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=1 --NUEVO REGISTRO
	BEGIN
		BEGIN TRAN INSERTAR
		BEGIN TRY 
			set @cbnumi=IIF((select COUNT(cbnumi) from TCE002)=0,0,(select MAX(cbnumi) from TCE002))+1
			IF @cbfot<>''
			BEGIN
				set @cbfot=CONCAT('alumno_',CONVERT(nvarchar(30),@cbnumi),'.jpg')
			END
			INSERT INTO TCE002 VALUES(@cbnumi,@cbci,@cbnom,@cbape,@cbdirec,@cbtelef1,@cbtelef2,@cbemail,@cbfnac,@cbfing,@cblnac,@cbeciv,
									 @cbprof,@cbtipo,@cbest,@cbfot,@cbobs,@cbnumiSoc,@cbparent,@cbmen,@cbtutci,@cbtutnom,@cbsuc,@cbnrogr,
									 @cbnfact,@newFecha,@newHora,@cbuact)
			
			-- DEVUELVO VALORES DE CONFIRMACION
			SELECT @cbnumi AS newNumi
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@cbuact)

			ROLLBACK TRAN
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRAN MODIFICACION
		BEGIN TRY 
			IF @cbfot<>''
			BEGIN
				set @cbfot=CONCAT('alumno_',CONVERT(nvarchar(30),@cbnumi),'.jpg')
			END
			UPDATE TCE002 SET cbci=@cbci,cbnom=@cbnom,cbape=@cbape,cbdirec=@cbdirec,cbtelef1=@cbtelef1,cbtelef2=@cbtelef2,cbemail=@cbemail,
							  cbfnac=@cbfnac,cbfing=@cbfing,cblnac=@cblnac,cbeciv=@cbeciv,cbprof=@cbprof,cbtipo=@cbtipo,cbest=@cbest,
							  cbfot=@cbfot,cbobs=@cbobs,cbnumiSoc=@cbnumiSoc,cbparent=@cbparent,cbmen=@cbmen,cbtutci=@cbtutci,
							  cbtutnom=@cbtutnom,cbsuc=@cbsuc,cbnrogr= @cbnrogr,cbnfact=@cbnfact,cbfact=@newFecha,cbhact=@newHora,cbuact=@cbuact
			Where cbnumi = @cbnumi;

			--DEVUELVO VALORES DE CONFIRMACION
			select @cbnumi as newNumi
			COMMIT TRAN
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@cbuact)
			ROLLBACK TRAN
		END CATCH
	END

	IF @tipo=3 --MOSTRAR TODOS
	BEGIN
		BEGIN TRY
			
			SELECT cbnumi,cbci,cbnom,cbape,cbdirec,cbtelef1,cbtelef2,cbemail,cbprof,prof.cedesc1 as profdesc,cbtipo,tipo.cedesc1 as tipodesc,CONVERT(VARCHAR(10), cbfnac, 103) as cbfnac,
				   cblnac,nac.cedesc1 as cblnac2,CONVERT(VARCHAR(10), cbfing, 103) as cbfing,cbfot,cbest,CAST(IIF ( cbest = 1, 1, 0 ) AS BIT) as cbest2,cbeciv,estCiv.cedesc1 as civildesc,cbobs,
				   cbnumiSoc,IIF(cbnumiSoc=0,'',(select CONCAT(TCS01.cfnom,' ',TCS01.cfapat,' ',TCS01.cfamat) from TCS01 where TCS01.cfnumi=cbnumisoc)) as sociodesc,
				   cbparent,IIF(cbparent=0,'',(select paren.cedesc1 from TC0051 paren where paren.cenum=TCE002.cbparent and paren.cecod1=3 and paren.cecod2=4)) as parentdesc,
				   cbmen,CAST(IIF ( cbmen = 1, 1, 0 ) AS BIT) as cbmen2,cbtutci,cbtutnom,cbsuc,TC001.cadesc,cbnrogr,cbnfact,
				   cbfact,cbhact,cbuact
			FROM TCE002,TC0051 tipo,TC0051 estCiv,TC0051 prof,TC001,TC0051 nac
			where tipo.cenum=TCE002.cbtipo and tipo.cecod1=3 and tipo.cecod2=1 and 
			      estCiv.cenum=TCE002.cbeciv and estCiv.cecod1=3 and estCiv.cecod2=2 and
				  prof.cenum=TCE002.cbprof and prof.cecod1=3 and prof.cecod2=3 and
				  nac.cenum=TCE002.cblnac and nac.cecod1=3 and nac.cecod2=5 and
				  cbsuc=TC001.canumi and 
				  1=IIF(@cbsuc=-1,1,iif(cbsuc=@cbsuc,1,0))
				  --cbsuc=@cbsuc
			order by cbnumi

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=4 --AYUDA SOCIOS
	BEGIN
		BEGIN TRY
			select TCS01.cfnumi,TCS01.cfci, CONCAT(TCS01.cfnom,' ',TCS01.cfapat,' ',TCS01.cfamat) as nombre
			from TCS01 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=5 --AYUDA AlUMNOS
	BEGIN
		BEGIN TRY
			select cbnumi, CONCAT(cbnom,' ',cbape) as cbnom2
			from TCE002
			where cbsuc=@cbsuc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=6 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			--declare @nroClsPrac int=(select eprac from TCE000);
			--declare @nroClsRef int=(select erefor from TCE000);

			--select b.egnumi,cbnumi,cbci, CONCAT(cbnom,' ',cbape) as cbnom2,'' as color,
			--IIF(b.egest=1 or b.egest=-1,@nroClsPrac,@nroClsRef)-(select COUNT(aa.ehlin) from TCE006 a,TCE0061 aa where a.egalum=cbnumi and a.egnumi=aa.ehnumi and (aa.ehest=0 or aa.ehest=1 or aa.ehest=-1) ) as clasesFalt,--and a.egchof=@cbnumi
			--b.egest
			--from TCE002 a,TCE006 b
			--where b.egchof=@cbnumi and b.egalum=a.cbnumi and cbsuc=@cbsuc and 
			--	  b.egnumi in(select c.ehnumi from TCE0061 c where MONTH(c.ehfec)=month(@auxFecha) and YEAR(c.ehfec)=YEAR(@auxFecha))
			--	  --b.egest>0

			select b.egnumi,cbnumi,cbci, CONCAT(cbnom,' ',cbape) as cbnom2,'' as color,
			IIF(b.egest=1 or b.egest=-1,b.egnclsprac,b.egnclsref)-(select COUNT(aa.ehlin) from TCE006 a,TCE0061 aa where a.egalum=cbnumi and a.egnumi=aa.ehnumi and (aa.ehest=0 or aa.ehest=1 or aa.ehest=-1) ) as clasesFalt,--and a.egchof=@cbnumi
			b.egest
			from TCE002 a,TCE006 b
			where b.egchof=@cbnumi and b.egalum=a.cbnumi and 
				  b.egnumi in(select c.ehnumi from TCE0061 c where MONTH(c.ehfec)=month(@auxFecha) and YEAR(c.ehfec)=YEAR(@auxFecha))
				  --b.egest>0

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=61 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			--declare @nroClsRef2 int=(select erefor from TCE000);

			select b.eqnumi,a.elnumi as cbnumi,a.elci as cbci, CONCAT(a.elnom,' ',a.elapep,' ',a.elapem) as cbnom2,'' as color,
			b.eqcant,b.eqcant-(select COUNT(aa.erlin) from TCE014 a1,TCE0141 aa where a1.eqalum=a.elnumi and a1.eqchof=@cbnumi and a1.eqnumi=aa.ernumi and (aa.erest=0 or aa.erest=1 or aa.erest=-1) ) as clasesFalt,
			b.eqest
			from TCE009 a,TCE014 b
			where b.eqchof=@cbnumi and b.eqalum=a.elnumi and --cbsuc=@cbsuc and 
				  b.eqnumi in(select c.ernumi from TCE0141 c where MONTH(c.erfec)=month(@auxFecha) and YEAR(c.erfec)=YEAR(@auxFecha))
				  --b.egest>0
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=7 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR al cual no estan asignados donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			select cbnumi,cbci, CONCAT(cbnom,' ',cbape) as cbnom2
			from TCE002
			where TCE002.cbnumi not in (select a.egalum from TCE006 a ) and cbsuc=@cbsuc--where a.egchof=@cbnumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END
		IF @tipo=7777 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR al cual no estan asignados donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			select venta.vcnumi ,venta.vcfdoc ,alumno .cbnumi ,alumno .cbci ,alumno .cbnom +alumno .cbape as cbnom2,
isnull((select top 1 detalle.vccantclases   from TV0021 as detalle where detalle.vdvc2numi =venta.vcnumi 
and detalle .vccantclases >0),0)as cantidadClases
 from TV002 as venta
inner join TCE002 as alumno 
on alumno .cbnumi =venta.vcclie and venta.vcest =1
and venta.vcsector  =1
and isnull((select top 1 detalle.vccantclases   from TV0021 as detalle where detalle.vdvc2numi =venta.vcnumi 
and detalle .vccantclases >0),0)>0 and venta.vcbanco =0
order by venta.vcnumi asc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END
	IF @tipo=71 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR al cual no estan asignados donde el @cbnumi es el numi del instructor en reforzamiento
	BEGIN
		BEGIN TRY
			select a.elnumi as cbnumi,a.elci cbci, CONCAT(a.elnom,' ',a.elapep,' ',a.elapem) as cbnom2
			from TCE009 a
			where a.elnumi not in (select a.eqalum from TCE014 a where a.eqchof=@cbnumi) 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=8 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			--declare @nroClsPrac1 int=(select eprac from TCE000);

			select distinct cbnumi,cbci, CONCAT(cbnom,' ',cbape) as cbnom2
			from TCE002,TCE006
			where TCE006.egalum=TCE002.cbnumi and --cbsuc=@cbsuc and--TCE006.egchof=@cbnumi and 
				  (egnclsprac-(select COUNT(aa.ehlin) from TCE006 a,TCE0061 aa where a.egalum=cbnumi and a.egnumi=aa.ehnumi and (aa.ehest=0 or aa.ehest=1 or aa.ehest=-1) ))>0 --and a.egchof=@cbnumi
				   
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=81 --AYUDA AlUMNOS FILTRADOS POR INSTRUCTOR donde el @cbnumi es el numi del instructor
	BEGIN
		BEGIN TRY
			select b.elnumi as cbnumi,b.elci as cbci, CONCAT(b.elnom,' ',b.elapep) as cbnom2
			from TCE009 b,TCE014 c
			where c.eqchof=@cbnumi and c.eqalum=b.elnumi and --cbsuc=@cbsuc and
				  (c.eqcant-(select COUNT(aa.erlin) from TCE014 a,TCE0141 aa where a.eqalum=b.elnumi and a.eqchof=@cbnumi and a.eqnumi=aa.ernumi and (aa.erest=0 or aa.erest=1 or aa.erest=-1) ))>0
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=9 --para el reporte de ficha de inscripcion cruzando con la inscripcion
	BEGIN
		BEGIN TRY
			SELECT cbnumi,cbci,cbnom,cbape,cbdirec,cbtelef1,cbtelef2,cbemail,cbprof,prof.cedesc1 as profdesc,cbtipo,tipo.cedesc1 as tipodesc,CONVERT(VARCHAR(10), cbfnac, 103) as cbfnac,
				   cblnac,nac.cedesc1 as cblnac2,CONVERT(VARCHAR(10), cbfing, 103) as cbfing,cbfot, CAST('' as image) as cbfot2,cbest,CAST(IIF ( cbest = 1, 1, 0 ) AS BIT) as cbest2,cbeciv,estCiv.cedesc1 as civildesc,cbobs,
				   cbnumiSoc,IIF(cbnumiSoc=0,'',(select CONCAT(TCS01.cfnom,' ',TCS01.cfapat,' ',TCS01.cfamat) from TCS01 where TCS01.cfnumi=cbnumisoc)) as sociodesc,
				   cbparent,IIF(cbparent=0,'',(select paren.cedesc1 from TC0051 paren where paren.cenum=TCE002.cbparent and paren.cecod1=3 and paren.cecod2=4)) as parentdesc,
				   cbmen,CAST(IIF ( cbmen = 1, 1, 0 ) AS BIT) as cbmen2,cbtutci,cbtutnom,cbsuc,TC001.cadesc,
				   a.egnumi,a.egest,a.egchof,CONCAT(c.panom,' ',c.paape,' ')as panom, convert(datetime,CONVERT(varchar(10), ehfec, 103),103) as ehfec ,b.ehhor,b.ehest,b.ehobs,
				   IIF(ehest=-1,'FALTA',IIF(ehest=0,'PROGRAMADO',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2
			FROM TCE002 ,TC0051 tipo,TC0051 estCiv,TC0051 prof,TC001,TC0051 nac,
				 TCE006 a,TCE0061 b,TP001 c
			where tipo.cenum=TCE002.cbtipo and tipo.cecod1=3 and tipo.cecod2=1 and 
			      estCiv.cenum=TCE002.cbeciv and estCiv.cecod1=3 and estCiv.cecod2=2 and
				  prof.cenum=TCE002.cbprof and prof.cecod1=3 and prof.cecod2=3 and
				  nac.cenum=TCE002.cblnac and nac.cecod1=3 and nac.cecod2=5 and
				  cbsuc=TC001.canumi and 
				  1=IIF(@cbsuc=-1,1,iif(cbsuc=@cbsuc,1,0)) and
				  a.egchof=c.panumi and
				  cbnumi=@cbnumi and
				  a.egalum=cbnumi and b.ehnumi=a.egnumi	 and
				  (b.ehest=-1 or b.ehest=0 or b.ehest=1)
			order by cbnumi,b.ehfec,b.ehhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=91 --para el reporte de ficha de inscripcion solamente los datos del alumno
	BEGIN
		BEGIN TRY
			SELECT cbnumi,cbci,cbnom,cbape,cbdirec,cbtelef1,cbtelef2,cbemail,cbprof,prof.cedesc1 as profdesc,cbtipo,tipo.cedesc1 as tipodesc,CONVERT(VARCHAR(10), cbfnac, 103) as cbfnac,
				   cblnac,nac.cedesc1 as cblnac2,CONVERT(VARCHAR(10), cbfing, 103) as cbfing,cbfot, CAST('' as image) as cbfot2,cbest,CAST(IIF ( cbest = 1, 1, 0 ) AS BIT) as cbest2,cbeciv,estCiv.cedesc1 as civildesc,cbobs,
				   cbnumiSoc,IIF(cbnumiSoc=0,'',(select CONCAT(TCS01.cfnom,' ',TCS01.cfapat,' ',TCS01.cfamat) from TCS01 where TCS01.cfnumi=cbnumisoc)) as sociodesc,
				   cbparent,IIF(cbparent=0,'',(select paren.cedesc1 from TC0051 paren where paren.cenum=TCE002.cbparent and paren.cecod1=3 and paren.cecod2=4)) as parentdesc,
				   cbmen,CAST(IIF ( cbmen = 1, 1, 0 ) AS BIT) as cbmen2,cbtutci,cbtutnom,cbsuc,TC001.cadesc,
				   0 as egnumi,0 as egest,0 as egchof,''as panom, '' as ehfec ,'' as ehhor,0 as ehest,'' as ehobs
			FROM TCE002 ,TC0051 tipo,TC0051 estCiv,TC0051 prof,TC001,TC0051 nac
			where tipo.cenum=TCE002.cbtipo and tipo.cecod1=3 and tipo.cecod2=1 and 
			      estCiv.cenum=TCE002.cbeciv and estCiv.cecod1=3 and estCiv.cecod2=2 and
				  prof.cenum=TCE002.cbprof and prof.cecod1=3 and prof.cecod2=3 and
				  nac.cenum=TCE002.cblnac and nac.cecod1=3 and nac.cecod2=5 and
				  cbsuc=TC001.canumi and 
				  1=IIF(@cbsuc=-1,1,iif(cbsuc=@cbsuc,1,0)) and
				  cbnumi=@cbnumi
			order by cbnumi;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END

	IF @tipo=10 --para el reporte de detallado de clases practicas de escuela, en donde dice que hizo en cada clase practica
	BEGIN
		BEGIN TRY
			SELECT cbnumi,cbci,cbnom,cbape,cbdirec,cbtelef1,cbtelef2,cbemail,cbprof,prof.cedesc1 as profdesc,cbtipo,tipo.cedesc1 as tipodesc,CONVERT(VARCHAR(10), cbfnac, 103) as cbfnac,
				   cblnac,nac.cedesc1 as cblnac2,CONVERT(VARCHAR(10), cbfing, 103) as cbfing,cbfot, CAST('' as image) as cbfot2,cbest,CAST(IIF ( cbest = 1, 1, 0 ) AS BIT) as cbest2,cbeciv,estCiv.cedesc1 as civildesc,cbobs,
				   cbnumiSoc,IIF(cbnumiSoc=0,'',(select CONCAT(TCS01.cfnom,' ',TCS01.cfapat,' ',TCS01.cfamat) from TCS01 where TCS01.cfnumi=cbnumisoc)) as sociodesc,
				   cbparent,IIF(cbparent=0,'',(select paren.cedesc1 from TC0051 paren where paren.cenum=TCE002.cbparent and paren.cecod1=3 and paren.cecod2=4)) as parentdesc,
				   cbmen,CAST(IIF ( cbmen = 1, 1, 0 ) AS BIT) as cbmen2,cbtutci,cbtutnom,cbsuc,TC001.cadesc,
				   a.egnumi,a.egest,a.egchof,CONCAT(c.panom,' ',c.paape,' ')as panom, convert(datetime,CONVERT(varchar(10), ehfec, 103),103) as ehfec ,b.ehhor,b.ehest,b.ehobs,
				   IIF(ehest=-1,'FALTA',IIF(ehest=0,'PROGRAMADO',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,
				   0 as nroClase,(select top 1 aa.caid from TCE001 aa where aa.caper=c.panumi) as caid,TCE002.cbnrogr
			FROM TCE002 ,TC0051 tipo,TC0051 estCiv,TC0051 prof,TC001,TC0051 nac,
				 TCE006 a,TCE0061 b,TP001 c
			where tipo.cenum=TCE002.cbtipo and tipo.cecod1=3 and tipo.cecod2=1 and 
			      estCiv.cenum=TCE002.cbeciv and estCiv.cecod1=3 and estCiv.cecod2=2 and
				  prof.cenum=TCE002.cbprof and prof.cecod1=3 and prof.cecod2=3 and
				  nac.cenum=TCE002.cblnac and nac.cecod1=3 and nac.cecod2=5 and
				  cbsuc=TC001.canumi and
				  a.egchof=c.panumi and
				  cbnumi=@cbnumi and
				  a.egalum=cbnumi and b.ehnumi=a.egnumi and
				  (b.ehest=-1 or b.ehest=0 or b.ehest=1)
			order by cbnumi,b.ehfec,b.ehhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@cbuact)
		END CATCH
	END
END










