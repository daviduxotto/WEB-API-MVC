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
    /// Esta entidad es sobre los pagos de clientes crédito realizados con tarjeta de crédito
    /// </summary>
    public class movimientoPagoTCController : ApiController
    {
        /// <summary>
        /// Obtiene el listado de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
        /// </summary>
        [HttpPost]
        public DataTable getMovPagoTCXFecha([FromBody]  string parMovimientoPagoTCJSON)
        {
            eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
            try
            {
                varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dMovimientoPagoTC obj = new dMovimientoPagoTC();
            return obj.getMovPagoTCXFecha(varMovimientoPagoTC);
        }
      /// <summary>
      /// Obtiene el listado de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable getMovPagoTCXFechaDetalle([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.getMovPagoTCXFechaDetalle(varMovimientoPagoTC);
      }
      /// <summary>
      /// conciliar | ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
        public eParSalida conciliar([FromBody]  string parMovimientoPagoTCJSON)
        {
            eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
            try
            {
                varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dMovimientoPagoTC obj = new dMovimientoPagoTC();
            return obj.conciliar(varMovimientoPagoTC);
        }
        /// <summary>
        /// cambia el codigo del documento| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
        /// </summary>
        [HttpPost]
        public eParSalida cambiocodigo([FromBody]  string parMovimientoPagoTCJSON)
        {
            eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
            try
            {
                varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dMovimientoPagoTC obj = new dMovimientoPagoTC();
            return obj.cambioCodigo(varMovimientoPagoTC);
        }

        /// <summary>
        /// Obtiene la liquidacion de pagos de cliente creditos| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
        /// </summary>
        [HttpPost]
        public DataTable getLiquidacionPagoClienteXempresa([FromBody]  string parMovimientoPagoTCJSON)
        {
            eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
            try
            {
                varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dMovimientoPagoTC obj = new dMovimientoPagoTC();
            return obj.getLiquidacionPagoClienteXempresa(varMovimientoPagoTC);
        }

      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoMovClienteTCXFecha([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.infoMovClienteTCXFecha(varMovimientoPagoTC);
      }


      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoClientetcVsConciliado([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.infoClientetcVsConciliado(varMovimientoPagoTC);
      }
      /// <summary>
      /// Obtiene el informe de Depositos Tarjeta Credito en una fecha especifica| ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public DataTable infoClientetcVsConciliadoGrafico([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.infoClientetcVsConciliadoGrafico(varMovimientoPagoTC);
      }
      /// <summary>
      /// obtener estado pantalla | ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.getEstadoPantalla(varMovimientoPagoTC);
      }
      /// <summary>
      /// abrirpantalla | ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.abrirPantallaUV(varMovimientoPagoTC);
      }

      /// <summary>
      /// cerrar pantalla | ENTIDAD: eMovimientoPagoTC | ATRIBUTOS: Empresa
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parMovimientoPagoTCJSON)
      {
         eMovimientoPagoTC varMovimientoPagoTC = new eMovimientoPagoTC();
         try
         {
            varMovimientoPagoTC = JsonConvert.DeserializeObject<eMovimientoPagoTC>(parMovimientoPagoTCJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dMovimientoPagoTC obj = new dMovimientoPagoTC();
         return obj.cerrarPantallaUV(varMovimientoPagoTC);
      }

     }
}
