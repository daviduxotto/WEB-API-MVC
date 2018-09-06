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
   public class empleadoController : ApiController
    {
      /// <summary>
      /// guarda nuevo empleado, necesita como parametro una entidad: eEmpleado
      /// </summary>
      [HttpPost]
      public eParSalida newEmpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.newEmpleado(varEmpleado);

      }

      /// <summary>
      /// guarda edicion empleado
      /// </summary>
      [HttpPost]
      public eParSalida editEmpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.editEmpleado(varEmpleado);

      }
      /// <summary>
      /// guarda traslado empleado
      /// </summary>
      [HttpPost]
      public eParSalida trasladarEmpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.trasladarEmpleado(varEmpleado);

      }
      /// <summary>
      /// guarda traslado empleado de empresa
      /// </summary>
      [HttpPost]
      public eParSalida trasladarEmpleadoEmpresa([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.trasladarEmpleadoEmpresa(varEmpleado);

      }

      /// <summary>
      /// guarda edicion del salario del empleado
      /// </summary>
      [HttpPost]
      public eParSalida editSalariosEmpleados([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.editSalariosEmpleados(varEmpleado);

      }
      /// <summary>
      ///elimina empleado
      /// </summary>
      [HttpPost]
      public eParSalida delEmpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.delEmpleado(varEmpleado);

      }

      /// <summary>
      /// obtiene empleados por empresa por area y por departamento
      /// </summary>
      [HttpPost]
      public DataTable getEmpleadoxEmpresaxAreaXDepartamento([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getEmpleadoxEmpresaxAreaXDepartamento(varEmpleado);

      }
      /// <summary>
      /// obtiene el informe dinamico de todos los empleados
      /// </summary>
      [HttpPost]
      public DataTable infoAllEmpleados([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.infoAllEmpleados(varEmpleado);

      }

      /// <summary>
      /// obtiene empleados por empresa por unidad de negocio (estacion)
      /// </summary>
      [HttpPost]
      public DataTable getEmpleadoxEmpresaxUnidad([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getEmpleadoxEmpresaxUnidad(varEmpleado);

      }

      /// <summary>
      /// obtiene los datos de un empleado
      /// </summary>
      [HttpPost]
      public DataTable getOneEMpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getOneEmpleado(varEmpleado);

      }

      /// <summary>
      /// obtiene los datos de baja de un empleado
      /// </summary>
      [HttpPost]
      public DataTable getBajaEmpleado([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getBajaEmpleado(varEmpleado);

      }

      /// <summary>
      /// get entorno empleado
      /// </summary>
      [HttpPost]
      public DataTable getEntorno([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getEntorno(varEmpleado);

      }

      /// <summary>
      /// guarda nuevo empleado para presupuesto, necesita como parametro una entidad: eEmpleado
      /// </summary>
      [HttpPost]
      public eParSalida newEmpleadoPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.newEmpleadoPresu(varEmpleado);

      }


      /// <summary>
      /// guarda edicion empleado para presupuesto
      /// </summary>
      [HttpPost]
      public eParSalida editEmpleadoPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.editEmpleadoPresu(varEmpleado);

      }

      /// <summary>
      ///elimina empleado presupuesto
      /// </summary>
      [HttpPost]
      public eParSalida delEmpleadoPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.delEmpleadoPresu(varEmpleado);

      }

      /// <summary>
      /// obtiene empleados por empresa por area y por departamento presupuesto
      /// </summary>
      [HttpPost]
      public DataTable getEmpleadoxEmpresaxAreaXDepartamentoPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getEmpleadoxEmpresaxAreaXDepartamentoPresu(varEmpleado);

      }

      /// <summary>
      /// obtiene empleados por empresa por unidad de negocio (estacion) presupuesto
      /// </summary>
      [HttpPost]
      public DataTable getEmpleadoxEmpresaxUnidadPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getEmpleadoxEmpresaxUnidadPresu(varEmpleado);

      }

      /// <summary>
      /// obtiene los datos de un empleado presupupuesto
      /// </summary>
      [HttpPost]
      public DataTable getOneEMpleadoPresu([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getOneEmpleadoPresu(varEmpleado);

      }

      // ------------- informes
      /// <summary>
      /// obtiene el informe de empleados por departamento: ENTIDAD eEmpleado atributos: empresa, sucursal, area y departamento
      /// </summary>
      [HttpPost]
      public DataTable getInformeEmpleadoxDeparamento([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getInformeEmpleadoxDepartamento(varEmpleado);
      }
      /// <summary>
      /// obtiene el informe de empleados por area: ENTIDAD eEmpleado atributos: empresa, sucursal, area
      /// </summary>
      [HttpPost]
      public DataTable getInformeEmpleadoxArea([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getInformeEmpleadoxArea(varEmpleado);
      }

      /// <summary>
      /// obtiene el informe de empleados por Unidad: ENTIDAD eEmpleado atributos: empresa, sucursal
      /// </summary>
      [HttpPost]
      public DataTable getInformeEmpleadoxUnidad([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getInformeEmpleadoxUnidad(varEmpleado);

      }
      /// <summary>
      /// obtiene el informe de empleados por Empresa: ENTIDAD eEmpleado atributos: empresa
      /// </summary>
      [HttpPost]
      public DataTable getInformeEmpleadoxEmpresa([FromBody]  string parEmpleadoJSON)
      {
         eEmpleado varEmpleado = new eEmpleado();
         try
         {
            varEmpleado = JsonConvert.DeserializeObject<eEmpleado>(parEmpleadoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dEmpleado obj = new dEmpleado();
         return obj.getInformeEmpleadoxEmpresa(varEmpleado);

      }
   }
}
