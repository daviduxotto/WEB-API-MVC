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
    public class pantallaController : Controller
    {
        public async Task<ActionResult> entornoAperturaPantalla()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            if (TempData["retorno"] != null)
            {
                ViewBag.retorno = TempData["retorno"].ToString();
                ViewBag.mensaje = TempData["mensaje"].ToString();
            }
            return View();
        }

        public async Task<ActionResult> guardarAperturaPantalla(ePantalla varpantalla)
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

                varpantalla.token = Session["token"].ToString();
                string pantallaJSON = JsonConvert.SerializeObject(varpantalla);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("pantalla/abrirPantallaCerrada", pantallaJSON);
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
            TempData["mensaje"] = salida.mensaje;
            TempData["retorno"] = salida.retorno;
            return RedirectToAction("entornoAperturaPantalla", "pantalla");
            //return new JsonResult { Data = new { salida = salida } };
        }


    }
}