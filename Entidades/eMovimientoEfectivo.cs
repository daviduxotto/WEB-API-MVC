using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eMovimientoEfectivo
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime dia { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime dia2 { get; set; }
      public string detalle { get; set; }
      public string token { get; set; }
      public bool informeResumido { get; set; }
      public bool pendienteConciliar { get; set; }
      public string formatoInforme { get; set; }

    }
}
