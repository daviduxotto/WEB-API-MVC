using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class movimientoPagoCCController : Controller
    {
        public async Task<ActionResult> entornoMovCcXFecha()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }
        public async Task<ActionResult> entornoMovCcXFechaXEstacion()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }
        public async Task<ActionResult> edit()
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {
            if (Session["diatrabajo"] == null)
            {
               return RedirectToAction("elegirDia", "usuario");
            }
            eEntorno entorno = new eEntorno();
            entorno = await getDescripcionEntorno();
            ViewBag.empresa = entorno.empresa + " -- " + entorno.descEmpresa;
            ViewBag.estacion = entorno.sucursal + " -- " + entorno.descSucursal;
            ViewBag.dia = Session["diatrabajo"].ToString();
            return View();
         }

      }

      public async Task<ActionResult> getMovimientoPagoCC()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eMovimientoPagoCC> ListMovimientoPagoCC = new List<eMovimientoPagoCC>();
         DataTable dtMovimientoPagoCC = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoPagoCC MovimientoPagoCC = new eMovimientoPagoCC();
            MovimientoPagoCC.codEmpresa = Session["empresa"].ToString();
            MovimientoPagoCC.codSucursal = Session["sucursal"].ToString();
            MovimientoPagoCC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string MovimientoPagoCCJSON = JsonConvert.SerializeObject(MovimientoPagoCC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoCC/getMovPagoCCXFecha", MovimientoPagoCCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoPagoCC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtMovimientoPagoCC.Rows)
            {
               eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
               varmovimiento.codEmpresa = row["cod_empresa"].ToString();
               varmovimiento.descEmpresa = row["desc_empresa"].ToString();
               varmovimiento.codSucursal = row["cod_estacion"].ToString();
               varmovimiento.descSucursal = row["desc_sucursal"].ToString();
               varmovimiento.codBanco = row["cod_banco"].ToString();
               varmovimiento.descBanco = row["desc_banco"].ToString();
               varmovimiento.codCuenta = row["cod_cuenta"].ToString();
               varmovimiento.descCuenta = row["desc_cuenta"].ToString();
               varmovimiento.codDoctoBanco = row["cod_docto_banco"].ToString();
               varmovimiento.descDoctoBanco = row["desc_tipodocto"].ToString();
               varmovimiento.codDocumento = row["cod_documento"].ToString();
               varmovimiento.codCliente = row["cod_cliente"].ToString();
               varmovimiento.descCliente = row["desc_cliente"].ToString();
               varmovimiento.fecha = Convert.ToDateTime(row["fecha"].ToString());
               varmovimiento.monto = Convert.ToDouble(row["monto_documento"].ToString());
               if (row["conciliado"].ToString() == "S") { varmovimiento.conciliado = true; } else { varmovimiento.conciliado = false; }
               if (row["reversado"].ToString() == "S") { varmovimiento.revertido = true; } else { varmovimiento.revertido = false; }
               //   varmovimiento.conciliado = libreria.chartoBool(row[""].ToString);
               ListMovimientoPagoCC.Add(varmovimiento);
               TempData["descempresa"] = varmovimiento.codEmpresa + " -- " + varmovimiento.descEmpresa;
               TempData["descestacion"] = varmovimiento.codSucursal + " -- " + varmovimiento.descSucursal;
               TempData["dia"] = varmovimiento.fecha;
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListMovimientoPagoCC }, JsonRequestBehavior.AllowGet);
      }

        public async Task<ActionResult> getMovPagoCCXFechaDetalle(string id, string Banco, string cuenta, string doctobanco, string documento)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eMovimientoPagoCC> ListMovimientoPagoCC = new List<eMovimientoPagoCC>();
            DataTable dtMovimientoPagoCC = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoCC MovimientoPagoCC = new eMovimientoPagoCC();
                MovimientoPagoCC.codEmpresa = Session["empresa"].ToString();
                MovimientoPagoCC.codSucursal = Session["sucursal"].ToString();
                MovimientoPagoCC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                MovimientoPagoCC.codBanco = Banco;
                MovimientoPagoCC.codCuenta = cuenta;
                MovimientoPagoCC.codDoctoBanco = doctobanco;
                MovimientoPagoCC.codDocumento = documento;
                string MovimientoPagoCCJSON = JsonConvert.SerializeObject(MovimientoPagoCC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoCC/getMovPagoCCXFechaDetalle", MovimientoPagoCCJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimientoPagoCC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtMovimientoPagoCC.Rows)
                {
                    eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
                    varmovimiento.codEmpresa = row["cod_empresa"].ToString();
                    varmovimiento.codSucursal = row["cod_estacion"].ToString();
                    varmovimiento.codDoctoPosFactu = row["cod_docto_pos_factu"].ToString();
                    varmovimiento.noSerie = row["no_serie"].ToString();
                    varmovimiento.noFactura = row["no_factura"].ToString();
                    varmovimiento.fechaFactura = row["fecha_factura"].ToString();
                    varmovimiento.montoFactura = Convert.ToDouble(row["monto_total_factura"].ToString());
                    varmovimiento.montoPagadoFactura = Convert.ToDouble(row["monto_pagado"].ToString());
                    varmovimiento.saldoPendienteFactura = Convert.ToDouble(row["saldo_pendiente"].ToString());
                    varmovimiento.codDoctoPosVale = row["cod_docto_pos_vale"].ToString();
                    varmovimiento.noVale = row["no_vale"].ToString();
                    varmovimiento.fechaVale= row["fecha_vale"].ToString();
                    varmovimiento.montoVale = Convert.ToDouble(row["monto_total_vale"].ToString());
                    ListMovimientoPagoCC.Add(varmovimiento);
                }
                
            }
            catch 
            {
                return null;
            }
            return Json(ListMovimientoPagoCC , JsonRequestBehavior.AllowGet);
        }

        public async Task<eEntorno> getDescripcionEntorno()
      {
         eEntorno entornoresult = new eEntorno();
         DataTable dtBancos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEntorno entorno = new eEntorno();
            entorno.empresa = Session["empresa"].ToString();
            entorno.sucursal = Session["sucursal"].ToString();
            string entornoJSON = JsonConvert.SerializeObject(entorno);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("entorno/getdescripcionentorno", entornoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtBancos.Rows)
            {
               entornoresult.empresa = row["cod_empresa"].ToString();
               entornoresult.descEmpresa = row["desc_empresa"].ToString();
               entornoresult.sucursal = row["cod_sucursal"].ToString();
               entornoresult.descSucursal = row["desc_sucursal"].ToString();
            }

         }
         catch
         {
            return null;
         }
         return entornoresult;
      }
      public ActionResult conciliar(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento = (documento).Replace("*", " ");

         return View(varmovimiento);
      }
      public async Task<ActionResult> guardarConciliar(eMovimientoPagoCC varmovimiento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         bool status = false;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());

            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            varmovimiento.codSucursal = Session["sucursal"].ToString();

            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/conciliar", MovimientoEfectivoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            }
            catch
            {
                salida.retorno = -9999;
                salida.mensaje = "Ha ocurrido un error en la comunicacion con la API";
            }
            return new JsonResult { Data = new { salida = salida } };
        }

      public async Task<ActionResult> getLiquidacionPagoClienteXempresa()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         DataTable dtResultado = new DataTable();
         eLiquidacionVentaDiariaxEmpresa liquidacion = new eLiquidacionVentaDiariaxEmpresa();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoEfectivoDet movimiento = new eMovimientoEfectivoDet();
            movimiento.codEmpresa = Session["empresa"].ToString();
            movimiento.codSucursal = Session["sucursal"].ToString();
            movimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
            movimiento.tipoPago = "N";
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/getLiquidacionPagoClienteXempresa", movimientoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtResultado = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtResultado.Rows)
            {

               liquidacion.montoTotalReportado = row["monto_total_reportado"].ToString();
               
               liquidacion.montoTotalConciliado = row["monto_total_doctos_concil"].ToString();
               liquidacion.diferenciaMontoConciliado = row["dif_montovrsdoctosconcil"].ToString();

            }

         }
         catch
         {
            return null;
         }
         return Json(liquidacion, JsonRequestBehavior.AllowGet);
      }

      public ActionResult cambiocodigo(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.token = Session["token"].ToString();
         varmovimiento.codBancoAnterior = Banco;
         varmovimiento.codCuentaAnterior = cuenta;
         varmovimiento.codDoctoBancoAnterior = doctobanco;
         varmovimiento.codDocumentoAnterior = (documento).Replace("*", " ");
         varmovimiento.codDocumento = (documento).Replace("*", " ");
         ViewBag.documento = (documento).Replace("*", " ");
         return View(varmovimiento);
      }

      public async Task<ActionResult> guardarCambioCodigo(eMovimientoPagoCC varmovimiento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         bool status = false;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());

            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            varmovimiento.codBanco = varmovimiento.codBancoAnterior;
            varmovimiento.codCuenta = varmovimiento.codCuentaAnterior;
            varmovimiento.codDoctoBanco = varmovimiento.codDoctoBancoAnterior;
            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/cambiocodigo", MovimientoEfectivoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);         
         }
         catch
         {
            salida.retorno = -9999;
            salida.mensaje = "Ha ocurrido un error en la comunicación con la API";
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> getBancosxEmpresa()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eBanco> listBancos = new List<eBanco>();
         DataTable dtBancos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eBanco parBanco = new eBanco();
            parBanco.codEmpresa = Session["empresa"].ToString();
            string BancoJSON = JsonConvert.SerializeObject(parBanco);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getBancosXEmpresa", BancoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtBancos.Rows)
            {
               eBanco Banco = new eBanco();
               Banco.codBanco = row["cod_Banco"].ToString();
               Banco.descBanco = row["descripcion"].ToString();
               listBancos.Add(Banco);
            }

         }
         catch
         {
            return null;
         }
         return Json(listBancos, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> getCuentasBancos(string codBanco)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eCuentaBanco> listCuentaBancos = new List<eCuentaBanco>();
         DataTable dtBancos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eCuentaBanco parBanco = new eCuentaBanco();
            parBanco.codEmpresa = Session["empresa"].ToString();
            parBanco.codSucursal = Session["sucursal"].ToString();
            parBanco.codBanco = codBanco;
            string BancoJSON = JsonConvert.SerializeObject(parBanco);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getCuentasxBanco", BancoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtBancos.Rows)
            {
               eCuentaBanco cuentaBanco = new eCuentaBanco();
               cuentaBanco.codCuentaBanco = row["cod_cuenta"].ToString();
               cuentaBanco.descCuentaBanco = row["descripcion"].ToString();
               listCuentaBancos.Add(cuentaBanco);
            }

         }
         catch
         {
            return null;
         }
         return Json(listCuentaBancos, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> getDoctoBanco(string codBanco, string codCuenta)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eDoctoBanco> listDoctoBancos = new List<eDoctoBanco>();
         DataTable dtBancos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eDoctoBanco parBanco = new eDoctoBanco();
            parBanco.codEmpresa = Session["empresa"].ToString();
            parBanco.codCuentaBanco = codCuenta;
            parBanco.codBanco = codBanco;
            string BancoJSON = JsonConvert.SerializeObject(parBanco);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getdoctoBanco", BancoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtBancos.Rows)
            {
               eDoctoBanco cuentaBanco = new eDoctoBanco();
               cuentaBanco.codDoctoBanco = row["cod_docto_banco"].ToString();
               cuentaBanco.descDoctoBanco = row["descripcion"].ToString();
               listDoctoBancos.Add(cuentaBanco);
            }

         }
         catch
         {
            return null;
         }
         return Json(listDoctoBancos, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> VerInformeMovCCXFecha(eMovimientoPagoCC movimientoPagoCC)
        {
          try
            {
                DataTable dtInformeCC = new DataTable();
                dtInformeCC = await getInformeMovCCXFecha(movimientoPagoCC);
                DataTable dtInformeGrafico = new DataTable();
                dtInformeGrafico = await getInformeMovCCXFechaGrafico(movimientoPagoCC);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Clientes_Credito-";               

                if (movimientoPagoCC.informeResumido == true)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteCreditoXEstacionXfechaResumido.rpt"));
                    nombreReporte = nombreReporte+"Resumen-";
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteCreditoXEstacionXfecha.rpt"));
                }
                //verifica si se ha solicitado mostrar solo registros pendientes de conciliar 
                if (movimientoPagoCC.pendienteConciliar == true)
                {
                    rd.DataDefinition.RecordSelectionFormula = "{info_deptoclienteconcixes.conciliado}='N'";
                    nombreReporte = nombreReporte+"Pendiente_Conciliar-";
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoPagoCC.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoPagoCC.dia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInformeCC);
                rd.Subreports["graficoMovCcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (movimientoPagoCC.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
            }
            catch (Exception ex)
            {
                return View("errorInformeMovimientoCC");
            }
        }

      public async Task<DataTable> getInformeMovCCXFecha(eMovimientoPagoCC movimientoPagoCC)
      {
        DataTable dtMovimientoCC = new DataTable();
        try
        {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoCCJSON = JsonConvert.SerializeObject(movimientoPagoCC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoCC/infoMovClienteXFecha", movimientoCCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoCC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
        }
        catch
        {
            return null;
        }
        return dtMovimientoCC;
      }

      public async Task<DataTable> getInformeMovCCXFechaGrafico(eMovimientoPagoCC movimientoPagoCC)
      {
        DataTable dtMovimientoCC = new DataTable();
        try
        {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoCCJSON = JsonConvert.SerializeObject(movimientoPagoCC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoCC/infoClienteefectivoVsConciliadoGrafico", movimientoCCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoCC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
        }
        catch
        {
            return null;
        }
        return dtMovimientoCC;
      }


      public async Task<ActionResult> VerInformeMovCCXFechaXEstacion(eMovimientoPagoCC movimientoPagoCC)
      {
          try
          {
            DataTable dtInformeCC = new DataTable();
            dtInformeCC = await getInformeMovCCXFecha(movimientoPagoCC);
            DataTable dtInformeGrafico = new DataTable();
            dtInformeGrafico = await getInformeMovCCXFechaGrafico(movimientoPagoCC);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_Clientes_Credito_Consolidado-";               

            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteCreditoXfechaXEstacionConsolidado.rpt"));
            
            //verifica si se ha solicitado mostrar solo registros pendientes de conciliar 
            if (movimientoPagoCC.pendienteConciliar == true)
            {
                rd.DataDefinition.RecordSelectionFormula = "{info_deptoclienteconcixes.conciliado}='N'";
                nombreReporte = nombreReporte+"Pendiente_Conciliar-";
            }
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoPagoCC.dia.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoPagoCC.dia2.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInformeCC);
            rd.Subreports["graficoMovCcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
            if (movimientoPagoCC.formatoInforme == "EXCEL")
            {
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
            }
            else
            {
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }

          }
          catch (Exception ex)
          {
            return View("errorInformeMovimientoCC");
          }
        }
        public async Task<ActionResult> getEstadoPantalla()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string informeDBRJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/getEstadoPantalla", informeDBRJSON);//Pte capa media
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }

        public async Task<ActionResult> abrirPantallaUV()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string informeDBRJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/abrirPantallaUV", informeDBRJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }

        public async Task<ActionResult> cerrarPantallaUV()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoCC varmovimiento = new eMovimientoPagoCC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string informeDBRJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientopagocc/cerrarPantallaUV", informeDBRJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }
    }
}