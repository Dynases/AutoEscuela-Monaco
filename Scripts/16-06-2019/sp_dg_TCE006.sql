USE [DBDies_Monaco]
GO
/****** Object:  StoredProcedure [dbo].[sp_dg_TCE006]    Script Date: 16/06/2019 16:19:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_dg_TCE006](@tipo int,@egnumi int=-1,@egchof int =-1,@egalum int=-1,@egest int=-1,@eguact nvarchar(10)='',
									 @fecha date=null,@ehest int=-1,@ehhfec date=null,@ehhhor nvarchar(5)='',@numiSuc int=-1,
									 @ehhobs nvarchar(200)='',@tipoHorario int=-1,@TCE0061 dbo.TCE0061Type Readonly,@TCE00611 dbo.TCE00611Type Readonly,
									 @TCE0062 dbo.TCE0062Type Readonly,@fecha1 date=null,@fecha2 date=null,
									 @egnclsprac int=-1,@egnclsref int=-1,
									 @numiChof1 int=-1,@numiChof2 int =-1,@TCE006 dbo.TCE006Type Readonly,
									 @TCE0063 dbo.TCE0063Type Readonly,@vcnumi int=-1)
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()

	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 
			--DELETE from TCE004 where ednumi=@ednumi;
			--DELETE FROM TCE0041 WHERE eenumi=@ednumi;
			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=1 --NUEVO REGISTRO
	BEGIN
		BEGIN TRY 
			set @egnumi=IIF((select COUNT(egnumi) from TCE006)=0,0,(select MAX(egnumi) from TCE006))+1;
			INSERT INTO TCE006 VALUES(@egnumi,@egchof,@egalum,@egest,@egnclsprac,@egnclsref,@newFecha,@newHora,@eguact);

			---- INSERTO EL DETALLE
			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs,ehchof)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs,@egchof FROM @TCE0061 AS td;

			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@eguact)
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRY
			UPDATE TCE006 SET egchof=@egchof,egalum=@egalum,egest=@egest,egfact=@newFecha,eghact=@newHora,eguact=@eguact  
					 Where egnumi = @egnumi

			----------MODIFICO EL DETALLE------------
			--INSERTO LOS NUEVOS
			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs FROM @TCE0061 AS td WHERE td.estado=0;
			--MODIFICO LOS REGISTROS
			--UPDATE TCE0061
			--SET TCE0041.eeano = td.eeano,TCE0041.eemes=td.eemes,TCE0041.eeprecio=td.eeprecio
			--FROM TCE0061 INNER JOIN @TCE0061 AS td
			--ON TCE0061.ehnumi = td.ehnumi and td.estado=2;
			----ELIMINO LOS REGISTROS
			--DELETE FROM TCE0061 WHERE TCE0061.ehnumi in (SELECT td.ehnumi FROM @TCE0061 AS td WHERE td.estado=-1)

			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=21 --Modificar las clases que estan programadas estado=0 al estado que esta entrando,filtrado por instructor y alumno
	BEGIN
		BEGIN TRY
			update TCE0061 set ehest=@ehest
			where ehest=0 and
				  ehnumi=(select top 1 TCE006.egnumi
						  FROM TCE006
						  where TCE006.egchof=@egchof and TCE006.egalum=@egalum order by egnumi desc );
			select 1 as respuesta ;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=211 --modificar el estado de la cabecera de TCE006
	BEGIN
		BEGIN TRY
			update TCE006 set egest=@egest
			where egnumi=@egnumi;
			select 1 as respuesta ;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=3 --INSERCION DE DETALLES TCE0061
	BEGIN
		BEGIN TRY
			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs FROM @TCE0061 AS td WHERE td.estado=0;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=4 --INSERTAR cabecera con detalle
	BEGIN
		BEGIN TRY
			--INSERTO LOS NUEVOS
			set @egnumi=IIF((select COUNT(egnumi) from TCE006)=0,0,(select MAX(egnumi) from TCE006))+1;
			INSERT INTO TCE006 VALUES(@egnumi,@egchof,@egalum,@egest,@egnclsprac,@egnclsref,@newFecha,@newHora,@eguact);

			---- INSERTO EL DETALLE
			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs,ehchof)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs,@egchof FROM @TCE0061 AS td;
			------ vcbnaco es la columna que me dira si el alumno ya fue programado sus clases
			update  TV002 set vcbanco =1 where vcnumi =@vcnumi 
			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=41 --INSERTAR cabecera con detalle con eliminacion de ya guardados
	BEGIN
		BEGIN TRY
			--INSERTO LOS NUEVOS
			set @egnumi=(select egnumi from TCE006 where egchof=@egchof and egalum=@egalum);
			--ELIMINO EL DETALLE DEL REGISTRO
			delete from TCE0061 where ehnumi=@egnumi;
			---- INSERTO EL DETALLE
			
			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs,ehchof)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs,@egchof FROM @TCE0061 AS td;

			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END
	IF @tipo=100001 --INSERTAR detalle por medio del line del detalle
	BEGIN
		BEGIN TRY
			---- INSERTO EL DETALLE
		select top 1 egnumi,egnclsprac ,egnclsref from TCE006 where egchof=@egchof and egalum=@egalum order by egnumi desc
			
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END
	IF @tipo=42 --INSERTAR detalle por medio del line del detalle
	BEGIN
		BEGIN TRY
			---- INSERTO EL DETALLE
			set @egnumi=(select top 1 egnumi from TCE006 where egchof=@egchof and egalum=@egalum order by egnumi desc);
			
			IF (@egnumi is null)--entonces hagarro y aumento un nuevo registro con el numi del nuevo chofer y alumno
			BEGIN
				set @egnumi=IIF((select COUNT(egnumi) from TCE006)=0,0,(select MAX(egnumi) from TCE006))+1;
				INSERT INTO TCE006 VALUES(@egnumi,@egchof,@egalum,@egest,@egnclsprac,@egnclsref,@newFecha,@newHora,@eguact);
			END

			INSERT INTO TCE0061(ehnumi,ehtser,ehfec,ehhor,ehest,ehobs,ehchof)
			SELECT @egnumi,td.ehtser,td.ehfec,td.ehhor,td.ehest,td.ehobs,@egchof FROM @TCE0061 AS td;

			select @egnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=5 --OBTENER DETALLE DE FECHAS filtrado por el numi del alumno y del chofer
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof=@egchof and TCE006.egalum=@egalum
			ORDER BY ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=51 --OBTENER DETALLE DE FECHAS filtrado por el numi del alumno y del chofer y la fecha
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,IIF(ehest=-1,'FALTA',IIF(ehest=0,'CLASE PROGRAMADA',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof=@egchof and 
				  TCE006.egalum=@egalum and MONTH(TCE0061.ehfec)=month(@fecha) and YEAR(TCE0061.ehfec)=YEAR(@fecha)
			ORDER BY ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=513 --OBTENER DETALLE DE FECHAS filtrado por solamente el chofer y la fecha
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,IIF(ehest=-1,'FALTA',IIF(ehest=0,'CLASE PROGRAMADA',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs,egalum
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof=@egchof and 
				  MONTH(TCE0061.ehfec)=month(@fecha) and YEAR(TCE0061.ehfec)=YEAR(@fecha)
			ORDER BY ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=511 --OBTENER DETALLE DE FECHAS filtrado por el numi del alumno y del chofer y la fecha y que solo esten estado 0,1,-1,osea las contables
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,IIF(ehest=-1,'FALTA',IIF(ehest=0,'CLASE PROGRAMADA',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof=@egchof and 
				  TCE006.egalum=@egalum and MONTH(TCE0061.ehfec)=month(@fecha) and YEAR(TCE0061.ehfec)=YEAR(@fecha) and
				  (ehest=1 or ehest=0 or ehest=-1)
			ORDER BY ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=514 --OBTENER DETALLE DE FECHAS filtrado SOLAMENTE POR EL CHOFER  y la fecha y que solo esten estado 0,1,-1,osea las contables
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,IIF(ehest=-1,'FALTA',IIF(ehest=0,'CLASE PROGRAMADA',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs,egalum
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof=@egchof and 
				   MONTH(TCE0061.ehfec)=month(@fecha) and YEAR(TCE0061.ehfec)=YEAR(@fecha) and
				  (ehest=1 or ehest=0 or ehest=-1)
			ORDER BY ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=512 --OBTENER DETALLE DE FECHAS filtrado por el numi del alumno y la fecha y que solo esten estado 0,1,-1,osea las contables
	BEGIN
		BEGIN TRY
			SELECT ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,IIF(ehest=-1,'FALTA',IIF(ehest=0,'CLASE PROGRAMADA',IIF(ehest=1,'ASISTENCIA',IIF(ehest=2,'PERMISO',IIF(ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs
			FROM TCE006,TCE0061
			where ehnumi=egnumi and TCE006.egchof<>@egchof and--and TCE006.egchof=@egchof
				  TCE006.egalum=@egalum and TCE0061.ehfec<@ehhfec and --tce0061.ehhor<@ehhhor and
				  --MONTH(TCE0061.ehfec)=month(@fecha) and YEAR(TCE0061.ehfec)=YEAR(@fecha) and
				  (ehest=1 or ehest=0 or ehest=-1)
			ORDER BY ehnumi,ehfec,ehhor
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=6 --OBTENER estructura de grilla de fechas
	BEGIN
		BEGIN TRY
			SELECT '' as hora,'' as d1,'' as d2,'' as d3,'' as d4,'' as d5,'' as d6,'' as d7,'' as d8,'' as d9,'' as d10
			                  ,'' as d11,'' as d12,'' as d13,'' as d14,'' as d15,'' as d16,'' as d17,'' as d18,'' as d19,'' as d20
							  ,'' as d21,'' as d22,'' as d23,'' as d24,'' as d25,'' as d26,'' as d27,'' as d28,'' as d29,'' as d30
							  ,'' as d31
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=7 --MODIFICAR ESTADO DE LA TCE0061
	BEGIN
		BEGIN TRY
			--MODIFICO LOS REGISTROS
			UPDATE TCE0061
			SET TCE0061.ehest = td.ehest,TCE0061.ehobs=IIF(td.ehobs='',TCE0061.ehobs,td.ehobs)
			FROM TCE0061 INNER JOIN @TCE0061 AS td
			ON TCE0061.ehlin = td.ehlin;

			select 1 as respuesta
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=8 --obtengo todos las clases programadas en esa fecha y hora mandada
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs,1 as estado
			from TCE0061 a
			where a.ehfec=@ehhfec and a.ehhor=@ehhhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=82 --obtengo todos las clases programadas en esa fecha y hora mandada con todos los datos filtrado por instructor
	BEGIN
		BEGIN TRY
			select CONCAT(c.cbnom,' ',c.cbape) as nomAlum,CONCAT(d.panom,' ',d.paape) as nomInst, ehnumi,ehtser,ehlin,ehfec,ehhor,
				   ehest,IIF(a.ehest=-1,'FALTA',IIF(a.ehest=0,'PROGRAMADO',IIF(a.ehest=1,'ASISTENCIA',IIF(a.ehest=2,'PERMISO',IIF(a.ehest=3,'SUSPENSION','SIN DEFINIR'))))) as ehest2,ehobs
			from TCE0061 a, TCE006 b,TCE002 c,TP001 d
			where a.ehfec=@ehhfec and a.ehhor=@ehhhor and
				  a.ehnumi=b.egnumi and 
				  b.egalum=c.cbnumi and
				  b.egchof=d.panumi and
				  b.egchof=@egchof;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=81 --obtengo todos las clases programadas en esa fecha y hora mandada,filtrado por instructor
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs,1 as estado
			from TCE0061 a,TCE006 b
			where b.egnumi=a.ehnumi and
				  a.ehfec=@ehhfec and a.ehhor=@ehhhor and b.egchof=@egchof;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=9 --INSERTAR HORAS LIBRES de un solo instructor
	BEGIN
		BEGIN TRY 
			---- INSERTO EL DETALLE
			INSERT INTO TCE0062(ehhchof,ehhfec,ehhhor,ehhobs)
			SELECT td.ehhchof,td.ehhfec,td.ehhhor,@ehhobs FROM @TCE0062 AS td;

			select 1 as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=901 --INSERTAR HORAS DILIGENCIA de un solo instructor
	BEGIN
		BEGIN TRY 
			---- INSERTO EL DETALLE
			INSERT INTO TCE0063(ehichof,ehifec,ehihor,ehiobs)
			SELECT td.ehichof,td.ehifec,td.ehihor,@ehhobs FROM @TCE0063 AS td;

			select 1 as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=91 --INSERTAR HORAS LIBRES de un todos los instructores
	BEGIN
		BEGIN TRY 
			---- INSERTO EL DETALLE
			INSERT INTO TCE0062(ehhchof,ehhfec,ehhhor,ehhobs)
			SELECT a.panumi,td.ehhfec,td.ehhhor,@ehhobs FROM @TCE0062 AS td,TP001 a where a.patipo=1 and a.pasuc=@numiSuc;

			select 1 as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=10 --obtener horas liberadas por mes y por instructor
	BEGIN
		BEGIN TRY
			select ehhlin,ehhchof,ehhfec,ehhhor,ehhobs
			from TCE0062
			where ehhchof=@egchof and MONTH(ehhfec)=month(@fecha) and YEAR(ehhfec)=YEAR(@fecha) ;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=1001 --obtener horas diligencias por mes y por instructor
	BEGIN
		BEGIN TRY
			select ehilin,ehichof,ehifec,ehihor,ehiobs
			from TCE0063
			where ehichof=@egchof and MONTH(ehifec)=month(@fecha) and YEAR(ehifec)=YEAR(@fecha) ;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=101 --obtener horas liberadas por rango de fecha y por instructor
	BEGIN
		BEGIN TRY
			select ehhlin,ehhchof,ehhfec,ehhhor,ehhobs
			from TCE0062
			where ehhchof=@egchof and ehhfec>=@fecha1 and ehhfec<=@fecha2
			order by ehhfec,ehhhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=10101 --obtener horas liberadas por rango de fecha y por instructor
	BEGIN
		BEGIN TRY
			select ehilin,ehichof,ehifec,ehihor,ehiobs
			from TCE0063
			where ehichof=@egchof and ehifec>=@fecha1 and ehifec<=@fecha2
			order by ehifec,ehihor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=111 --obtengo las clases filtradas por fecha y hora y chofer
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs,1 as estado
			from TCE0061 a,TCE006 b
			where b.egnumi=a.ehnumi and
				  a.ehfec=@ehhfec and a.ehhor=@ehhhor and b.egchof=@egchof;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=12 --obtengo si tiene alguna clase con permiso,lo busco por el line(@egnumi) de la hora y filtrandolo por su numi
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs
			from TCE0061 a
			where a.ehnumi=(select top 1 b.ehnumi  from TCE0061 b where b.ehlin=@egnumi) and a.ehest=2;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=13 --Obtener el horario de acuerdo a la fecha y sucursal
	BEGIN
		BEGIN TRY
			SELECT  1 AS nro, b.panumi as egchof,CONCAT(b.panom,' ',b.paape)  AS panom2, a.cchora, 1 as egalum, '' AS cbnom2,
					1 as camar, 
					(select top 1 bb.cedesc1 from TCE001 aa,TC0051 bb where aa.camar=bb.cenum and bb.cecod1=1 and bb.cecod2=1) AS camar2, 
					1 as camod, 
					(select top 1 bb.cedesc1 from TCE001 aa,TC0051 bb where aa.camod=bb.cenum and bb.cecod1=1 and bb.cecod2=2) AS camod2, 
					(select top 1 aa.caid from TCE001 aa where aa.caper=b.panumi) as caid,'' as nroClase
					FROM	TC0021 a,TP001 b
					where	a.ccnumi=(select top 1 cbnumi from TC002 where @fecha>=TC002.cbfecha and cbsuc=@numiSuc and cbtipo=@tipoHorario order by cbfecha desc) and
							b.patipo=1 and b.paest=1 and b.pasuc=@numiSuc
					order by b.panumi,a.cchora

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=14 --obtengo las clases filtradas por fecha y chofer
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs,CONCAT(c.cbnom,' ',c.cbape) as cbnom2,
				  (select COUNT (aa.ehnumi) from TCE0061 aa where b.egnumi=aa.ehnumi and (aa.ehfec<@ehhfec or (aa.ehfec<=@ehhfec and aa.ehhor<a.ehhor)) and (aa.ehest=0 or aa.ehest=1 or aa.ehest=-1))+1 as nroClase,
				  egalum, b.egest
			from TCE0061 a,TCE006 b,TCE002 c
			where b.egnumi=a.ehnumi and
				  a.ehfec=@ehhfec  and b.egchof=@egchof and
				  b.egalum=c.cbnumi and
				  (a.ehest=1 or a.ehest=0)--esto se aumento para solo traer las clases grabadas y habilitadas
			order by ehfec,ehhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=141 --obtengo las clases filtradas por fecha y chofer
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs,CONCAT(c.cbnom,' ',c.cbape) as cbnom2,
				  (select COUNT (aa.ehnumi) from TCE0061 aa where b.egnumi=aa.ehnumi and (aa.ehfec<@ehhfec or (aa.ehfec<=@ehhfec and aa.ehhor<a.ehhor)) and (aa.ehest=0 or aa.ehest=1 or aa.ehest=-1))+1 as nroClase
			from TCE0061 a,TCE006 b,TCE002 c
			where b.egnumi=a.ehnumi and
				  a.ehfec=@ehhfec  and b.egchof=@egchof and
				  b.egalum=c.cbnumi and
				  (a.ehest=1 or a.ehest=0 or a.ehest=-1)--esto se aumento para solo traer las clases grabadas y habilitadas y tambien las faltas
			order by ehfec,ehhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=15 --Obtener el horario de acuerdo a la fecha y sucursal y filtrando por instructor
	BEGIN
		BEGIN TRY
			SELECT  1 AS nro, b.panumi as egchof,CONCAT(b.panom,' ',b.paape)  AS panom2, a.cchora, 1 as egalum, '' AS cbnom2,
					1 as camar, 
					(select top 1 bb.cedesc1 from TCE001 aa,TC0051 bb where aa.camar=bb.cenum and bb.cecod1=1 and bb.cecod2=1) AS camar2, 
					1 as camod, 
					(select top 1 bb.cedesc1 from TCE001 aa,TC0051 bb where aa.camod=bb.cenum and bb.cecod1=1 and bb.cecod2=2) AS camod2, 
					(select top 1 aa.caid from TCE001 aa where aa.caper=b.panumi) as caid,'' as nroClase
					FROM	TC0021 a,TP001 b
					where	a.ccnumi=(select top 1 cbnumi from TC002 where @fecha>=TC002.cbfecha and cbsuc=@numiSuc and cbtipo=@tipoHorario order by cbfecha desc) and
							b.patipo=1 and b.paest=1 and b.pasuc=@numiSuc and b.panumi=@egchof
					order by b.panumi,a.cchora

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=16 --consulta para reporte de horas trabajadas por instructor
	BEGIN
		BEGIN TRY
			declare @patipo int=1;

			SELECT panumi,CONCAT(panom,' ',paape) as panom1,0 as horasTot,0 as horasT,0 as horasL,pasuc,0 as minutos,b.cadesc
			FROM TP001 a,TC001 b
			WHERE patipo=@patipo and 
				  a.pasuc=b.canumi and
				  b.canumi in (select cbsuc from TC002 x,TC0021 y where x.cbnumi=y.ccnumi)
					 
			order by pasuc asc;

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END
	IF @tipo=17 --sacar que sucursales hay
	BEGIN
		BEGIN TRY
			declare @patipo2 int=1;

			SELECT distinct pasuc
			FROM TP001
			WHERE patipo=@patipo2
			order by pasuc asc;

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=18 --buscar alguna clase que tenga programada un instructor
	BEGIN
		BEGIN TRY
			select ehnumi,ehtser,ehlin,ehfec,ehhor,ehest,ehobs
			from TCE0061 a,TCE006 b
			where b.egnumi=a.ehnumi and
			      b.egchof=@egchof 
			order by ehfec,ehhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=20 --ELIMINAR hora liberada
	BEGIN
		BEGIN TRY 
			delete from TCE0062 where ehhlin=@egnumi
			select 1 as resp
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=2001 --ELIMINAR hora Diligencia
	BEGIN
		BEGIN TRY 
			delete from TCE0063 where ehilin=@egnumi
			select 1 as resp
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=31 --HACER EL CAMBIO DE INSTRUCTORES
	BEGIN
		BEGIN TRY 
			
			update TCE006 set egchof=@numiChof2 where egchof=@numiChof1

			update TP001 set patipo=1 where panumi=@numiChof2
			update TP001 set pasuc=(select a.pasuc from TP001 a where a.panumi=@numiChof1) where panumi=@numiChof2

			--igualmente asignarle las horas liberadas y las horas diligencia
			update TCE0062 set ehhchof=@numiChof2 where ehhchof=@numiChof1
			update TCE0063 set ehichof=@numiChof2 where ehichof=@numiChof1

			select 1 as resp
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=32 --obtener todas las clases de un instructor
	BEGIN
		BEGIN TRY 
			select egnumi,egchof,egalum,egest,egnclsprac,egnclsref,egfact,eghact,eguact,1 as estado
			from TCE006
			where egchof=@numiChof1
			
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=33 --HACER EL CAMBIO DE INSTRUCTORE por data table
	BEGIN
		BEGIN TRY 
			declare @numiSuc2 int=(select a.pasuc from TP001 a where a.panumi=@numiChof2);

			update TCE006 set egchof=@numiChof2 where egchof=@numiChof1

			update TP001 set patipo=1 where panumi=@numiChof2
			update TP001 set pasuc=(select a.pasuc from TP001 a where a.panumi=@numiChof1) where panumi=@numiChof2


			--ahora hago el segundo cambio
			update TCE006 set egchof=@numiChof1 where egnumi in(select x.egnumi from @TCE006 x)

			update TP001 set patipo=1 where panumi=@numiChof1
			update TP001 set pasuc=@numiSuc2 where panumi=@numiChof1


			----------------------------------------------------------------------------------------------
			--igualmente asignarle las horas liberadas y las horas diligencia
			update TCE0062 set ehhchof=@numiChof2 where ehhchof=@numiChof1
			update TCE0063 set ehichof=@numiChof2 where ehichof=@numiChof1

			--igualmente asignarle las horas liberadas y las horas diligencia
			update TCE0062 set ehhchof=@numiChof1 where ehhlin in (select x.ehhlin from @TCE0062 x)
			update TCE0063 set ehichof=@numiChof1 where ehilin in (select x.ehilin from @TCE0063 x)

			select 1 as resp
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=34 --obtener horas liberadas por instructor
	BEGIN
		BEGIN TRY
			select ehhlin,ehhchof,ehhfec,ehhhor,ehhobs,1 as estado
			from TCE0062
			where ehhchof=@egchof
			order by ehhfec,ehhhor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END

	IF @tipo=35 --obtener horas diligencia por instructor
	BEGIN
		BEGIN TRY
			select ehilin,ehichof,ehifec,ehihor,ehiobs,1 as estado
			from TCE0063
			where ehichof=@egchof
			order by ehifec,ehihor;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eguact)
		END CATCH
	END
END


