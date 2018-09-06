using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class empleadoController : Controller
    {
        // GET: empleado
        public ActionResult Index()
        {
            return View();
        }

      public async Task<ActionResult> listar(eEmpleado empleado)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         Session["empresaempleado"] = empleado.empresa;
         Session["estacionempleado"] = empleado.estacion;
         Session["areaempleado"] = empleado.area;
         Session["Departamentoempleado"] = empleado.departamento;
         empleado = await getEntorno();
         ViewBag.empresa = empleado.descEmpresa;
         ViewBag.estacion = empleado.descEstacion;
         ViewBag.area = empleado.descArea;
         ViewBag.departamento = empleado.descDepartamento;
         return View();
      }

      public ActionResult nuevo()
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {


            return View();
         }
      }

      public ActionResult entorno()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();

      }

      public ActionResult entornoInfoEmpleadoxDepartamento()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();

      }

      public ActionResult entornoIngresoSalarios()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();

      }

      public ActionResult entornoInfoEmpleados()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();

      }
      public async Task<ActionResult> guardarNuevo(eEmpleado empleado)
      {

         empleado.empresa = Session["empresaEmpleado"].ToString();
         empleado.estacion = Session["estacionEmpleado"].ToString();
         empleado.area = Session["areaEmpleado"].ToString();
         empleado.departamento = Session["DepartamentoEmpleado"].ToString();
         empleado.token = Session["token"].ToString();
         empleado.primerNombre = empleado.primerNombre.ToUpper();
         if (empleado.segundoNombre != null) { empleado.segundoNombre = empleado.segundoNombre.ToUpper(); }
         if (empleado.tercerNombre != null) { empleado.tercerNombre = empleado.tercerNombre.ToUpper(); }
         if (empleado.primerApellido != null) { empleado.primerApellido = empleado.primerApellido.ToUpper(); }
         if (empleado.segundoApellido != null) { empleado.segundoApellido = empleado.segundoApellido.ToUpper(); }
         if (empleado.apellidoCasada!= null) { empleado.apellidoCasada = empleado.apellidoCasada.ToUpper(); }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/newempleado", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }

      public async Task<ActionResult> editar(string codigo)
      {

       
            eEmpleado empleado = new eEmpleado();
            empleado = await getOneEmpleaedo(codigo);
            return View(empleado);
         

      }
      public async Task<ActionResult>ver(string codigo)
      {


         eEmpleado empleado = new eEmpleado();
         empleado = await getOneEmpleaedo(codigo);
         return View(empleado);


      }
      public async Task<ActionResult> trasladar(string codigocorp, string codigo, string empresa, string estacion, string area, string departamento, string puesto, string salario)
      {
         eEmpleado empleado = new eEmpleado();
         empleado = await getOneEmpleaedo(codigocorp);
         empleado.codigoCorp = Convert.ToInt32( codigocorp);
         empleado.codigo = Convert.ToInt32(codigo);
         empleado.empresa = empresa;
         empleado.estacion = estacion;
         empleado.area = area;
         empleado.departamento = departamento;
         empleado.codPuesto = puesto;
         empleado.salario =Convert.ToDecimal( salario);
         return View(empleado);


      }
      public async Task<ActionResult> trasladarEmpresa(string codigocorp, string codigo, string empresa, string estacion, string area, string departamento, string puesto, string salario)
      {
         eEmpleado empleado = new eEmpleado();
         empleado = await getOneEmpleaedo(codigocorp);
         empleado.codigoCorp = Convert.ToInt32(codigocorp);
         empleado.codigo = Convert.ToInt32(codigo);
         empleado.empresa = empresa;
         empleado.estacion = estacion;
         empleado.area = area;
         empleado.departamento = departamento;
         empleado.codPuesto = puesto;
         empleado.salario = Convert.ToDecimal(salario);
         return View(empleado);


      }
      public async Task<ActionResult> guardarEditar(eEmpleado empleado)
      {
         empleado.empresa = Session["empresaEmpleado"].ToString();
         empleado.estacion = Session["estacionEmpleado"].ToString();
         empleado.area = Session["areaEmpleado"].ToString();
         empleado.departamento = Session["DepartamentoEmpleado"].ToString();
         empleado.token = Session["token"].ToString();
         empleado.primerNombre = empleado.primerNombre.ToUpper();
         if (empleado.segundoNombre != null) { empleado.segundoNombre = empleado.segundoNombre.ToUpper(); }
         if (empleado.tercerNombre != null) { empleado.tercerNombre = empleado.tercerNombre.ToUpper(); }
         empleado.primerApellido = empleado.primerApellido.ToUpper();
         if (empleado.segundoApellido != null) { empleado.segundoApellido = empleado.segundoApellido.ToUpper(); }
         if (empleado.apellidoCasada != null) { empleado.apellidoCasada = empleado.apellidoCasada.ToUpper(); }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/editempleado", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public async Task<ActionResult> guardarTrasladar(eEmpleado empleado)
      {
         
         empleado.token = Session["token"].ToString();        
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/trasladarempleado", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public async Task<ActionResult> guardarTrasladarEmpresa(eEmpleado empleado)
      {

         empleado.token = Session["token"].ToString();
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/trasladarempleadoempresa", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public async Task<ActionResult> procesarEliminar(eEmpleado empleado)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();

         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            empleado.token = Session["token"].ToString();
            string empleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/delempleado", empleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public ActionResult eliminar(string codigo, string nombre)
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {
            eEmpleado empleado = new eEmpleado();
            empleado.codigoCorp =   Convert.ToInt32( codigo);
            empleado.primerNombre = nombre;
            return View(empleado);
         }

      }
      public async Task<ActionResult> getEmpresas()
      {
         List<eEmpresa> listEmpresas = new List<eEmpresa>();
         DataTable dtEmpresas = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eUsuario parUsuario = new eUsuario();
            parUsuario.aplicacion = ConfigurationManager.ConnectionStrings["keyapp"].ToString();
            parUsuario.usuariologin = Session["usuario"].ToString();
            string usuarioJSON = JsonConvert.SerializeObject(parUsuario);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empresa/getEmpresasXUsuario", usuarioJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpresas = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtEmpresas.Rows)
            {
               eEmpresa empresa = new eEmpresa();
               empresa.codigo = row["cod_empresa"].ToString();
               empresa.descripcion = row["razon_social"].ToString();
               listEmpresas.Add(empresa);
            }

         }
         catch
         {
            return null;
         }
         return Json(listEmpresas, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getSucursales(string codEmpresa)
      {
         List<eSucursal> listSucursales = new List<eSucursal>();
         DataTable dtSucursales = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eSucursal sucursal = new eSucursal();
            sucursal.codigoUsuario = Session["usuario"].ToString();
            sucursal.codigoEmpresa = codEmpresa;
            string sucursalJSON = JsonConvert.SerializeObject(sucursal);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("sucursal/getSucursalXUserXEmpre", sucursalJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtSucursales = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtSucursales.Rows)
            {
               eSucursal varsucursal = new eSucursal();
               varsucursal.codigoSucursal = row["cod_sucursal"].ToString();
               varsucursal.descripcion = row["descripcion"].ToString();
               listSucursales.Add(varsucursal);
            }

         }
         catch
         {
            return null;
         }
         return Json(listSucursales, JsonRequestBehavior.AllowGet);
      }


      public async Task<ActionResult> getAreaxUnidadNegocio(string codEmpresa, string codSucursal)
      {
         List<eArea> listArea = new List<eArea>();
         DataTable dtArea = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eArea area = new eArea();
            area.empresa = codEmpresa;
            area.sucursal = codSucursal;
            string areaJSON = JsonConvert.SerializeObject(area);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("area/getareaxunidadnegocio", areaJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtArea = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtArea.Rows)
            {
               eArea varArea = new eArea();
               varArea.codigo = row["cod_area"].ToString();
               varArea.descripcion = row["descripcion"].ToString();

               listArea.Add(varArea);
            }
         }
         catch
         {
            return null;
         }
         return Json(listArea, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> getDepartamentoxUnidadNegocioxArea(string codEmpresa, string codSucursal, string codArea)
      {
         List<eDepartamento> listDepartamento = new List<eDepartamento>();
         DataTable dtDepartamento = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eDepartamento Departamento = new eDepartamento();
            Departamento.empresa = codEmpresa;
            Departamento.sucursal = codSucursal;
            Departamento.area = codArea;
            string DepartamentoJSON = JsonConvert.SerializeObject(Departamento);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Departamento/getdepartamentoxunidadnegocioxarea", DepartamentoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtDepartamento = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtDepartamento.Rows)
            {
               eDepartamento varDepartamento = new eDepartamento();
               varDepartamento.codigo = row["cod_Departamento"].ToString();
               varDepartamento.descripcion = row["descripcion"].ToString();

               listDepartamento.Add(varDepartamento);
            }
         }
         catch
         {
            return null;
         }
         return Json(listDepartamento, JsonRequestBehavior.AllowGet);
      }

      public async Task<ActionResult> getPuestoxEmpresa()
      {
         List<ePuesto> listPuestos = new List<ePuesto>();
         DataTable dtPuestos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            ePuesto parPuesto = new ePuesto();
            parPuesto.codEmpresa = Session["empresaempleado"].ToString();
            string PuestoJSON = JsonConvert.SerializeObject(parPuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("puesto/getPuestoxEmpresa", PuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPuestos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Puestos
            foreach (DataRow row in dtPuestos.Rows)
            {
               ePuesto Puesto = new ePuesto();
               Puesto.codigo = row["cod_Puesto"].ToString();
               Puesto.descripcion = row["desc_puesto"].ToString();
               listPuestos.Add(Puesto);
            }

         }
         catch
         {
            return null;
         }
         return Json(listPuestos, JsonRequestBehavior.AllowGet);
      }
      // este metodo es para el traslado de empleados
      public async Task<ActionResult> getPuesto(string empresa)
      {
         List<ePuesto> listPuestos = new List<ePuesto>();
         DataTable dtPuestos = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            ePuesto parPuesto = new ePuesto();
            parPuesto.codEmpresa = empresa;
            string PuestoJSON = JsonConvert.SerializeObject(parPuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("puesto/getPuestoxEmpresa", PuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPuestos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Puestos
            foreach (DataRow row in dtPuestos.Rows)
            {
               ePuesto Puesto = new ePuesto();
               Puesto.codigo = row["cod_Puesto"].ToString();
               Puesto.descripcion = row["desc_puesto"].ToString();
               listPuestos.Add(Puesto);
            }

         }
         catch
         {
            return null;
         }
         return Json(listPuestos, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getEmpleadoxEmpresaxAreaXDepartamento()
      {
         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaEmpleado"].ToString();
            empleado.estacion = Session["estacionEmpleado"].ToString();
            empleado.area = Session["areaEmpleado"].ToString();
            empleado.departamento = Session["DepartamentoEmpleado"].ToString();
     
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getEmpleadoxEmpresaxAreaXDepartamento", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               eEmpleado Empleado = new eEmpleado();
               Empleado.codigoCorp = Convert.ToInt32( row["cod_empleado_corp"].ToString());
               Empleado.empresa = row["cod_empresa"].ToString();
               Empleado.codigo= Convert.ToInt32(row["cod_empleado"].ToString());
               Empleado.estacion = row["cod_estacion"].ToString();
               Empleado.area = row["cod_area"].ToString();
               Empleado.departamento = row["cod_departamento"].ToString();
               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();
               Empleado.codPuesto = row["cod_puesto"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal( row["salario_ordinario_mensual"].ToString());
               Empleado.fechaIngreso = Convert.ToDateTime(row["fecha_ingreso"].ToString());

               listEmpleados.Add(Empleado);
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = listEmpleados }, JsonRequestBehavior.AllowGet);
      }
      public async Task<List<eEmpleado>> getEmpleadoxEmpresaxUnidad()
      {
         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaEmpleado"].ToString();
            empleado.estacion = Session["estacionEmpleado"].ToString();
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getEmpleadoPresuxEmpresaxUnidad", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               eEmpleado Empleado = new eEmpleado();
               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());
            
               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();
               Empleado.descArea = row["desc_area"].ToString();
               Empleado.descDepartamento = row["desc_departamento"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario"].ToString());
               listEmpleados.Add(Empleado);
            }

         }
         catch
         {
            return null;
         }
         return listEmpleados;
      }

      public async Task<eEmpleado> getOneEmpleaedo(string codigo)
      {
         eEmpleado Empleado = new eEmpleado();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.codigoCorp = Convert.ToInt32( codigo);
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getOneEmpleado", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {

               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());

               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();

               Empleado.dpi = row["dpi"].ToString();
               Empleado.afiliacionIgss = row["afiliacion_igss"].ToString();
               Empleado.nit = row["nit"].ToString();
               Empleado.genero = row["genero"].ToString();
               Empleado.telefonoMovil = row["telefono_movil"].ToString();
               Empleado.telefonoCasa = row["telefono_casa"].ToString();
               Empleado.email = row["correo_electronico"].ToString();

                    if (row["fecha_nacimiento"].ToString() != "")
                    {
                        Empleado.fechaNacimiento = Convert.ToDateTime(row["fecha_nacimiento"].ToString());
                        ViewBag.nacimiento = Empleado.fechaNacimiento;
                    }

               Empleado.codPaisNacimiento = row["cod_pais_nacimiento"].ToString();
               Empleado.codDepartamentoNacimiento = row["cod_estadodepto_nacimiento"].ToString();
               Empleado.codMunicipioNacimiento = row["cod_municipio_nacimiento"].ToString();

               Empleado.codPaisDireccion = row["cod_pais_direccion"].ToString();
               Empleado.codDepartamentoDireccion = row["cod_estadodepto_Direccion"].ToString();
               Empleado.codMunicipioDireccion = row["cod_municipio_Direccion"].ToString();
               Empleado.direccion = row["direccion"].ToString();

               Empleado.peso = Convert.ToInt32(row["peso_libras"].ToString()); 
               Empleado.altura = Convert.ToInt32(row["altura_centimetros"].ToString());
               Empleado.tieneEnfermedadCronica = row["tiene_enfermedad_cronica"].ToString();
               if(Empleado.tieneEnfermedadCronica=="S")
               {
                  Empleado.enfermedadCronica = row["enfermedad_cronica"].ToString();
               }            
               
               Empleado.codPuesto = row["cod_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario_mensual"].ToString());
               if (row["fecha_ingreso"].ToString() != "")
               {
                  Empleado.fechaIngreso = Convert.ToDateTime(row["fecha_ingreso"].ToString());
                  ViewBag.ingreso = Empleado.fechaIngreso;
               }
            }

         }
         catch
         {
            return null;
         }
         return Empleado;
      }
      public async Task<eEmpleado> getBajaEmpleado(string codigo)
      {
         eEmpleado Empleado = new eEmpleado();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.codigoCorp = Convert.ToInt32(codigo);
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getbajaempleado", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {

               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());

               Empleado.primerNombre = row["nombre"].ToString();
               Empleado.descEmpresa = row["empresa"].ToString();
               Empleado.descEstacion = row["sucursal"].ToString();
               Empleado.fechaBaja =Convert.ToDateTime(  row["fecha_baja"].ToString());
               Empleado.descTipoBaja = row["desc_tipo_baja"].ToString();
               Empleado.observacion = row["observacion"].ToString();

            }

         }
         catch
         {
            return null;
         }
         return Empleado;
      }

      public async Task<ActionResult> bajaEmpleado(string id, string codigocorp)
      {
         eEmpleado empleado = await getBajaEmpleado(codigocorp);
         return View(empleado);
      }
      public async Task<eEmpleado> getEntorno()
      {
         eEmpleado Empleado = new eEmpleado();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaEmpleado"].ToString();
            empleado.estacion = Session["estacionEmpleado"].ToString();
            empleado.area = Session["areaEmpleado"].ToString();
            empleado.departamento = Session["DepartamentoEmpleado"].ToString();
            empleado.token = Session["token"].ToString();
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getEntorno", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               Empleado.descEmpresa = row["desc_empresa"].ToString();
               Empleado.descEstacion = row["desc_sucursal"].ToString();
               Empleado.descArea = row["desc_area"].ToString();
               Empleado.descDepartamento = row["desc_departamento"].ToString();
            }

         }
         catch
         {
            return null;
         }
         return Empleado;
      }
      // ---------------- para presupuesto
      public async Task<string> getAnioActual()
      {
         string anio = "";
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/getAnioActual", "");
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            DataTable dtSalida = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            foreach (DataRow row in dtSalida.Rows)
            {
               anio = row["anio"].ToString();
            }
         }
         catch
         {
            return null;
         }
         return anio;
      }
      public async Task<ActionResult> ingresarSalarios(eEmpleado empleado)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }

         Session["empresaempleado"] = empleado.empresa;
         Session["estacionempleado"] = empleado.estacion;
         eEmpleado varempleado = new eEmpleado();

         ViewBag.estacion = varempleado.descEstacion;

         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         listEmpleados = await getEmpleadoxEmpresaxUnidadPresu();

         if (TempData["retorno"] != null)
         {
            ViewBag.retorno = TempData["retorno"].ToString();
            ViewBag.mensaje = TempData["mensaje"].ToString();
         }
         return View(listEmpleados);
      }

      public async Task<ActionResult> guardarIngresarSalarios(List<eEmpleado> listEmpleado)
      {
         eEmpleado varEmpleado = new eEmpleado();
         string detalleEmpleado = libreria.genXMLSalariosEmpleados(listEmpleado);
         varEmpleado.empresa = Session["empresaempleado"].ToString();
         varEmpleado.estacion = Session["estacionempleado"].ToString();
         varEmpleado.token = Session["token"].ToString();
         varEmpleado.detalleXML = detalleEmpleado;
         varEmpleado.anioPresupuesto = Convert.ToInt32( await getAnioActual());
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string empleadoJSON = JsonConvert.SerializeObject(varEmpleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/editSalariosEmpleados", empleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);

         }
         catch
         {
            salida.retorno = -9999;
            salida.token = "";
            salida.mensaje = "No hay conexion con el servidor";
         }

         TempData["mensaje"] = salida.mensaje;
         TempData["retorno"] = salida.retorno;
         varEmpleado.detalleXML = "";
         return RedirectToAction("ingresarSalarios", "empleado", varEmpleado);
      }
      public async Task<ActionResult> listarPresu(eEmpleado empleado)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         Session["empresaempleado"] = empleado.empresa;
         Session["estacionempleado"] = empleado.estacion;
         Session["areaempleado"] = empleado.area;
         Session["Departamentoempleado"] = empleado.departamento;
         empleado = await getEntorno();
         ViewBag.empresa = empleado.descEmpresa;
         ViewBag.estacion = empleado.descEstacion;
         ViewBag.area = empleado.descArea;
         ViewBag.departamento = empleado.descDepartamento;
         return View();
      }

      public ActionResult nuevoPresu()
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {


            return View();
         }
      }
      public ActionResult entornoPresu()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         return View();

      }
      public async Task<ActionResult> guardarNuevoPresu(eEmpleado empleado)
      {

         empleado.empresa = Session["empresaEmpleado"].ToString();
         empleado.estacion = Session["estacionEmpleado"].ToString();
         empleado.area = Session["areaEmpleado"].ToString();
         empleado.departamento = Session["DepartamentoEmpleado"].ToString();
         empleado.token = Session["token"].ToString();
         empleado.primerNombre = empleado.primerNombre.ToUpper();
         empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
         if (empleado.segundoNombre != null) { empleado.segundoNombre = empleado.segundoNombre.ToUpper(); }
         if (empleado.tercerNombre != null) { empleado.tercerNombre = empleado.tercerNombre.ToUpper(); }
         empleado.primerApellido = empleado.primerApellido.ToUpper();
         if (empleado.segundoApellido != null) { empleado.segundoApellido = empleado.segundoApellido.ToUpper(); }
         if (empleado.apellidoCasada != null) { empleado.apellidoCasada = empleado.apellidoCasada.ToUpper(); }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/newempleadoPresu", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }

      public async Task<ActionResult> editarPresu(string codigo)
      {


         eEmpleado empleado = new eEmpleado();
         empleado = await getOneEmpleaedoPresu(codigo);
         return View(empleado);


      }

      public async Task<ActionResult> guardarEditarPresu(eEmpleado empleado)
      {


         empleado.departamento = Session["DepartamentoEmpleado"].ToString();
         empleado.token = Session["token"].ToString();
         empleado.primerNombre = empleado.primerNombre.ToUpper();
         empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
         if (empleado.segundoNombre != null) { empleado.segundoNombre = empleado.segundoNombre.ToUpper(); }
         if (empleado.tercerNombre != null) { empleado.tercerNombre = empleado.tercerNombre.ToUpper(); }
         empleado.primerApellido = empleado.primerApellido.ToUpper();
         if (empleado.segundoApellido != null) { empleado.segundoApellido = empleado.segundoApellido.ToUpper(); }
         if (empleado.apellidoCasada != null) { empleado.apellidoCasada = empleado.apellidoCasada.ToUpper(); }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/editempleadopresu", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }

      public async Task<ActionResult> procesarEliminarPresu(eEmpleado empleado)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         int retorno;
         string mensaje;
         eParSalida salida = new eParSalida();

         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            empleado.token = Session["token"].ToString();
            empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
            string empleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/delempleadopresu", empleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            retorno = salida.retorno;
            mensaje = salida.mensaje;
         }
         catch
         {
            retorno = -999;
            mensaje = "";
         }
         return new JsonResult { Data = new { retorno = retorno, mensaje = mensaje } };
      }
      public ActionResult eliminarPresu(string codigo, string nombre)
      {

         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         else
         {
            eEmpleado empleado = new eEmpleado();
            empleado.codigoCorp = Convert.ToInt32(codigo);
            empleado.primerNombre = nombre;
            return View(empleado);
         }

      }

      public async Task<eEmpleado> getOneEmpleaedoPresu(string codigo)
      {
         eEmpleado Empleado = new eEmpleado();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.codigoCorp = Convert.ToInt32(codigo);
            empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getOneEmpleadopresu", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {

               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());
               Empleado.empresa = row["cod_empresa"].ToString();
               Empleado.codigo = Convert.ToInt32(row["cod_empleado"].ToString());
               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();
               Empleado.codPuesto = row["cod_puesto"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario"].ToString());
            }

         }
         catch
         {
            return null;
         }
         return Empleado;
      }
      public async Task<ActionResult> getEmpleadoxEmpresaxAreaXDepartamentoPresu()
      {
         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaEmpleado"].ToString();
            empleado.estacion = Session["estacionEmpleado"].ToString();
            empleado.area = Session["areaEmpleado"].ToString();
            empleado.departamento = Session["DepartamentoEmpleado"].ToString();
            empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getEmpleadoxEmpresaxAreaXDepartamentoPresu", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               eEmpleado Empleado = new eEmpleado();
               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());
               Empleado.empresa = row["cod_empresa"].ToString();
               Empleado.codigo = Convert.ToInt32(row["cod_empleado"].ToString());
               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();
               Empleado.codPuesto = row["cod_puesto"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario"].ToString());
               listEmpleados.Add(Empleado);
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = listEmpleados }, JsonRequestBehavior.AllowGet);
      }

      public async Task<List<eEmpleado>> getEmpleadoxEmpresaxUnidadPresu()
      {
         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaEmpleado"].ToString();
            empleado.estacion = Session["estacionEmpleado"].ToString();
            empleado.anioPresupuesto = Convert.ToInt32(await getAnioActual());
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/getEmpleadoxEmpresaxUnidadPresu", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               eEmpleado Empleado = new eEmpleado();
               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());

               Empleado.primerNombre = row["primer_nombre"].ToString();
               Empleado.segundoNombre = row["segundo_nombre"].ToString();
               Empleado.tercerNombre = row["tercer_nombre"].ToString();
               Empleado.primerApellido = row["primer_apellido"].ToString();
               Empleado.segundoApellido = row["segundo_apellido"].ToString();
               Empleado.apellidoCasada = row["apellido_de_casada"].ToString();
               Empleado.descArea = row["desc_area"].ToString();
               Empleado.descDepartamento = row["desc_departamento"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario"].ToString());
               listEmpleados.Add(Empleado);
            }

         }
         catch
         {
            return null;
         }
         return listEmpleados;
      }

      //  ------------- informes empleados RRHH
      public async Task<DataTable> getInformeEmpleadoxDepartamento(eEmpleado empleado)
      {
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/getInformeEmpleadoxDeparamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtEmpleados;
      }
      public async Task<DataTable> getInformeEmpleadoxArea(eEmpleado empleado)
      {
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/getInformeEmpleadoxArea", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtEmpleados;
      }
      public async Task<DataTable> getInformeEmpleadoxUnidad(eEmpleado empleado)
      {
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/getInformeEmpleadoxUnidad", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtEmpleados;
      }
      public async Task<DataTable> getInformeEmpleadoxEmpresa(eEmpleado empleado)
      {
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/getInformeEmpleadoxEmpresa", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtEmpleados;
      }

      public async Task<ActionResult> VerInformeEmpleadosxDepartamento()
      {

         try
         {
            DataTable dtInforme = new DataTable();

            eEmpleado empleado = new eEmpleado();         
            empleado.empresa = Session["empresaempleado"].ToString();
            empleado.estacion = Session["estacionempleado"].ToString();            
            empleado.area = Session["areaempleado"].ToString();
            empleado.departamento = Session["Departamentoempleado"].ToString();
            dtInforme = await getInformeEmpleadoxDepartamento(empleado);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_EmpleadoXEmpreXUNXAreaXDepto.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Informe_empleado_departamento-" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch (Exception ex)
         {
            return JavaScript("alert('No hay informe para mostrar, por favor verifique el tipo de empleado sea el correcto');");
         }
      }
      public async Task<ActionResult> VerInformeEmpleadosxDepartamento2(eEmpleado empleado)
      {

         try
         {
            DataTable dtInforme = new DataTable();

         
            dtInforme = await getInformeEmpleadoxDepartamento(empleado);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_EmpleadoXEmpreXUNXAreaXDepto.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Informe_empleado_departamento-" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch (Exception ex)
         {
            return JavaScript("alert('No hay informe para mostrar, por favor verifique el tipo de empleado sea el correcto');");
         }
      }
      public async Task<ActionResult> VerInformeEmpleadosxArea()
      {

         try
         {
            DataTable dtInforme = new DataTable();

            eEmpleado empleado = new eEmpleado();
            empleado.empresa = Session["empresaempleado"].ToString();
            empleado.estacion = Session["estacionempleado"].ToString();
            empleado.area = Session["areaempleado"].ToString();    
            dtInforme = await getInformeEmpleadoxArea(empleado);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoOpexEmpreXUNXAreaXDepto.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Informe_empleado_area-" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch
         {
            return JavaScript("alert('No hay informe para mostrar, por favor verifique el tipo de empleado sea el correcto');");
         }
      }
      public async Task<DataTable> getInfoAllEmpleados(eEmpleado empleado)
      {
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("empleado/infoAllEmpleados", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtEmpleados;
      }
     
      public async Task<ActionResult> VerInformeEmpleados(eEmpleado empleado)
      {
         try
         {
            String filtro="A";
            DataTable dtInforme = new DataTable();                
            dtInforme = await getInfoAllEmpleados(empleado);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_AllEmpleados.rpt"));
            if (empleado.bajaEmpleado == true)
                {
                    rd.DataDefinition.RecordSelectionFormula = "{info_all_emple.estado}='B'";
                    filtro = "B";
                }
            rd.DataDefinition.FormulaFields["estado_empleado"].Text = "'" + filtro + "'";
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Informe_empleados-" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch (Exception ex)
         {
            return View("errorInformeMovimientoEfectivo");
         }
      }
      public async Task<ActionResult> getAllEmpleados()
      {
         List<eEmpleado> listEmpleados = new List<eEmpleado>();
         DataTable dtEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eEmpleado empleado = new eEmpleado();
            empleado.empresa = "00000";
            empleado.estacion = "0000";
            empleado.area = "000";
            empleado.departamento = "000";
            string EmpleadoJSON = JsonConvert.SerializeObject(empleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Empleado/infoAllEmpleados", EmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de Empleados
            foreach (DataRow row in dtEmpleados.Rows)
            {
               eEmpleado Empleado = new eEmpleado();
               Empleado.codigoCorp = Convert.ToInt32(row["cod_empleado_corp"].ToString());
               Empleado.nombreCompleto = row["nombre_completo"].ToString();
               Empleado.descEmpresa = row["desc_empresa"].ToString();
               Empleado.descEstacion = row["desc_sucursal"].ToString();
               Empleado.descArea = row["desc_area"].ToString();
               Empleado.descDepartamento = row["desc_departamento"].ToString();
               Empleado.descPuesto = row["desc_puesto"].ToString();
               Empleado.salario = Convert.ToDecimal(row["salario_ordinario_mensual"].ToString());
               Empleado.fechaIngreso = Convert.ToDateTime( row["fecha_ingreso"].ToString());
               Empleado.estado = row["estado"].ToString();
               Empleado.dpi = row["dpi"].ToString();
               Empleado.direccion = row["direccion"].ToString();
               try { Empleado.StrfechaNacimiento = row["fecha_nacimiento"].ToString().Substring(0, 10); } catch { };
               try { Empleado.StrfechaBaja = row["fecha_baja"].ToString().Substring(0, 10); } catch { };
               Empleado.telefonoMovil = row["telefono_movil"].ToString();
               listEmpleados.Add(Empleado);
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = listEmpleados }, JsonRequestBehavior.AllowGet);
      }

      public ActionResult busqueda()
      {
         return View();
      }
   }
}