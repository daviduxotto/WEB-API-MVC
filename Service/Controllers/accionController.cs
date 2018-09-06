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
    public class accionController : ApiController
    {
      [HttpPost]
      public eParSalida verificarAccesoAccion([FromBody]string parAccesoJSON)
      {
         eAcceso acceso = new eAcceso();
         try
         {
            acceso= JsonConvert.DeserializeObject<eAcceso>(parAccesoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dAcceso obj = new dAcceso();
         return obj.verificarAccesoAccion(acceso);
      }

      [HttpPost]
      public eParSalida verificarAccesoVista([FromBody]string parAccesoJSON)
      {
         eAcceso acceso = new eAcceso();
         try
         {
            acceso = JsonConvert.DeserializeObject<eAcceso>(parAccesoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dAcceso obj = new dAcceso();
         return obj.verificarAccesoVista(acceso);
      }
   }
}
