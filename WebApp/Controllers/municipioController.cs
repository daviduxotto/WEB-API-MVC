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
    public class municipioController : Controller
    {
        public async Task<ActionResult> getMunicipioxDepartamento(string codPais, string codDepto)
        {
            List<eMunicipio> listMunicipio = new List<eMunicipio>();
            DataTable dtMunicipio = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eMunicipio municipio = new eMunicipio();
                municipio.codPais = codPais;
                municipio.codDepartamento = codDepto;
                string municipioJSON = JsonConvert.SerializeObject(municipio);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("municipio/getMunicipioxDepartamento", municipioJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que responde la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtMunicipio = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //conviertir la tabla en una lista de empresas
                foreach (DataRow row in dtMunicipio.Rows)
                {
                    eMunicipio varMunicipio = new eMunicipio();
                    varMunicipio.codigo = row["cod_municipio"].ToString();
                    varMunicipio.descripcion = row["desc_municipio"].ToString();

                    listMunicipio.Add(varMunicipio);
                }
            }
            catch
            {
                return null;
            }
            return Json(listMunicipio, JsonRequestBehavior.AllowGet);
        }
    }
}