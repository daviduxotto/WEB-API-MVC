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
   /// 
   /// </summary>
    public class proveedorController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de proovedoresCajaChica| ENTIDAD: eProveedor | ATRIBUTOS: --
      /// </summary>
      [HttpPost]
      public DataTable getProveedorCajaChicaxEmpresa([FromBody]  string parProveedorJSON)
      {
         eProveedor varProveedor = new eProveedor();
         try
         {
            varProveedor = JsonConvert.DeserializeObject<eProveedor>(parProveedorJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dProveedor obj = new dProveedor();
         return obj.getProveedorCajaChicaxEmpresa(varProveedor);

      }
   }
}
