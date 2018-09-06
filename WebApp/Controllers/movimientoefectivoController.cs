using System;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using System.Configuration;
using System.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace WebApp.Controllers
{
    public class movimientoefectivoController : Controller
    {

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

      public async Task<ActionResult> getMovimientoEfectivo() 
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eMovimientoEfectivoDet> ListMovimientoEfectivo = new List<eMovimientoEfectivoDet>();
         DataTable dtMovimientoEfectivo = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoEfectivo MovimientoEfectivo = new eMovimientoEfectivo();
            MovimientoEfectivo.codEmpresa = Session["empresa"].ToString();
            MovimientoEfectivo.codSucursal = Session["sucursal"].ToString();
            MovimientoEfectivo.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(MovimientoEfectivo);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/getMovEfectivoXFecha", MovimientoEfectivoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMovimientoEfectivo = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtMovimientoEfectivo.Rows)
            {
               eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
               varmovimiento.codEmpresa = row["cod_empresa"].ToString();
               varmovimiento.descEmpresa = row["desc_empresa"].ToString();
               varmovimiento.codSucursal = row["cod_estacion"].ToString();
               varmovimiento.descSucursal = row["desc_sucursal"].ToString();
               varmovimiento.codBanco = row["cod_banco"].ToString();
               varmovimiento.descBanco= row["desc_banco"].ToString();
               varmovimiento.codCuenta = row["cod_cuenta"].ToString();
               varmovimiento.descCuenta = row["desc_cuenta"].ToString();
               varmovimiento.codDoctoBanco = row["cod_docto_banco"].ToString();
               varmovimiento.descDoctoBanco = row["desc_tipodocto"].ToString();
               varmovimiento.codDocumento= row["cod_documento"].ToString();
               varmovimiento.codUnidadBolsa= row["cod_unidad_bolsa"].ToString();
               varmovimiento.fecha = Convert.ToDateTime(row["fecha"].ToString());
               varmovimiento.monto = Convert.ToDouble(row["monto_documento"].ToString());
               if (row["conciliado"].ToString() == "S") { varmovimiento.conciliado = true; } else { varmovimiento.conciliado= false; }
               if (row["reversado"].ToString() == "S") { varmovimiento.revertido = true; } else { varmovimiento.revertido = false; }
               //   varmovimiento.conciliado = libreria.chartoBool(row[""].ToString);
               ListMovimientoEfectivo.Add(varmovimiento);
               TempData["descempresa"] = varmovimiento.codEmpresa + " -- " + varmovimiento.descEmpresa;
               TempData["descestacion"] = varmovimiento.codSucursal + " -- " + varmovimiento.descSucursal;
               TempData["dia"] = varmovimiento.fecha;
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListMovimientoEfectivo }, JsonRequestBehavior.AllowGet);
      }

      public ActionResult entornoMovEfectivoXFecha()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }
      public ActionResult entornoMovEfectivoXFechaXEstacion()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }
      public ActionResult entornoVentaDiariaVSConciliadoXFecha()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }

      public ActionResult entornoVentaDiariaVSConciliadoXFechaxEstacion()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }
      public ActionResult entornoFlujoEfectivoxMes()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }
        public ActionResult entornoFlujoEfectivoxMensual()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }

        public ActionResult cambiomonto(string id, string Banco, string cuenta, string doctobanco, string documento, string monto)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento =  (documento).Replace("*", " ");
         varmovimiento.monto = Convert.ToDouble( monto);
         return View(varmovimiento);
      }

      public ActionResult conciliar(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento =  (documento).Replace("*", " ");
         ViewBag.dia = Session["diatrabajo"].ToString();

         return View(varmovimiento);
      }

      public ActionResult reversar(string id, string Banco, string cuenta, string doctobanco, string documento)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codBanco = Banco;
         varmovimiento.codCuenta = cuenta;
         varmovimiento.codDoctoBanco = doctobanco;
         varmovimiento.codDocumento =  (documento).Replace("*", " ");

         return View(varmovimiento);
      }

      public async Task<ActionResult> guardarCambioMonto(eMovimientoEfectivoDet varmovimiento)
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
       
            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/cambiomonto", MovimientoEfectivoJSON);
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
      public async Task<ActionResult> guardarConciliar(eMovimientoEfectivoDet varmovimiento)
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

            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/conciliar", MovimientoEfectivoJSON);
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
      public async Task<ActionResult> guardarCambioCodigo(eMovimientoEfectivoDet varmovimiento)
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

            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/cambiocodigo", MovimientoEfectivoJSON);
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
      public async Task<ActionResult> guardarNuevo(eMovimientoEfectivoDet varmovimiento)
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
            varmovimiento.fecha = Convert.ToDateTime( Session["diatrabajo"].ToString());
            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/nuevo", MovimientoEfectivoJSON);
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
      public async Task<ActionResult> eliminarDocumento(eMovimientoEfectivoDet varmovimiento)
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
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/del", MovimientoEfectivoJSON);
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
         eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.token = Session["token"].ToString();
         varmovimiento.codBancoAnterior = Banco;
         varmovimiento.codCuentaAnterior = cuenta;
         varmovimiento.codDoctoBancoAnterior = doctobanco;
         varmovimiento.codDocumentoAnterior =  (documento).Replace("*", " ");
         varmovimiento.codDocumento =  (documento).Replace("*", " ");
         ViewBag.documento =  (documento).Replace("*", " ");
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
            eMovimientoEfectivoDet varmovimiento = new eMovimientoEfectivoDet();
            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            varmovimiento.codBanco = Banco;
            varmovimiento.codCuenta = cuenta;
            varmovimiento.codDoctoBanco = doctobanco;
            varmovimiento.codDocumento =  (documento).Replace("*", " ");
            ViewBag.documento =  (documento).Replace("*", " ");
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
            parBanco.codSucursal= Session["sucursal"].ToString();
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

     public async Task<ActionResult> getDoctoBancoNegativo(string codBanco, string codCuenta)
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
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getDoctoBancoNegativos", BancoJSON);
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
      public async Task<ActionResult> getLiquidacionVtaDiariaxEmpreEfectivo()
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
            movimiento.fecha = Convert.ToDateTime( Session["diatrabajo"].ToString());
            movimiento.tipoPago = "N";
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/getLiquidacionVtaDiariaxEmpre", movimientoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtResultado= JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Bancos
            foreach (DataRow row in dtResultado.Rows)
            {
              
               liquidacion.montoTotalReportado = row["monto_total_reportado"].ToString();
               liquidacion.montoTotalDocumentos= row["monto_total_doctos"].ToString();
               liquidacion.diferenciaMontoDocto = row["dif_montovrsdoctos"].ToString();
               liquidacion.montoTotalConciliado = row["monto_total_doctos_concil"].ToString();
               liquidacion.diferenciaMontoConciliado= row["dif_montovrsdoctosconcil"].ToString();

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
               entornoresult.empresa= row["cod_empresa"].ToString();
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

      public async Task<ActionResult> VerInformeMovEfectivoXFecha(eMovimientoEfectivo movimiento)
      {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeMovEfectivoXFecha(movimiento);

                DataTable dtInformeGrafico = new DataTable();
                dtInformeGrafico = await infoVentaDiariaVSConciliadoXFechaGrafico(movimiento);

                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Movimimiento_Efectivo-";
                
                if (movimiento.informeResumido == true)
                {
                    if (movimiento.pendienteConciliar == true)
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovBancoXEstacionXfechaResumidoPteConciliar.rpt"));
                        nombreReporte = nombreReporte + "Pendiente_Conciilar_Resumen-";
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovBancoXEstacionXfechaResumido.rpt"));
                        nombreReporte = nombreReporte + "Resumen-";
                    }                  
                    
                }
                else
                {
                    //verifica si se ha solicitado mostrar solo pendiente de conciliar
                    if (movimiento.pendienteConciliar == true)
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovBancoXEstacionXfechaPteConciliar.rpt"));
                        //rd.DataDefinition.RecordSelectionFormula = "{info_deptoconcixesxfecha.conciliado}='N'";
                        nombreReporte = nombreReporte + "Pendiente_Conciliar-";
                    }
                    else
                    {
                        rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovBancoXEstacionXfecha.rpt"));
                    }
                    
                }
                
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimiento.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimiento.dia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);
                rd.Subreports["graficoMovEfectivoConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
                /*  if (movimiento.codEmpresa != "00000")
                  {
                      rd.ReportDefinition.Areas["GroupHeaderArea1"].Sections["GroupHeaderSection1"].SectionFormat.EnableSuppress = true;
                      rd.ReportDefinition.Areas["GroupFooterArea1"].Sections["GroupFooterSection1"].SectionFormat.EnableSuppress = true;
                  }*/
                //rd.ReportDefinition.Areas["GroupHeaderArea3"].Sections["GroupHeaderSection3"].SectionFormat.EnableSuppress=true;
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (movimiento.formatoInforme == "EXCEL")
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
                return View("errorInformeMovimientoEfectivo");
            }
        }

      public async Task<DataTable> getInformeMovEfectivoXFecha(eMovimientoEfectivo movimiento)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoMovEfectivoXFecha", movimientoJSON);
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
        public async Task<DataTable> infoVentaDiariaVSConciliadoXFechaGrafico(eMovimientoEfectivo movimiento)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoVentaDiariaVSConciliadoXFechaGrafico", movimientoJSON);
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
      public async Task<ActionResult> VerInformeMovEfectivoXFechaXEstacion(eMovimientoEfectivo movimiento)
      {
        try
        {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformeMovEfectivoXFechaXEstacion(movimiento);

            DataTable dtInformeGrafico = new DataTable();
            dtInformeGrafico = await infoVentaDiariaVSConciliadoXFechaGrafico(movimiento);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_Movimimiento_Efectivo_Consolidado_Estaciones-";
           
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovBancoXfechaXEstacionResumido.rpt"));
            
            //verifica si se ha solicitado mostrar solo pendiente de conciliar
            if (movimiento.pendienteConciliar == true)
            {
                rd.DataDefinition.RecordSelectionFormula = "{info_deptoconcixesxfecha.conciliado}='N'";
                nombreReporte = nombreReporte+"Pendiente_Conciliar-";
            }
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimiento.dia.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimiento.dia2.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);
            rd.Subreports["graficoMovEfectivoConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
            if (movimiento.formatoInforme == "EXCEL")
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
            return View("errorInformeMovimientoEfectivo");
        }
      }

      public async Task<DataTable> getInformeMovEfectivoXFechaXEstacion(eMovimientoEfectivo movimiento)
      {
        DataTable dtMovimiento = new DataTable();
        try
        {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoMovEfectivoXFecha", movimientoJSON);
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

      public async Task<DataTable> getInformeFlujoEfectivoxMes(eMovimientoEfectivo movimiento)
      {
         DataTable dtMovimiento = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoflujoefectivoxmes", movimientoJSON);
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

      public async Task<DataTable> getInformeFlujoEfectivoxMensual(eMovimientoEfectivo movimiento)
      {
         DataTable dtMovimiento = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoFlujoEfectivoxMes2", movimientoJSON);
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
      public async Task<ActionResult> verInformeFlujoEfectivoxMes(eMovimientoEfectivo movimiento)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformeFlujoEfectivoxMes(movimiento);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_flujo_efectivo_mes-";
            int mes = movimiento.dia.Month;
            int anio = movimiento.dia.Year;
            String tipoInforme = "POR EMPRESA";
            if (movimiento.codEmpresa == "00000") {
                    tipoInforme = "CORPORATIVO";
            }
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_flujo_efectivo_mes.rpt"));
            
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + mes + "'";  
            rd.DataDefinition.FormulaFields["anio"].Text = "'" + anio + "'";  
            rd.DataDefinition.FormulaFields["tipoInforme"].Text = "'" + tipoInforme + "'";
            rd.DataDefinition.FormulaFields["Estacion"].Text = "'" + movimiento.codSucursal + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch (Exception ex)
         {
            return View("errorInformeMovimientoEfectivo");
         }
      }
      public async Task<ActionResult> verInformeFlujoEfectivoxMensual(eMovimientoEfectivo movimiento)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformeFlujoEfectivoxMensual(movimiento);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_flujo_efectivo_mes-";
            int mes = movimiento.dia.Month;
            int anio = movimiento.dia.Year;
            String tipoInforme = "POR EMPRESA";
            if (movimiento.codEmpresa == "00000") {
                    tipoInforme = "CORPORATIVO";
            }
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_flujo_efectivo_mensual.rpt"));
            
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + mes + "'";  
            rd.DataDefinition.FormulaFields["anio"].Text = "'" + anio + "'";  
            rd.DataDefinition.FormulaFields["tipoInforme"].Text = "'" + tipoInforme + "'";
            rd.DataDefinition.FormulaFields["Estacion"].Text = "'" + movimiento.codSucursal + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch (Exception ex)
         {
            return View("errorInformeMovimientoEfectivo");
         }
      }

      public async Task<ActionResult> VerInformeVentaDiariaVSConciliadoXFecha(eMovimientoEfectivo movimiento)
      {
        try
        {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformeVentaDiariaVSConciliadoXFecha(movimiento);
            ReportDocument rd = new ReportDocument();
            String nombreReporte = "Informe_Comparativo_Efectivo_Consolidado_Estaciones-";
            String tipoInforme = "POR EMPRESA";
       
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_VentaDiariaVSConciliadoXFecha.rpt"));

            if (movimiento.codEmpresa == "00000") {
                    tipoInforme = "CORPORATIVO";
            }
            rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimiento.dia.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimiento.dia2.ToString("dd/MM/yyyy") + "'";
            rd.DataDefinition.FormulaFields["tipoInforme"].Text = "'" + tipoInforme + "'";
            rd.DataDefinition.FormulaFields["Estacion"].Text = "'" + movimiento.codSucursal + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);
            
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
            if (movimiento.formatoInforme == "EXCEL")
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
            return View("errorInformeMovimientoEfectivo");
        }
      }

      public async Task<DataTable> getInformeVentaDiariaVSConciliadoXFecha(eMovimientoEfectivo movimiento)
      {
        DataTable dtMovimiento = new DataTable();
        try
        {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoVentaDiariaVSConciliadoXFecha", movimientoJSON);
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


        public async Task<ActionResult> VerInformeVentaDiariaVSConciliadoXFechaxEstacion(eMovimientoEfectivo movimiento)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeVentaDiariaVSConciliadoXFechaxEstacion(movimiento);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Comparativo_Efectivo_Consolidado_Estaciones-";
                String tipoInforme = "POR EMPRESA";

                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_VentaDiariaVSConciliadoXFechaXestacion.rpt"));

                if (movimiento.codEmpresa == "00000")
                {
                    tipoInforme = "CORPORATIVO";
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimiento.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimiento.dia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["tipoInforme"].Text = "'" + tipoInforme + "'";
                rd.DataDefinition.FormulaFields["Estacion"].Text = "'" + movimiento.codSucursal + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);
                rd.Subreports["tablaDatos"].SetDataSource(dtInforme);


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (movimiento.formatoInforme == "EXCEL")
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
                return View("errorInformeMovimientoEfectivo");
            }
        }

        public async Task<DataTable> getInformeVentaDiariaVSConciliadoXFechaxEstacion(eMovimientoEfectivo movimiento)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/infoVentaDiariaVSConciliadoXFechaxEstacion", movimientoJSON);
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
        public async Task<ActionResult> guardarReversar(eMovimientoEfectivoDet varmovimiento)
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
            varmovimiento.codBancoAnterior = varmovimiento.codBanco;
            varmovimiento.codCuentaAnterior = varmovimiento.codCuenta;
            varmovimiento.codDoctoBancoAnterior = varmovimiento.codDoctoBanco;
            varmovimiento.codDocumentoAnterior = varmovimiento.codDocumento;

          

            string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/revertir", MovimientoEfectivoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
           
         }
         catch
         {
                salida.retorno = -9999;
                salida.mensaje = "Ha ocurrido un Error al conectarse con la Api";
         }
         return new JsonResult { Data = new { salida = salida } };
      }
      public async Task<ActionResult> getEstadoPantalla()
      {
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eMovimientoEfectivo varmovimiento = new eMovimientoEfectivo();
            varmovimiento.codEmpresa = Session["empresa"].ToString();                
            varmovimiento.codSucursal = Session["sucursal"].ToString();
            varmovimiento.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
       
            string varmovimientoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/getEstadoPantalla", varmovimientoJSON);
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
            eMovimientoEfectivo varmovimiento = new eMovimientoEfectivo();
            varmovimiento.codEmpresa = Session["empresa"].ToString();                
            varmovimiento.codSucursal = Session["sucursal"].ToString();
            varmovimiento.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
            varmovimiento.token = Session["token"].ToString();
       
            string varmovimientoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/abrirPantallaUV", varmovimientoJSON);
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
            eMovimientoEfectivo varmovimiento = new eMovimientoEfectivo();
            varmovimiento.codEmpresa = Session["empresa"].ToString();                
            varmovimiento.codSucursal = Session["sucursal"].ToString();
            varmovimiento.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
            varmovimiento.token = Session["token"].ToString();
       
            string varmovimientoJSON = JsonConvert.SerializeObject(varmovimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoefectivo/cerrarPantallaUV", varmovimientoJSON);//Pte capa media
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