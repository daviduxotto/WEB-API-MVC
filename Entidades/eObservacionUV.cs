using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eObservacionUV
   {
      public int codObservacion { get; set; } 
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime codDia{ get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime codDia2 { get; set; }
      public string codPantalla { get; set; }
      public string descPantalla { get; set; }
      public string usuario { get; set; }
      public string descripcion { get; set; }
      public string token { get; set; }
      public string fechaObservacion { get; set; }
      public string tipoObservacion { get; set; }
      public string fecha { get; set; }
      public string formatoInforme { get; set; }
    } 
}
