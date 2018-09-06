using Entidades;
using Datos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
   /// <summary>
   /// controlador para infomres en el dashboard
   /// </summary>
    public class dashboardController : ApiController
    {
      [HttpPost]
      public DataTable infoVentavsPreyeccion([FromBody]  string pardashboardJSON)
      {
         eDashboard vardashboard = new eDashboard();
         try
         {
            vardashboard = JsonConvert.DeserializeObject<eDashboard>(pardashboardJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dDashboard obj = new dDashboard();
         return obj.infoVentavsProyeccion (vardashboard);

      }
   }
}
