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
    public class movimientotcController : Controller
    {

        public async Task<ActionResult> entornoMovTcXFecha()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }

        public async Task<ActionResult> entornoMovTcXFechaXEstacion()
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

      public ActionResult nuevo()
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {


            return View();
         }
      }

      public async Task<ActionResult> getMovimientoTC()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eMovimientoTC> ListMovimientoTC = new List<eMovimientoTC>();
         DataTable dtMovimientoTC = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoTC MovimientoTC = new eMovimientoTC();
            MovimientoTC.codEmpresa = Session["empresa"].ToString();
            MovimientoTC.codSucursal = Session["sucursal"].ToString();
            MovimientoTC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string MovimientoTCJSON = JsonConvert.SerializeObject(MovimientoTC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/getMovTCXFecha", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtMovimientoTC.Rows)
            {
               eMovimientoTC varmovimiento = new eMovimientoTC();
               varmovimiento.codEmpresa = row["cod_empresa"].ToString();
               varmovimiento.descEmpresa = row["desc_empresa"].ToString();
               varmovimiento.codSucursal = row["cod_estacion"].ToString();
               varmovimiento.descSucursal = row["desc_sucursal"].ToString();
               varmovimiento.codEmisor = row["cod_emisor_tarjeta"].ToString();
               varmovimiento.descEmisor = row["desc_emisortc"].ToString();
               varmovimiento.codBanco = row["cod_banco"].ToString();
               varmovimiento.descBanco = row["desc_banco"].ToString();
               varmovimiento.codCuenta = row["cod_cuenta"].ToString();
               varmovimiento.descCuenta = row["desc_cuenta"].ToString();
               varmovimiento.codDoctoBanco = row["cod_docto_banco"].ToString();
               varmovimiento.descDoctoBanco = row["desc_tipodocto"].ToString();
               varmovimiento.codDocumento = row["cod_documento"].ToString();
               varmovimiento.codUnidadBolsa = row["cod_unidad_bolsa"].ToString();
               varmovimiento.fecha = Convert.ToDateTime(row["fecha"].ToString());
               varmovimiento.monto = Convert.ToDouble(row["monto_documento"].ToString());
               if (row["conciliado"].ToString() == "S") { varmovimiento.conciliado = true; } else { varmovimiento.conciliado = false; }
               if (row["reversado"].ToString() == "S") { varmovimiento.revertido = true; } else { varmovimiento.revertido = false; }
               //   varmovimiento.conciliado = libreria.chartoBool(row[""].ToString);
               ListMovimientoTC.Add(varmovimiento);
               TempData["descempresa"] = varmovimiento.codEmpresa + " -- " + varmovimiento.descEmpresa;
               TempData["descestacion"] = varmovimiento.codSucursal + " -- " + varmovimiento.descSucursal;
               TempData["dia"] = varmovimiento.fecha;
            }

         }
         catch
         {
            return null;
         }
         string saaa = (Json(ListMovimientoTC)).ToString();
         return Json(new { data = ListMovimientoTC }, JsonRequestBehavior.AllowGet);
      }



      public ActionResult cambiomonto(string id, string Banco, string cuenta, string doctobanco, string documento, string monto)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoTC varmovimiento = new eMovimientoTC();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento =  (documento).Replace("~", " ");
         varmovimiento.monto = Convert.ToDouble(monto);
         return View(varmovimiento);
      }

      public ActionResult conciliar(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoTC varmovimiento = new eMovimientoTC();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento =  (documento).Replace("~", " ");

         return View(varmovimiento);
      }

      public async Task<ActionResult> guardarCambioMonto(eMovimientoTC varmovimiento)
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

            string MovimientoTCJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/cambiomonto", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            if (salida.retorno >= 1)
            {
               status = true;
            }
         }
         catch
         {
            status = false;
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> guardarConciliar(eMovimientoTC varmovimiento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());

            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();

            string MovimientoTCJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/conciliar", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> guardarCambioCodigo(eMovimientoTC varmovimiento)
      {
         varmovimiento.codBanco = varmovimiento.codBancoAnterior;
         varmovimiento.codCuenta = varmovimiento.codCuentaAnterior;
         varmovimiento.codDoctoBanco = varmovimiento.codDoctoBancoAnterior;
       
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            string MovimientoTCJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/cambiocodigo", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);          
            retorno = salida.retorno;
            mensaje = salida.mensaje;         
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> guardarNuevo(eMovimientoTC varmovimiento)
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
            varmovimiento.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
            string MovimientoTCJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/newMovimientoTC", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            if (salida.retorno >= 1)
            {
               status = true;
            }
         }
         catch
         {
            status = false;
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> eliminarDocumento(eMovimientoTC varmovimiento)
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
            string MovimientoTCJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/delMovimientoTC", MovimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            if (salida.retorno >= 1)
            {
               status = true;
            }
         }
         catch
         {
            status = false;
         }
         return new JsonResult { Data = new { salida = salida } };
      }

      public ActionResult cambiocodigo(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoTC varmovimiento = new eMovimientoTC();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.token = Session["token"].ToString();
         varmovimiento.codBancoAnterior = Banco;
         varmovimiento.codCuentaAnterior = cuenta;
         varmovimiento.codDoctoBancoAnterior = doctobanco;
         varmovimiento.codDocumentoAnterior =  (documento).Replace("~", " ");
         varmovimiento.codDocumento =  (documento).Replace("~", " ");
         ViewBag.documento = documento;
         return View(varmovimiento);
      }
      public ActionResult eliminar(string id, string Banco, string cuenta, string doctobanco, string documento)
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {
            eMovimientoTC varmovimiento = new eMovimientoTC();
            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            varmovimiento.codBanco = Banco;
            varmovimiento.codCuenta = cuenta;
            varmovimiento.codDoctoBanco = doctobanco;
            varmovimiento.codDocumento = documento;
            ViewBag.documento = documento;
            return View(varmovimiento);
         }

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
      public async Task<ActionResult> getLiquidacionVtaDiariaxEmpreTC()
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
            eMovimientoTC movimiento = new eMovimientoTC();
            movimiento.codEmpresa = Session["empresa"].ToString();
            movimiento.codSucursal = Session["sucursal"].ToString();
            movimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
            movimiento.tipoPago = "S";
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/getLiquidacionVtaDiariaxEmpre", movimientoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtResultado = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtResultado.Rows)
            {

               liquidacion.montoTotalReportado = row["monto_total_reportado"].ToString();
               liquidacion.montoTotalDocumentos = row["monto_total_doctos"].ToString();
               liquidacion.diferenciaMontoDocto = row["dif_montovrsdoctos"].ToString();
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

      public async Task<ActionResult> VerInformeMovTCXFecha(eMovimientoTC movimientoTC)
      {
        try
        {
            DataTable dtInformeTC = new DataTable();
            dtInformeTC = await getInformeMovTCXFecha(movimientoTC);
            DataTable dtInformeGrafico = new DataTable();
            dtInformeGrafico = await getInfoVentaDiariaTCvsConciliadoGrafico(movimientoTC);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_Movimimiento_TC-";                

            if (movimientoTC.informeResumido == true)
            {
                if (movimientoTC.pendienteConciliar == true)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovTcXEstacionXfechaResumidoPteConciliar.rpt"));
                    nombreReporte = nombreReporte + "Resumen_Pendiente_Conciliar-";
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovTcXEstacionXfechaResumido.rpt"));
                    nombreReporte = nombreReporte + "Resumen-";
                }
                        
            }
            else
                {
                    if (movimientoTC.pendienteConciliar == true)
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovTcXEstacionXfechaPteConciliar.rpt"));
                        nombreReporte = nombreReporte + "Pendiente_Conciliar-";
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovTcXEstacionXfecha.rpt"));
                    }                    
                }
            //verifica si se ha solicitado mostrar solo las cuentas con movimiento
            if (movimientoTC.pendienteConciliar == true)
            {
                rd.DataDefinition.RecordSelectionFormula = "{info_deptoconcixesxfecha.conciliado}='N'";
                nombreReporte = nombreReporte+"Pendiente_Conciliar-";
            }
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoTC.dia.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoTC.dia2.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInformeTC);
            
            rd.Subreports["graficoMovTcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
            if (movimientoTC.formatoInforme == "EXCEL")
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
            return View("errorInformeMovimientoTC");
        }
      }

      public async Task<DataTable> getInformeMovTCXFecha(eMovimientoTC movimientoTC)
      {
        DataTable dtMovimientoTC = new DataTable();
        try
        {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoTCJSON = JsonConvert.SerializeObject(movimientoTC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/infoMovTCXFecha", movimientoTCJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
        }
        catch
        {
            return null;
        }
        return dtMovimientoTC;
      }
    
      public async Task<ActionResult> VerInformeMovTCXFechaXEstacion(eMovimientoTC movimientoTC)
      {
        try
        {
            DataTable dtInformeTC = new DataTable();
            dtInformeTC = await getInformeMovTCXFecha(movimientoTC);
                
            DataTable dtInformeGrafico = new DataTable();
            dtInformeGrafico = await getInfoVentaDiariaTCvsConciliadoGrafico(movimientoTC);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_Movimimiento_TC_Consolidado-";                

            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovTcXfechaXEstacionConsolidado.rpt"));
            
            //verifica si se ha solicitado mostrar solo las cuentas con movimiento
            if (movimientoTC.pendienteConciliar == true)
            {
                rd.DataDefinition.RecordSelectionFormula = "{info_deptoconcixesxfecha.conciliado}='N'";
                nombreReporte = nombreReporte+"Pendiente_Conciliar-";
            }
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoTC.dia.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoTC.dia2.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInformeTC);
            rd.Subreports["graficoMovTcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
            if (movimientoTC.formatoInforme == "EXCEL")
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
            return View("errorInformeMovimientoTC");
        }
      }

        public async Task<DataTable> getInfoVentaDiariaTCvsConciliadoGrafico(eMovimientoTC movimientoTC)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimientoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/infoVentaDiariaTCvsConciliadoGrafico", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimiento = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtMovimiento;
        }



        public async Task<ActionResult> getEstadoPantalla()
      {
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoTC movimientoTC = new eMovimientoTC();
            movimientoTC.codEmpresa = Session["empresa"].ToString();                
            movimientoTC.codSucursal = Session["sucursal"].ToString();
            movimientoTC.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
       
            string informeDBRJSON = JsonConvert.SerializeObject(movimientoTC);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/getEstadoPantalla", informeDBRJSON);//Pte capa media
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
                eMovimientoTC movimientoTC = new eMovimientoTC();
                movimientoTC.codEmpresa = Session["empresa"].ToString();
                movimientoTC.codSucursal = Session["sucursal"].ToString();
                movimientoTC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                movimientoTC.token = Session["token"].ToString();

                string informeDBRJSON = JsonConvert.SerializeObject(movimientoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/abrirPantallaUV", informeDBRJSON);//Pte capa media
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
                eMovimientoTC movimientoTC = new eMovimientoTC();
                movimientoTC.codEmpresa = Session["empresa"].ToString();
                movimientoTC.codSucursal = Session["sucursal"].ToString();
                movimientoTC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                movimientoTC.token = Session["token"].ToString();

                string informeDBRJSON = JsonConvert.SerializeObject(movimientoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoTC/cerrarPantallaUV", informeDBRJSON);//Pte capa media
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