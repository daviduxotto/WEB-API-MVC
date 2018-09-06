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
{/// <summary>
/// 
/// </summary>
    public class observacionUVController : ApiController
    {
      /// <summary>
      /// obtiene el las observaciones de las pantallas de uv
      /// </summary>
      [HttpPost]
      public DataTable getObservacionUV([FromBody]  string parObservacionUVJSON)
      {
         eObservacionUV varObservacionUV = new eObservacionUV();
         try
         {
            varObservacionUV = JsonConvert.DeserializeObject<eObservacionUV>(parObservacionUVJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dObservacionUV obj = new dObservacionUV();
         return obj.getObservacionUV(varObservacionUV);
      }

      /// <summary>
      /// obtiene el las observaciones de las pantallas de uv
      /// </summary>
      [HttpPost]
      public DataTable getPantallaUV([FromBody]  string parObservacionUVJSON)
      {
         eObservacionUV varObservacionUV = new eObservacionUV();
         try
         {
            varObservacionUV = JsonConvert.DeserializeObject<eObservacionUV>(parObservacionUVJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dObservacionUV obj = new dObservacionUV();
         return obj.getPantallaUV(varObservacionUV);

      }

      /// <summary>
      /// obtiene el las observaciones de las pantallas de uv
      /// </summary>
      [HttpPost]
      public DataTable getInfoObservacionUV([FromBody]  string parObservacionUVJSON)
      {
         eObservacionUV varObservacionUV = new eObservacionUV();
         try
         {
            varObservacionUV = JsonConvert.DeserializeObject<eObservacionUV>(parObservacionUVJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dObservacionUV obj = new dObservacionUV();
         return obj.getInfoObservacionUV(varObservacionUV);

      }

      /// <summary>
      /// crea nuevo observaciones de las pantallas de uv
      /// </summary>
      [HttpPost]
      public eParSalida newObservacionUV([FromBody]  string parObservacionUVJSON)
      {
         eObservacionUV varObservacionUV = new eObservacionUV();
         try
         {
            varObservacionUV = JsonConvert.DeserializeObject<eObservacionUV>(parObservacionUVJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dObservacionUV obj = new dObservacionUV();
         return obj.newObservacionUV(varObservacionUV);

      }
      /// <summary>
      /// elimina observaciones de las pantallas de uv
      /// </summary>
      [HttpPost]
      public eParSalida delObservacionUV([FromBody]  string parObservacionUVJSON)
      {
         eObservacionUV varObservacionUV = new eObservacionUV();
         try
         {
            varObservacionUV = JsonConvert.DeserializeObject<eObservacionUV>(parObservacionUVJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dObservacionUV obj = new dObservacionUV();
         return obj.delObservacionUV(varObservacionUV);

      }
   }
}
