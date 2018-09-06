using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eCupon
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      public string descEmpresa { get; set; }
      public string descSucursal { get; set; }
      public DateTime dia { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codEmisor { get; set; }
      public string descEmisor { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDenominacion { get; set; }
      public string descDenominacion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Int32.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public int cantidad { get; set; }
      public decimal valorQuetzales { get; set; }
      public decimal montoTotal { get; set; }
      public bool validado { get; set; }
      public string token { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Int32.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public int numero { get; set; }
      public int numeroAnterior { get; set; }

   }
}
