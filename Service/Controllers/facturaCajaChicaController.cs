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
{  /// <summary>
/// </summary>
    public class facturaCajaChicaController : ApiController
    {
      dFacturaCajaChica obj = new dFacturaCajaChica();
      /// <summary>
      /// Obtiene el listado de facturas de caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha
      /// </summary>
  
      [HttpPost]
      public DataTable getFacturaCajaChicaesxEstacionXfecha([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }

         return obj.getFacturaCajaChicaesxEstacionxFecha(varFacturaCajaChica);
      }
      /// <summary>
      /// Obtiene liquidacion de facturas de caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionCajaChicaesxEstacionXfecha([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }

         return obj.getLiquidacionCajaChicaesxEstacionxFecha(varFacturaCajaChica);
      }

      /// <summary>
      /// valida factura caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha, docto,numero,serie,proveedor
      /// </summary>
      [HttpPost]
      public eParSalida validarFacturaFacturaCajaChica([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.validarFacturaFacturaCajaChica(varFacturaCajaChica);
      }
      /// <summary>
      /// vabre pantalla factura caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha, docto,numero,serie,proveedor
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.abrirPantallaUV(varFacturaCajaChica);
      }
      /// <summary>
      /// get estado pantalla factura caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha, docto,numero,serie,proveedor
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.getEstadoPantalla(varFacturaCajaChica);
      }

      /// <summary>
      /// cierra pantalla factura caja chica | ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha, docto,numero,serie,proveedor
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.cerrarPantallaUV(varFacturaCajaChica);
      }

      /// <summary>
      /// edita codigo factura caja chica| ENTIDAD: eFacturaCajaChica |  parametros: empresa, sucursal, fecha, docto,numero,serie,proveedor
      /// </summary>
      [HttpPost]
      public eParSalida editCodigoFacturaCajaChica([FromBody]  string parFacturaCajaChicaJSON)
      {
         eFacturaCajaChica varFacturaCajaChica = new eFacturaCajaChica();
         try
         {
            varFacturaCajaChica = JsonConvert.DeserializeObject<eFacturaCajaChica>(parFacturaCajaChicaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.editCodigoFacturaCajaChica(varFacturaCajaChica);
      }
   }
}
