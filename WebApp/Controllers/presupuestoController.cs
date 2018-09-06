using Entidades;
using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApp.Controllers
{
    public class presupuestoController : Controller
    {
        // GET: presupuesto
        public ActionResult Index()
        {
            return View();
        }

      public async Task<ActionResult> entornoOpex()
      {         
         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoOpexjecutado()
      {         
         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoInfoOpexxDepartamento()
      {

         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoInfoOpexxArea()
      {

         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoInfoOpexxUnidadNegocio()
      {

         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoInfoOpexxEmpresa()
      {

         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoING()
      {
         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public async Task<ActionResult> entornoINGretail()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
      public ActionResult calcularprestacionlaboral()
      {
         return View();
      }
      public async Task<ActionResult> entornoInfoIngreRetail()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         Session["aniopresupuesto"] = await getAnioActual();
         return View();
      }
        public async Task<ActionResult> entornoInfoIngreVolxUnidadNegocio()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }

        public async Task<ActionResult> entornoInfoTotalRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }

        public async Task<ActionResult> entornoInfoTotalPresupuesto()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }

        public async Task<ActionResult> entornoInfoCorporativoRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }
        public async Task<ActionResult> entornoDashboardCorporativoRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }
        public async Task<ActionResult> entornoInfoSubCorporativoRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }
        public async Task<ActionResult> entornoInfoEmpresaRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }

        public async Task<ActionResult> entornoInfoSucursalRetail()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            Session["aniopresupuesto"] = await getAnioActual();
            return View();
        }
        public async Task<ActionResult> establecerPrecioMargen(String id, String parAnio, String parEmpresa, String parEstacion, String parTipoPresto, String parArea, String parDepto )
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            // guardo en las variable session el entorno del presupuesto
            Session["aniopresupuesto"] = parAnio;
            Session["empresaPresupuesto"] = parEmpresa;
            Session["estacionPresupuesto"] = parEstacion;
            Session["tipoPresupuesto"] = parTipoPresto;
            Session["areaPresupuesto"] = parArea;
            Session["DepartamentoPresupuesto"] = parDepto;
            ePresupuesto presupuesto = new ePresupuesto();
            presupuesto.anio = Convert.ToInt32(parAnio);
            presupuesto.empresa = parEmpresa;
            presupuesto.estacion = parEstacion;
            presupuesto.tipoPresupuesto = parTipoPresto;
            presupuesto.area = parArea;
            presupuesto.departamento = parDepto;

            ePresupuesto entornoPresupuesto = new ePresupuesto();
            entornoPresupuesto = await getEncabezadoPresupuestoIngresoDepartamento(presupuesto);
            string empresa = entornoPresupuesto.descEmpresa;
            string estacion = entornoPresupuesto.descEstacion;
            string area = entornoPresupuesto.descArea;
            string departamento = entornoPresupuesto.descDepartamento;
            string tipopresupuesto = entornoPresupuesto.tipoPresupuesto;
            ViewBag.empresa = empresa;
            ViewBag.estacion = estacion;
            ViewBag.area = area;
            ViewBag.departamento = departamento;
            ViewBag.tipopresupuesto = tipopresupuesto;
            List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
            listPresupuesto = await getDetallePresupuestoIngresoDepartamento(presupuesto);
            if (TempData["retorno"] != null)
            {
                ViewBag.retorno = TempData["retorno"].ToString();
                ViewBag.mensaje = TempData["mensaje"].ToString();
            }
            return View(listPresupuesto);
        }
        public ActionResult establecerPrecioMargenDistro()
        {
            return View();
        }

        public async Task<ActionResult> procesarcalcularPL(ePresupuesto varpresupuesto)
      {                  
         varpresupuesto.token = Session["token"].ToString();
         varpresupuesto.anioPresupuesto = Convert.ToInt32(await getAnioActual());
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/calculaPrestacionLaboral", presupuestoJSON);
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
         ViewBag.mensaje = salida.mensaje;
         ViewBag.retorno = salida.retorno;
         return View();
      }
      public async Task<ActionResult> procesarSetPrecioMargen(List<ePresupuesto> listPresupuesto)
      {
            ePresupuesto varpresupuesto = new ePresupuesto();
            string detallePresupuesto = libreria.genXMLSetCostMrgenTransptoIngRetail(listPresupuesto);
            varpresupuesto.empresa = Session["empresaPresupuesto"].ToString();
            varpresupuesto.estacion = Session["estacionPresupuesto"].ToString();
            varpresupuesto.area = Session["areaPresupuesto"].ToString();
            varpresupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
            varpresupuesto.anioPresupuesto = Convert.ToInt32(Session["aniopresupuesto"].ToString());
            varpresupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
            varpresupuesto.detalleXML = detallePresupuesto;
            varpresupuesto.token = Session["token"].ToString();

            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/setPrecioMargenIngresoxUnidad", presupuestoJSON);
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

            ViewBag.mensaje = salida.mensaje;
            ViewBag.retorno = salida.retorno;
            varpresupuesto.token = "";
            varpresupuesto.detalleXML = "";
            return View("entornoINGretail");
        }

        public async Task<ActionResult> procesarSetPrecioMargenDistro(ePresupuesto varpresupuesto) //copia para Distribuidora
        {
            varpresupuesto.token = Session["token"].ToString();
            varpresupuesto.anioPresupuesto = Convert.ToInt32(await getAnioActual());
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/setPrecioMargenIngresoDistro", presupuestoJSON);
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
            ViewBag.mensaje = salida.mensaje;
            ViewBag.retorno = salida.retorno;
            return View();
        }

        public async Task<ActionResult> verPresupuestoOpex(ePresupuesto presupuesto)
      {

         // guardo en las variable session el entorno del presupuesto
         Session["aniopresupuesto"] = presupuesto.anio;
         Session["empresaPresupuesto"] = presupuesto.empresa;
         Session["estacionPresupuesto"] = presupuesto.estacion;
         Session["tipoPresupuesto"] = presupuesto.tipoPresupuesto;
         Session["areaPresupuesto"] = presupuesto.area;
         Session["DepartamentoPresupuesto"] = presupuesto.departamento;
         ePresupuesto entornoPresupuesto = new ePresupuesto();
         entornoPresupuesto = await getEncabezadoPresupuestoDepartamento(presupuesto);
         string empresa = entornoPresupuesto.descEmpresa;
         string estacion = entornoPresupuesto.descEstacion;
         string area = entornoPresupuesto.descArea;
         string departamento = entornoPresupuesto.descDepartamento;
         string tipopresupuesto = entornoPresupuesto.tipoPresupuesto;
         ViewBag.empresa = empresa;
         ViewBag.estacion = estacion;
         ViewBag.area = area;
         ViewBag.departamento = departamento;
         ViewBag.tipopresupuesto = tipopresupuesto;
         List <ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         listPresupuesto = await getDetallePresupuestoDepartamento(presupuesto);
         if (TempData["retorno"] != null)
         {
            ViewBag.retorno = TempData["retorno"].ToString();
            ViewBag.mensaje = TempData["mensaje"].ToString();
         }
         return View(listPresupuesto);
      }
      public async Task<ActionResult> verPresupuestoOpexEjecutado(ePresupuesto presupuesto)
      {

         // guardo en las variable session el entorno del presupuesto
         Session["aniopresupuesto"] = presupuesto.anio;
         Session["empresaPresupuesto"] = presupuesto.empresa;
         Session["estacionPresupuesto"] = presupuesto.estacion;
         Session["tipoPresupuesto"] = presupuesto.tipoPresupuesto;
         Session["areaPresupuesto"] = presupuesto.area;
         Session["DepartamentoPresupuesto"] = presupuesto.departamento;
         ePresupuesto entornoPresupuesto = new ePresupuesto();
         entornoPresupuesto = await getEncabezadoPresupuestoDepartamento(presupuesto);
         string empresa = entornoPresupuesto.descEmpresa;
         string estacion = entornoPresupuesto.descEstacion;
         string area = entornoPresupuesto.descArea;
         string departamento = entornoPresupuesto.descDepartamento;
         string tipopresupuesto = entornoPresupuesto.tipoPresupuesto;
         ViewBag.empresa = empresa;
         ViewBag.estacion = estacion;
         ViewBag.area = area;
         ViewBag.departamento = departamento;
         ViewBag.tipopresupuesto = tipopresupuesto;
         List <ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         listPresupuesto = await getDetallePresupuestoEjecutadoDepartamento(presupuesto);
         if (TempData["retorno"] != null)
         {
            ViewBag.retorno = TempData["retorno"].ToString();
            ViewBag.mensaje = TempData["mensaje"].ToString();
         }
         return View(listPresupuesto);
      }
      public async Task<ActionResult> verPresupuestoING(ePresupuesto presupuesto)
      {

         // guardo en las variable session el entorno del presupuesto
         Session["aniopresupuesto"] = presupuesto.anio;
         Session["empresaPresupuesto"] = presupuesto.empresa;
         Session["estacionPresupuesto"] = presupuesto.estacion;
         Session["tipoPresupuesto"] = presupuesto.tipoPresupuesto;
         Session["areaPresupuesto"] = presupuesto.area;
         Session["DepartamentoPresupuesto"] = presupuesto.departamento;
         ePresupuesto entornoPresupuesto = new ePresupuesto();
         entornoPresupuesto = await getEncabezadoPresupuestoIngresoDepartamento(presupuesto);
         string empresa = entornoPresupuesto.descEmpresa;
         string estacion = entornoPresupuesto.descEstacion;
         string area = entornoPresupuesto.descArea;
         string departamento = entornoPresupuesto.descDepartamento;
         string tipopresupuesto = entornoPresupuesto.tipoPresupuesto;
         ViewBag.empresa = empresa;
         ViewBag.estacion = estacion;
         ViewBag.area = area;
         ViewBag.departamento = departamento;
         ViewBag.tipopresupuesto = tipopresupuesto;
         List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         listPresupuesto = await getDetallePresupuestoIngresoDepartamento(presupuesto);
         if (TempData["retorno"] != null)
         {
            ViewBag.retorno = TempData["retorno"].ToString();
            ViewBag.mensaje = TempData["mensaje"].ToString();
         }
         return View(listPresupuesto);
      }
        
        public async Task<ActionResult> guardarPresupuestoOpex(List<ePresupuesto> listPresupuesto)
      {
         ePresupuesto varpresupuesto = new ePresupuesto();
         string detallePresupuesto = libreria.genXMLPrsupuesto(listPresupuesto);
         varpresupuesto.anio = Convert.ToInt32(Session["aniopresupuesto"].ToString());
         varpresupuesto.empresa = Session["empresaPresupuesto"].ToString();
         varpresupuesto.estacion = Session["estacionPresupuesto"].ToString();
         varpresupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
         varpresupuesto.area = Session["areaPresupuesto"].ToString();
         varpresupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
         varpresupuesto.detalleXML = detallePresupuesto;
         varpresupuesto.token = Session["token"].ToString();


         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/editPresupuesto", presupuestoJSON);
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
         varpresupuesto.token = "";
         varpresupuesto.detalleXML = "";
         return RedirectToAction("verPresupuestoOpex", "presupuesto" ,varpresupuesto );
      }

      public async Task<ActionResult> guardarPresupuestoOpexEjecutado(List<ePresupuesto> listPresupuesto)
      {
         ePresupuesto varpresupuesto = new ePresupuesto();
         string detallePresupuesto = libreria.genXMLPresupuestoOpexEjecutado(listPresupuesto);
         varpresupuesto.anio = Convert.ToInt32(Session["aniopresupuesto"].ToString());
         varpresupuesto.empresa = Session["empresaPresupuesto"].ToString();
         varpresupuesto.estacion = Session["estacionPresupuesto"].ToString();
         varpresupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
         varpresupuesto.area = Session["areaPresupuesto"].ToString();
         varpresupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
         varpresupuesto.detalleXML = detallePresupuesto;
         varpresupuesto.token = Session["token"].ToString();


         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/editPresupuestoEjecutado", presupuestoJSON);
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
         varpresupuesto.token = "";
         varpresupuesto.detalleXML = "";
         return RedirectToAction("verPresupuestoOpexEjecutado", "presupuesto" ,varpresupuesto );
      }
      public async Task<ActionResult> guardarPresupuestoING(List<ePresupuesto> listPresupuesto)
      {
         ePresupuesto varpresupuesto = new ePresupuesto();
         string detallePresupuesto = libreria.genXMLPrsupuestoIngreso(listPresupuesto);
         varpresupuesto.anio = Convert.ToInt32(Session["aniopresupuesto"].ToString());
         varpresupuesto.empresa = Session["empresaPresupuesto"].ToString();
         varpresupuesto.estacion = Session["estacionPresupuesto"].ToString();
         varpresupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
         varpresupuesto.area = Session["areaPresupuesto"].ToString();
         varpresupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
         varpresupuesto.detalleXML = detallePresupuesto;
         varpresupuesto.token = Session["token"].ToString();


         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string presupuestoJSON = JsonConvert.SerializeObject(varpresupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/editPresupuestoIngreso", presupuestoJSON);
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
         varpresupuesto.token = "";
         varpresupuesto.detalleXML = "";
         return RedirectToAction("verPresupuestoING", "presupuesto", varpresupuesto);
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

      public async Task<ActionResult> getTipoPresupuesto()
      {
         List<eTipoPresupuesto> listTipoPresupuesto = new List<eTipoPresupuesto>();
         DataTable dtTipoPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eTipoPresupuesto tipoPresupuesto = new eTipoPresupuesto();     
            string tipoPresupuestoJSON = JsonConvert.SerializeObject(tipoPresupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("tipopresupuesto/gettipopresupuesto",tipoPresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtTipoPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtTipoPresupuesto.Rows)
            {
               eTipoPresupuesto presupuesto = new eTipoPresupuesto();
               presupuesto.codigo = row["cod_tipo_presupuesto"].ToString();
              
               listTipoPresupuesto.Add(presupuesto);
            }
         }
         catch
         {
            return null;
         }
         return Json(listTipoPresupuesto, JsonRequestBehavior.AllowGet);
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
      public async Task<string> getAnioActual()
      {
         string anio=""; 
         try
         {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());          
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("presupuesto/getAnioActual","");
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
             DataTable dtSalida = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            foreach (DataRow row in dtSalida.Rows)
            {               
               anio= row["anio"].ToString();                       
            }
         }
         catch
         {
            return null;
         }
         return anio;
      }

      public async Task<List<ePresupuesto>> getDetallePresupuestoDepartamento(ePresupuesto presupuesto)
      {

         List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getDetallePresupuestoDepartamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtPresupuesto.Rows)
            {
               ePresupuesto varPresupuesto = new ePresupuesto();
               varPresupuesto.CodcuentaContable = row["cod_cuenta_contable"].ToString();
               varPresupuesto.DesccuentaContable = row["desc_cuentacontable"].ToString();
               varPresupuesto.pres01 = Convert.ToDouble( row["pres_01"].ToString());
               varPresupuesto.pres02 = Convert.ToDouble(row["pres_02"].ToString());
               varPresupuesto.pres03 = Convert.ToDouble(row["pres_03"].ToString());
               varPresupuesto.pres04 = Convert.ToDouble(row["pres_04"].ToString());
               varPresupuesto.pres05 = Convert.ToDouble(row["pres_05"].ToString());
               varPresupuesto.pres06 = Convert.ToDouble(row["pres_06"].ToString());
               varPresupuesto.pres07 = Convert.ToDouble(row["pres_07"].ToString());
               varPresupuesto.pres08 = Convert.ToDouble(row["pres_08"].ToString());
               varPresupuesto.pres09 = Convert.ToDouble(row["pres_09"].ToString());
               varPresupuesto.pres10 = Convert.ToDouble(row["pres_10"].ToString());
               varPresupuesto.pres11 = Convert.ToDouble(row["pres_11"].ToString());
               varPresupuesto.pres12 = Convert.ToDouble(row["pres_12"].ToString());


               listPresupuesto.Add(varPresupuesto);
            }
         }
         catch
         {
            return null;
         }
         return listPresupuesto;
      }

      public async Task<List<ePresupuesto>> getDetallePresupuestoEjecutadoDepartamento(ePresupuesto presupuesto)
      {

         List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getDetallePresupuestoEjecutadoDepartamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtPresupuesto.Rows)
            {
               ePresupuesto varPresupuesto = new ePresupuesto();
               varPresupuesto.CodcuentaContable = row["cod_cuenta_contable"].ToString();
               varPresupuesto.DesccuentaContable = row["desc_cuentacontable"].ToString();
               varPresupuesto.ejec01 = Convert.ToDouble( row["ejec_01"].ToString());
               varPresupuesto.ejec02 = Convert.ToDouble(row["ejec_02"].ToString());
               varPresupuesto.ejec03 = Convert.ToDouble(row["ejec_03"].ToString());
               varPresupuesto.ejec04 = Convert.ToDouble(row["ejec_04"].ToString());
               varPresupuesto.ejec05 = Convert.ToDouble(row["ejec_05"].ToString());
               varPresupuesto.ejec06 = Convert.ToDouble(row["ejec_06"].ToString());
               varPresupuesto.ejec07 = Convert.ToDouble(row["ejec_07"].ToString());
               varPresupuesto.ejec08 = Convert.ToDouble(row["ejec_08"].ToString());
               varPresupuesto.ejec09 = Convert.ToDouble(row["ejec_09"].ToString());
               varPresupuesto.ejec10 = Convert.ToDouble(row["ejec_10"].ToString());
               varPresupuesto.ejec11 = Convert.ToDouble(row["ejec_11"].ToString());
               varPresupuesto.ejec12 = Convert.ToDouble(row["ejec_12"].ToString());


               listPresupuesto.Add(varPresupuesto);
            }
         }
         catch
         {
            return null;
         }
         return listPresupuesto;
      }

      public async Task<List<ePresupuesto>> getDetallePresupuestoIngresoDepartamento(ePresupuesto presupuesto)
      {

         List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getDetallePresupuestoIngresoDepartamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtPresupuesto.Rows)
            {
               ePresupuesto varPresupuesto = new ePresupuesto();
               varPresupuesto.claseProducto = row["cod_clase"].ToString();
               varPresupuesto.familiaProducto= row["cod_familia"].ToString();
               varPresupuesto.codProducto= row["cod_producto"].ToString();
               varPresupuesto.descProducto= row["desc_producto"].ToString();
               varPresupuesto.pres01 = Convert.ToDouble(row["pres_01"].ToString());
               varPresupuesto.pres02 = Convert.ToDouble(row["pres_02"].ToString());
               varPresupuesto.pres03 = Convert.ToDouble(row["pres_03"].ToString());
               varPresupuesto.pres04 = Convert.ToDouble(row["pres_04"].ToString());
               varPresupuesto.pres05 = Convert.ToDouble(row["pres_05"].ToString());
               varPresupuesto.pres06 = Convert.ToDouble(row["pres_06"].ToString());
               varPresupuesto.pres07 = Convert.ToDouble(row["pres_07"].ToString());
               varPresupuesto.pres08 = Convert.ToDouble(row["pres_08"].ToString());
               varPresupuesto.pres09 = Convert.ToDouble(row["pres_09"].ToString());
               varPresupuesto.pres10 = Convert.ToDouble(row["pres_10"].ToString());
               varPresupuesto.pres11 = Convert.ToDouble(row["pres_11"].ToString());
               varPresupuesto.pres12 = Convert.ToDouble(row["pres_12"].ToString());


               listPresupuesto.Add(varPresupuesto);
            }
         }
         catch
         {
            return null;
         }
         return listPresupuesto;
      }

        public async Task<List<ePresupuesto>> getProductosPrestoIngreEmpresaDepartamento(ePresupuesto presupuesto)
        {

            List<ePresupuesto> listPresupuesto = new List<ePresupuesto>();
            DataTable dtPresupuesto = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getProductosPrestoIngreEmpresaDepartamento", PresupuestoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtPresupuesto.Rows)
                {
                    ePresupuesto varPresupuesto = new ePresupuesto();
                    varPresupuesto.claseProducto = row["cod_clase"].ToString();
                    varPresupuesto.familiaProducto = row["cod_familia"].ToString();
                    varPresupuesto.codProducto = row["cod_producto"].ToString();
                    varPresupuesto.descProducto = row["desc_producto"].ToString();
                    varPresupuesto.precioPublico = Convert.ToDecimal(row["precio_publico"].ToString());
                    varPresupuesto.margenUtilidad = Convert.ToDecimal(row["margen_utilidad"].ToString());
                    varPresupuesto.precioTransporte = Convert.ToDecimal(row["precio_transporte"].ToString());
                    listPresupuesto.Add(varPresupuesto);
                }
            }
            catch
            {
                return null;
            }
            return listPresupuesto;
        }

        public async Task<ePresupuesto> getEncabezadoPresupuestoDepartamento(ePresupuesto presupuesto)
      {

         ePresupuesto varPresupuesto = new ePresupuesto();
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getEncabezadoPresupuestoDepartamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtPresupuesto.Rows)
            {              
               varPresupuesto.descEmpresa = row["desc_empresa"].ToString();
               varPresupuesto.descEstacion = row["desc_sucursal"].ToString();
               varPresupuesto.descArea = row["desc_area"].ToString();
               varPresupuesto.descDepartamento= row["desc_departamento"].ToString();
               varPresupuesto.tipoPresupuesto = row["cod_tipo_presupuesto"].ToString();
               varPresupuesto.anio = Convert.ToInt32( row["anio"].ToString());
            }
         }
         catch
         {
            return null;
         }
         return varPresupuesto;
      }
      public async Task<ePresupuesto> getEncabezadoPresupuestoIngresoDepartamento(ePresupuesto presupuesto)
      {

         ePresupuesto varPresupuesto = new ePresupuesto();
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getEncabezadoPresupuestoIngresoDepartamento", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtPresupuesto.Rows)
            {
               varPresupuesto.descEmpresa = row["desc_empresa"].ToString();
               varPresupuesto.descEstacion = row["desc_sucursal"].ToString();
               varPresupuesto.descArea = row["desc_area"].ToString();
               varPresupuesto.descDepartamento = row["desc_departamento"].ToString();
               varPresupuesto.tipoPresupuesto = row["cod_tipo_presupuesto"].ToString();
               varPresupuesto.anio = Convert.ToInt32(row["anio"].ToString());
            }
         }
         catch
         {
            return null;
         }
         return varPresupuesto;
      }


      //  --------------- INFORMES ------------------------------------------
      public async Task<ActionResult> VerInformePresupuestoOpex()
      {

         try
         {
            DataTable dtInforme = new DataTable();
            String nombreReporte = "Informe_Presupuesto_departamento-";
            ePresupuesto presupuesto = new ePresupuesto();
            presupuesto.anio = Convert.ToInt32(Session["aniopresupuesto"].ToString());
            presupuesto.empresa = Session["empresaPresupuesto"].ToString();
            presupuesto.estacion = Session["estacionPresupuesto"].ToString();
            presupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
            presupuesto.area = Session["areaPresupuesto"].ToString();
            presupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
            dtInforme = await getInformePresupuestoOpex(presupuesto);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoEmpreXUNXAreaXDepto.rpt"));
            rd.SetDataSource(dtInforme);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
         } catch
         {
           return JavaScript("alert('No hay informe para mostrar, por favor verifique el tipo de presupuesto sea opex');");
         }
      }

      public async Task<ActionResult> VerInformePresupuestoEjecutadoOpex()// PENDIENTE
      {

         try
         {
            DataTable dtInforme = new DataTable();
            String nombreReporte = "Informe_Presupuesto_departamento-";
            ePresupuesto presupuesto = new ePresupuesto();
            presupuesto.anio = Convert.ToInt32(Session["aniopresupuesto"].ToString());
            presupuesto.empresa = Session["empresaPresupuesto"].ToString();
            presupuesto.estacion = Session["estacionPresupuesto"].ToString();
            presupuesto.tipoPresupuesto = Session["tipoPresupuesto"].ToString();
            presupuesto.area = Session["areaPresupuesto"].ToString();
            presupuesto.departamento = Session["DepartamentoPresupuesto"].ToString();
            dtInforme = await getInformePresupuestoOpex(presupuesto);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoEmpreXUNXAreaXDepto.rpt"));
            rd.SetDataSource(dtInforme);

            Response.Buffer = false;
            Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
            } catch
         {
           return JavaScript("alert('No hay informe para mostrar, por favor verifique el tipo de presupuesto sea opex');");
         }
      }
      public async Task<ActionResult> VerInformePresupuestoOpexDepartamento(ePresupuesto presupuesto)
      {

         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoOpex(presupuesto);
            String nombreReporte = "Informe_Presupuesto_departamento-";

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoEmpreXUNXAreaXDepto.rpt"));
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                
                //verifica si se ha solicitado mostrar solo las cuentas con movimiento
                if (presupuesto.cuentaMovimientoVacia == true)
                {
                    rd.DataDefinition.RecordSelectionFormula = "{info_detprestoopexareaxdepto.pres_total}>0";
                }
                rd.SetDataSource(dtInforme);
                
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
         }
         catch(Exception ex)

         {
            return View("errorInformeOpex");
         }      
      }
      public async Task<ActionResult> VerInformePresupuestoOpexxArea(ePresupuesto presupuesto)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoOpexxArea(presupuesto);
            String nombreReporte = "Informe_Presupuesto_area-";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoOpexEmpreXUNXArea.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
         } catch
         {
            return View("errorInformeOpex");
         }

      }
      public async Task<ActionResult> VerInformePresupuestoOpexxUnidad(ePresupuesto presupuesto)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoOpexxUnidad(presupuesto);
            String nombreReporte = "Informe_Presupuesto_unidad_de_negocio-";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoOpexEmpreXUN.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
         }
         catch
         {
            return View("errorInformeOpex");
         }
      }
      public async Task<ActionResult> VerInformePresupuestoOpexxEmpresa(ePresupuesto presupuesto)
      {
         try { 
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoOpexxEmpresa(presupuesto);
            String nombreReporte = "Informe_Presupuesto_Empresa-";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoOpexEmpre.rpt"));
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
                
         }
         catch
         {
            return View("errorInformeOpex");
         }
      }
      public async Task<DataTable> getInformePresupuestoOpex(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuesto", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformePresupuestoOpexxArea(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuestoxarea", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformePresupuestoOpexxUnidad(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuestoxunidad", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformePresupuestoOpexxEmpresa(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuestoxempresa", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

        public async Task<ActionResult> VerInformePresupuestoIngreRetailxUneg(ePresupuesto presupuesto)
      {

         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoIngrexVolumxEmpxSuc(presupuesto);

            DataTable dtInformeC1 = new DataTable();
            dtInformeC1 = await getInformePresupuestoIngreC1xEmpxSuc(presupuesto);

            DataTable dtInformeC2 = new DataTable();
            dtInformeC2 = await getInformePresupuestoIngreC2xEmpxSuc(presupuesto);

            DataTable dtInformeFlete = new DataTable();
            dtInformeFlete = await getInformePresupuestoIngreFletexEmpxSuc(presupuesto);

            DataTable dtInformeC3 = new DataTable();
            dtInformeC3 = await getInformePresupuestoIngreC3xEmpxSuc(presupuesto);

           

            DataTable dtInformeC4 = new DataTable();
            dtInformeC4 = await getInformePresupuestoIngreC4xEmpxSuc(presupuesto);

            DataTable dtInformeTotal = new DataTable();
            dtInformeTotal = await getInformeTotPresupuestoIngreAnualxUneg(presupuesto);
            String nombreReporte = "Informe_Presupuesto_Ingreso_Retail_Uneg-";

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoIngrRetailXEmpreXUN.rpt"));
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                
            rd.SetDataSource(dtInforme);
            rd.Subreports["PrestoIngrRetailC1"].SetDataSource(dtInformeC1);
            rd.Subreports["PrestoIngrRetailC2"].SetDataSource(dtInformeC2);
            rd.Subreports["PrestoIngrRetailFlete"].SetDataSource(dtInformeFlete);
            rd.Subreports["PrestoIngrRetailC3"].SetDataSource(dtInformeC3);
            rd.Subreports["PrestoIngrRetailOpex"].SetDataSource(dtInformeTotal);
            rd.Subreports["PrestoIngrRetailC4"].SetDataSource(dtInformeC4);
                
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
         }
         catch(Exception ex)

         {
            return View("errorInformeIngreRetail");
         }      
      }

      public async Task<DataTable> getInformePresupuestoIngrexVolumxEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngrexVolumxEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

        public async Task<DataTable> getInformePrestoVolumenAnualRetailEmpresa(ePresupuesto presupuesto)
        {
            DataTable dtPresupuesto = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoVolumenAnualRetailEmpresa", PresupuestoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtPresupuesto;
        }

        public async Task<DataTable> getInformePrestoVolumenAnualSucursal(ePresupuesto presupuesto)
        {
            DataTable dtPresupuesto = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoVolumenAnualRetailEmpresaSucursal", PresupuestoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtPresupuesto;
        }
        public async Task<DataTable> getInformePresupuestoIngreC1xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngreC1xEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformePresupuestoIngreC2xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngreC2xEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformePresupuestoIngreFletexEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngreFletexEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<DataTable> getInformePresupuestoIngreC3xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngreC3xEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<DataTable> getInformeTotPresupuestoIngreAnualxUneg(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            presupuesto.tipoPresupuesto = "OPEX";
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeTotPrestoIngreAnualxUneg", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<DataTable> getInformePresupuestoIngreC4xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoIngreC4xEmpxSuc", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<ActionResult> VerInformePresupuestoVolxUnidad(ePresupuesto presupuesto)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoVolxUnidad(presupuesto);
            String nombreReporte = "Informe_Presupuesto_Volumen_unidad_de_negocio-";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolEmpreXUN.rpt"));
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
            
         }
         catch
         {
            return View("errorInformeOpex");
         }
      }

      public async Task<DataTable> getInformePresupuestoVolxUnidad(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuestoVolumenxUnidad", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<ActionResult> entornoInfoIngreVolxEmpresa(ePresupuesto presupuesto)
      {
         try
         {
            DataTable dtInforme = new DataTable();
            dtInforme = await getInformePresupuestoVolxEmpresa(presupuesto);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolEmpreXEmpresa.rpt"));// Reporte a crear 
            rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
            rd.SetDataSource(dtInforme);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Informe_Presupuesto_Volumen_Empresa-" + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
         }
         catch
         {
            return View("errorInformeOpex");
         }
      }

      public async Task<DataTable> getInformePresupuestoVolxEmpresa(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePresupuestoVolumenxCAPAMEDIAAAAA", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }


        public async Task<ActionResult> VerInformeTotalizadoRetail(ePresupuesto presupuesto)
        {
            presupuesto.distribRetailUneg = "N";
            string nombreReporte="";
            if (presupuesto.grupoPresupuesto == "RETA")
            {
                nombreReporte = "Informe_Presupuesto_Ingreso_Retail_Total-";
            }
            else
            {
                nombreReporte = "Informe_Presupuesto_Ingreso_Distribuidora_Total-";
            }
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeIngresoVolumenRetail(presupuesto);

                DataTable dtInformeC1 = new DataTable();
                dtInformeC1 = await getInformeIngresoC1Retail(presupuesto);

                DataTable dtInformeC2 = new DataTable();
                dtInformeC2 = await getInformeIngresoC2Retail(presupuesto);

                DataTable dtInformeFlete = new DataTable();
                dtInformeFlete = await getInformeIngresoFleteRetail(presupuesto);

                DataTable dtInformeC3 = new DataTable();
                dtInformeC3 = await getInformeIngresoC3Retail(presupuesto);
                
                DataTable dtInformeC4 = new DataTable();
                dtInformeC4 = await getInformeIngresoC4Retail(presupuesto);

                DataTable dtInformeTotal = new DataTable();
                dtInformeTotal = await getInformeTotalAnualRetail(presupuesto);

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoIngrRetailTotalizado.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;

                rd.SetDataSource(dtInforme);
                rd.Subreports["PrestoIngrRetailC1"].SetDataSource(dtInformeC1);
                rd.Subreports["PrestoIngrRetailC2"].SetDataSource(dtInformeC2);
                rd.Subreports["PrestoIngrRetailFlete"].SetDataSource(dtInformeFlete);
                rd.Subreports["PrestoIngrRetailC3"].SetDataSource(dtInformeC3);
                rd.Subreports["PrestoIngrRetailOpex"].SetDataSource(dtInformeTotal);
                rd.Subreports["PrestoIngrRetailC4"].SetDataSource(dtInformeC4);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
                
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }
        public async Task<ActionResult> VerInformeTotalizadoPresupuesto(ePresupuesto presupuesto)
        {
            presupuesto.distribRetailUneg = "S";
            string nombreReporte = "";
            if (presupuesto.grupoPresupuesto == "RETA")
            {
                nombreReporte = "Informe_Estado_Resultados_Retail_Total-";
            }
            else
            {
                nombreReporte = "Informe_Estado_Resultados_Distribuidora_Total-";
            }
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeIngresoVolumenRetail(presupuesto);

                DataTable dtInformeC1 = new DataTable();
                dtInformeC1 = await getInformeIngresoC1Retail(presupuesto);

                DataTable dtInformeC2 = new DataTable();
                dtInformeC2 = await getInformeIngresoC2Retail(presupuesto);

                DataTable dtInformeFlete = new DataTable();
                dtInformeFlete = await getInformeIngresoFleteRetail(presupuesto);

                DataTable dtInformeC3 = new DataTable();
                dtInformeC3 = await getInformeIngresoC3Retail(presupuesto);

                DataTable dtInformeC4 = new DataTable();
                dtInformeC4 = await getInformeIngresoC4Retail(presupuesto);

                DataTable dtInformeTotal = new DataTable();
                dtInformeTotal = await getInformeTotalAnualRetail(presupuesto);

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoIngrRetailTotalizadoDistribTipoNegocio.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";

                rd.SetDataSource(dtInforme);
                rd.Subreports["PrestoIngrRetailC1"].SetDataSource(dtInformeC1);
                rd.Subreports["PrestoIngrRetailC2"].SetDataSource(dtInformeC2);
                rd.Subreports["PrestoIngrRetailFlete"].SetDataSource(dtInformeFlete);
                rd.Subreports["PrestoIngrRetailC3"].SetDataSource(dtInformeC3);
                rd.Subreports["PrestoIngrRetailOpex"].SetDataSource(dtInformeTotal);
                rd.Subreports["PrestoIngrRetailC4"].SetDataSource(dtInformeC4);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (presupuesto.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
                }
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }
        public async Task<ActionResult> VerInformeCorporativoRetail(ePresupuesto presupuesto)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformePrestoVolumenAnualRetail(presupuesto);
                String nombreReporte = "Informe_Presupuesto_Corporativo_Retail-";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolumenAnualRetail.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                rd.DataDefinition.FormulaFields["anio"].Text = "'" + presupuesto.anio + "'"; ;
                rd.SetDataSource(dtInforme);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }

        public async Task<ActionResult> VerDashboardCorporativoRetail(ePresupuesto presupuesto)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformePrestoVolumenAnualRetail(presupuesto);
                String nombreReporte = "Dashboard_Presupuesto_Corporativo_Retail-";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolumenAnualRetailDashboard.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                rd.DataDefinition.FormulaFields["anio"].Text = "'" + presupuesto.anio + "'"; ;
                rd.SetDataSource(dtInforme);
                rd.Subreports["tablaDatos"].SetDataSource(dtInforme);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }
        public async Task<ActionResult> VerInformeSubCorporativoRetail(ePresupuesto presupuesto)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformePrestoVolumenAnualRetail2(presupuesto);
                String nombreReporte = "Informe_Presupuesto_SubCorporativo_Retail-";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolumenAnualRetail2.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                rd.DataDefinition.FormulaFields["anio"].Text = "'" + presupuesto.anio + "'"; ;
                rd.SetDataSource(dtInforme);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }

        public async Task<ActionResult> VerInformeVolumenRetailxEmpresa(ePresupuesto presupuesto)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformePrestoVolumenAnualRetailEmpresa(presupuesto);
                String nombreReporte = "Informe_Presupuesto_Por_Empresa_Retail-";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolumenAnualRetailxEmpresa.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                rd.DataDefinition.FormulaFields["anio"].Text = "'" + presupuesto.anio + "'"; ;
                rd.SetDataSource(dtInforme);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }
        public async Task<ActionResult> VerInformeVolumenRetailxSucursal(ePresupuesto presupuesto)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformePrestoVolumenAnualSucursal(presupuesto);
                String nombreReporte = "Informe_Presupuesto_Por_Sucursal_Retail-";
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_PrestoVolumenAnualRetailxSucursal.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'"; ;
                rd.DataDefinition.FormulaFields["anio"].Text = "'" + presupuesto.anio + "'"; ;
                rd.SetDataSource(dtInforme);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)

            {
                return View("errorInformeIngreRetail");
            }
        }
        public async Task<DataTable> getInformeIngresoVolumenRetail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoVolumenRetail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<DataTable> getInformePrestoVolumenAnualRetail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoVolumenAnualRetail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

      public async Task<DataTable> getInformePrestoVolumenAnualRetail2(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformePrestoVolumenAnualRetail2", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeIngresoC1Retail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoC1Retail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeIngresoC2Retail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoC2Retail", PresupuestoJSON); 
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeIngresoFleteRetail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoFleteRetail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeIngresoC3Retail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoC3Retail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeIngresoC4Retail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeIngresoC4Retail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }
      public async Task<DataTable> getInformeTotalAnualRetail(ePresupuesto presupuesto)
      {
         DataTable dtPresupuesto = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            presupuesto.tipoPresupuesto = "OPEX";
            string PresupuestoJSON = JsonConvert.SerializeObject(presupuesto);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("Presupuesto/getInformeTotalAnualRetail", PresupuestoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtPresupuesto = JsonConvert.DeserializeObject<DataTable>(salidaJSON);       
         }
         catch
         {
            return null;
         }
         return dtPresupuesto;
      }

    }
}