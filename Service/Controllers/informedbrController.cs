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
    public class informedbrController : ApiController
    {
      [HttpPost]
      public DataTable getAuditoriainformesdbr([FromBody]  string parEntornoJSON)
      {
         eEntorno varentorno = new eEntorno();
         try
         {
            varentorno = JsonConvert.DeserializeObject<eEntorno>(parEntornoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.getAuditoriainformesdbr(varentorno);

      }

      [HttpPost]
      public DataTable getAuditoriainformesdbrctrlcalidad([FromBody]  string parEntornoJSON)
      {
         eEntorno varentorno = new eEntorno();
         try
         {
            varentorno = JsonConvert.DeserializeObject<eEntorno>(parEntornoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.getAuditoriainformesdbrctrlcalidad(varentorno);

      }

      [HttpPost]
      public eParSalida editInformesDBR([FromBody]  string parInformesJSON)
      {
         eInformeDBR varinforme = new eInformeDBR();
         try
         {
            varinforme = JsonConvert.DeserializeObject<eInformeDBR>(parInformesJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.editInformesDBR(varinforme);

      }

      [HttpPost]
      public eParSalida getEstadoPantalla([FromBody]  string parInformesJSON)
      {
         eInformeDBR varinforme = new eInformeDBR();
         try
         {
            varinforme = JsonConvert.DeserializeObject<eInformeDBR>(parInformesJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.getEstadoPantalla(varinforme);

      }

      [HttpPost]
      public eParSalida abrirPantallaUV([FromBody]  string parInformesJSON)
      {
         eInformeDBR varinforme = new eInformeDBR();
         try
         {
            varinforme = JsonConvert.DeserializeObject<eInformeDBR>(parInformesJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.abrirPantallaUV(varinforme);

      }

      [HttpPost]
      public eParSalida cerrarPantallaUV([FromBody]  string parInformesJSON)
      {
         eInformeDBR varinforme = new eInformeDBR();
         try
         {
            varinforme = JsonConvert.DeserializeObject<eInformeDBR>(parInformesJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dInformeDBR obj = new dInformeDBR();
         return obj.cerrarPantallaUV(varinforme);

      }
   }
}
