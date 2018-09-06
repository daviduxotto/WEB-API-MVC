using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class usuarioController : Controller
    {
        // --------------ACTION VIEW------------------
        public ActionResult login()
        {
         Session.Clear();
            Session.Abandon();
            ViewBag.login = TempData["login"]; //saber si el login fue exitoso
            return View();
        }

      public ActionResult entorno()
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


      public async Task<ActionResult> procesarEntorno(eEntorno parEntorno)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {

            Session["empresa"] = parEntorno.empresa;
            Session["sucursal"] = parEntorno.sucursal;
            eEntorno entorno = await getDescripcionEntorno();
            Session["empresaDescripcion"] = entorno.descEmpresa;
            Session["sucursalDescripcion"] = entorno.descSucursal;
            Session["diatrabajo"] = null;
            return RedirectToAction("index", "home");
         }

      }


      public async Task<ActionResult> procesarDia(eEntorno entorno)
      {
         entorno.token = Session["token"].ToString();
         entorno.empresa = Session["empresa"].ToString();
         entorno.sucursal = Session["sucursal"].ToString();
         eParSalida salida = new eParSalida();
         salida = await fnProcesarDia(entorno);
         if (salida.retorno < 1)
         {
            TempData["procesardia"] = "false"; // decimos que el login fracaso
            TempData["mensaje"] = salida.mensaje;
            return RedirectToAction("elegirdia", "usuario");
         }
         else
         {
            Session["diatrabajo"] = entorno.dia;
            return RedirectToAction("index", "home");
         }

      }

      public ActionResult elegirDia()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {           
            ViewBag.procesardia = TempData["procesardia"];
            ViewBag.mensaje = TempData["mensaje"];
            return View();
         }

      }
      // ---------------ACTION FUNCTION -----------------------
      public async Task<ActionResult> getEmpresas()
      {
         List<eEmpresa> listEmpresas = new List<eEmpresa>();
         DataTable dtEmpresas = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eUsuario parUsuario = new eUsuario();
            parUsuario.aplicacion = ConfigurationManager.ConnectionStrings["keyapp"].ToString();
            parUsuario.usuariologin = Session["usuario"].ToString();
            string usuarioJSON = JsonConvert.SerializeObject(parUsuario);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empresa/getEmpresasXUsuario", usuarioJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpresas = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtEmpresas.Rows)
            {
               eEmpresa empresa = new eEmpresa();
               empresa.codigo = row["cod_empresa"].ToString();
               empresa.descripcion = row["razon_social"].ToString();
               listEmpresas.Add(empresa);
            }

         }
         catch
         {
            return null;
         }
         return Json(listEmpresas, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getSucursales(string codEmpresa)
      {
         List<eSucursal> listSucursales = new List<eSucursal>();
         DataTable dtSucursales = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eSucursal sucursal = new eSucursal();
             sucursal.codigoUsuario = Session["usuario"].ToString();
            sucursal.codigoEmpresa = codEmpresa;           
            string sucursalJSON = JsonConvert.SerializeObject(sucursal);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("sucursal/getSucursalXUserXEmpre", sucursalJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtSucursales = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtSucursales.Rows)
            {
               eSucursal varsucursal = new eSucursal();
               varsucursal.codigoSucursal = row["cod_sucursal"].ToString();
               varsucursal.descripcion = row["descripcion"].ToString();
               listSucursales.Add(varsucursal);
            }

         }
         catch
         {
            return null;
         }
         return Json(listSucursales, JsonRequestBehavior.AllowGet);
      }
    
      public async Task<ActionResult> loginVerificar(eUsuario parUsuario)
        {
       
            eParSalida salida = new eParSalida();
            parUsuario.codigo = parUsuario.codigo.ToUpper();
            parUsuario.ipEquipo = Request.ServerVariables["REMOTE_ADDR"];
         parUsuario.ipInternet =Request.ServerVariables["REMOTE_ADDR"];
            parUsuario.equipo = System.Net.Dns.GetHostName();
            salida = await verificarUsuario(parUsuario);
            if (salida.retorno >= 1)
            {
               Session["token"] = salida.token;
               Session["usuario"] = parUsuario.codigo;   
            return RedirectToAction("entorno", "usuario");
            }
            else
            {
            TempData["login"] = "false"; // decimos que el login fracaso
            return RedirectToAction("login", "usuario");
            }       
        }

      // ------------------- ACCES API -------------------------
      public static async Task<eParSalida> verificarUsuario(eUsuario parUsuario)
      {
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            parUsuario.aplicacion = ConfigurationManager.ConnectionStrings["keyapp"].ToString();  
            string usuarioJSON = JsonConvert.SerializeObject(parUsuario);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("usuario/verificarusuario", usuarioJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
       
         }
         catch
         {
            salida.retorno = -9999;
            salida.token = "";
            salida.mensaje = "No hay conexion con el servidor";
         }
         return salida;
      }
      public static async Task<eParSalida> fnProcesarDia(eEntorno entorno)
      {
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string usuarioJSON = JsonConvert.SerializeObject(entorno);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("entorno/procesardia", usuarioJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);

         }
         catch
         {
            salida.retorno = -9999;
            salida.token = "";
            salida.mensaje = "No hay conexion con el servidor";
         }
         return salida;
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

      public async Task<ActionResult> verificarTodoAcceso()
      {
         List<string> listMenu = new List<string>();
         DataTable dtMenu = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eUsuario usuario = new eUsuario();
            usuario.token = Session["token"].ToString();
            string usuarioJSON = JsonConvert.SerializeObject(usuario);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("usuario/verificarTodoAccesoUsuario", usuarioJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtMenu = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtMenu.Rows)
            {

               listMenu.Add(row["cod_menu"].ToString());
            }

         }
         catch
         {
            return null;
         }
         return Json(listMenu, JsonRequestBehavior.AllowGet);
      }
   }
}