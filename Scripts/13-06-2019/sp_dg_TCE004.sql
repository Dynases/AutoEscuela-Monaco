USE [DBDies_Monaco]
GO
/****** Object:  StoredProcedure [dbo].[sp_dg_TCE004]    Script Date: 13/06/2019 5:11:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[sp_dg_TCE004](@tipo int,@ednumi int=-1,@edcod nvarchar(50)='',@eddesc nvarchar(50)='',@edprec float=0,
									 @edtipo int=-1,@edest int=-1,@edcant int=-1,@edsuc int =-1,@eduact nvarchar(10)='',@TCE0041 dbo.TCE0041Type Readonly,
									 @TCE0042 dbo.TCE0042Type readonly)
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))

	DECLARE @newFecha date
	set @newFecha=GETDATE()

	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		Begin tran ELIMINAR
		BEGIN TRY 
			DELETE FROM TCE0042 WHERE TCE0042.eqtce4=@ednumi;
			DELETE FROM TCE0041 WHERE eenumi=@ednumi;
			DELETE from TCE004 where ednumi=@ednumi;
			
			select 1 as respuesta;

			commit tran;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@eduact);
			select 0  as respuesta,ERROR_NUMBER() nroError; 
			rollback tran;
		END CATCH
	END

	IF @tipo=1 --NUEVO REGISTRO
	BEGIN
		BEGIN TRAN INSERTAR
		BEGIN TRY 
			set @ednumi=IIF((select COUNT(ednumi) from TCE004)=0,0,(select MAX(ednumi) from TCE004))+1;
			INSERT INTO TCE004 VALUES(@ednumi,@edcod,@eddesc,@edprec,@edtipo,@edest,@edsuc ,@edcant ,@newFecha,@newHora,@eduact);

			---- INSERTO EL DETALLE 
			INSERT INTO TCE0041(eenumi,eeano,eemes,eeprecio)
			SELECT @ednumi,td.eeano,td.eemes,td.eeprecio FROM @TCE0041 AS td;

			---- INSERTO EL DETALLE TCE0042
			INSERT INTO TCE0042(eqtce4,eqtip1_4,eqmes,eqano,eqprecio)
			SELECT @ednumi,td.eqtip1_4,td.eqmes,td.eqano,td.eqprecio FROM @TCE0042 AS td ;

			select @ednumi as newNumi;
			COMMIT TRAN;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@eduact);
			ROLLBACK TRAN;
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRAN MODIFICAR
		BEGIN TRY 
			UPDATE TCE004 SET edcod=@edcod,eddesc=@eddesc,edprec=@edprec,edsuc=@edsuc,
			edcant=@edcant  ,edtipo=@edtipo,edest=@edest,edfact=@newFecha,edhact=@newHora,eduact=@eduact  
					 Where ednumi = @ednumi

			----------MODIFICO EL DETALLE------------
			--INSERTO LOS NUEVOS
			INSERT INTO TCE0041(eenumi,eeano,eemes,eeprecio)
			SELECT @ednumi,td.eeano,td.eemes,td.eeprecio FROM @TCE0041 AS td WHERE td.estado=0;
			--MODIFICO LOS REGISTROS
			UPDATE TCE0041
			SET TCE0041.eeano = td.eeano,TCE0041.eemes=td.eemes,TCE0041.eeprecio=td.eeprecio
			FROM TCE0041 INNER JOIN @TCE0041 AS td
			ON TCE0041.eelinea = td.eelinea and td.estado=2;
			--ELIMINO LOS REGISTROS
			DELETE FROM TCE0041 WHERE TCE0041.eelinea in (SELECT td.eelinea FROM @TCE0041 AS td WHERE td.estado=-1)

			----------MODIFICO EL DETALLE TCE0042 ------------
			--INSERTO LOS NUEVOS
			INSERT INTO TCE0042(eqtce4,eqtip1_4,eqmes,eqano,eqprecio)
			SELECT @ednumi,td.eqtip1_4,td.eqmes,td.eqano,td.eqprecio FROM @TCE0042 AS td WHERE td.estado=0;
			--MODIFICO LOS REGISTROS
				--UPDATE TCE0041
				--SET TCE0041.eeano = td.eeano,TCE0041.eemes=td.eemes,TCE0041.eeprecio=td.eeprecio
				--FROM TCE0041 INNER JOIN @TCE0041 AS td
				--ON TCE0041.eelinea = td.eelinea and td.estado=2;
			--ELIMINO LOS REGISTROS
			DELETE FROM TCE0042 WHERE TCE0042.eqnumi in (SELECT td.eqnumi FROM @TCE0042 AS td WHERE td.estado=-1)


			select 1 as respuesta;
			commit tran;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@eduact);

			select 0  as respuesta,ERROR_NUMBER() nroError; 
			rollback tran;
		END CATCH
	END

	IF @tipo=3 --MOSTRAR TODOS
	BEGIN
		BEGIN TRY
			SELECT ednumi,edcod,eddesc,edprec,edtipo,tipo.cedesc1 as tipodesc,edest,CAST(IIF ( edest = 1, 1, 0 ) AS BIT) as edest2,edfact,edhact,eduact
,edsuc,sucursal .cadesc as sucursal,isnull(edcant,0) as edcant
			from TCE004,TC0051 tipo,TC001 as sucursal
			where TCE004.edtipo=tipo.cenum and tipo.cecod1=6 and tipo.cecod2=1
			and sucursal .canumi =edsuc 
			order by ednumi asc;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@eduact)
		END CATCH
	END

	IF @tipo=4 --OBTENER DETALLE DE SUELDOS
	BEGIN
		BEGIN TRY
			SELECT eelinea,eenumi,eeano,eemes,eeprecio,1 as estado FROM TCE0041
			WHERE eenumi=@ednumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END

	IF @tipo=5 --OBTENER DETALLE DE HISTORICO DE PRECIO DE CUOTAS
	BEGIN
		BEGIN TRY
			SELECT b.eelinea, b.eenumi, b.eeano, b.eemes, b.eeprecio 
			FROM TCE004 a inner join TCE0041 b on a.ednumi = b.eenumi
				 and a.edtipo = @edtipo 
			ORDER BY b.eeano, b.eemes
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END

	IF @tipo=6 --AYUDA SERVICIOS
	BEGIN
		BEGIN TRY
			SELECT ednumi,eddesc
			FROM TCE004
			ORDER BY eddesc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END

	IF @tipo=7 --AYUDA SERVICIOS AUTOESCUELA
	BEGIN
		BEGIN TRY
			SELECT ednumi,eddesc
			FROM TCE004
			Where tce004.edtipo=1
			ORDER BY eddesc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END

	IF @tipo=8 --DETALLE TCE0042
	BEGIN
		BEGIN TRY
			SELECT eqnumi,eqtce4,eqtip1_4,b.cedesc1,eqmes,eqano,eqprecio,1 as estado
			FROM TCE0042 a,TC0051 b
			Where a.eqtce4=@ednumi and
				  a.eqtip1_4=b.cenum and b.cecod1=1 and b.cecod2=4
			ORDER BY eqtip1_4,eqnumi asc;
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END

		IF @tipo=9 --LISTAR SUCURSALES
	BEGIN
		BEGIN TRY
			Select a.canumi ,a.cadesc 
			from TC001 as a 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),5,@newFecha,@newHora,@eduact)
		END CATCH
	END
END










