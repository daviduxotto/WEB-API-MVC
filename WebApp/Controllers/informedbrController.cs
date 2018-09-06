using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
namespace WebApp.Controllers
{
    public class informedbrController : Controller
    {
      //acciones con vistas
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
            List<eInformeDBR> listInforme = new List<eInformeDBR>();
            listInforme = await getInformeDBR(Session["empresa"].ToString(), Session["sucursal"].ToString(), Session["diatrabajo"].ToString());
            eEntorno entorno = new eEntorno();
            entorno = await getDescripcionEntorno();
                
            eInformeDBR informe = new eInformeDBR();
            
            ViewBag.empresa = entorno.empresa + " -- " + entorno.descEmpresa;
            ViewBag.estacion = entorno.sucursal + " -- " + entorno.descSucursal;
            ViewBag.dia = Session["diatrabajo"].ToString();
            List<eControlCalidad> listControl = new List<eControlCalidad>();
            listControl= await getControlCalidad(Session["empresa"].ToString(), Session["sucursal"].ToString(), Session["diatrabajo"].ToString());
            var tuple = new Tuple<List<eInformeDBR>, List<eControlCalidad>>(listInforme, listControl);
            if (TempData["retorno"] != null)
            {
               ViewBag.retorno = TempData["retorno"].ToString();
               ViewBag.mensaje = TempData["mensaje"].ToString();
            }
            return View(tuple);
         }
      }

      public async Task<ActionResult> saveInformeDBR([Bind(Prefix = "Item1")] List<eInformeDBR> listInformes, [Bind(Prefix = "Item2")] List<eControlCalidad> listControl)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eInformeDBR informe = new eInformeDBR();
         informe.codEmpresa = Session["empresa"].ToString();
         informe.codEstacion = Session["sucursal"].ToString();
         informe.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
         informe.token = Session["token"].ToString();
         informe.xmlinformesdbr = libreria.genXMLinformesDBR(listInformes);
         informe.xmlinformectrl = libreria.genXMLinformesCtrol(listControl);

         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string informesDBRJSON = JsonConvert.SerializeObject(informe);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/editInformesDBR", informesDBRJSON);
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
  
            TempData["mensaje"] = salida.mensaje;
            TempData["retorno"] = salida.retorno;     
            return RedirectToAction("edit", "informedbr");
      }

      public async Task<List<eInformeDBR>> getInformeDBR(string empresa, string sucursal, string fecha)
      {
         
         List<eInformeDBR> ListinformeDBR = new List<eInformeDBR>();
         DataTable dtInformeDBR = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEntorno entorno = new eEntorno();
            entorno.empresa = empresa;
            entorno.sucursal = sucursal;
            entorno.dia = Convert.ToDateTime(fecha);
            string entornoJSON = JsonConvert.SerializeObject(entorno);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/getAuditoriainformesdbr", entornoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtInformeDBR = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtInformeDBR.Rows)
            {
               eInformeDBR varinforme = new eInformeDBR();
               varinforme.codEmpresa = row["cod_empresa"].ToString();
               varinforme.descEmpresa = row["desc_empresa"].ToString();
               varinforme.codEstacion = row["cod_estacion"].ToString();
               varinforme.descEstacion = row["desc_estacion"].ToString();
               varinforme.dia = Convert.ToDateTime(row["cod_dia"].ToString());
               varinforme.codGrupo = row["cod_grupo"].ToString();
               varinforme.descGrupo = row["desc_grupo"].ToString();
               varinforme.codInforme = row["cod_informe"].ToString();
               varinforme.descInforme = row["desc_informe"].ToString();
               if (row["obligatorio"].ToString() == "S") { varinforme.obligatorio = true; } else { varinforme.obligatorio = false; }
               if (row["recibido"].ToString() == "S") { varinforme.recibido = true; } else { varinforme.recibido = false; }
               if (row["no_aplica"].ToString() == "S") { varinforme.noAplica = true; } else { varinforme.noAplica = false; }
               if (row["observaciones_uv"].ToString() == "S") { varinforme.observacionesUV = true; } else { varinforme.observacionesUV = false; }
               if(varinforme.observacionesUV==true) { varinforme.observaciones = row["observaciones"].ToString(); }
               ListinformeDBR.Add(varinforme);
               TempData["descempresa"] = varinforme.codEmpresa + " -- " + varinforme.descEmpresa;
               TempData["descestacion"] = varinforme.codEstacion + " -- " + varinforme.descEstacion;
               TempData["dia"] = varinforme.dia;
            }

         }
         catch
         {
            return null;
         }
         return ListinformeDBR;
      }
      public async Task<List<eControlCalidad>> getControlCalidad(string empresa, string sucursal, string fecha)
      {

         List<eControlCalidad> ListControl = new List<eControlCalidad>();
         DataTable dtInformeDBR = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEntorno entorno = new eEntorno();
            entorno.empresa = empresa;
            entorno.sucursal = sucursal;
            entorno.dia = Convert.ToDateTime(fecha);
            string entornoJSON = JsonConvert.SerializeObject(entorno);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/getAuditoriainformesdbrctrlcalidad", entornoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtInformeDBR = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtInformeDBR.Rows)
            {
               eControlCalidad varControl = new eControlCalidad();
               varControl.codEmpresa = row["cod_empresa"].ToString();
               varControl.codEstacion = row["cod_estacion"].ToString();
               varControl.dia = Convert.ToDateTime(row["cod_dia"].ToString());
               varControl.codControlCalidad = row["cod_control_calidad"].ToString();
               varControl.descControlCalidad = row["desc_ctrlcalidad"].ToString();
               if (row["aprobado"].ToString() == "S") { varControl.aprobado = true; } else { varControl.aprobado = false; }
               if (row["observaciones_uv"].ToString() == "S") { varControl.observacionesUV = true; } else { varControl.observacionesUV = false; }
               if (varControl.observacionesUV == true) { varControl.observaciones = row["observaciones"].ToString(); }
               ListControl.Add(varControl);
            }

         }
         catch
         {
            return null;
         }
         return ListControl;
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
      public async Task<ActionResult> getEstadoPantalla()
      {
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eInformeDBR varInformeDBR = new eInformeDBR();
            varInformeDBR.codEmpresa = Session["empresa"].ToString();                
            varInformeDBR.codEstacion = Session["sucursal"].ToString();
            varInformeDBR.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
       
            string informeDBRJSON = JsonConvert.SerializeObject(varInformeDBR);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/getEstadoPantalla", informeDBRJSON);
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
            eInformeDBR varInformeDBR = new eInformeDBR();
            varInformeDBR.codEmpresa = Session["empresa"].ToString();                
            varInformeDBR.codEstacion = Session["sucursal"].ToString();
            varInformeDBR.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
            varInformeDBR.token = Session["token"].ToString();
       
            string informeDBRJSON = JsonConvert.SerializeObject(varInformeDBR);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/abrirPantallaUV", informeDBRJSON);
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
            eInformeDBR varInformeDBR = new eInformeDBR();
            varInformeDBR.codEmpresa = Session["empresa"].ToString();                
            varInformeDBR.codEstacion = Session["sucursal"].ToString();
            varInformeDBR.dia = Convert.ToDateTime( Session["diatrabajo"].ToString());
            varInformeDBR.token = Session["token"].ToString();
       
            string informeDBRJSON = JsonConvert.SerializeObject(varInformeDBR);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("informedbr/cerrarPantallaUV", informeDBRJSON);
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