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
    public class tipobajaController : Controller
    {
        // GET: tipobaja
        public ActionResult Index()
        {
            return View();
        }
      public ActionResult listar()
      {
         return View();
      }
      public async Task<ActionResult> getAllTipoBaja()
      {
         List<eTipoBaja> listTipoBaja = new List<eTipoBaja>();
         DataTable dtTipoBaja = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eTipoBaja parTipoBaja = new eTipoBaja();
            
            string tipoBajaJSON = JsonConvert.SerializeObject(parTipoBaja);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("tipobaja/getalltipobaja", tipoBajaJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtTipoBaja = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtTipoBaja.Rows)
            {
               eTipoBaja tipobaja = new eTipoBaja();
               tipobaja.codigo = Convert.ToInt32( row["codigo"].ToString());
               tipobaja.descripcion = row["descripcion"].ToString();
               tipobaja.recontratacion = row["recontratacion"].ToString();
               listTipoBaja.Add(tipobaja);
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = listTipoBaja }, JsonRequestBehavior.AllowGet);
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
      public async Task<ActionResult> guardarNuevo(eTipoBaja TipoBaja)
      {

         TipoBaja.token = Session["token"].ToString();
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string TipoBajaJSON = JsonConvert.SerializeObject(TipoBaja);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("TipoBaja/nuevo", TipoBajaJSON);
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
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }

      public ActionResult eliminar(string id, string codigo)
      {
         eTipoBaja tipoBaja = new eTipoBaja();
         tipoBaja.codigo = Convert.ToInt32( codigo);
         return View(tipoBaja);
      }
      public async Task<ActionResult> guardarEliminar(eTipoBaja TipoBaja)
      {

         TipoBaja.token = Session["token"].ToString();
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string TipoBajaJSON = JsonConvert.SerializeObject(TipoBaja);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("TipoBaja/eliminar", TipoBajaJSON);
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
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public ActionResult editar(string id, string codigo, string descripcion, string recontratacio)
      {
         eTipoBaja tipoBaja = new eTipoBaja();
         tipoBaja.codigo = Convert.ToInt32(codigo);
         return View(tipoBaja);
      }
   }
}