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
   public class movimientoTCController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getMovTCXFecha([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.getMovTCXFecha(varMovimientoTC);
      }

      /// <summary>
      /// Obtiene montos totales de pagos TC/tarjeta_credito, frente a lo conciliado| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa,sucursal,fecha,tipopago
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionVtaDiariaxEmpre([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.getLiquidacionMonetariaVentaDiariaXempresa(varMovimientoTC);
      }

      [HttpPost]
      public eParSalida CambioMonto([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.cambiarMonto(varMovimientoTC);

      }

      [HttpPost]
      public eParSalida nuevo([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.nuevo(varMovimientoTC);

      }

      public eParSalida CambioCodigo([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.cambiarCodigo(varMovimientoTC);

      }

      [HttpPost]
      public eParSalida del([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.del(varMovimientoTC);

      }

      [HttpPost]
      public eParSalida conciliar([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.conciliar(varMovimientoTC);

      }

      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoMovTCXFecha([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.infoMovTCXFecha(varMovimientoTC);
      }


      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito en una fecha especifica vs conciliado| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoVentaDiariaTCvsConciliado([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.infoVentaDiariaTCvsConciliado(varMovimientoTC);
      }
      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito grafico en una fecha especifica vs conciliado| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoVentaDiariaTCvsConciliadoGrafico([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.infoVentaDiariaTCvsConciliadoGrafico(varMovimientoTC);
      }
      /// <summary>
      /// inserta un nuevo registtro de movimiento tarjeta de credito venta diaria saes web| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida newMovimientoTC([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.newMovimientoTC(varMovimientoTC);

      }

      /// <summary>
      /// elimibnaa registtro de movimiento tarjeta de credito venta diaria saes web| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida delMovimientoTC([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.delMovimientoTC(varMovimientoTC);

      }
      /// <summary>
      /// eabre la pantall alimibnaa registtro de movimiento tarjeta de credito venta diaria saes web| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.abrirPantallaUV(varMovimientoTC);

      }

      /// <summary>
      /// cierra la pantall alimibnaa registtro de movimiento tarjeta de credito venta diaria saes web| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.cerrarPantallaUV(varMovimientoTC);

      }
      /// <summary>
      /// obtiene el estado de la pantalla registtro de movimiento tarjeta de credito venta diaria saes web| ENTIDAD: eMovimientoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla ([FromBody]  string parMovimientoTCJSON)
      {
         eMovimientoTC varMovimientoTC = new eMovimientoTC();
         try
         {
            varMovimientoTC = JsonConvert.DeserializeObject<eMovimientoTC>(parMovimientoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoTC obj = new dMovimientoTC();
         return obj.getEstadoPantalla(varMovimientoTC);

      }
   }
}
