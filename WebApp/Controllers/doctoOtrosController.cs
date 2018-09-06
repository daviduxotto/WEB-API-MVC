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
    public class doctoOtrosController : Controller
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

      public ActionResult entornoMovOtrosXFechaXEstacion()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();
      }
      public async Task<eEntorno> getDescripcionEntorno()
      {
         eEntorno entornoresult = new eEntorno();
         DataTable dtEntorno = new DataTable();
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
            dtEntorno = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista
            foreach (DataRow row in dtEntorno.Rows)
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


      public async Task<ActionResult> getDoctoOtrosxEstacionxFecha() 
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eDoctoOtros> ListDoctoOtros = new List<eDoctoOtros>();
         DataTable dtdoctoOtros = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eDoctoOtros doctoOtros = new eDoctoOtros();
            doctoOtros.codEmpresa = Session["empresa"].ToString();
            doctoOtros.codSucursal = Session["sucursal"].ToString();
            doctoOtros.fechaTrabajo = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string doctoOtrosJSON = JsonConvert.SerializeObject(doctoOtros);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/getDoctoOtrosxEstacionxFecha", doctoOtrosJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtdoctoOtros = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtdoctoOtros.Rows)
            {
               eDoctoOtros vardoctoOtros = new eDoctoOtros();
               vardoctoOtros.codTipoDocto = row["codTipoDocto"].ToString();
               vardoctoOtros.descTipoDocto = row["descTipoDocto"].ToString();
               vardoctoOtros.codDocto = row["codDocto"].ToString();                  
               vardoctoOtros.fecha = row["fecha"].ToString();
               vardoctoOtros.justificacion = row["justificacion"].ToString();
               vardoctoOtros.monto = Convert.ToDecimal(row["monto"].ToString());
             //  if (row["estado_autorizacion"].ToString() == "A") { vardoctoOtros.autorizacion = true; } else { vardoctoOtros.autorizacion = false; }
               if (row["validado"].ToString() == "S") { vardoctoOtros.validado = true; } else { vardoctoOtros.validado = false; }
                    ListDoctoOtros.Add(vardoctoOtros);
              
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListDoctoOtros }, JsonRequestBehavior.AllowGet);
      }
       
      public async Task<ActionResult> getLiquidacionDoctoOtrosxEstacionxFecha()
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
            eDoctoOtros movimiento = new eDoctoOtros();
            movimiento.codEmpresa = Session["empresa"].ToString();
            movimiento.codSucursal = Session["sucursal"].ToString();
            movimiento.fechaTrabajo = Convert.ToDateTime( Session["diatrabajo"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/getLiquidacionDoctoOtrosxEstacionxFecha", movimientoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtResultado= JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista
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
      public ActionResult validar(string docto,  string tipoDocto)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eDoctoOtros varmovimiento = new eDoctoOtros();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codSucursal = Session["sucursal"].ToString();
         varmovimiento.codDocto = docto;
         varmovimiento.codTipoDocto = tipoDocto;
         ViewBag.dia = Session["diatrabajo"].ToString();

         return View(varmovimiento);
      }

      public async Task<ActionResult> guardarValidar(string id, string docto, string tipoDocto, string fecha)
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
                eDoctoOtros doctoOtros = new eDoctoOtros();
                doctoOtros.codEmpresa = Session["empresa"].ToString();
                doctoOtros.codSucursal = Session["sucursal"].ToString();
                doctoOtros.token = Session["token"].ToString();
                doctoOtros.fechaTrabajo = Convert.ToDateTime(fecha);
                doctoOtros.codDocto = docto;
                doctoOtros.codTipoDocto = tipoDocto;

            string doctoOtrosJSON = JsonConvert.SerializeObject(doctoOtros);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/validarDoctoOtros", doctoOtrosJSON);
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
            return Json(salida, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> getEstadoPantalla()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eDoctoOtros varmovimiento = new eDoctoOtros();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fechaTrabajo = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/getEstadoPantalla", movimientoJSON);
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
                eDoctoOtros varmovimiento = new eDoctoOtros();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fechaTrabajo = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/abrirPantallaUV", movimientoJSON);
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
                eDoctoOtros varmovimiento = new eDoctoOtros();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fechaTrabajo = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/cerrarPantallaUV", movimientoJSON);
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

        public ActionResult cambiocodigo(string docto, string tipoDocto) //Pte evaluar, preguntar Juanjo
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            eDoctoOtros varDoctoOtros = new eDoctoOtros();
            varDoctoOtros.codEmpresa = Session["empresa"].ToString();
            varDoctoOtros.token = Session["token"].ToString();
            varDoctoOtros.codTipoDocto = tipoDocto;
            varDoctoOtros.codDocto = docto;
            ViewBag.numero = docto;
            return View(varDoctoOtros);
        }
        public async Task<ActionResult> guardarCambioCodigo(eDoctoOtros varCajaChica)
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

                varCajaChica.codEmpresa = Session["empresa"].ToString();
                varCajaChica.codSucursal = Session["sucursal"].ToString();
                varCajaChica.token = Session["token"].ToString();

                string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varCajaChica);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/editCodigoDoctoOtros", MovimientoEfectivoJSON);
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
        public async Task<ActionResult> VerInformeMovOtrosXFechaXEstacion(eDoctoOtros movimiento)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeMovOtrosXFechaXEstacion(movimiento);
                
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Movimimiento_Otros_Consolidado_Estaciones-";

                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_doctoOtrosXfechaXEstacion.rpt"));
                
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimiento.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimiento.dia2.ToString("dd/MM/yyyy") + "'";
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

        public async Task<DataTable> getInformeMovOtrosXFechaXEstacion(eDoctoOtros movimiento)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoOtros/infoConsolidadoOtrosXFecha", movimientoJSON);
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

    }
    
}