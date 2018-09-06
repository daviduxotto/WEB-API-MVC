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
    public class trasladoempleadoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de traslado de un empleado
      /// </summary>
      [HttpPost]
      public DataTable getTrasladoEmpleado([FromBody]  string parTrasladoEmpleadoJSON)
      {
         eTrasladoEmpleado varTrasladoEmpleado = new eTrasladoEmpleado();
         try
         {
            varTrasladoEmpleado = JsonConvert.DeserializeObject<eTrasladoEmpleado>(parTrasladoEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dTrasladoEmpleado obj = new dTrasladoEmpleado();
         return obj.getTrasladoEmpleado(varTrasladoEmpleado);
      }

   }
}
