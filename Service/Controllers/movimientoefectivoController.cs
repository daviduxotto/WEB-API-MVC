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
    public class movimientoefectivoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Depositos Bancarios en una fecha especifica| ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getMovEfectivoXFecha([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.getMovEfectivoXFecha(varMovimientoEfectivo);
      }

      /// <summary>
      /// Obtiene montos totales de pagos efectivo/tarjeta_credito, frente a lo conciliado| ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa,sucursal,fecha,tipopago
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionVtaDiariaxEmpre([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.getLiquidacionMonetariaVentaDiariaXempresa(varMovimientoEfectivo);
      }

      [HttpPost]
      public eParSalida CambioMonto([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.cambiarMonto(varMovimientoEfectivo);

      }

      [HttpPost]
      public eParSalida nuevo([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.nuevo(varMovimientoEfectivo);

      }

      public eParSalida CambioCodigo([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.cambiarCodigo(varMovimientoEfectivo);

      }

      [HttpPost]
      public eParSalida del([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.del(varMovimientoEfectivo);

      }

      [HttpPost]
      public eParSalida conciliar([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.conciliar(varMovimientoEfectivo);

      }

      /// <summary>
      /// Obtiene el informe de Depositos Bancarios en una fecha especifica| ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoMovEfectivoXFecha([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoMovEfectivoXFecha(varMovimientoEfectivo);
      }

      /// <summary>
      /// Obtiene el informe de total venta diaria vs lo conciliado | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoVentaDiariaVSConciliadoXFecha([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoVentaDiariaVSConciliadoXFecha(varMovimientoEfectivo);
      }
      /// <summary>
      /// Obtiene el informe de total venta diaria vs lo conciliado | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoVentaDiariaVSConciliadoXFechaGrafico([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoVentaDiariaVSConciliadoXFechaGrafico(varMovimientoEfectivo);
      }
      /// <summary>
      /// Obtiene el informe de total venta diaria vs lo conciliado | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoVentaDiariaVSConciliadoXFechaxEstacion([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoVentaDiariaVSConciliadoXFechaxEstacion(varMovimientoEfectivo);
      }

      /// <summary>
      /// Obtiene lo cinciliado en el mes actual y lo conciliado en el mes siguiente | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoFlujoEfectivoxMes([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoFlujoEfectivoxMes(varMovimientoEfectivo);
      }
      /// <summary>
      /// Obtiene lo cinciliado en el mes actual y lo conciliado en el mes siguiente mejorado | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoFlujoEfectivoxMes2([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivo varMovimientoEfectivo = new eMovimientoEfectivo();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivo>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.infoFlujoEfectivoxMes2(varMovimientoEfectivo);
      }
      /// <summary>
      /// revierte documento bancaro de la venta diaria en efectivo| ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa, banco, cuentabanco, coddoctobanco, coddocumento,coddoctobancoreversion,coddocumentoreversion,montodocumentoreversion,concepto
      /// </summary>
      [HttpPost]
      public eParSalida revertir([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.revertir(varMovimientoEfectivo);
      }

      /// <summary>
      ///apertura el la pantalla de movimiento de efectivo | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa, banco, cuentabanco, coddoctobanco, coddocumento,coddoctobancoreversion,coddocumentoreversion,montodocumentoreversion,concepto
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.abrirPantallaUV(varMovimientoEfectivo);

      }
      /// <summary>
      ///obtiene el estado de  la pantalla de movimiento de efectivo | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa, banco, cuentabanco, coddoctobanco, coddocumento,coddoctobancoreversion,coddocumentoreversion,montodocumentoreversion,concepto
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla ([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.getEstadoPantalla(varMovimientoEfectivo);

      }

      /// <summary>
      ///cierra la pantalla de movimiento de efectivo | ENTIDAD: eMovimientoEfectivoDet | ATRIBUTOS: Empresa, banco, cuentabanco, coddoctobanco, coddocumento,coddoctobancoreversion,coddocumentoreversion,montodocumentoreversion,concepto
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parMovimientoEfectivoJSON)
      {
         eMovimientoEfectivoDet varMovimientoEfectivo = new eMovimientoEfectivoDet();
         try
         {
            varMovimientoEfectivo = JsonConvert.DeserializeObject<eMovimientoEfectivoDet>(parMovimientoEfectivoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoEfectivo obj = new dMovimientoEfectivo();
         return obj.cerrarPantallaUV(varMovimientoEfectivo);

      }
   }
}
