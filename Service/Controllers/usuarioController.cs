using Entidades;
using Datos;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;

namespace Service.Controllers
{
   public class usuarioController : ApiController
   {

      [HttpPost]
      public eParSalida verificarUsuario([FromBody] string parUsuarioJSON)
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
         dUsuario obj = new dUsuario();
         return obj.verificarUsuario(varusuario);
      }

      [HttpPost]
      public int validaTokenUsuario ([FromBody] string parUsuarioJSON)
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
         dAutenticacion obj = new dAutenticacion();
         return obj.validartokenUsuario(varusuario);
      }

      [HttpPost]
      public eParSalida verificarAccesoUsuario([FromBody] string parUsuarioJSON)
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
         dUsuario obj = new dUsuario();
         return obj.verificarAccesoUsuario(varusuario);
      }
      [HttpPost]
      public DataTable verificarTodoAccesoUsuario([FromBody] string parUsuarioJSON)
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
         dUsuario obj = new dUsuario();
         return obj.verificarTodoAccesoUsuario(varusuario);
      }

   }
}
