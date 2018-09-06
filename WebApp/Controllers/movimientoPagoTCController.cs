using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public class movimientoPagoTCController : Controller
    {
        public async Task<ActionResult> entornoMovPagoTCXFecha()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }
        public async Task<ActionResult> entornoMovPagoTCXFechaXEstacion()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            return View();
        }

        public async Task<ActionResult> edit()
        {

            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            else
            {
                if (Session["diatrabajo"] == null)
                {
                    return RedirectToAction("elegirDia", "usuario");
                }
                eEntorno entorno = new eEntorno();
                entorno = await getDescripcionEntorno();
                ViewBag.empresa = entorno.empresa + " -- " + entorno.descEmpresa;
                ViewBag.estacion = entorno.sucursal + " -- " + entorno.descSucursal;
                ViewBag.dia = Session["diatrabajo"].ToString();
                return View();
            }
        }



        public async Task<ActionResult> getMovimientoPagoTC()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eMovimientoPagoTC> ListMovimientoPagoTC = new List<eMovimientoPagoTC>();
            DataTable dtMovimientoPagoTC = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoTC movimientoPagoTC = new eMovimientoPagoTC();
                movimientoPagoTC.codEmpresa = Session["empresa"].ToString();
                movimientoPagoTC.codSucursal = Session["sucursal"].ToString();
                movimientoPagoTC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                string movimientoPagoTCJSON = JsonConvert.SerializeObject(movimientoPagoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/getMovPagoTCXFecha", movimientoPagoTCJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimientoPagoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtMovimientoPagoTC.Rows)
                {
                    eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
                    varmovimiento.codEmpresa = row["cod_empresa"].ToString();
                    varmovimiento.descEmpresa = row["desc_empresa"].ToString();
                    varmovimiento.codSucursal = row["cod_estacion"].ToString();
                    varmovimiento.descSucursal = row["desc_sucursal"].ToString();
                    varmovimiento.codEmisor = row["cod_emisor_tarjeta"].ToString();
                    varmovimiento.descEmisor = row["desc_emisortc"].ToString();
                    varmovimiento.codBanco = row["cod_banco"].ToString();
                    varmovimiento.descBanco = row["desc_banco"].ToString();
                    varmovimiento.codCuenta = row["cod_cuenta"].ToString();
                    varmovimiento.descCuenta = row["desc_cuenta"].ToString();
                    varmovimiento.codDoctoBanco = row["cod_docto_banco"].ToString();
                    varmovimiento.descDoctoBanco = row["desc_tipodocto"].ToString();
                    varmovimiento.codDocumento = row["cod_documento"].ToString();
                    varmovimiento.codCliente = row["cod_cliente"].ToString();
                    varmovimiento.descCliente = row["desc_cliente"].ToString();
                    varmovimiento.fecha = Convert.ToDateTime(row["fecha"].ToString());
                    varmovimiento.monto = Convert.ToDouble(row["monto_documento"].ToString());
                    if (row["conciliado"].ToString() == "S") { varmovimiento.conciliado = true; } else { varmovimiento.conciliado = false; }
                    if (row["reversado"].ToString() == "S") { varmovimiento.revertido = true; } else { varmovimiento.revertido = false; }
                    //   varmovimiento.conciliado = libreria.chartoBool(row[""].ToString);
                    ListMovimientoPagoTC.Add(varmovimiento);
                    TempData["descempresa"] = varmovimiento.codEmpresa + " -- " + varmovimiento.descEmpresa;
                    TempData["descestacion"] = varmovimiento.codSucursal + " -- " + varmovimiento.descSucursal;
                    TempData["dia"] = varmovimiento.fecha;
                }

            }
            catch
            {
                return null;
            }
            string saaa = (Json(ListMovimientoPagoTC)).ToString();
            return Json(new { data = ListMovimientoPagoTC }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getMovPagoTCXFechaDetalle(string id, string Banco, string cuenta, string doctobanco, string documento)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eMovimientoPagoTC> ListMovimientoPagoTC = new List<eMovimientoPagoTC>();
            DataTable dtMovimientoPagoTC = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoTC movimientoPagoTC = new eMovimientoPagoTC();
                movimientoPagoTC.codEmpresa = Session["empresa"].ToString();
                movimientoPagoTC.codSucursal = Session["sucursal"].ToString();
                movimientoPagoTC.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                movimientoPagoTC.codBanco = Banco;
                movimientoPagoTC.codCuenta = cuenta;
                movimientoPagoTC.codDoctoBanco = doctobanco;
                movimientoPagoTC.codDocumento = documento;
                string movimientoPagoTCJSON = JsonConvert.SerializeObject(movimientoPagoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/getMovPagoTCXFechaDetalle", movimientoPagoTCJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimientoPagoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtMovimientoPagoTC.Rows)
                {
                    eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
                    varmovimiento.codEmpresa = row["cod_empresa"].ToString();
                    varmovimiento.codSucursal = row["cod_estacion"].ToString();
                    varmovimiento.codDoctoPosFactu = row["cod_docto_pos_factu"].ToString();
                    varmovimiento.noSerie = row["no_serie"].ToString();
                    varmovimiento.noFactura = row["no_factura"].ToString();
                    varmovimiento.fechaFactura = row["fecha_factura"].ToString();
                    varmovimiento.montoFactura = Convert.ToDouble(row["monto_total_factura"].ToString());
                    varmovimiento.montoPagadoFactura = Convert.ToDouble(row["monto_pagado"].ToString());
                    varmovimiento.saldoPendienteFactura = Convert.ToDouble(row["saldo_pendiente"].ToString());
                    varmovimiento.codDoctoPosVale = row["cod_docto_pos_vale"].ToString();
                    varmovimiento.noVale = row["no_vale"].ToString();
                    varmovimiento.fechaVale = row["fecha_vale"].ToString();
                    varmovimiento.montoVale = Convert.ToDouble(row["monto_total_vale"].ToString());
                    ListMovimientoPagoTC.Add(varmovimiento);
                    TempData["descempresa"] = varmovimiento.codEmpresa + " -- " + varmovimiento.descEmpresa;
                    TempData["descestacion"] = varmovimiento.codSucursal + " -- " + varmovimiento.descSucursal;
                    TempData["dia"] = varmovimiento.fecha;
                }

            }
            catch
            {
                return null;
            }
            string saaa = (Json(ListMovimientoPagoTC)).ToString();
            return Json(ListMovimientoPagoTC, JsonRequestBehavior.AllowGet);
        }

        public ActionResult conciliar(string id, string Banco, string cuenta, string doctobanco, string documento)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.codBanco = Banco;
            varmovimiento.codCuenta = cuenta;
            varmovimiento.codDoctoBanco = doctobanco;
            varmovimiento.codDocumento = (documento).Replace("*", " ");

            return View(varmovimiento);
        }
        public async Task<ActionResult> guardarConciliar(eMovimientoPagoTC varmovimiento)
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

                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.token = Session["token"].ToString();

                string MovimientoEfectivoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/conciliar", MovimientoEfectivoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
            }
            catch
            {
                salida.retorno = -9999;
                salida.mensaje = "Ha ocurrido un error en la comunicacion con la API";
            }
            return new JsonResult { Data = new { salida = salida } };
        }

        public async Task<ActionResult> getLiquidacionPagoClienteXempresa()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            DataTable dtResultado = new DataTable();
            eLiquidacionVentaDiariaxEmpresa liquidacion = new eLiquidacionVentaDiariaxEmpresa();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoEfectivoDet movimiento = new eMovimientoEfectivoDet();
                movimiento.codEmpresa = Session["empresa"].ToString();
                movimiento.codSucursal = Session["sucursal"].ToString();
                movimiento.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
                movimiento.tipoPago = "S";
                string movimientoJSON = JsonConvert.SerializeObject(movimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/getLiquidacionPagoClienteXempresa", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtResultado = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtResultado.Rows)
                {

                    liquidacion.montoTotalReportado = row["monto_total_reportado"].ToString();

                    liquidacion.montoTotalConciliado = row["monto_total_doctos_concil"].ToString();
                    liquidacion.diferenciaMontoConciliado = row["dif_montovrsdoctosconcil"].ToString();

                }

            }
            catch
            {
                return null;
            }
            return Json(liquidacion, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cambiocodigo(string id, string Banco, string cuenta, string doctobanco, string documento)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
            varmovimiento.codEmpresa = Session["empresa"].ToString();
            varmovimiento.token = Session["token"].ToString();
            varmovimiento.codBancoAnterior = Banco;
            varmovimiento.codCuentaAnterior = cuenta;
            varmovimiento.codDoctoBancoAnterior = doctobanco;
            varmovimiento.codDocumentoAnterior = (documento).Replace("*", " ");
            varmovimiento.codDocumento = (documento).Replace("*", " ");
            ViewBag.documento = (documento).Replace("*", " ");
            return View(varmovimiento);
        }

        public async Task<ActionResult> guardarCambioCodigo(eMovimientoPagoTC varmovimiento)
        {
            varmovimiento.codBanco = varmovimiento.codBancoAnterior;
            varmovimiento.codCuenta = varmovimiento.codCuentaAnterior;
            varmovimiento.codDoctoBanco = varmovimiento.codDoctoBancoAnterior;

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
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.token = Session["token"].ToString();
                string MovimientoPagoTCJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/cambiocodigo", MovimientoPagoTCJSON);
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
            return new JsonResult { Data = new { salida = salida } };
        }
        public async Task<ActionResult> getBancosxEmpresa()
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eBanco> listBancos = new List<eBanco>();
            DataTable dtBancos = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eBanco parBanco = new eBanco();
                parBanco.codEmpresa = Session["empresa"].ToString();
                string BancoJSON = JsonConvert.SerializeObject(parBanco);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getBancosXEmpresa", BancoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtBancos.Rows)
                {
                    eBanco Banco = new eBanco();
                    Banco.codBanco = row["cod_Banco"].ToString();
                    Banco.descBanco = row["descripcion"].ToString();
                    listBancos.Add(Banco);
                }

            }
            catch
            {
                return null;
            }
            return Json(listBancos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getCuentasBancos(string codBanco)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eCuentaBanco> listCuentaBancos = new List<eCuentaBanco>();
            DataTable dtBancos = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eCuentaBanco parBanco = new eCuentaBanco();
                parBanco.codEmpresa = Session["empresa"].ToString();
                parBanco.codSucursal = Session["sucursal"].ToString();
                parBanco.codBanco = codBanco;
                string BancoJSON = JsonConvert.SerializeObject(parBanco);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getCuentasxBanco", BancoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtBancos.Rows)
                {
                    eCuentaBanco cuentaBanco = new eCuentaBanco();
                    cuentaBanco.codCuentaBanco = row["cod_cuenta"].ToString();
                    cuentaBanco.descCuentaBanco = row["descripcion"].ToString();
                    listCuentaBancos.Add(cuentaBanco);
                }

            }
            catch
            {
                return null;
            }
            return Json(listCuentaBancos, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getDoctoBanco(string codBanco, string codCuenta)
        {
            if (Session["usuario"] == null && Session["token"] == null)
            {
                return RedirectToAction("login", "usuario");
            }
            List<eDoctoBanco> listDoctoBancos = new List<eDoctoBanco>();
            DataTable dtBancos = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eDoctoBanco parBanco = new eDoctoBanco();
                parBanco.codEmpresa = Session["empresa"].ToString();
                parBanco.codCuentaBanco = codCuenta;
                parBanco.codBanco = codBanco;
                string BancoJSON = JsonConvert.SerializeObject(parBanco);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("banco/getdoctoBanco", BancoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtBancos.Rows)
                {
                    eDoctoBanco cuentaBanco = new eDoctoBanco();
                    cuentaBanco.codDoctoBanco = row["cod_docto_banco"].ToString();
                    cuentaBanco.descDoctoBanco = row["descripcion"].ToString();
                    listDoctoBancos.Add(cuentaBanco);
                }

            }
            catch
            {
                return null;
            }
            return Json(listDoctoBancos, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<eEntorno> getDescripcionEntorno()
        {
            eEntorno entornoresult = new eEntorno();
            DataTable dtBancos = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eEntorno entorno = new eEntorno();
                entorno.empresa = Session["empresa"].ToString();
                entorno.sucursal = Session["sucursal"].ToString();
                string entornoJSON = JsonConvert.SerializeObject(entorno);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("entorno/getdescripcionentorno", entornoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtBancos = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de Bancos
                foreach (DataRow row in dtBancos.Rows)
                {
                    entornoresult.empresa = row["cod_empresa"].ToString();
                    entornoresult.descEmpresa = row["desc_empresa"].ToString();
                    entornoresult.sucursal = row["cod_sucursal"].ToString();
                    entornoresult.descSucursal = row["desc_sucursal"].ToString();
                }

            }
            catch
            {
                return null;
            }
            return entornoresult;
        }
        
        public async Task<ActionResult> VerInformeMovPagoTcXFecha(eMovimientoPagoTC movimientoPagoTC)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeMovPagoTCXFecha(movimientoPagoTC);
                DataTable dtInformeGrafico = new DataTable();
                dtInformeGrafico = await getInformeMovPagoTCXFechaGrafico(movimientoPagoTC);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Movimimiento_Pago_Clientes_Credito_TC-";

                if (movimientoPagoTC.informeResumido == true)
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteTCXFechaResumido.rpt"));//
                    nombreReporte = nombreReporte+"Resumen-";
                }
                else
                {
                    rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteTCXFecha.rpt"));//
                }
                //verifica si se ha solicitado mostrar solo registros pendientes de conciliar 
                if (movimientoPagoTC.pendienteConciliar == true)
                {
                    rd.DataDefinition.RecordSelectionFormula = "{info_deptoclientetcconcixes.conciliado}='N'";
                    nombreReporte = nombreReporte + "Pendiente_Conciliar-";
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoPagoTC.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoPagoTC.dia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);
                rd.Subreports["graficoMovCcTcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (movimientoPagoTC.formatoInforme == "EXCEL")
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
                return View("errorInformeMovimientoPagoTC");
            }
        }

        public async Task<DataTable> getInformeMovPagoTCXFecha(eMovimientoPagoTC movimientoPagoTC)
        {
            DataTable dtMovimientoPagoTC = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimientoPagoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/infoMovClienteTCXFecha", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimientoPagoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtMovimientoPagoTC;
        }

        public async Task<DataTable> getInformeMovPagoTCXFechaGrafico(eMovimientoPagoTC movimientoPagoTC)
        {
            DataTable dtMovimientoPagoTC = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                string movimientoJSON = JsonConvert.SerializeObject(movimientoPagoTC);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/infoClientetcVsConciliadoGrafico", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMovimientoPagoTC = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            }
            catch
            {
                return null;
            }
            return dtMovimientoPagoTC;
        }

        public async Task<ActionResult> VerInformeMovPagoTcXFechaXEstacion(eMovimientoPagoTC movimientoPagoTC)
        {
            try
            {
                DataTable dtInforme = new DataTable();
                dtInforme = await getInformeMovPagoTCXFecha(movimientoPagoTC);
                DataTable dtInformeGrafico = new DataTable();
                dtInformeGrafico = await getInformeMovPagoTCXFechaGrafico(movimientoPagoTC);
                ReportDocument rd = new ReportDocument();
                String nombreReporte = "Informe_Movimimiento_Pago_Clientes_Credito_TC_Consolidado-";

                rd.Load(Path.Combine(Server.MapPath("~/info"), "Det_MovClienteTCxFechaxEstacionConsolidado.rpt"));
                
                //verifica si se ha solicitado mostrar solo registros pendientes de conciliar 
                if (movimientoPagoTC.pendienteConciliar == true)
                {
                    rd.DataDefinition.RecordSelectionFormula = "{info_deptoclientetcconcixes.conciliado}='N'";
                    nombreReporte = nombreReporte + "Pendiente_Conciliar-";
                }
                rd.DataDefinition.FormulaFields["Fecha_Inicial"].Text = "'" + movimientoPagoTC.dia.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["Fecha_Final"].Text = "'" + movimientoPagoTC.dia2.ToString("dd/MM/yyyy") + "'";
                rd.DataDefinition.FormulaFields["impreso_por"].Text = "'" + Session["usuario"].ToString() + "'";
                rd.SetDataSource(dtInforme);
                rd.Subreports["graficoMovCcTcConciliadoSinConciliar"].SetDataSource(dtInformeGrafico);
                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                Stream stream;
                if (movimientoPagoTC.formatoInforme == "EXCEL")
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
                return View("errorInformeMovimientoPagoTC");
            }
        }
        public async Task<ActionResult> getEstadoPantalla()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/getEstadoPantalla", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }

        public async Task<ActionResult> abrirPantallaUV()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/abrirPantallaUV", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }
        public async Task<ActionResult> cerrarPantallaUV()
        {
            eParSalida salida = new eParSalida();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMovimientoPagoTC varmovimiento = new eMovimientoPagoTC();
                varmovimiento.codEmpresa = Session["empresa"].ToString();
                varmovimiento.codSucursal = Session["sucursal"].ToString();
                varmovimiento.dia = Convert.ToDateTime(Session["diatrabajo"].ToString());
                varmovimiento.token = Session["token"].ToString();

                string movimientoJSON = JsonConvert.SerializeObject(varmovimiento);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("movimientoPagoTC/cerrarPantallaUV", movimientoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                salida = JsonConvert.DeserializeObject<eParSalida>(salidaJSON);
                ViewBag.dia = salida.retorno;
            }
            catch
            {
                bool status;
                status = false;
            }
            return new JsonResult { Data = new { salida = salida } };
        }
    }
}