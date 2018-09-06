using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class paisController : Controller
    {
        public async Task<ActionResult> getPaises()
        {
            List<ePais> listPaises = new List<ePais>();
            DataTable dtPais = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                ePais varPais = new ePais();
                string paisJSON = JsonConvert.SerializeObject(varPais);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("pais/getPaises", paisJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtPais = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convrtir la tabla en una lista de Puestos
                foreach (DataRow row in dtPais.Rows)
                {
                    ePais Pais = new ePais();
                    Pais.codigo = row["cod_pais"].ToString();
                    Pais.descripcion = row["desc_pais"].ToString();
                    Pais.abreviatgura = row["abreviatura"].ToString();
                    listPaises.Add(Pais);
                }
            }
            catch
            {
                return null;
            }
            return Json(listPaises, JsonRequestBehavior.AllowGet);
        }


    }
}