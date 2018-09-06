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
    public class bancoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Bancos por Empresa | ENTIDAD: eBanco | ATRIBUTOS: codEmpresa
      /// </summary>
      [HttpPost]
      public DataTable getBancosXEmpresa([FromBody]  string parBancoJSON)
      {
         eBanco varBanco = new eBanco();
         try
         {
            varBanco = JsonConvert.DeserializeObject<eBanco>(parBancoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBanco obj = new dBanco();
         return obj.getBancosxEmpresa(varBanco);

      }

      [HttpPost]
      public DataTable getCuentasxBanco([FromBody]  string parCuentaBancoJSON)
      {
         eCuentaBanco varCuentaBanco = new eCuentaBanco();
         try
         {
            varCuentaBanco = JsonConvert.DeserializeObject<eCuentaBanco>(parCuentaBancoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBanco obj = new dBanco();
         return obj.getCuentasXBanco(varCuentaBanco);

      }

      [HttpPost]
      public DataTable getDoctoBanco([FromBody]  string parBancoJSON)
      {
         eDoctoBanco varBanco = new eDoctoBanco();
         try
         {
            varBanco = JsonConvert.DeserializeObject<eDoctoBanco>(parBancoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBanco obj = new dBanco();
         return obj.getDoctoBanco(varBanco);

      }
      /// <summary>
      /// Obtiene el listado de tipos de documentos bancarios negativos | ENTIDAD: eBanco | ATRIBUTOS: codEmpresa codbanco, codcuentabanco
      /// </summary>
      [HttpPost]
      public DataTable getDoctoBancoNegativos([FromBody]  string parBancoJSON)
      {
         eDoctoBanco varBanco = new eDoctoBanco();
         try
         {
            varBanco = JsonConvert.DeserializeObject<eDoctoBanco>(parBancoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBanco obj = new dBanco();
         return obj.getDoctoBancoNegativos(varBanco);

      }

      [HttpPost]
      public DataTable getDoctoBancoTC([FromBody]  string parBancoJSON)
      {
         eDoctoBanco varBanco = new eDoctoBanco();
         try
         {
            varBanco = JsonConvert.DeserializeObject<eDoctoBanco>(parBancoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dBanco obj = new dBanco();
         return obj.getDoctoBancoTC(varBanco);

      }
   }
}
