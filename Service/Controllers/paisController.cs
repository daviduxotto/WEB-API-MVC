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
   public class paisController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de paises| ENTIDAD: ePais | ATRIBUTOS: --
      /// </summary>
     [HttpPost]
      public DataTable getPaises([FromBody]  string parPaisJSON)
      {
         ePais varPais = new ePais();
         try
         {
            varPais = JsonConvert.DeserializeObject<ePais>(parPaisJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPais obj = new dPais();
         return obj.getPaises(varPais);

      }
   }
}
