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
    public class doctoCxPController : ApiController
    {
      /// <summary>
      ///  obtiene el listado de documentos por pagar por empresa 
      /// </summary>
      [HttpPost]
      public DataTable getDoctoCxPxEmpresa([FromBody] string pardoctocxoJSON)
      {
         eDoctoCxP doctocxp = new eDoctoCxP();
         try
         {
            doctocxp = JsonConvert.DeserializeObject<eDoctoCxP>(pardoctocxoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dDoctoCxP obj = new dDoctoCxP();
         return obj.getDoctoCxPxEmpresa(doctocxp);
      }
   }
}
