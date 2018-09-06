using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Datos;
using Entidades;
using Newtonsoft.Json;
namespace Service.Controllers
{
    public class systemController : ApiController
    {
      [HttpPost]
      public string getFechaSistema([FromBody]string parTokenJSON)
      {
         eToken vartoken = new eToken();
         try
         {
            vartoken = JsonConvert.DeserializeObject<eToken>(parTokenJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dSystem obj = new dSystem();
         return obj.getFechaSistema(vartoken);
      }

      [HttpPost]
      public string getFechaHoraSistema([FromBody]string parTokenJSON)
      {
         eToken vartoken = new eToken();
         try
         {
            vartoken = JsonConvert.DeserializeObject<eToken>(parTokenJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dSystem obj = new dSystem();
         return obj.getFechaHoraSistema(vartoken);
      }

      [HttpPost]
      public string getLogoEmpresa ([FromBody]string parEmpresaJSON)
      {
         eEmpresa varEmpresa = new eEmpresa();
         try
         {
            varEmpresa= JsonConvert.DeserializeObject<eEmpresa>(parEmpresaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dSystem obj = new dSystem();
         return obj.getLogoEmpresa(varEmpresa);
      }

      
   }
}
