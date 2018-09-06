using CrystalDecisions.CrystalReports.Engine;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class observacionUVController : Controller
    {
        public ActionResult entornoInformeBitacora()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }
        public async Task<ActionResult> getObservacionUV(string id, string codPantalla)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eObservacionUV> ListObservaciones = new List<eObservacionUV>();
            DataTable dtObservacion = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eObservacionUV observacion = new eObservacionUV();
                observacion.codEmpresa = Session["empresa"].ToString();
                observacion.codSucursal = Session["sucursal"].ToString();
                observacion.codDia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                observacion.codPantalla = codPantalla;
                string observacionJSON = JsonConvert.SerializeObject(observacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("observacionUV/getObservacionUV", observacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtObservacion = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de observaciones
                foreach (DataRow row in dtObservacion.Rows)
                {
                    eObservacionUV varObservacion = new eObservacionUV();
                    varObservacion.codObservacion = Convert.ToInt32(row["cod_observacion"]);
                    varObservacion.codEmpresa = row["cod_empresa"].ToString();
                    varObservacion.codSucursal = row["cod_estacion"].ToString();
                    varObservacion.codPantalla = row["cod_pantalla"].ToString();
                    varObservacion.usuario = row["usuario"].ToString();
                    varObservacion.descripcion = row["descripcion"].ToString();
                    varObservacion.tipoObservacion = row["tipo_observacion"].ToString();
                    varObservacion.fecha = row["fecha"].ToString();
                    ListObservaciones.Add(varObservacion);
                }

            }
            catch
            {
                return null;
            }
            return Json(ListObservaciones, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> guardarNuevaObservacion(string codPantalla, string descripcion)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            bool status = false;
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eObservacionUV ObservacionUV = new eObservacionUV();
                ObservacionUV.codEmpresa = Session["empresa"].ToString();
                ObservacionUV.codSucursal = Session["sucursal"].ToString();
                ObservacionUV.codDia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                ObservacionUV.codPantalla = codPantalla;
                ObservacionUV.descripcion = descripcion;
                ObservacionUV.token = Session["token"].ToString();
                string ObservacionuvJSON = JsonConvert.SerializeObject(ObservacionUV);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("observacionUV/newObservacionUV", ObservacionuvJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                if (salida.retorno >= 1)
                {
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }
        public ActionResult nuevaObservacion(string pantalla)
        {

            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            else
            {
                ViewBag.codPantalla = pantalla; 
                return View();
            }
        }

        public ActionResult eliminar(string id, String codObservacion)
        {

            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            else
            {
                eObservacionUV varObservacion = new eObservacionUV();
                varObservacion.codObservacion = Convert.ToInt32(codObservacion);
                return View(varObservacion);
            }

        }
        public async Task<ActionResult> eliminarObservacion(eObservacionUV varObservacion)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            bool status = false;
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                varObservacion.token = Session["token"].ToString();
                string observacionJSON = JsonConvert.SerializeObject(varObservacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("observacionUV/delObservacionUV", observacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                if (salida.retorno >= 1)
                {
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }

        public async Task<ActionResult> verInformeObservacionPantalla(eObservacionUV varObservacion, string pantalla)
        {
            try
            {
                varObservacion.codEmpresa = Session["empresa"].ToString();
                varObservacion.codSucursal = Session["sucursal"].ToString();
                varObservacion.codDia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varObservacion.codDia2 = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varObservacion.codPantalla = pantalla;

                DataTable dtInforme = new DataTable();
                dtInforme = await getInfoObservacionUV(varObservacion);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_detalle_observaciones-";
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_ObservacionUVdetalle.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            }
            catch (Exception ex)
            {
                return View("errorInformeObservacionUV");
            }
        }
        public async Task<ActionResult> verInformeObservacion(eObservacionUV varObservacion)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInfoObservacionUV(varObservacion);
                ReportDocument rd = new ReportDocument();
                string tipoReporte = "Corporativo";
                string nombreReporte = "Informe_Bitacora-";
                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_ObservacionUVBitacora.rpt"));
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);
                if (varObservacion.codEmpresa != "00000")
                 {
                     rd.ReportDefinition.Areas["GroupHeaderArea1"].Sections["GroupHeaderSection1"].SectionFormat.EnableSuppress = true;
                    tipoReporte = "Empresa";
                 }
                if (varObservacion.codSucursal != "0000")
                {
                    rd.ReportDefinition.Areas["GroupHeaderArea3"].Sections["GroupHeaderSection3"].SectionFormat.EnableSuppress = true;
                    tipoReporte = "Sucursal";
                }
                if (varObservacion.codPantalla != "00")
                {
                    rd.ReportDefinition.Areas["GroupHeaderArea4"].Sections["GroupHeaderSection4"].SectionFormat.EnableSuppress = true;
                    tipoReporte = "Especifico";
                }
                rd.DataDefinition.FormulaFields["tipoReporte"].Text = "'" + tipoReporte + "'";
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (varObservacion.formatoInforme == "EXCEL")
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelWorkbook);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/Excel", nombreReporte + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx");
                }
                else
                {
                    stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/PDF", nombreReporte + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf");
                }
                
            }
            catch (Exception ex)
            {
                return View("errorInformeObservacionUV");
            }
        }

        public async Task<DataTable> getInfoObservacionUV(eObservacionUV varObservacion)
        {
            DataTable dtMovimiento = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string observacionJSON = JsonConvert.SerializeObject(varObservacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("observacionUV/getInfoObservacionUV", observacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimiento = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtMovimiento;
        }

        public async Task<ActionResult> getPantallaUV(string codEmpresa, string codSucursal)
        {
            List<eObservacionUV> listPantallas = new List<eObservacionUV>();
            DataTable dtPantallas = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eObservacionUV observacion = new eObservacionUV();
                observacion.codEmpresa = codEmpresa;
                observacion.codSucursal = codSucursal;
                string observacionJSON = JsonConvert.SerializeObject(observacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("observacionUV/getPantallaUV", observacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtPantallas = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtPantallas.Rows)
                {
                    eObservacionUV varObservacion = new eObservacionUV();
                    varObservacion.codPantalla = row["cod_pantalla"].ToString();
                    varObservacion.descPantalla = row["desc_pantalla"].ToString();
                    listPantallas.Add(varObservacion);
                }
            }
            catch
            {
                return null;
            }
            return Json(listPantallas, JsonRequestBehavior.AllowGet);
        }
    }
}