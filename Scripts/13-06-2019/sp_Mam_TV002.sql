USE [DBDies_Monaco]
GO
/****** Object:  StoredProcedure [dbo].[sp_Mam_TV002]    Script Date: 13/06/2019 5:44:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--drop procedure sp_Mam_TV002
alter PROCEDURE [dbo].[sp_Mam_TV002] (@tipo int,@vcnumi int=-1,@vcsector int=-1,
@vcalm int =-1,
@vcfdoc date=null,@vcclie int=-1,
@vcfvcr date=null,@vctipo int=-1,@vcest int=-1,@vcobs nvarchar(50)='',@vcdesc decimal(18,2)=0,
@vctotal decimal(18,2)=0,@vcmoneda int=-1,@vcbanco int=-1,@TV0021 TV0021Type Readonly,@vcuact nvarchar(10)='',@fechai date=null,@fechaf date=null)

AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))
	DECLARE @newFecha date
	set @newFecha=GETDATE()

	IF @tipo=-1 --ELIMINAR REGISTRO
	BEGIN
		BEGIN TRY 
		update TV002 set vcest =-1 where vcnumi =@vcnumi 
			--DELETE from TV002 where vcnumi  =@vcnumi	 
		
			select @vcnumi as newNumi  --Consultar que hace newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),-1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
	
	
	IF @tipo=1 --NUEVO REGISTRO PARA LOS DEMAS
	BEGIN
		BEGIN TRY 
			set @vcnumi=IIF((select COUNT(vcnumi) from TV002)=0,0,(select MAX(vcnumi) from TV002))+1
			
			INSERT INTO TV002 VALUES(@vcnumi  ,@vcsector ,@vcalm ,@vcfdoc ,@vcclie ,@vcfvcr ,@vctipo ,
			@vcest ,@vcobs ,@vcdesc ,@vctotal ,@vcmoneda,@vcbanco )
			----INSERTO EL DETALLE

		    INSERT INTO TV0021 (vdvc2numi ,vccantclases,vdserv  ,vdcmin ,vdpbas ,vdptot ,vdporc ,vddesc ,vdtotdesc ,vdobs ,vdpcos ,vdptot2 )
			SELECT @vcnumi,td.edcant,td.vdserv  ,td.vdcmin ,td.vdpbas ,td.vdptot ,td.vdporc ,td.vddesc ,td.vdtotdesc ,
			td.vdobs ,td.vdpcos ,td.vdptot2  FROM @TV0021 AS td
			where td.estado =0

			select @vcnumi as newNumi
			
			

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@vcuact)
		END CATCH
	END
	
	IF @tipo=2--MODIFICACION
	BEGIN
		BEGIN TRY 
				
			UPDATE TV002 SET vcsector =@vcsector ,
			vcalm =@vcalm ,vcfdoc =@vcfdoc ,vcclie =@vcclie ,vcfvcr =@vcfvcr ,vctipo =@vctipo ,vcest =@vcest ,
			vcobs =@vcobs ,vcdesc =@vcdesc ,vctotal =@vctotal,vcmoneda =@vcmoneda,vcbanco=@vcbanco 
			Where vcnumi = @vcnumi

			

		 ----------MODIFICO EL DETALLE DE EQUIPO------------
			--INSERTO LOS NUEVOS
		    INSERT INTO TV0021 (vdvc2numi ,vccantclases,vdserv  ,vdcmin ,vdpbas ,vdptot ,vdporc ,vddesc ,vdtotdesc ,vdobs ,vdpcos ,vdptot2 )
			SELECT @vcnumi,td.edcant,td.vdserv  ,td.vdcmin ,td.vdpbas ,td.vdptot ,td.vdporc ,td.vddesc ,td.vdtotdesc ,
			td.vdobs ,td.vdpcos ,td.vdptot2  FROM @TV0021 AS td
			where td.estado =0

					--MODIFICO LOS REGISTROS
			UPDATE TV0021
			SET vdserv =td.vdserv ,vdcmin =td.vdcmin ,vdpbas=td.vdpbas ,vdptot =td.vdptot ,vccantclases=td.edcant,
			vdporc =td.vdporc ,vddesc =td.vddesc ,vdtotdesc =td.vdtotdesc ,vdobs =td.vdobs ,vdpcos =td.vdpcos ,
			vdptot2 =td.vdptot2 
			FROM TV0021  INNER JOIN @TV0021 AS td
			ON TV0021 .vdnumi     = td.vdnumi   and td.estado=2;

			--ELIMINO LOS REGISTROS
			DELETE FROM TV0021 WHERE vdnumi   in (SELECT td.vdnumi   FROM @TV0021 AS td WHERE td.estado=-1)


			select @vcnumi as newNumi
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),2,@newFecha,@newHora,@vcuact)
		END CATCH
	END

	IF @tipo=3 --MOSTRaR TODOS
	BEGIN
		BEGIN TRY
	
select venta.vcnumi ,venta.vcsector ,a.cedesc1 as sector,venta.vcalm ,venta.vcfdoc ,venta.vcclie,
IIF (vcsector =1,(select concat (a.cbnom ,' ', a.cbape ) as nombre from TCE002 as a where a.cbnumi=venta.vcclie),
(select concat (a.elnom ,' ',a.elapep ,' ',a.elapem ) as nombree from TCE009 as a where a.elnumi=venta.vcclie)) as alumno,venta.vcfvcr ,venta.vctipo ,venta.vcest ,
venta.vcobs ,(select sum (detalle.vdtotdesc ) from TV0021 as detalle where detalle .vdvc2numi =venta.vcnumi ) as vctotal ,venta.vcmoneda
from TV002 as venta inner join TC0051 as a on a.cecod1 =6 and a.cecod2 =1 and a.cenum =venta.vcsector 
where venta.vcest <>-1
order by vcnumi asc
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

IF @tipo=4 --MOSTRaR DETALLE de la venta de servicios con personal y sin personal
	BEGIN
		BEGIN TRY


	select detalle .vdnumi ,detalle .vdvc2numi ,detalle .vdserv ,isnull(detalle.vccantclases,0) as edcant ,servicio.eddesc as servicio,
	detalle.vdcmin ,detalle.vdpbas ,detalle .vdptot ,detalle.vdporc ,detalle .vddesc ,detalle.vdtotdesc ,detalle.vdobs ,detalle .vdpcos ,
	detalle.vdptot2 ,1 as estado
	from TV0021 as detalle inner join TCE004 as servicio 
	on servicio.ednumi =detalle.vdserv 
	where detalle .vdvc2numi =@vcnumi 

		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	IF @tipo=5 --LISTAR servicios
	BEGIN
		BEGIN TRY
		
		select servicio .ednumi,servicio.eddesc ,servicio .edprec,isnull(servicio.edcant,0) as edcant 
		from TCE004 as servicio
		where servicio.edtipo =@vctipo and servicio .ednumi not in (
		select td.vdserv   from @TV0021 as td where td.estado >=0) 
		and servicio.edprec >0
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
	IF @tipo=6 --LISTAR Alumnos
	BEGIN
		BEGIN TRY
		
		if (@vctipo = 1)
		begin
		select  a.cbnumi as codigo, a.cbci as ci,concat (a.cbnom ,' ', a.cbape ) as nombre
		from TCE002 as a
		order by a.cbnumi desc
		end
		else
		begin
		select a.elnumi as codigo,a.elci as ci,concat (a.elnom ,' ',a.elapep ,' ',a.elapem ) as nombre
		from TCE009 as a 
		order by a.elnumi desc
		end



		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
	IF @tipo=7 --LISTAR Alumnos
	BEGIN
		BEGIN TRY
		select venta.vcfdoc as ldfdoc ,IIF (vcsector =1,(select a.cbci from TCE002 as a where a.cbnumi=venta.vcclie),
(select elci from TCE009 as a where a.elnumi=venta.vcclie)) as laci ,
IIF (vcsector =1,(select concat (a.cbnom ,' ', a.cbape ) as nombre from TCE002 as a where a.cbnumi=venta.vcclie),
(select concat (a.elnom ,' ',a.elapep ,' ',a.elapem ) as nombree from TCE009 as a where a.elnumi=venta.vcclie)) as nombre,
IIF (vcsector =1,(select a.cbtelef1 from TCE002 as a where a.cbnumi=venta.vcclie),
(select eltelf1 as nombree from TCE009 as a where a.elnumi=venta.vcclie)) as latelf1,
servicio.eddesc ,detail .vdpbas as lcpuni,detail .vdcmin as lcpuni,detail.vdcmin as lccant,
detail .vdporc as lcpdes,detail .vddesc as lcmdes,detail .vdtotdesc as lcptot
from TV002 as venta inner join TC0051 as a on a.cecod1 =6 and a.cecod2 =1 and a.cenum =venta.vcsector 
inner join TV0021 as detail on detail .vdvc2numi =venta.vcnumi 
inner join TCE004 as servicio on servicio .ednumi =detail.vdserv 
where venta.vcnumi =@vcnumi 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	IF @tipo=8 --CAJa
	BEGIN
		BEGIN TRY
		
	select venta.vcnumi as tanumi,venta.vcfdoc as tafdoc,IIF (vcsector =1,(select concat (a.cbnom ,' ', a.cbape ) as nombre from TCE002 as a where a.cbnumi=venta.vcclie),
(select concat (a.elnom ,' ',a.elapep ,' ',a.elapem ) as nombree from TCE009 as a where a.elnumi=venta.vcclie)) as cliente,
tipoventa .cedesc1 as tipoventa,detalle .vdnumi as codigoproducto,servicio .eddesc as producto,
detalle.vdcmin as tbcmin,detalle.vdpbas as tbpbas,detalle.vdptot as tbptot,detalle.vddesc as tbdesc,detalle.vdtotdesc as tbtotdesc 
from TV002 as venta inner join
TC0051 as tipoventa on tipoventa .cecod1 =14 and tipoventa .cecod2 =3 and tipoventa .cenum =venta.vctipo 
inner join TV0021 as detalle on detalle.vdvc2numi =venta.vcnumi 
inner join TCE004 as servicio on servicio .ednumi =detalle.vdserv 
where venta.vcfdoc >=@fechai  and venta.vcfdoc <=@fechaf and venta.vcest >=0



		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END

	IF @tipo=9 --CAJa
	BEGIN
		BEGIN TRY
		
select tipoventa .cenum as yccod3,tipoventa .cedesc1 as ycdes3,(sum (detalle.vdtotdesc ))as total
from TV002 as venta inner join
TV0021 as detalle on detalle .vdvc2numi =venta.vcnumi 
inner join TC0051 as tipoventa on tipoventa .cecod1 =14 and tipoventa .cecod2 =3 
and tipoventa .cenum =venta.vctipo  and venta.vcest >=0
where venta.vcfdoc >=@fechai  and venta.vcfdoc <=@fechaf 
group by tipoventa .cenum ,tipoventa .cedesc1 



		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@vcuact)
		END CATCH

END
End


