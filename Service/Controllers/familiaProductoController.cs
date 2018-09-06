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
    public class familiaProductoController : ApiController
    {
      [HttpPost]
      public eParSalida newFamiliaProducto([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.newFamiliaProducto(FamiliaProducto);
      }

      [HttpPost]
      public eParSalida editFamiliaProducto([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.editFamiliaProducto(FamiliaProducto);
      }

      [HttpPost]
      public eParSalida delFamiliaProducto([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.delFamiliaProducto(FamiliaProducto);
      }

      [HttpPost]
      public DataTable getOneFamiliaProducto([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.getOneFamiliaProducto(FamiliaProducto);
      }

      [HttpPost]
      public DataTable findFamiliaProductoxCodigo([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.findFamiliaProductoxCodigo(FamiliaProducto);
      }

      [HttpPost]
      public DataTable getFamiliaProductoxClaseProducto([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.getFamiliaProductoxClaseProducto(FamiliaProducto);
      }

      [HttpPost]
      public DataTable findFamiliaProductoxDesc([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.findFamiliaProductoxDesc(FamiliaProducto);
      }

      [HttpPost]
      public DataTable infoFamiliaProductoxEmpresa([FromBody] string parFamiliaProductoJSON)
      {
         eFamiliaProducto FamiliaProducto = new eFamiliaProducto();
         try
         {
            FamiliaProducto = JsonConvert.DeserializeObject<eFamiliaProducto>(parFamiliaProductoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFamiliaProducto obj = new dFamiliaProducto();
         return obj.infoFamiliaProductoxEmpresa(FamiliaProducto);
      }

   }
}
