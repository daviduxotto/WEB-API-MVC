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
    public class tipoBodegaController : ApiController
    {
      [HttpPost]
      public eParSalida newTipoBodega([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.newTipoBodega(tipobodega);
      }

      [HttpPost]
      public eParSalida editTipoBodega([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.editTipoBodega(tipobodega);
      }

      [HttpPost]
      public eParSalida delTipoBodega([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.delTipoBodega(tipobodega);
      }

      [HttpPost]
      public DataTable getOneTipoBodega([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.getOneTipoBodega(tipobodega);
      }

      [HttpPost]
      public DataTable findTipoBodegaxCodigo([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.findTipoBodegaxCodigo(tipobodega);
      }

      [HttpPost]
      public DataTable findTipoBodegaxDesc([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.findTipoBodegaxDesc(tipobodega);
      }

      [HttpPost]
      public DataTable infoTipoBodegaxEmpresa([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.infoTipoBodegaxEmpresa(tipobodega);
      }


      [HttpPost]
      public DataTable getAllTipoBodegaxEmpresa([FromBody] string parTipoBodegaJSON)
      {
         eTipoBodega tipobodega = new eTipoBodega();
         try
         {
            tipobodega = JsonConvert.DeserializeObject<eTipoBodega>(parTipoBodegaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTipoBodega obj = new dTipoBodega();
         return obj.getAllTipoBodegaxEmpresa(tipobodega);
      }
   }
}
