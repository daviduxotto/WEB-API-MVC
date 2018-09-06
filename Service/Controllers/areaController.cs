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
   public class areaController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Areas por Empresa y unidad negocio| ENTIDAD: eArea | ATRIBUTOS: 
      /// </summary>
      [HttpPost]
      public DataTable getAreaxUnidadNegocio([FromBody]  string parAreaJSON)
      {
         eArea varArea = new eArea();
         try
         {
            varArea = JsonConvert.DeserializeObject<eArea>(parAreaJSON);
         }
         catch{}
         dArea obj = new dArea();
         return obj.getAreaxUnidadNegocio(varArea);

      }
   }
}
