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
   public class municipioController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de municipios por | ENTIDAD: eMunicipio | ATRIBUTOS: codPais
      /// </summary>
      [HttpPost]
      public DataTable getMunicipioxDepartamento([FromBody]  string parMunicipioJSON)
      {
         eMunicipio varMunicipio = new eMunicipio();
         try
         {
            varMunicipio = JsonConvert.DeserializeObject<eMunicipio>(parMunicipioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMunicipio obj = new dMunicipio();
         return obj.getMunicipioxDepartamento(varMunicipio);

      }
   }
}
