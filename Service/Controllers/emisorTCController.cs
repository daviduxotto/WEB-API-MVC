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

namespace Service.Areas
{
   /// <summary>
  
   /// </summary>
   public class emisorTCController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de EmisorTCes de una  determinada empresa ENTIDAD: eEmisorTC |  parametros: empresa
      /// </summary>
      [HttpPost]
      public DataTable getEmisorTCxEmpresa([FromBody]  string parEmisorTCJSON)
      {
         eEmisorTC varEmisorTC = new eEmisorTC();
         try
         {
            varEmisorTC = JsonConvert.DeserializeObject<eEmisorTC>(parEmisorTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmisorTC obj = new dEmisorTC();
         return obj.getEmisorTCxEmpresa(varEmisorTC);
      }
   }
}
