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
    public class unidadMedidaProductoController : ApiController
    {
      [HttpPost]
      public eParSalida newUnidadMedidaProducto([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.newUnidadMedidaProducto(UnidadMedidaProducto);
      }

      [HttpPost]
      public eParSalida editUnidadMedidaProducto([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.editUnidadMedidaProducto(UnidadMedidaProducto);
      }

      [HttpPost]
      public eParSalida delUnidadMedidaProducto([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.delUnidadMedidaProducto(UnidadMedidaProducto);
      }

      [HttpPost]
      public DataTable getOneUnidadMedidaProducto([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.getOneUnidadMedidaProducto(UnidadMedidaProducto);
      }
      [HttpPost]
      public DataTable getUnidadMedidaProductoxEmpresa([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.getUnidadMedidaProductoxEmpresa(UnidadMedidaProducto);
      }

      [HttpPost]
      public DataTable findUnidadMedidaProductoxCodigo([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.findUnidadMedidaProductoxCodigo(UnidadMedidaProducto);
      }

      [HttpPost]
      public DataTable findUnidadMedidaProductoxDesc([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.findUnidadMedidaProductoxDesc(UnidadMedidaProducto);
      }

      [HttpPost]
      public DataTable infoUnidadMedidaProductoxEmpresa([FromBody] string parUnidadMedidaProductoJSON)
      {
         eUnidadMedidaProducto UnidadMedidaProducto = new eUnidadMedidaProducto();
         try
         {
            UnidadMedidaProducto = JsonConvert.DeserializeObject<eUnidadMedidaProducto>(parUnidadMedidaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dUnidadMedidaProducto obj = new dUnidadMedidaProducto();
         return obj.infoUnidadMedidaProductoxEmpresa(UnidadMedidaProducto);
      }
   }
}
