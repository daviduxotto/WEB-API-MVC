using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;
using System.Data;

namespace Service.Controllers
{
    public class bodegaController : ApiController
    {
      [HttpPost]
      public eParSalida newBodega([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.newBodega(bodega);
      }

      [HttpPost]
      public eParSalida editBodega([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.editBodega(bodega);
      }

      [HttpPost]
      public eParSalida delBodega([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.delBodega(bodega);
      }

      [HttpPost]
      public DataTable getOneBodega([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.getOneBodega(bodega);
      }

      [HttpPost]
      public DataTable findBodegaxCodigo([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.findBodegaxCodigo(bodega);
      }

      [HttpPost]
      public DataTable findBodegaxDesc([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.findBodegaxDesc(bodega);
      }

      [HttpPost]
      public DataTable infoBodegaxEmpresa([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.infoBodegaxEmpresa(bodega);
      }

      [HttpPost]
      public DataTable getBodegaxEmpresa([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.getBodegaxEmpre(bodega);
      }

      [HttpPost]
      public DataTable getBodegaxEmpresaxSucursal([FromBody] string parBodegaJSON)
      {
         eBodega bodega = new eBodega();
         try
         {
            bodega = JsonConvert.DeserializeObject<eBodega>(parBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBodega obj = new dBodega();
         return obj.getBodegaxEmprexSucursal(bodega);
      }
   }
}
