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
    public class proveedorController : Controller
    {
        // GET: proveedor
        public async Task<ActionResult> getProveedorCajaChicaxEmpresa()
        {
            List<eProveedor> listProveedores = new List<eProveedor>();
            DataTable dtProveedor = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eProveedor parProveedor = new eProveedor();
                parProveedor.codEmpresa = Session["empresa"].ToString();
                string proveedorJSON = JsonConvert.SerializeObject(parProveedor);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("proveedor/getProveedorCajaChicaxEmpresa", proveedorJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtProveedor = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista 
                foreach (DataRow row in dtProveedor.Rows)
                {
                    eProveedor proveedor = new eProveedor();
                    proveedor.codProveedor = row["cod_proveedor"].ToString();
                    proveedor.descProveedor = row["desc_proveedor"].ToString();
                    listProveedores.Add(proveedor);
                }

            }
            catch
            {
                return null;
            }
            return Json(listProveedores, JsonRequestBehavior.AllowGet);
        }
    }
}