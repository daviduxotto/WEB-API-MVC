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
    public class doctoOtrosController : ApiController
    {
      dDoctoOtros obj = new dDoctoOtros();
        /// <summary>
        /// Obtiene el listado de documentos de categoría Otros | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, fecha
        /// </summary>

        [HttpPost]
      public DataTable getDoctoOtrosxEstacionxFecha([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
                varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }

         return obj.getDoctoOtrosxEstacionxFecha(varOtrosDoctos);
      }
      /// <summary>
      /// Obtiene liquidación de documentos de categoría Otros | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, fecha
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionDoctoOtrosxEstacionxFecha([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
                varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
            }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }

         return obj.getLiquidacionDoctoOtrosxEstacionxFecha(varOtrosDoctos);
      }

      /// <summary>
      /// valida documento de categoría Otros | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, codTipoDocto, fecha, nodocto
      /// </summary>
      [HttpPost]
      public eParSalida validarDoctoOtros([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
            varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.validarDoctoOtros(varOtrosDoctos);
      }
      /// <summary>
      /// abre pantalla docto otros | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, fecha, codTipoDocto, nodocto
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
            varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
            }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.abrirPantallaUV(varOtrosDoctos);
      }
      /// <summary>
      /// get estado pantalla docto otros  | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, fecha, codTipoDocto, nodocto
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
            varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.getEstadoPantalla(varOtrosDoctos);
      }

      /// <summary>
      /// cierra pantalla docto otros | ENTIDAD: eDoctoOtros |  parametros: empresa, sucursal, fecha, codTipoDocto, nodocto
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parOtrosDoctosJSON)
      {
         eDoctoOtros varOtrosDoctos = new eDoctoOtros();
         try
         {
            varOtrosDoctos = JsonConvert.DeserializeObject<eDoctoOtros>(parOtrosDoctosJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         return obj.cerrarPantallaUV(varOtrosDoctos);
      }

        /// <summary>
      /// Obtiene el consolidado de otros documentos| ENTIDAD: eDoctoOtros | ATRIBUTOS: Empresa,Estacion, fechaIn, FechaFin
      /// </summary>
      [HttpPost]
      public DataTable infoConsolidadoOtrosXFecha([FromBody]  string parMovimientoOtrosJSON)
      {
         eDoctoOtros varMovimientoOtros = new eDoctoOtros();
         try
         {
            varMovimientoOtros = JsonConvert.DeserializeObject<eDoctoOtros>(parMovimientoOtrosJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dDoctoOtros obj = new dDoctoOtros();
         return obj.infoConsolidadoOtrosXFecha(varMovimientoOtros);
      }

   }
}
