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
   /// 
   /// </summary>
   public class movimientoPagoCCController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Depositos Bancarios en una fecha especifica| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getMovPagoCCXFecha([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.getMovPagoCCXFecha(varMovimientoPagoCC);
      }
      /// <summary>
      /// Obtiene el listado de Depositos Bancarios en una fecha especifica incluye facturas y vales| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getMovPagoCCXFechaDetalle([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.getMovPagoCCXFechaDetalle(varMovimientoPagoCC);
      }
      /// <summary>
      /// conciliar | ENTIDAD: eMovimientoPagoCC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida conciliar([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.conciliar(varMovimientoPagoCC);
      }
      /// <summary>
      /// cambia el codigo del documento| ENTIDAD: eMovimientoPagoCC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida cambiocodigo([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.cambioCodigo(varMovimientoPagoCC);
      }

      /// <summary>
      /// Obtiene la liquidacion de pagos de cliente creditos| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionPagoClienteXempresa([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.getLiquidacionPagoClienteXempresa(varMovimientoPagoCC);
      }

      /// <summary>
      /// Obtiene el informe de Depositos Bancarios de pago de clientes credito con tarjeta de credito en una fecha especifica| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoMovClienteXFecha([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.infoMovClienteXFecha(varMovimientoPagoCC);
      }

      /// <summary>
      /// Obtiene el informe de Depositos Bancarios de pago de clientes credito con tarjeta de credito en una fecha especifica| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoClienteefectivoVsConciliado([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.infoClienteefectivoVsConciliado(varMovimientoPagoCC);
      }
      /// <summary>
      /// Obtiene el informe de Depositos Bancarios de pago de clientes credito con tarjeta de credito en una fecha especifica| ENTIDAD: eMovimientoPagoCCDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoClienteefectivoVsConciliadoGrafico([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.infoClienteefectivoVsConciliadoGrafico(varMovimientoPagoCC);
      }

      /// <summary>
      /// obtiene el estado de la pantalla| ENTIDAD: eMovimientoPagoCC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.getEstadoPantalla(varMovimientoPagoCC);
      }
      /// <summary>
      /// abre la pantalla| ENTIDAD: eMovimientoPagoCC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.abrirPantallaUV(varMovimientoPagoCC);
      }

      /// <summary>
      ///  cierra la pantalla| ENTIDAD: eMovimientoPagoCC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parMovimientoPagoCCJSON)
      {
         eMovimientoPagoCC varMovimientoPagoCC = new eMovimientoPagoCC();
         try
         {
            varMovimientoPagoCC = JsonConvert.DeserializeObject<eMovimientoPagoCC>(parMovimientoPagoCCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoCC obj = new dMovimientoPagoCC();
         return obj.cerrarPantallaUV(varMovimientoPagoCC);
      }
   }
}
