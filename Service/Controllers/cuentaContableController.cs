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
    public class cuentaContableController : ApiController
    {
      [HttpPost]
      public DataTable getCtaCtableDeta([FromBody] string parCuentaContableJSON)
      {
         eCuentaContable cuentacontable = new eCuentaContable();
         try
         {
            cuentacontable = JsonConvert.DeserializeObject<eCuentaContable>(parCuentaContableJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCuentaContable obj = new dCuentaContable();
         return obj.getCtaCtableDeta(cuentacontable);
      }

      [HttpPost]
      public DataTable getCtaCtableDetaDebe([FromBody] string parCuentaContableJSON)
      {
         eCuentaContable cuentacontable = new eCuentaContable();
         try
         {
            cuentacontable = JsonConvert.DeserializeObject<eCuentaContable>(parCuentaContableJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCuentaContable obj = new dCuentaContable();
         return obj.getCtaCtableDetaDebe(cuentacontable);
      }

      [HttpPost]
      public DataTable getCtaCtableDetaHaber([FromBody] string parCuentaContableJSON)
      {
         eCuentaContable cuentacontable = new eCuentaContable();
         try
         {
            cuentacontable = JsonConvert.DeserializeObject<eCuentaContable>(parCuentaContableJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCuentaContable obj = new dCuentaContable();
         return obj.getCtaCtableDetaHaber(cuentacontable);
      }
   }
}
