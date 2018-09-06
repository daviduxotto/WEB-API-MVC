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
    public class periodoController : ApiController
    {
      [HttpPost]
      public DataTable getPeriodoXEmpreorDDes([FromBody]string parEmpresaJSON)
      {
         eEmpresa varempresa = new eEmpresa();
         try
         {
            varempresa = JsonConvert.DeserializeObject<eEmpresa>(parEmpresaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPeriodo obj = new dPeriodo();
         return obj.getPeriodoXEmpreorDDesc(varempresa);
      }

      [HttpPost]
      public DataTable getPeriodoXEmprexUser([FromBody]string parEmpresaJSON)
      {
         eEmpresa varempresa = new eEmpresa();
         try
         {
            varempresa = JsonConvert.DeserializeObject<eEmpresa>(parEmpresaJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPeriodo obj = new dPeriodo();
         return obj.getPeriodoXEmprexUser(varempresa);
      }
   }
}
