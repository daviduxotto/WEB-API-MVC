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
    public class entornoController : ApiController
    {
      [HttpPost]
      public eParSalida procesarDia([FromBody] string parEntornoJSON)
      {
         eEntorno varEntorno = new eEntorno();
         try
         {

            varEntorno = JsonConvert.DeserializeObject<eEntorno>(parEntornoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEntorno obj = new dEntorno();
         return obj.procesarDia(varEntorno);
      }

      [HttpPost]
      public DataTable getDescripcionEntorno([FromBody] string parEntornoJSON)
      {
         eEntorno varEntorno = new eEntorno();
         try
         {

            varEntorno = JsonConvert.DeserializeObject<eEntorno>(parEntornoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEntorno obj = new dEntorno();
         return obj.getDescripcionEntorno(varEntorno);
      }

   }
}
