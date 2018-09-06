using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eDoctoOtros
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      public string codTipoDocto { get; set; }
      public string descTipoDocto { get; set; }
      public string codDocto { get; set; }
      public DateTime fechaTrabajo { get; set; }
      public string fecha { get; set; }
      public string justificacion { get; set; }
      public decimal monto { get; set; }
      public bool autorizacion { get; set; }
      public bool validado { get; set; }
      public string token { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime dia { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime dia2 { get; set; }
      public string formatoInforme { get; set; }
        public string codDoctoNuevo { get; set; }

   }
}
