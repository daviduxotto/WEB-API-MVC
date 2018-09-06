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
    public class facturaCajaChicaController : Controller
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
      /*public ActionResult nuevo()
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {


            return View();
         }
      }*/

      public async Task<ActionResult> getFacturaCajaChicaesxEstacionXfecha() 
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eFacturaCajaChica> ListFacturaCajaChica = new List<eFacturaCajaChica>();
         DataTable dtFacturaCajaChica = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eFacturaCajaChica facturaCajaChica = new eFacturaCajaChica();
            facturaCajaChica.codEmpresa = Session["empresa"].ToString();
            facturaCajaChica.codSucursal = Session["sucursal"].ToString();
            facturaCajaChica.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string facturaCajaChicaJSON = JsonConvert.SerializeObject(facturaCajaChica);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/getFacturaCajaChicaesxEstacionXfecha", facturaCajaChicaJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtFacturaCajaChica = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtFacturaCajaChica.Rows)
            {
               eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
               varFacturaCajaChica.codProveedor = row["cod_proveedor"].ToString();
               varFacturaCajaChica.descProveedor = row["desc_proveedor"].ToString();
               varFacturaCajaChica.codDoctoCxP = row["cod_docto_cxp"].ToString();
               varFacturaCajaChica.descDoctoCxP = row["desc_doctocxp"].ToString();
               varFacturaCajaChica.serie = row["no_serie"].ToString();
               varFacturaCajaChica.numero= row["no_factura"].ToString();                    
               varFacturaCajaChica.fecha = Convert.ToDateTime(row["fecha"].ToString());
               varFacturaCajaChica.justificacion = row["justificacion"].ToString();
               varFacturaCajaChica.montoBien = Convert.ToDecimal(row["monto_bien"].ToString());
               varFacturaCajaChica.montoServicio = Convert.ToDecimal(row["monto_servicio"].ToString());
               varFacturaCajaChica.montoTotal = Convert.ToDecimal(row["monto_total"].ToString());
               if (row["estado_autorizacion"].ToString() == "A") { varFacturaCajaChica.autorizacion = true; } else { varFacturaCajaChica.autorizacion = false; }
               if (row["validado"].ToString() == "S") { varFacturaCajaChica.validado = true; } else { varFacturaCajaChica.validado = false; }               
               ListFacturaCajaChica.Add(varFacturaCajaChica);
              
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListFacturaCajaChica }, JsonRequestBehavior.AllowGet);
      }
       
      public async Task<ActionResult> getLiquidacionCajaChicaesxEstacionXfecha()
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
            eFacturaCajaChica movimiento = new eFacturaCajaChica();
            movimiento.codEmpresa = Session["empresa"].ToString();
            movimiento.codSucursal = Session["sucursal"].ToString();
            movimiento.fecha = Convert.ToDateTime( Session["diatrabajo"].ToString());
            string movimientoJSON = JsonConvert.SerializeObject(movimiento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/getLiquidacionCajaChicaesxEstacionXfecha", movimientoJSON);
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
      public ActionResult validar(string docto, string noSerie, string noFactura, string proveedor)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eFacturaCajaChica varmovimiento = new eFacturaCajaChica();
         varmovimiento.codEmpresa = Session["empresa"].ToString();
         varmovimiento.codSucursal = Session["sucursal"].ToString();
         varmovimiento.codDoctoCxP = docto;
         varmovimiento.serie = noSerie;
         varmovimiento.numero = noFactura;
         varmovimiento.codProveedor = proveedor;
         ViewBag.dia = Session["diatrabajo"].ToString();

         return View(varmovimiento);
      }

      public async Task<ActionResult> guardarValidar(string docto, string noSerie, string noFactura, string proveedor)
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
            eFacturaCajaChica facturaCajaChica = new eFacturaCajaChica();
            facturaCajaChica.codEmpresa = Session["empresa"].ToString();
            facturaCajaChica.codSucursal = Session["sucursal"].ToString();
            facturaCajaChica.token = Session["token"].ToString();
            facturaCajaChica.codDoctoCxP = docto;
            facturaCajaChica.serie = noSerie;
            facturaCajaChica.numero = noFactura;
            facturaCajaChica.codProveedor = proveedor;

            string facturaCajaChicaJSON = JsonConvert.SerializeObject(facturaCajaChica);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/validarFacturaFacturaCajaChica", facturaCajaChicaJSON);
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
                eFacturaCajaChica varmovimiento = new eFacturaCajaChica();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/getEstadoPantalla", movimientoJSON);
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
                eFacturaCajaChica varmovimiento = new eFacturaCajaChica();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/abrirPantallaUV", movimientoJSON);
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
                eFacturaCajaChica varmovimiento = new eFacturaCajaChica();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajaChica/cerrarPantallaUV", movimientoJSON);
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

        public ActionResult cambiocodigo(string docto, string noSerie, string noFactura, string proveedor)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
            varFacturaCajaChica.codEmpresa = Session["empresa"].ToString();
            varFacturaCajaChica.token = Session["token"].ToString();
            varFacturaCajaChica.codProveedor = proveedor;
            varFacturaCajaChica.codDoctoCxP = docto;
            varFacturaCajaChica.codProveedorAnterior = proveedor;
            varFacturaCajaChica.codDoctoCxPAnterior = docto;
            varFacturaCajaChica.serie = noSerie;
            varFacturaCajaChica.numero = noFactura;
            ViewBag.numero = noFactura;
            return View(varFacturaCajaChica);
        }
        public async Task<ActionResult> guardarCambioCodigo(eFacturaCajaChica varCajaChica)
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
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCajachica/editCodigoFacturaCajaChica", MovimientoEfectivoJSON);
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
    }
    
}