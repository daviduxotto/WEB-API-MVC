using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class cuponController : Controller
    {
        //acciones con vistas
        public ActionResult Index()
        {
            return View();
        }

      public async Task<ActionResult> verCupones()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
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

      //acciones sin vistas 
      public async Task<ActionResult> getCuponesxEstacionxFecha()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eCupon> ListCupones= new List<eCupon>();
         DataTable dtCupones= new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eCupon Cupon = new eCupon();
            Cupon.codEmpresa = Session["empresa"].ToString();
            Cupon.codSucursal = Session["sucursal"].ToString();
            Cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string CuponJSON = JsonConvert.SerializeObject(Cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Cupon/getcuponesxestacionxfecha", CuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtCupones.Rows)
            {
               eCupon varcupon = new eCupon();
               varcupon.codEmpresa = row["cod_empresa"].ToString();
               varcupon.descEmpresa = row["desc_empresa"].ToString();
               varcupon.codSucursal = row["cod_estacion"].ToString();
               varcupon.descSucursal = row["desc_estacion"].ToString();
               varcupon.dia = Convert.ToDateTime( row["cod_dia"].ToString());
               varcupon.codEmisor = row["cod_emisor_cupon"].ToString();
               varcupon.descEmisor = row["desc_emisor_cupon"].ToString();
               varcupon.codDenominacion = row["cod_denominacion_cupon"].ToString();
               varcupon.descDenominacion = row["desc_denominacion_cupon"].ToString();
               varcupon.numero = Convert.ToInt32( row["no_cupon"].ToString());
               varcupon.valorQuetzales =  Convert.ToDecimal( row["valor_quetzales"].ToString());              
               if (row["validado_uv"].ToString() == "S") { varcupon.validado = true; } else { varcupon.validado = false; }
               ListCupones.Add(varcupon);      
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListCupones }, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getLiquidacionCupones()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eLiquidacionCupones liquidacion = new eLiquidacionCupones();
         DataTable dtCupones = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eCupon Cupon = new eCupon();
            Cupon.codEmpresa = Session["empresa"].ToString();
            Cupon.codSucursal = Session["sucursal"].ToString();
            Cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string CuponJSON = JsonConvert.SerializeObject(Cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Cupon/getLiquidacionCupones", CuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtCupones.Rows)
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
      public async Task<ActionResult> validarcupon(string id, string emisor, string denominacion, string numero)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eCupon varcupon = new eCupon();
         varcupon.codEmpresa = Session["empresa"].ToString();
         varcupon.codSucursal = Session["sucursal"].ToString();
         varcupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
         varcupon.codEmisor = emisor;
         varcupon.codDenominacion = denominacion;
         varcupon.numero = Convert.ToInt32(numero);
         varcupon.token = Session["token"].ToString();
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string cuponJSON = JsonConvert.SerializeObject(varcupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/validarcupon", cuponJSON);
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
         return Json(salida, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> getEmisorCupones()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eCupon> ListCupones = new List<eCupon>();
         DataTable dtCupones = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eCupon Cupon = new eCupon();
            Cupon.codEmpresa = Session["empresa"].ToString();
           
            string CuponJSON = JsonConvert.SerializeObject(Cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Cupon/getemisorcupon", CuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtCupones.Rows)
            {
               eCupon varcupon = new eCupon();
             
               varcupon.codEmisor = row["cod_emisor_cupon"].ToString();
               varcupon.descEmisor = row["descripcion"].ToString();
            
               ListCupones.Add(varcupon);
            }

         }
         catch
         {
            return null;
         }
         return Json(ListCupones, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getDenominacionCupones(string emisor)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eCupon> ListCupones = new List<eCupon>();
         DataTable dtCupones = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eCupon Cupon = new eCupon();
            Cupon.codEmpresa = Session["empresa"].ToString();
            Cupon.codEmisor = emisor;
            string CuponJSON = JsonConvert.SerializeObject(Cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Cupon/getdenominacioncupon", CuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtCupones.Rows)
            {
               eCupon varcupon = new eCupon();

               varcupon.codDenominacion = row["cod_denominacion"].ToString();
               varcupon.descDenominacion = row["descripcion"].ToString();

               ListCupones.Add(varcupon);
            }

         }
         catch
         {
            return null;
         }
         return Json(ListCupones, JsonRequestBehavior.AllowGet);
      }





      public ActionResult nuevoCupon()
      {
         return View();
      }

      public ActionResult editarCupon(string id, string emisor, string denominacion, string numero)
      {
         eCupon cupon = new eCupon();
         cupon.codEmisor = emisor;
         cupon.codDenominacion = denominacion;
         cupon.numeroAnterior = Convert.ToInt32(numero);
         cupon.codEmpresa = Session["empresa"].ToString();
         cupon.codSucursal = Session["sucursal"].ToString();
         cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
         return View(cupon);
      }

      public ActionResult eliminarCupon(string id, string emisor, string denominacion, string numero)
      {
         eCupon cupon = new eCupon();
         cupon.codEmisor = emisor;
         cupon.codDenominacion = denominacion;
         cupon.codEmpresa = Session["empresa"].ToString();
         cupon.codSucursal = Session["sucursal"].ToString();
         cupon.numero = Convert.ToInt32(numero);
         cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
         return View(cupon);
      }
      public async Task<ActionResult> guardarNuevoCupon(eCupon cupon)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
    
         cupon.codEmpresa = Session["empresa"].ToString();
         cupon.codSucursal = Session["sucursal"].ToString();
         cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());   
         cupon.token = Session["token"].ToString();
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string cuponJSON = JsonConvert.SerializeObject(cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/nuevocupon", cuponJSON);
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

      public async Task<ActionResult> guardarEditarCupon(eCupon cupon)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }

         cupon.codEmpresa = Session["empresa"].ToString();
         cupon.codSucursal = Session["sucursal"].ToString();
         cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
         cupon.token = Session["token"].ToString();
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string cuponJSON = JsonConvert.SerializeObject(cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/editarcupon", cuponJSON);
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

      public async Task<ActionResult> guardarEliminarCupon(eCupon cupon)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }

         cupon.codEmpresa = Session["empresa"].ToString();
         cupon.codSucursal = Session["sucursal"].ToString();
         cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
         cupon.token = Session["token"].ToString();
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string cuponJSON = JsonConvert.SerializeObject(cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/eliminarcupon", cuponJSON);
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

        public async Task<ActionResult> getEstadoPantalla()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eCupon cupon = new eCupon();
                cupon.codEmpresa = Session["empresa"].ToString();
                cupon.codSucursal = Session["sucursal"].ToString();
                cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                cupon.token = Session["token"].ToString();

                string cuponJSON = JsonConvert.SerializeObject(cupon);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/getEstadoPantalla", cuponJSON);//Pte capa media
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
                eCupon cupon = new eCupon();
                cupon.codEmpresa = Session["empresa"].ToString();
                cupon.codSucursal = Session["sucursal"].ToString();
                cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                cupon.token = Session["token"].ToString();

                string cuponJSON = JsonConvert.SerializeObject(cupon);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/abrirPantallaUV", cuponJSON);
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
                eCupon cupon = new eCupon();
                cupon.codEmpresa = Session["empresa"].ToString();
                cupon.codSucursal = Session["sucursal"].ToString();
                cupon.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                cupon.token = Session["token"].ToString();

                string cuponJSON = JsonConvert.SerializeObject(cupon);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("cupon/cerrarPantallaUV", cuponJSON);
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