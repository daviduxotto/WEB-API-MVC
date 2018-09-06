using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;
using Entidades;
using Newtonsoft.Json;
namespace Service.Controllers
{
    public class sucursalController : ApiController
    {
      [HttpPost]
      public DataTable getSucursalXUserXEmpre([FromBody]string parSucursalJSON)
      {
         eSucursal varsucursal = new eSucursal();
         try
         {
            varsucursal = JsonConvert.DeserializeObject<eSucursal>(parSucursalJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dSucursal obj = new dSucursal();
         return obj.getSucursalXUserXEmpre(varsucursal);

      }
   }
}
