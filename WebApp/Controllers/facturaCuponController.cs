using Entidades;
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

namespace WebApp.Controllers
{
    public class FacturaCuponController : Controller
    {
      public async Task<ActionResult> getFacturaCuponesxEstacionxFecha()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         List<eFacturaCupon> ListFacturaCupones = new List<eFacturaCupon>();
         DataTable dtFacturaCupones = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eFacturaCupon FacturaCupon = new eFacturaCupon();
            FacturaCupon.codEmpresa = Session["empresa"].ToString();
            FacturaCupon.codSucursal = Session["sucursal"].ToString();
            FacturaCupon.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string FacturaCuponJSON = JsonConvert.SerializeObject(FacturaCupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("FacturaCupon/getFactuCuponesxestacionxfecha", FacturaCuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtFacturaCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtFacturaCupones.Rows)
            {
               eFacturaCupon varFacturaCupon = new eFacturaCupon();
               varFacturaCupon.codEmpresa = row["cod_empresa"].ToString();
               varFacturaCupon.descEmpresa = row["desc_empresa"].ToString();
               varFacturaCupon.codSucursal = row["cod_estacion"].ToString();
               varFacturaCupon.descSucursal = row["desc_sucursal"].ToString();
               varFacturaCupon.fecha= Convert.ToDateTime(row["fecha"].ToString());
               varFacturaCupon.codEmisor = row["cod_emisor_cupon"].ToString();
               varFacturaCupon.descEmisor = row["desc_emisorcupon"].ToString();

               varFacturaCupon.codDoctoPos = row["cod_docto_pos"].ToString();
               varFacturaCupon.descTipoDocto = row["desc_tipodocto"].ToString();
               varFacturaCupon.noSerie= row["no_serie"].ToString();
               varFacturaCupon.noFactura = row["no_factura"].ToString();

               varFacturaCupon.codCliente= row["cod_cliente"].ToString();
               varFacturaCupon.descCliente = row["desc_cliente"].ToString();
               varFacturaCupon.aplicaDescuento = libreria.chartoBool(Convert.ToChar( row["aplica_descuento_general"].ToString()));
               varFacturaCupon.porcentajeDescuento = Convert.ToDecimal(row["porcentaje_descuento_general"].ToString());
               varFacturaCupon.montoDescuento= Convert.ToDecimal(row["monto_descuento_general"].ToString());
               varFacturaCupon.montoTotal = Convert.ToDecimal(row["monto_total_factura"].ToString());
               if (row["validado"].ToString() == "S") { varFacturaCupon.validado = true; } else { varFacturaCupon.validado = false; }
               ListFacturaCupones.Add(varFacturaCupon);
            }

         }
         catch
         {
            return null;
         }
         return Json(new { data = ListFacturaCupones }, JsonRequestBehavior.AllowGet);
      }
      public async Task<ActionResult> getLiquidacionFacturaCupones()
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eLiquidacionCupones liquidacion = new eLiquidacionCupones();
         DataTable dtCupones = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eFacturaCupon Cupon = new eFacturaCupon();
            Cupon.codEmpresa = Session["empresa"].ToString();
            Cupon.codSucursal = Session["sucursal"].ToString();
            Cupon.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
            string CuponJSON = JsonConvert.SerializeObject(Cupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturaCupon/getLiquidacionFacturaCupones", CuponJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtCupones = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de empresas
            foreach (DataRow row in dtCupones.Rows)
            {

               liquidacion.montoTotalReportado = row["monto_total_reportado"].ToString();
               liquidacion.montoTotalDocumentos = row["monto_total_doctos"].ToString();
               liquidacion.diferenciaMontoDocto = row["dif_montovrsdoctos"].ToString();
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

      public async Task<ActionResult> validarfacturacupon(string id, string docto, string serie, string factura)
      {
         if (Session["usuario"] == null && Session["token"] == null)
         {
            return RedirectToAction("login", "usuario");
         }
         eFacturaCupon varcupon = new eFacturaCupon();
         varcupon.codEmpresa = Session["empresa"].ToString();
         varcupon.codSucursal = Session["sucursal"].ToString();
         varcupon.fecha = Convert.ToDateTime(Session["diatrabajo"].ToString());
         varcupon.codDoctoPos = docto;
         varcupon.noSerie = serie;
         varcupon.noFactura = factura;
         varcupon.token = Session["token"].ToString();
         eParSalida salida = new eParSalida();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            string cuponJSON = JsonConvert.SerializeObject(varcupon);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("facturacupon/validarfacturacupon", cuponJSON);
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
         return Json(salida, JsonRequestBehavior.AllowGet);
      }
   }
}