using System;
using System.Data;
using System.Web.Http;
using Entidades;
using Datos;
using Newtonsoft.Json;

namespace Service.Controllers
{
   public class nivelUsuarioController : ApiController
   {

      [HttpPost]
      public eParSalida newNivel([FromBody] string parNivelUsuarioJSON)
      {
         eNivelUsuario  varnivelusuario= new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.newNivel(varnivelusuario);
      }

      [HttpPost]
      public eParSalida editNivel([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.editNivel(varnivelusuario);
      }
      [HttpPost]
      public eParSalida delNivel([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.delNivel(varnivelusuario);
      }


      [HttpPost]
      public DataTable getAllNivelUsuario([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.getAllNivelUsuario(varnivelusuario);
      }

      [HttpPost]
      public DataTable getNivelUsuarioXCodigo([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.getNivelUsuarioXCodigo(varnivelusuario);
      }

      [HttpPost]
      public DataTable getNivelUsuarioXDesc([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.getNivelUsuarioXDesc(varnivelusuario);
      }

      [HttpPost]
      public DataTable getOneEncNivelUsuario([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.getOneEncNivelUsuario(varnivelusuario);
      }


      [HttpPost]
      public DataTable getOneDetNivelUsuario([FromBody]string parNivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parNivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.getOneDetNivelUsuario(varnivelusuario);
      }
      [HttpPost]
      public DataTable infoNivelUsuario([FromBody]string parnivelUsuarioJSON)
      {
         eNivelUsuario varnivelusuario = new eNivelUsuario();
         try
         {
            varnivelusuario = JsonConvert.DeserializeObject<eNivelUsuario>(parnivelUsuarioJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dNivelUsuario obj = new dNivelUsuario();
         return obj.infoNivelUsuario(varnivelusuario);

      }

   }
}
