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
    public class trasladoEmpleadoController : Controller
    {
        // GET: trasladoEmpleado
        public async Task<ActionResult> trasladoEmpleados(string id,string codigocorp)
        {
         List<eTrasladoEmpleado> listTrasladoEmpleados = new List<eTrasladoEmpleado>();
         listTrasladoEmpleados = await getTrasladoEmpleado(codigocorp);
         return View(listTrasladoEmpleados);
        }
      public async Task<List<eTrasladoEmpleado>> getTrasladoEmpleado(string codigocorp)
      {
         List<eTrasladoEmpleado> listTrasladoEmpleados = new List<eTrasladoEmpleado>();
         DataTable dtTrasladoEmpleados = new DataTable();
         try
         {
            HttpClient client = new HttpClient();
            //le doy la direccion del servidor y key de la app cliente
            client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
            eTrasladoEmpleado parTrasladoEmpleado = new eTrasladoEmpleado();
            parTrasladoEmpleado.codigoCorp = Convert.ToInt32(codigocorp);
            string TrasladoEmpleadoJSON = JsonConvert.SerializeObject(parTrasladoEmpleado);
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("TrasladoEmpleado/getTrasladoEmpleado", TrasladoEmpleadoJSON);
            response.EnsureSuccessStatusCode();
            //leo lo que me respondio la peticion
            string salidaJSON = await response.Content.ReadAsStringAsync();
            dtTrasladoEmpleados = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
            //convierto la tabla en una lista de TrasladoEmpleados
            foreach (DataRow row in dtTrasladoEmpleados.Rows)
            {
               eTrasladoEmpleado TrasladoEmpleado = new eTrasladoEmpleado();
               TrasladoEmpleado.codigoCorp =Convert.ToInt32( row["cod_empleado_corp"].ToString());
               TrasladoEmpleado.nombre = row["nombre"].ToString();
               TrasladoEmpleado.empresaAnterior = row["empresa anterior"].ToString();
               TrasladoEmpleado.sucursalAnterior = row["sucursal anterior"].ToString();
               TrasladoEmpleado.accion = row["accion"].ToString();
               TrasladoEmpleado.empresa = row["empresa"].ToString();
               TrasladoEmpleado.sucursal = row["sucursal"].ToString();
               TrasladoEmpleado.fechaTraslado = row["fecha_traslado"].ToString();
               TrasladoEmpleado.fechaTraslado = TrasladoEmpleado.fechaTraslado.Substring(0, 10);
               listTrasladoEmpleados.Add(TrasladoEmpleado);
            }

         }
         catch
         {
            return null;
         }
         return listTrasladoEmpleados;
      }
   }
}