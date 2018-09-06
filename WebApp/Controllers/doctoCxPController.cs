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
    public class doctoCxPController : Controller
    {
        // GET: doctoCxP
        public async Task<ActionResult> getDoctoCxPxEmpresa()
        {
            List<eDoctoCxP> listDoctoCxP = new List<eDoctoCxP>();
            DataTable dtDoctoCxP = new DataTable();
            try
            {
                HttpClient client = new HttpClient();
                //le doy la direccion del servidor y key de la app cliente
                client.BaseAddress = new Uri(ConfigurationManager.ConnectionStrings["servidor"].ToString());
                eDoctoCxP parDoctoCxP = new eDoctoCxP();
                parDoctoCxP.codEmpresa = Session["empresa"].ToString();
                string parDoctoCxPJSON = JsonConvert.SerializeObject(parDoctoCxP);
                HttpResponseMessage response = await client.PostAsJsonAsync<string>("doctoCxP/getDoctoCxPxEmpresa", parDoctoCxPJSON);
                response.EnsureSuccessStatusCode();
                //leo lo que me respondio la peticion
                string salidaJSON = await response.Content.ReadAsStringAsync();
                dtDoctoCxP = JsonConvert.DeserializeObject<DataTable>(salidaJSON);
                //convierto la tabla en una lista 
                foreach (DataRow row in dtDoctoCxP.Rows)
                {
                    eDoctoCxP documento = new eDoctoCxP();
                    documento.codDoctoCxP = row["cod_docto_cxp"].ToString();
                    documento.descDoctoCxP = row["desc_docto_cxp"].ToString();
                    listDoctoCxP.Add(documento);
                }

            }
            catch
            {
                return null;
            }
            return Json(listDoctoCxP, JsonRequestBehavior.AllowGet);
        }
    }
}