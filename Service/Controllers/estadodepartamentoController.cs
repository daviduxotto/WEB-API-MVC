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
   public class estadodepartamentoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de EstadoDepartamentoes| ENTIDAD: eEstadoDepartamento | ATRIBUTOS: codPais
      /// </summary>
      [HttpPost]
      public DataTable getDepartamentoxPais([FromBody]  string parEstadoDepartamentoJSON)
      {
         eEstadoDepartamento varEstadoDepartamento = new eEstadoDepartamento();
         try
         {
            varEstadoDepartamento = JsonConvert.DeserializeObject<eEstadoDepartamento>(parEstadoDepartamentoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEstadoDepartamento obj = new dEstadoDepartamento();
         return obj.getDepartamentoxPais (varEstadoDepartamento);

      }
   }
}
