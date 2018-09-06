using Datos;
using Entidades;
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
    public class claseProductoController : ApiController
    {
      [HttpPost]
      public eParSalida newclaseProducto([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.newClaseProducto(claseProducto);
      }

      [HttpPost]
      public eParSalida editclaseProducto([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.editClaseProducto(claseProducto);
      }

      [HttpPost]
      public eParSalida delclaseProducto([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.delClaseProducto(claseProducto);
      }

      [HttpPost]
      public DataTable getOneclaseProducto([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.getOneClaseProducto(claseProducto);
      }

      [HttpPost]
      public DataTable findclaseProductoxCodigo([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.findClaseProductoxCodigo(claseProducto);
      }

      [HttpPost]
      public DataTable findclaseProductoxDesc([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.findClaseProductoxDesc(claseProducto);
      }

      [HttpPost]
      public DataTable infoclaseProductoxEmpresa([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.infoClaseProductoxEmpresa(claseProducto);
      }


      [HttpPost]
      public DataTable getclaseProductoxEmpresa([FromBody] string parclaseProductoJSON)
      {
         eClaseProducto claseProducto = new eClaseProducto();
         try
         {
            claseProducto = JsonConvert.DeserializeObject<eClaseProducto>(parclaseProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dClaseProducto obj = new dClaseProducto();
         return obj.getClaseProductoxEmpresa(claseProducto);
      }
      
   }
}
