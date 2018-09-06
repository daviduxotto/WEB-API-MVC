using Datos;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class productoController : ApiController
    {
      [HttpPost]
      public eParSalida newProducto([FromBody] string parProductoJSON)
      {
         eProducto Producto = new eProducto();
         try
         {
            Producto = JsonConvert.DeserializeObject<eProducto>(parProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dProducto obj = new dProducto();
         return obj.newProducto(Producto);
      }
   }
}
