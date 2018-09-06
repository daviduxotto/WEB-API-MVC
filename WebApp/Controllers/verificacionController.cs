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
    public class verificacionController : Controller
    {
        public ActionResult entornoInformeTrabajoVerificado()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }

        public ActionResult entornoInformeDesgloseTrabajoVerificado()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }

        public async Task<ActionResult> VerInformeVefificadoUV(eVerificacion verificacion)
        {
            try
            {
                DataTable dtVerificacion = new DataTable();
                dtVerificacion = await getInformeVefificadoUV(verificacion);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Verificación-";
                if (verificacion.informeResumido == true)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_InformeTrabajoVerificadoResumido.rpt"));
                    nombreReporte = nombreReporte + "Resumen-";
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_InformeTrabajoVerificado.rpt"));
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + verificacion.codDia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + verificacion.codDia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtVerificacion);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (verificacion.formatoInforme == "EXCEL")
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
                return View("errorInformeUv");
            }
        }

        public async Task<ActionResult> VerInformeVefificadoUVDesglose(eVerificacion verificacion)
        {
            try
            {
                DataTable dtVerificacion = new DataTable();
                dtVerificacion = await getInformeVefificadoUVDesglose(verificacion);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Desglose_Verificación-";

                if (verificacion.informeResumido == true)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_InformeDesgloseTrabajoVerificadoResumido.rpt"));
                    nombreReporte = nombreReporte + "Resumen-";
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_InformeDesgloseTrabajoVerificado.rpt"));
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + verificacion.codDia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + verificacion.codDia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtVerificacion);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (verificacion.formatoInforme == "EXCEL")
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
                return View("errorInformeUv");
            }
        }

        public async Task<DataTable> getInformeVefificadoUV(eVerificacion verificacion)
        {
            DataTable dtVerificacion = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                
                string verificacionJSON = JsonConvert.SerializeObject(verificacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("verificacion/getInformeVefificadoUV", verificacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtVerificacion = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtVerificacion;
        }

        public async Task<DataTable> getInformeVefificadoUVDesglose(eVerificacion verificacion)
        {
            DataTable dtVerificacion = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());

                string verificacionJSON = JsonConvert.SerializeObject(verificacion);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("verificacion/getInformeVefificadoUVHorizontal", verificacionJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtVerificacion = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtVerificacion;
        }

    }
}