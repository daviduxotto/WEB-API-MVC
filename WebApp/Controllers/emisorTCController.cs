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
    public class emisorTCController : Controller
    {
        // GET: emisorTC
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> getEmisorTCxEmpresa()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eEmisorTC> listEmisorTC = new List<eEmisorTC>();
            DataTable dtEmisor = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eEmisorTC parEmisorTC = new eEmisorTC();
                parEmisorTC.codEmpresa = Session["empresa"].ToString();
                string BancoJSON = JsonConvert.SerializeObject(parEmisorTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("emisorTC/getEmisorTCxEmpresa", BancoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtEmisor = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtEmisor.Rows)
                {
                    eEmisorTC emisor = new eEmisorTC();
                    emisor.codEmisorTC = row["cod_emisor_tarjeta"].ToString();
                    emisor.descEmisorTC = row["desc_emisor_tarjeta"].ToString();
                    listEmisorTC.Add(emisor);
                }

            }
            catch
            {
                return null;
            }
            return Json(listEmisorTC, JsonRequestBehavior.AllowGet);
        }
    }
}