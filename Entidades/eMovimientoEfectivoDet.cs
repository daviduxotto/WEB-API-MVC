using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
   public class eMovimientoEfectivoDet
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      public string descEmpresa { get; set; }
      public string descSucursal { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codBanco { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codBancoAnterior { get; set; }
      public string descBanco { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codCuenta { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codCuentaAnterior { get; set; }
      public string descCuenta { get; set; }
      public string codDoctoBanco { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDoctoBancoAnterior { get; set; }
      public string descDoctoBanco { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDocumento { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDocumentoAnterior { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codUnidadBolsa { get; set; }
      public string token { get; set; }
      public DateTime fecha { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public DateTime fechaBanco { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string concepto { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double monto { get; set; }
      public string tipoPago { get; set; }
      public bool conciliado { get; set; }
      public bool revertido { get; set; }
      public DateTime dia { get; set; }
      // campos para la revesion 
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDoctoBancoReversion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDocumentoReversion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double montoReversion { get; set; }
   }
}
