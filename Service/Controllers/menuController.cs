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
    public class menuController : ApiController
    {

      [HttpPost]
      public DataTable getAllMenu([FromBody]  string parUsuarioJSON)
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
         dMenu obj = new dMenu();
         return obj.getAllMenu(varusuario);

      }
   }
}
