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
    public class EstadoDepartamentoController : Controller
    {
        public async Task<ActionResult> getDepartamentoxPais(string codPais)
        {
            List<eEstadoDepartamento> listDeptoPais = new List<eEstadoDepartamento>();
            DataTable dtDeptoPais = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                // direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eEstadoDepartamento varDeptoPais = new eEstadoDepartamento();
                varDeptoPais.codPais = codPais;
                string deptoJSON = JsonConvert.SerializeObject(varDeptoPais);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("estadodepartamento/getDepartamentoxPais", deptoJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que responde la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtDeptoPais = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista de empresas
                foreach (DataRow row in dtDeptoPais.Rows)
                {
                    eEstadoDepartamento varDepto = new eEstadoDepartamento();
                    varDepto.codigo = row["cod_estadodepto"].ToString();
                    varDepto.descripcion = row["desc_estadodepto"].ToString();
                    listDeptoPais.Add(varDepto);
                }

            }
            catch
            {
                return null;
            }
            return Json(listDeptoPais, JsonRequestBehavior.AllowGet);
        }
    }
}