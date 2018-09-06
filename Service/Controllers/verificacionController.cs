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
    /// Entidad Verificacion - para obtener informes finales del modulo de la unidad de verificacion
    /// </summary>
    public class verificacionController : ApiController
    {
      /// <summary>
      /// obtiene el informe general del trabajo verificado del modulo de UV| ENTIDAD: eVerificacion | ATRIBUTOS: codEmpresa  codEstacion coddia coddia2
      /// </summary>
      [HttpPost]
      public DataTable getInformeVefificadoUV([FromBody]  string parVerificacionJSON)
      {
         eVerificacion varVerificacion = new eVerificacion();
         try
         {
            varVerificacion = JsonConvert.DeserializeObject<eVerificacion>(parVerificacionJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dVerificacion obj = new dVerificacion();
         return obj.getInformeVefificadoUV(varVerificacion);

      }
      /// <summary>
      /// obtiene el informe general del trabajo verificado del modulo de UV Horizontal| ENTIDAD: eVerificacion | ATRIBUTOS: codEmpresa  codEstacion coddia coddia2
      /// </summary>
      [HttpPost]
      public DataTable getInformeVefificadoUVHorizontal([FromBody]  string parVerificacionJSON)
      {
         eVerificacion varVerificacion = new eVerificacion();
         try
         {
            varVerificacion = JsonConvert.DeserializeObject<eVerificacion>(parVerificacionJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dVerificacion obj = new dVerificacion();
         return obj.getInformeVefificadoUVHorizontal(varVerificacion);

      }
   }
}
