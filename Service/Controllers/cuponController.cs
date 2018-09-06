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
{ /// <summary>
  /// </summary>
   public class cuponController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de cupones de un determinado dia de una estacion | ENTIDAD: eCupon |  parametros: empresa, sucursal, dia
      /// </summary>
      [HttpPost]
      public DataTable getCuponesxEstacionXfecha([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.getCuponesxEstacionxFecha(varCupon);
      }

      /// <summary>
      ///Valida los cupones| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion y token
      /// </summary>
      [HttpPost]
      public eParSalida validarcupon( [FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.validarCupon(varCupon);
      }

      /// <summary>
      /// Obtiene el listado emisores de cupones por empresa | ENTIDAD: eCupon |  parametros: empresa,
      /// </summary>
      [HttpPost]
      public DataTable getEmisorCupon([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.getEmisorCupon(varCupon);
      }


      /// <summary>
      /// Obtiene el listado denominacinoes cupones por empresa | ENTIDAD: eCupon |  parametros: empresa, emisor
      /// </summary>
      [HttpPost]
      public DataTable getDenominacionCupon([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.getDenominacionCupon(varCupon);
      }

      /// <summary>
      ///nuevo cupon| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion cantidad y token
      /// </summary>
      [HttpPost]
      public eParSalida nuevocupon([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.nuevoCupon(varCupon);
      }

      /// <summary>
      ///editar cupon| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion cantidad y token
      /// </summary>
      [HttpPost]
      public eParSalida editarcupon([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.editarCupon(varCupon);
      }

      /// <summary>
      ///eliminar cupon| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion y token
      /// </summary>
      [HttpPost]
      public eParSalida eliminarcupon([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.eliminarCupon(varCupon);
      }


      /// <summary>
      /// Obtiene la liquidacion de los cupones por empresa por dia y por estacion | entidad eCupon   | parametros: empresa sucursal dia
      /// </summary>
      [HttpPost]
      public DataTable getLiquidacionCupones([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.getLiquidacionCupones(varCupon);
      }

      /// <summary>
      ///obtiene estado pantalla  los cupones| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion y token
      /// </summary>
      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.getEstadoPantalla(varCupon);
      }
      /// <summary>
      ///abrir pantalla  los cupones| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion y token
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.abrirPantallaUV(varCupon);
      }
      /// <summary>
      ///cerrar estado pantalla  los cupones| ENTIDAD: eCupon |  parametros: empresa, sucursal, dia, emisor, denominacnion y token
      /// </summary>
      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parCuponJSON)
      {
         eCupon varCupon = new eCupon();
         try
         {
            varCupon = JsonConvert.DeserializeObject<eCupon>(parCuponJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dCupon obj = new dCupon();
         return obj.cerrarPantallaUV(varCupon);
      }
   }
}
