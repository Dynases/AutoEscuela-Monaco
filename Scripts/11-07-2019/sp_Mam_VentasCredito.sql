USE [DBDies_Monaco]
GO
/****** Object:  StoredProcedure [dbo].[sp_Mam_VentasCredito]    Script Date: 11/07/2019 6:23:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--drop procedure sp_Mam_TY004
ALTER PROCEDURE [dbo].[sp_Mam_VentasCredito] (@tipo int=-1,@fechaI date=null,@fechaF date=null,@yduact nvarchar(10)='',
@cliente int=-1,@codCredito int=-1,@catPrecio int=-1,@almacen int=-1,@vendedor int=-1,@alumno int=-1)

--TZ0013Type
AS
BEGIN
	DECLARE @newHora nvarchar(5)
	set @newHora=CONCAT(DATEPART(HOUR,GETDATE()),':',DATEPART(MINUTE,GETDATE()))
	declare @numi int
	DECLARE @newFecha date
	set @newFecha=GETDATE()


	IF @tipo=1 --
	BEGIN
		BEGIN TRY 
		select cliente.cbnumi   as numicliente,concat (cliente.cbnom ,' ', cliente.cbape ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte


,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie   
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,


venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE002 as cliente on cliente .cbnumi =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF 
and venta.vcsector =1
group by cliente .cbnumi ,cliente.cbci  ,cliente .cbnom ,venta.vcclie ,cliente .cbape   ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 

union all
		select cliente.elnumi   as numicliente,concat (cliente.elnom  ,' ', cliente.elapep ,' ',cliente.elapem  ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte

,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie  
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,
venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE009 as cliente on cliente .elnumi  =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF 
and venta.vcsector <>1
group by cliente .elnumi ,cliente.elci   ,cliente .elnom  ,venta.vcclie ,cliente .elapep ,cliente.elapem    ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@yduact)
		END CATCH
	END

	--IF @tipo=111 -- Kardex de Cliente Resumen PR_KardexCredito
	--BEGIN
	--	BEGIN TRY 
	--	select Isnull(sum(pagos.tdmonto),0) as pago,concat(cliente.ydalmacen  ,'-',cliente.ydcod) as numicliente,cliente .yddesc  as cliente,
	--	       cliente.yddirec AS direc, cliente.ydtelf1 AS telf1, cliente.ydtelf2 AS contacto, 
	--		   cliente.ydnumivend AS codven, cliente.ydlcred, venta.tanumi, venta.tafdoc, venta.tafvcr, yddias, venta.tatotal,
 --              (SELECT        yddesc
 --                 FROM            ty004
 --                WHERE        ydnumi = cliente.ydnumivend) AS desven,
	--			  (cliente.ydlcred - ((SELECT SUM (a.tatotal) FROM tv001 a WHERE a.taclpr = cliente.ydnumi and a.tatven = 0) - isnull((select sum(c.tdmonto) from tv001 a, tv0012 b, tv00121 c where a.tanumi = b.tctv1numi and c.tdtv12numi = b.tcnumi and a.taclpr = cliente.ydnumi),0))) as disponible 
	--	FRom ty004 as cliente, tv001 as venta, tv0012 cred LEFT OUTER JOIN tv00121 pagos on cred.tcnumi = pagos.tdtv12numi
	--	where venta.taclpr = cliente.ydnumi
	--	  and venta.tanumi = cred.tctv1numi
	--	  and venta.tafdoc >= @fechaI
	--	  and venta.tafdoc <= @fechaF
	--	  group by ydnumi,cliente .yddesc,cliente.ydalmacen ,cliente.ydcod  ,
	--	       cliente.yddirec, cliente.ydtelf1, cliente.ydtelf2, 
	--		   cliente.ydnumivend, cliente.ydlcred, venta.tanumi, venta.tafdoc,venta.tafvcr, cliente.yddias, venta.tatotal
	--	END TRY
	--	BEGIN CATCH
	--		INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
	--			   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@yduact)
	--	END CATCH
	--END

	--IF @tipo=112 -- Kardex de Cliente Resumen PR_KardexCredito
	--BEGIN
	--	BEGIN TRY 
	--	select Isnull(sum(pagos.tdmonto),0) as pago,concat(cliente.ydalmacen  ,'-',cliente.ydcod)as numicliente,cliente .yddesc   as cliente,
	--	       cliente.yddirec AS direc, cliente.ydtelf1 AS telf1, cliente.ydtelf2 AS contacto, 
	--		   cliente.ydnumivend AS codven, cliente.ydlcred, venta.tanumi, venta.tafdoc, venta.tafvcr, yddias, venta.tatotal,
 --              (SELECT        yddesc
 --                 FROM            ty004
 --                WHERE        ydnumi = cliente.ydnumivend) AS desven,
	--			  (cliente.ydlcred - ((SELECT SUM (a.tatotal) FROM tv001 a WHERE a.taclpr = cliente.ydnumi and a.tatven = 0) - isnull((select sum(c.tdmonto) from tv001 a, tv0012 b, tv00121 c where a.tanumi = b.tctv1numi and c.tdtv12numi = b.tcnumi and a.taclpr = cliente.ydnumi),0))) as disponible 
	--	FRom ty004 as cliente, tv001 as venta, tv0012 cred LEFT OUTER JOIN tv00121 pagos on cred.tcnumi = pagos.tdtv12numi
	--	where venta.taclpr = cliente.ydnumi
	--	  and venta.tanumi = cred.tctv1numi
	--	  and venta.tafdoc >= @fechaI
	--	  and venta.tafdoc <= @fechaF
	--	  and venta.taclpr = @cliente
	--	  group by ydnumi,cliente .yddesc ,cliente.ydalmacen ,cliente.ydcod ,
	--	       cliente.yddirec, cliente.ydtelf1, cliente.ydtelf2, 
	--		   cliente.ydnumivend, cliente.ydlcred, venta.tanumi, venta.tafdoc,venta.tafvcr, cliente.yddias, venta.tatotal
	--	END TRY
	--	BEGIN CATCH
	--		INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
	--			   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@yduact)
	--	END CATCH
	--END
--		IF @tipo=2 --
--	BEGIN
--		BEGIN TRY
--	select a.ydnumi ,a.ydcod ,a.ydrazonsocial as yddesc  ,a.yddctnum ,a.yddirec 
--		,a.ydtelf1 ,a.ydfnac 
--		from TY004 as a inner join TY0031 as tipodocumento on
--		 tipodocumento .yccod1 =2 and tipodocumento .yccod2 =1 and tipodocumento .yccod3 =a.yddct and a.ydtip =1
--				order by ydcod 
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END
	IF @tipo=3 --
	BEGIN
		BEGIN TRY 
				select cliente.elnumi    as numicliente,concat (cliente.elnom  ,' ', cliente.elapep ,' ',cliente.elapem  ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte

,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie  
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,
venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE009 as cliente on cliente .elnumi  =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF 
and venta.vcsector <>1 and venta.vcclie =@cliente 
group by cliente .elnumi ,cliente.elci  ,cliente .elnom  ,venta.vcclie ,cliente .elapep ,cliente.elapem    ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 

union all
		select cliente.cbnumi   as numicliente,concat (cliente.cbnom ,' ', cliente.cbape ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte

,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie  
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,
venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE002 as cliente on cliente .cbnumi =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF and venta.vcclie =@cliente 
and venta.vcsector =1
group by cliente .cbnumi ,cliente.cbci  ,cliente .cbnom ,venta.vcclie ,cliente .cbape   ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 


		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),1,@newFecha,@newHora,@yduact)
		END CATCH
	END


		IF @tipo=4 -- LISTAR CUENTAS POR CLIENTES
	BEGIN
		BEGIN TRY
	select a.tcnumi ,a.tctv1numi as numiVenta,''
 as numeroFactura,a.tcfdoc as fechaVenta, a.tcfvencre as FechaVencCredito,a.tctotcre as totalVenta
from TV0012 as a where a.tcty4clie =@cliente 
order by a.tctv1numi 
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
		END CATCH

END

	IF @tipo=5 
	BEGIN
		BEGIN TRY
					select cliente.cbnumi   as numicliente,concat (cliente.cbnom ,' ', cliente.cbape ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte

,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie  
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,
venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE002 as cliente on cliente .cbnumi =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF 
and venta.vcsector =1 and venta.vcclie =@cliente  and a.tcnumi =@codCredito
group by cliente .cbnumi ,cliente.cbci  ,cliente .cbnom ,venta.vcclie ,cliente .cbape   ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 

union all
		select cliente.elnumi   as numicliente,concat (cliente.elnom  ,' ', cliente.elapep ,' ',cliente.elapem  ) as cliente,
(select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
and totalcredito .tctv1numi =venta.vcnumi  
and totalcredito.tcfdoc  >=@fechaI and totalcredito.tcfdoc <=@fechaF)as credito,

isnull((select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux .tctv1numi =venta.vcnumi  
and aux.tcty4clie =venta.vcclie  and aux.tcfdoc >=@fechaI and aux.tcfdoc <=@fechaF),0) as aporte

,isnull(((select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie  
and totalcredito .tctv1numi =venta.vcnumi   and totalcredito .tcfdoc >=@fechaI
and totalcredito .tcfdoc <=@fechaF)-
(select Sum(aporte.tdmonto )  from TV00121 as aporte ,TV0012 as aux where aux.tcnumi =aporte .tdtv12numi 
and aux.tcty4clie =venta.vcclie and aux .tctv1numi =venta.vcnumi   and aux.tcfdoc >=@fechaI and aux .tcfdoc <=@fechaF )),
 (select  Sum(totalcredito .tctotcre ) from TV0012 as totalcredito where totalcredito .tcty4clie =venta.vcclie 
 and totalcredito .tctv1numi =venta.vcnumi and totalcredito .tcfdoc >=@fechaI and totalcredito .tcfdoc <=@fechaF))as deuda,
venta.vcnumi as numiventa,'PRINCIPAL' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy') as fechaventa,FORMAT (venta.vcfvcr  , 'dd-MM-yyyy') as fechacredito,a.tctotcre,
detallepago .tdnrorecibo ,FORMAT (detallepago .tdfechaPago , 'dd-MM-yyyy') as tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto 
,IIF((select Sum(auxdetallepago.tdmonto) from TV00121 as auxdetallepago where auxdetallepago.tdtv12numi=a.tcnumi)=a.tctotcre 
,IIF((select Max(ayuda.tdnumi) from TV00121 ayuda where ayuda.tdtv12numi=a.tcnumi)=detallepago.tdnumi,'CANCELACION TOTAL',
'CANCELACION PARCIAL'),'CANCELACION PARCIAL')as observacion
from TV0012  as a 
inner join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi  =a.tctv1numi 
inner join TCE009 as cliente on cliente .elnumi =venta.vcclie
and venta.vcfdoc  >=@fechaI and venta.vcfdoc <=@fechaF and venta.vcclie =@cliente 
and venta.vcsector <>1 and a.tcnumi =@codCredito
group by cliente .elnumi  ,cliente.elci   ,cliente .elnom  ,venta.vcclie ,cliente .elapep ,cliente.elapem    ,venta.vcnumi 
 ,venta . vcfdoc  ,venta.vcfvcr   ,a.tctotcre,
detallepago .tdnrorecibo ,detallepago .tdfechaPago ,detallepago .tdnrodoc  ,detallepago .tdmonto ,a.tcnumi,detallepago .tdnumi 


		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
		END CATCH

END


--	IF @tipo=6 -- REPORTE UTILIDAD 
--	BEGIN
--		BEGIN TRY
	
--SELECT      deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod,Sum(c.iccven) as iccven, b.yccod3 , b.ycdes3 ,precio.yhprecio  as preciou,
-- Cast((Sum(c.iccven)*precio.yhprecio ) as decimal(18,2))as total
--FROM            dbo.TY005  AS a INNER JOIN
--                         dbo.TI001 AS c ON a.yfnumi  = c.iccprod INNER JOIN
--                         dbo.TY0031 AS b ON a.yfMed  = b.yccod3 
--						 inner join TA002 as deposito on deposito .abnumi =c.icalm 
--						 inner join TY007 as precio on precio .yhalm=@almacen--almacen
--						 and precio.yhcatpre =@catPrecio and precio .yhprod =a.yfnumi 
--WHERE        (b.yccod1  = 1) AND (b.yccod2  = 5)

--group by deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod ,b.yccod3 , b.ycdes3 ,precio.yhprecio 
--order by deposito .abnumi asc
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END

--IF @tipo=7 -- LISTAR PRECIOS DE VENTA
--	BEGIN
--		BEGIN TRY
	
--select a.ygnumi  ,a.ygdesc 
--from TY006 as a where a.ygpcv =1  ----precio de venta
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END
--IF @tipo=8 -- LISTAR PRECIOS DE VENTA
--	BEGIN
--		BEGIN TRY
	
--select a.ygnumi  ,a.ygdesc 
--from TY006 as a where a.ygpcv =0  ----precio de costo
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END

--IF @tipo=9 -- utilidad de productos
--	BEGIN
--		BEGIN TRY
	
--SELECT      deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod,Sum(c.iccven) as iccven, b.yccod3 , b.ycdes3 ,precio.yhprecio  as preciou,
-- Cast((Sum(c.iccven)*precio.yhprecio ) as decimal(18,2))as total
--FROM            dbo.TY005  AS a INNER JOIN
--                         dbo.TI001 AS c ON a.yfnumi  = c.iccprod INNER JOIN
--                         dbo.TY0031 AS b ON a.yfMed  = b.yccod3 
--						 inner join TA002 as deposito on deposito .abnumi =c.icalm 
--						 inner join TY007 as precio on precio .yhalm=@almacen ---almacen
--						 and precio.yhcatpre =@catPrecio  and precio .yhprod =a.yfnumi 
--WHERE        (b.yccod1  = 1) AND (b.yccod2  = 5)

--group by deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod ,b.yccod3 , b.ycdes3 ,precio.yhprecio 
--order by deposito .abnumi asc
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END


IF @tipo=10 -- REPORTE DE MOROSIDAD TODOS ALMACEN - VENDEDOR
	BEGIN
		BEGIN TRY
	





select cliente.cbnumi   as numicliente,concat(cliente.cbnom ,' ',cliente.cbape  )  as cliente
,venta.vcnumi  as numiventa,'Principal' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy')  as fechaventa,FORMAT (venta.vcfvcr , 'dd-MM-yyyy')  as fechacredito,a.tctotcre as venta,
Sum(Isnull(detallepago.tdmonto,0) ) as Pagos,((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0) ))as saldo,
DATEDIFF(DAY,venta.vcfdoc  , Getdate()) as mora
from TV0012  as a 
inner join TCE002 as cliente on 
 cliente .cbnumi  =a.tcty4clie 
left join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi =a.tctv1numi 
and venta.vcsector =1
group by cliente .cbnumi  ,cliente .cbnom ,cliente .cbape ,venta.vcnumi ,venta .vcfdoc  ,venta.vcfvcr   ,a.tctotcre
having ((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0)))>0

union ALl

select cliente.elnumi as numicliente,concat(cliente.elnom  ,' ',cliente.elapep ,' ',elapem   )  as cliente
,venta.vcnumi  as numiventa,'Principal' as aabdes ,FORMAT (venta.vcfdoc , 'dd-MM-yyyy')  as fechaventa,FORMAT (venta.vcfvcr , 'dd-MM-yyyy')  as fechacredito,a.tctotcre as venta,
Sum(Isnull(detallepago.tdmonto,0) ) as Pagos,((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0) ))as saldo,
DATEDIFF(DAY,venta.vcfdoc  , Getdate()) as mora
from TV0012  as a 
inner join TCE009 as cliente on 
 cliente .elnumi  =a.tcty4clie 
left join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
inner join TV002 as venta on venta.vcnumi =a.tctv1numi 
and venta.vcsector <>1
group by cliente .elnumi   ,cliente .elnom ,cliente.elapep  ,cliente .elapem  ,venta.vcnumi ,venta .vcfdoc  ,venta.vcfvcr   ,a.tctotcre
having ((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0)))>0
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
		END CATCH

END
--IF @tipo=11 -- REPORTE DE MOROSIDAD TODOS ALMACEN - VENDEDOR
--	BEGIN
--		BEGIN TRY
	

--select  concat (cliente.ydalmacen  ,'-',cliente.ydcod)  as numicliente,cliente .yddesc  as cliente
--,venta.tanumi as numiventa,almacen .aabdes ,FORMAT (venta.tafdoc, 'dd-MM-yyyy')  as fechaventa,FORMAT (venta.tafvcr, 'dd-MM-yyyy')  as fechacredito,a.tctotcre as venta,
--Sum(Isnull(detallepago.tdmonto,0) ) as Pagos,((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0) ))as saldo,
--DATEDIFF(DAY,venta.tafdoc , venta.tafvcr) as mora
--from TV0012  as a 
--inner join TY004 as cliente on cliente .ydtip =1
--and cliente .ydnumi =a.tcty4clie 
--left join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
--inner join TV001 as venta on venta.tanumi =a.tctv1numi 
--inner join TA001 as almacen on almacen .aanumi =venta.taalm 
--and a.tcty4vend =@vendedor
--group by cliente .ydnumi ,cliente.ydalmacen,cliente.ydcod,cliente .yddesc ,venta.tanumi ,almacen .aabdes ,venta .tafdoc ,venta.tafvcr  ,a.tctotcre
--having ((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0)))>0
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END

--IF @tipo=12 -- REPORTE DE MOROSIDAD TODOS ALMACEN - VENDEDOR
--	BEGIN
--		BEGIN TRY
	

--select  concat (cliente.ydalmacen  ,'-',cliente.ydcod)  as numicliente,cliente .yddesc  as cliente

--,venta.tanumi as numiventa,almacen .aabdes ,venta.tafdoc as fechaventa,venta.tafvcr as fechacredito,a.tctotcre as venta,
--Sum(Isnull(detallepago.tdmonto,0) ) as Pagos,((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0) ))as saldo,
--DATEDIFF(DAY,venta.tafdoc , venta.tafvcr) as mora
--from TV0012  as a 
--inner join TY004 as cliente on cliente .ydtip =1
--and cliente .ydnumi =a.tcty4clie 
--left join TV00121 as detallepago on detallepago .tdtv12numi =a.tcnumi 
--inner join TV001 as venta on venta.tanumi =a.tctv1numi 
--inner join TA001 as almacen on almacen .aanumi =venta.taalm 
--and a.tcty4vend =@vendedor  and almacen .aanumi =@almacen 
--group by cliente .ydnumi ,cliente.ydalmacen ,cliente .ydcod ,cliente .yddesc ,venta.tanumi ,almacen .aabdes ,venta .tafdoc ,venta.tafvcr  ,a.tctotcre
--having ((a.tctotcre)-Sum(Isnull(detallepago.tdmonto,0)))>0
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END

--IF @tipo=13 -- utilidad de productos stock mayor a cero
--	BEGIN
--		BEGIN TRY
	
--SELECT      deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod,Sum(c.iccven) as iccven, b.yccod3 , b.ycdes3 ,precio.yhprecio  as preciou,
-- Cast((Sum(c.iccven)*precio.yhprecio ) as decimal(18,2))as total
--FROM            dbo.TY005  AS a INNER JOIN
--                         dbo.TI001 AS c ON a.yfnumi  = c.iccprod INNER JOIN
--                         dbo.TY0031 AS b ON a.yfMed  = b.yccod3 
--						 inner join TA002 as deposito on deposito .abnumi =c.icalm 
--						 inner join TY007 as precio on precio .yhalm=@almacen ---almacen
--						 and precio.yhcatpre =@catPrecio  and precio .yhprod =a.yfnumi 
--WHERE        (b.yccod1  = 1) AND (b.yccod2  = 5) and (c.iccven>0)

--group by deposito .abnumi ,deposito .abdesc  ,  a.yfnumi , a.yfcprod  , a.yfcdprod1 , a.yfMed
-- , a.yfap , c.iccprod ,b.yccod3 , b.ycdes3 ,precio.yhprecio 
--order by deposito .abnumi asc
		
--		END TRY
--		BEGIN CATCH
--			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
--				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
--		END CATCH

--END

IF @tipo=20 -- LISTAR HISTORIAL DE CLASE DE ALUMNOS
	BEGIN
		BEGIN TRY
	

	
	
	SELECT 1 as numi,a.egalum, concat(b.cbape,' ',b.cbnom )as cbape,concat(b.cbnom,' ',b.cbape ) as cbnom,  a.egchof,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec asc) as fechaIni,
				   (select top 1 aa.ehfec from TCE0061 aa where aa.ehnumi=a.egnumi order by aa.ehfec desc) as fechaFin,
				   c.panumi as ejchof,(CONCAT(c.panom,' ',c.paape)) as panom2,'Clases Practicas Programadas' as concepto,
				   (select count(bb.ehnumi) from tce0061 bb where a.egnumi=bb.ehnumi and bb.ehest in (0,1) ) as cantidaddias,
					cast(a.egfact as nvarchar(20)) as fecha,
				   c.pasuc,d.cadesc as cadesc1
			FROM TCE006 a,TCE002 b,TP001 c,tc001 as d
			where b.cbnumi=a.egalum and c.patipo=1 and c.panumi=a.egchof and
				  a.egalum not in (select tce007.ejalum from TCE007) and 
				  d.canumi=b.cbsuc and b.cbnumi =@alumno
				  and a.egfact >=@fechaI and a.egfact <=@fechaF 

	union
		
	SELECT 2 as numi,a.egalum, concat(b.cbape,' ',b.cbnom )as cbape,concat(b.cbnom,' ',b.cbape ) as cbnom,
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
				  d.canumi=b.cbsuc and b.cbnumi =@alumno 
				    and a.egfact >=@fechaI and a.egfact <=@fechaF 

union
		
	SELECT 3 as numi, a.egalum,concat(b.cbape,' ',b.cbnom )as cbape,concat(b.cbnom,' ',b.cbape ) as cbnom,
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
				  d.canumi=b.cbsuc and b.cbnumi =@alumno 
				    and a.egfact >=@fechaI and a.egfact <=@fechaF 
union
		
	SELECT 4 as numi,a.egalum,concat(b.cbape,' ',b.cbnom )as cbape,concat(b.cbnom,' ',b.cbape ) as cbnom,
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
				  d.canumi=b.cbsuc and b.cbnumi =@alumno 
				    and a.egfact >=@fechaI and a.egfact <=@fechaF 
union
		
	SELECT 5 as numi, a.egalum, concat(b.cbape,' ',b.cbnom )as cbape,concat(b.cbnom,' ',b.cbape ) as cbnom,
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
				  d.canumi=b.cbsuc and b.cbnumi =@alumno 
				    and a.egfact >=@fechaI and a.egfact <=@fechaF 

	
	order by numi asc
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
		END CATCH

END

IF @tipo=21 -- LISTAR PRECIOS DE VENTA
	BEGIN
		BEGIN TRY
	
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
 where venta.vcclie=@alumno and venta .vcfdoc >=@fechaI and venta.vcfdoc <=@fechaF 
group by cliente .cbnumi  ,cliente .cbnom ,cliente .cbape ,venta.vcnumi ,venta .vcfdoc  ,venta.vcfvcr,venta.vctotal,venta.vctipo    
		
		END TRY
		BEGIN CATCH
			INSERT INTO TB001 (banum,baproc,balinea,bamensaje,batipo,bafact,bahact,bauact)
				   VALUES(ERROR_NUMBER(),ERROR_PROCEDURE(),ERROR_LINE(),ERROR_MESSAGE(),3,@newFecha,@newHora,@yduact)
		END CATCH

END
End


