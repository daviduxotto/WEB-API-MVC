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
   public class tipopresupuestoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de TipoPresupuestos por Empresa | ENTIDAD: eTipoPresupuesto | ATRIBUTOS: 
      /// </summary>
      [HttpPost]
      public DataTable getTipoPresupuesto([FromBody]  string parTipoPresupuestoJSON)
      {
         eTipoPresupuesto varTipoPresupuesto = new eTipoPresupuesto();
         try
         {
            varTipoPresupuesto = JsonConvert.DeserializeObject<eTipoPresupuesto>(parTipoPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoPresupuesto obj = new dTipoPresupuesto();
         return obj.getTipoPresupuesto(varTipoPresupuesto);

      }
   }
}
