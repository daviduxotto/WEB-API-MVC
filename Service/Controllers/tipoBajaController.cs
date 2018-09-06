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
   /// tipo de baja de empleados
   /// </summary>
    public class tipoBajaController : ApiController
   {
       
      /// <summary>
      /// Obtiene todos los tipos de baja de empleados
      /// </summary>
      [HttpPost]
      public DataTable getAllTipoBaja([FromBody]  string parTipoBajaJSON)
      {
         eTipoBaja varTipoBaja = new eTipoBaja();
         try
         {
            varTipoBaja = JsonConvert.DeserializeObject<eTipoBaja>(parTipoBajaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBaja obj = new dTipoBaja();
         return obj.getAllTipoBaja(varTipoBaja);
      }

      [HttpPost]
      public eParSalida nuevo([FromBody]  string parTipoBajaJSON)
      {
         eTipoBaja varTipoBaja = new eTipoBaja();
         try
         {
            varTipoBaja = JsonConvert.DeserializeObject<eTipoBaja>(parTipoBajaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBaja obj = new dTipoBaja();
         return obj.nuevo(varTipoBaja);
      }
      [HttpPost]
      public eParSalida editar([FromBody]  string parTipoBajaJSON)
      {
         eTipoBaja varTipoBaja = new eTipoBaja();
         try
         {
            varTipoBaja = JsonConvert.DeserializeObject<eTipoBaja>(parTipoBajaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBaja obj = new dTipoBaja();
         return obj.editar(varTipoBaja);
      }
      [HttpPost]
      public eParSalida eliminar([FromBody]  string parTipoBajaJSON)
      {
         eTipoBaja varTipoBaja = new eTipoBaja();
         try
         {
            varTipoBaja = JsonConvert.DeserializeObject<eTipoBaja>(parTipoBajaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBaja obj = new dTipoBaja();
         return obj.eliminar(varTipoBaja);
      }
   }
}
