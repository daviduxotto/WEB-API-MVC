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
   public class facturaCuponController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de facturas cupones de un determinado dia de una estacion | ENTIDAD: eFacturaCupon |  parametros: empresa, sucursal, fecha
      /// </summary>
      [HttpPost]
      public DataTable getFactuCuponesxEstacionXfecha([FromBody]  string parFacturaCuponJSON)
      {
         eFacturaCupon varFacturaCupon = new eFacturaCupon();
         try
         {
            varFacturaCupon = JsonConvert.DeserializeObject<eFacturaCupon>(parFacturaCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFacturaCupon obj = new dFacturaCupon();
         return obj.getFactuCuponesxEstacionxFecha(varFacturaCupon);
      }

      /// <summary>
      /// Obtiene liquidacion facturas cupones de un determinado dia de una estacion | ENTIDAD: eFacturaCupon |  parametros: empresa, sucursal, fecha
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionFacturaCupones([FromBody]  string parFacturaCuponJSON)
      {
         eFacturaCupon varFacturaCupon = new eFacturaCupon();
         try
         {
            varFacturaCupon = JsonConvert.DeserializeObject<eFacturaCupon>(parFacturaCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFacturaCupon obj = new dFacturaCupon();
         return obj.getLiquidacionFacturaCupones(varFacturaCupon);
      }

      /// <summary>
      /// Valida facturas cupones de un determinado dia de una estacion | ENTIDAD: eFacturaCupon |  parametros: empresa, sucursal, fecha
      /// </summary>
      [HttpPost]
      public eParSalida validarFacturaCupon([FromBody]  string parFacturaCuponJSON)
      {
         eFacturaCupon varFacturaCupon = new eFacturaCupon();
         try
         {
            varFacturaCupon = JsonConvert.DeserializeObject<eFacturaCupon>(parFacturaCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dFacturaCupon obj = new dFacturaCupon();
         return obj.validarFacturaCupon(varFacturaCupon);
      }
   }
}
