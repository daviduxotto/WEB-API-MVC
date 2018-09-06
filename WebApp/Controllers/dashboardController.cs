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
    public class dashboardController : Controller
    {
        // GET: dashboard
        public ActionResult Index()
        {
            return View();
        }

      public ActionResult ventasvsproyeccion()
      {
         return View();
      }

      public async Task<ActionResult> infoVentavsPreyeccion()
      {
                   
            List<eVentasvsProyeccion> ListVentasProyeccion = new List<eVentasvsProyeccion>();
            DataTable dtDashboard = new DataTable();
            try
            {
               HttpClient client = new HttpClient();
               //le doy la direccion del servidor y key de la app cliente
               client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
               eDashboard Dashboard = new eDashboard();
               Dashboard.fecha = Convert.ToDateTime("09/07/2018");
               string DashboardJSON = JsonConvert.SerializeObject(Dashboard);
               HttpResponseMessage response = await client.PostAsJsonAsync<string>("Dashboard/infoVentavsPreyeccion", DashboardJSON);
               response.EnsureSuccessStatusCode();
               //leo lo que me respondio la peticion
               string salidaJSON = await response.Content.ReadAsStringAsync();
               dtDashboard = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
               //convierto la tabla en una lista de empresas
               foreach (DataRow row in dtDashboard.Rows)
               {
                  eVentasvsProyeccion varVentasProyeccion = new eVentasvsProyeccion();
                  varVentasProyeccion.codEmpresa = row["cod_empresa"].ToString();
                  varVentasProyeccion.descEmpresa = row["desc_empresa"].ToString();
                  varVentasProyeccion.codSucursal = row["cod_estacion"].ToString();
                  varVentasProyeccion.descSucursal = row["desc_sucursal"].ToString();
                  varVentasProyeccion.ventaGalones = Convert.ToDouble(row["venta_galones"].ToString());
                  varVentasProyeccion.ventaGalonesProyeccion = Convert.ToDouble(row["venta_galon_proyeccion"].ToString());
                  varVentasProyeccion.ventaGalonesPresupuesto = Convert.ToDouble(row["presupuesto_galones"].ToString());
                  ListVentasProyeccion.Add(varVentasProyeccion);

               }

            }
            catch
            {
               return null;
            }
            return Json(ListVentasProyeccion, JsonRequestBehavior.AllowGet);
         
      }
    }
}