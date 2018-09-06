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
    public class bancoController : Controller
    {
        // GET: banco
        public ActionResult Index()
        {
            return View();
        }

      public async Task<ActionResult> getBancos()
      {
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

      public async Task<ActionResult> getCuentaBancos(string codBanco)
      {
         List<eCuentaBanco> listCuentaBancos = new List<eCuentaBanco>();
         DataTable dtBancos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eBanco parBanco = new eBanco();
            parBanco.codEmpresa = Session["empresa"].ToString();
            parBanco.codEmpresa = Session["sucursal"].ToString();
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

      public async Task<ActionResult> getDoctoBancoTC(string codEmisor)
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
            parBanco.codSucursal = Session["sucursal"].ToString();
            parBanco.codEmisorTC = codEmisor;
            string BancoJSON = JsonConvert.SerializeObject(parBanco);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getDoctoBancoTC", BancoJSON);
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
        
   }
}