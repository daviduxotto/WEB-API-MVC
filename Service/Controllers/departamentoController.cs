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
{  /// <summary>
/// 
/// </summary>
    public class departamentoController : ApiController
    {
      /// <summary>
      /// Obtiene el listado de Departamentos por Empresa y unidad negocio x area| ENTIDAD: eDepartamento | ATRIBUTOS: 
      /// </summary>
      [HttpPost]
      public DataTable getDepartamentoxUnidadNegocioxArea([FromBody]  string parDepartamentoJSON)
      {
         eDepartamento varDepartamento = new eDepartamento();
         try
         {
            varDepartamento = JsonConvert.DeserializeObject<eDepartamento>(parDepartamentoJSON);
         }
         catch { }
         dDepartamento obj = new dDepartamento();
         return obj.getDepartamentoxUnidadNegocioxArea(varDepartamento);

      }
   }
}
