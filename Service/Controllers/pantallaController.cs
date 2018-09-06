using Datos;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
   /// <summary>
   /// pantallas de verificacion
   /// </summary>
    public class pantallaController : ApiController
    {
      /// <summary>
      /// Abre una pantalla de verificacion que ya estaba cerrada| ENTIDAD: ePantalla | ATRIBUTOS: 
      /// </summary>
      [HttpPost]
      public eParSalida abrirPantallaCerrada([FromBody]  string parPantallaJSON)
      {
         ePantalla varPantalla = new ePantalla();
         try
         {
            varPantalla = JsonConvert.DeserializeObject<ePantalla>(parPantallaJSON);
         }
         catch { }
         dPantalla obj = new dPantalla();
         return obj.abrirPantallaCerrada(varPantalla);

      }
   }
}
