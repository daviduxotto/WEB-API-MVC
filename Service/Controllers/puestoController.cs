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
   /// <summary>
   /// </summary>
   public class puestoController : ApiController
    {
      /// <summary>
      /// obtiene los puestos por empresa 
      /// </summary>
      [HttpPost]
      public DataTable getPuestoxEmpresa([FromBody]  string parPuestoJSON)
      {
         ePuesto varPuesto = new ePuesto();
         try
         {
            varPuesto = JsonConvert.DeserializeObject<ePuesto>(parPuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPuesto obj = new dPuesto();
         return obj.getPuestoxEmpresa(varPuesto);

      }
   }
}
