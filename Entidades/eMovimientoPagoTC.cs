using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class eMovimientoPagoTC
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
        [Range(0, double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
        public double monto { get; set; }
        public string tipoPago { get; set; }
        public bool conciliado { get; set; }
        public bool revertido { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dia { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dia2 { get; set; }
        public string codCliente { get; set; }
        public string descCliente { get; set; }
        public string codEmisor { get; set; }
        public string descEmisor { get; set; }
        public bool informeResumido { get; set; }
        public bool pendienteConciliar { get; set; }
        public string formatoInforme { get; set; }
      public string codDoctoPosFactu { get; set; }
      public string noSerie { get; set; }
      public string noFactura { get; set; }
      public double montoFactura { get; set; }
      public string codDoctoPosVale { get; set; }
      public string noVale { get; set; }
      [RegularExpression(@"^\d+\.\d{0,2}$")]
      [Range(0, 9999999999999999.99)]
      public double montoVale { get; set; }
      public string fechaVale { get; set; }
      public string fechaFactura { get; set; }
      public double saldoPendienteFactura { get; set; }
      public double montoPagadoFactura { get; set; }
   }
}
