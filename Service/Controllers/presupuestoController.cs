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
   public class presupuestoController : ApiController
    {
      /// <summary>
      /// Obtiene el año actual del presupuesto
      /// </summary>
      [HttpPost]
      public DataTable getAnioActual()
      {       
         dPresupuesto obj = new dPresupuesto();
         return obj.getAnioActual();

      }

      /// <summary>
      /// Obtiene detalle de presupuesto opex
      /// </summary>
      [HttpPost]
      public DataTable getDetallePresupuestoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getDetallePresupuestoDepartamento(varPresupuesto);
      }
      /// <summary>
      /// Obtiene detalle de presupuesto ejecutado opex
      /// </summary>
      [HttpPost]
      public DataTable getDetallePresupuestoEJecutadoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getDetallePresupuestoEjecutadoDepartamento(varPresupuesto);
      }

      /// <summary>
      /// Obtiene detalle de presupuesto ingreso
      /// </summary>
      [HttpPost]
      public DataTable getDetallePresupuestoIngresoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getDetallePresupuestoIngresoDepartamento(varPresupuesto);
      }
      /// <summary>
      /// Obtiene detalle de presupuesto ingreso ejecutado
      /// </summary>
      [HttpPost]
      public DataTable getDetallePresupuestoIngresoEjecutadoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getDetallePresupuestoIngresoEjecutadoDepartamento(varPresupuesto);
      }

      /// <summary>
      /// Obtiene detalle de productos presupuesto ingreso 
      /// </summary>
      [HttpPost]
        public DataTable getProductosPrestoIngreEmpresaDepartamento([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getProductosPrestoIngreEmpresaDepartamento(varPresupuesto);
        }

        /// <summary>
        /// Obtiene encabezado de presupuesto
        /// </summary>
        [HttpPost]
      public DataTable getEncabezadoPresupuestoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getEncabezadoPresupuestoDepartamento(varPresupuesto);
      }

      /// <summary>
      /// Obtiene encabezado de presupuesto Ingreso
      /// </summary>
      [HttpPost]
      public DataTable getEncabezadoPresupuestoIngresoDepartamento([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getEncabezadoPresupuestoIngresoDepartamento(varPresupuesto);
      }
      /// <summary>
      /// guarda la edicion del detalle presupuesto
      /// </summary>
      [HttpPost]
      public eParSalida editPresupuesto ([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.editPresupuesto(varPresupuesto);

      }
      /// <summary>
      /// guarda la edicion del detalle presupuesto ejecutado
      /// </summary>
      [HttpPost]
      public eParSalida editPresupuestoEjecutado([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.editPresupuestoEjecutado(varPresupuesto);

      }


      /// <summary>
      /// guarda la edicion del detalle presupuesto ingreso
      /// </summary>
      [HttpPost]
      public eParSalida editPresupuestoIngreso([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.editPresupuestoIngreso(varPresupuesto);

      }

      /// <summary>
      /// Obtiene informe presupuesto por departamento
      /// </summary>
      [HttpPost]
      public DataTable getInformePresupuesto([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePresupuesto(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto por area
      /// </summary>
      [HttpPost]
      public DataTable getInformePresupuestoxArea([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePresupuestoxArea(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformePresupuestoxUnidad([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePresupuestoxUnidad(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto por empresa
      /// </summary>
      [HttpPost]
      public DataTable getInformePresupuestoxEmpresa([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePresupuestoxEmpresa(varPresupuesto);
      }

      /// <summary>
      /// calcula la prestacion laboral por puesto 
      /// </summary>
      [HttpPost]
      public eParSalida calculaPrestacionLaboral([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.calculoPrestacionLaboral(varPresupuesto);

      }

      /// <summary>
      /// establece precio y margen presupuesto ingreso por unidad de negocio
      /// </summary>
      [HttpPost]
      public eParSalida setPrecioMargenIngresoxUnidad ([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.setPrecioMargenIngresoxUnidad(varPresupuesto);

      }

        /// <summary>
        /// establece precio y margen presupuesto ingreso para distribuidora
        /// </summary>
        [HttpPost]
        public eParSalida setPrecioMargenIngresoDistro([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.setPrecioMargenIngresoDistro(varPresupuesto);

        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de Volumen por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngrexVolumxEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngrexVolumxEmpxSuc(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de C1 por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngreC1xEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngreC1xEmpxSuc(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de C2 por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngreC2xEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngreC2xEmpxSuc(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de Flete por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngreFletexEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngreFletexEmpxSuc(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de C3 por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngreC3xEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngreC3xEmpxSuc(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso Total por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformeTotPrestoIngreAnualxUneg([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformeTotPrestoIngreAnualxUneg(varPresupuesto);
        }

        /// <summary>
        /// Retail: Obtiene informe presupuesto de ingreso de C4 por unidad de negocio 
        /// </summary>
        [HttpPost]
        public DataTable getInformePrestoIngreC4xEmpxSuc([FromBody]  string parPresupuestoJSON)
        {
            ePresupuesto varPresupuesto = new ePresupuesto();
            try
            {
                varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
            }
            catch (JsonException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            dPresupuesto obj = new dPresupuesto();
            return obj.getInformePrestoIngreC4xEmpxSuc(varPresupuesto);
        }

      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformePresupuestoVolumenxUnidad([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePresupuestoVolumenxUnidad(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoVolumenRetail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoVolumenRetail(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoC1Retail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoC1Retail(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoC2Retail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoC2Retail(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoC3Retail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoC3Retail(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoC4Retail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoC4Retail(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeIngresoFleteRetail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeIngresoFleteRetail(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto de Volumen por Unidad de Negocio
      /// </summary>
      [HttpPost]
      public DataTable getInformeTotalAnualRetail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformeTotalAnualRetail(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto vs ejectuado de Volumen corporativo 
      /// </summary>
      [HttpPost]
      public DataTable getInformePrestoVolumenAnualRetail([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePrestoVolumenAnualRetail(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto vs ejectuado de Volumen corporativo 
      /// </summary>
      [HttpPost]
      public DataTable getInformePrestoVolumenAnualRetail2([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePrestoVolumenAnualRetail2(varPresupuesto);
      }
      /// <summary>
      /// Obtiene informe presupuesto vs ejectuado de Volumen por empresa
      /// </summary>
      [HttpPost]
      public DataTable getInformePrestoVolumenAnualRetailEmpresa([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePrestoVolumenAnualRetailEmpresa(varPresupuesto);
      }

      /// <summary>
      /// Obtiene informe presupuesto vs ejectuado de Volumen por empresa x sucursal
      /// </summary>
      [HttpPost]
      public DataTable getInformePrestoVolumenAnualRetailEmpresaSucursal([FromBody]  string parPresupuestoJSON)
      {
         ePresupuesto varPresupuesto = new ePresupuesto();
         try
         {
            varPresupuesto = JsonConvert.DeserializeObject<ePresupuesto>(parPresupuestoJSON);
         }
         catch (JsonException ex)
         {
            System.Console.WriteLine(ex.Message);
         }
         dPresupuesto obj = new dPresupuesto();
         return obj.getInformePrestoVolumenAnualRetailEmpresaSucursal(varPresupuesto);
      }
   }
}
