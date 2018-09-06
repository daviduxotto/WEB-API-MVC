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
    public class empresaController : ApiController
    {
      [HttpPost]
      public DataTable getEmpresasXUsuario([FromBody]  string parUsuarioJSON)
      {
         eUsuario varusuario = new eUsuario();
         try
         {
            varusuario = JsonConvert.DeserializeObject<eUsuario>(parUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpresa obj = new dEmpresa();
         return obj.getEmpresasXUsuario(varusuario);

      }

      [HttpPost]
      public DataTable get_empreconfigcodprod([FromBody]  string parEmpresaJSON)
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
         dEmpresa obj = new dEmpresa();
         return obj.get_empreconfigcodprod(varempresa);

      }
   }
}
