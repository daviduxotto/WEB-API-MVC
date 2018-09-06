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
    public class homeController : Controller
    {
      // GET: home
      public async Task<ActionResult> Index()
      {
         List<eMenu> listMenu = new List<eMenu>();

         if (Session["usuario"] == null || Session["token"] == null || Session["empresa"] == null || Session["sucursal"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {
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
                  eMenu menu = new eMenu();
                  menu.menu=row["cod_menu"].ToString();
                  listMenu.Add(menu);
               }

            }
            catch { }
            return View(listMenu);
         }

      }

     }
}