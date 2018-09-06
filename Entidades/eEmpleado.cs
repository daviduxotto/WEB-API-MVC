using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eEmpleado
   {
      public int codigoCorp { get; set; }
      public int codigoEmpresa { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string empresa { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string estacion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string area { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string departamento { get; set; }
      public int codigo { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string primerNombre { get; set; }

      public string segundoNombre { get; set; }
      public string tercerNombre { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string primerApellido { get; set; }

      public string segundoApellido { get; set; }
      public string apellidoCasada { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codEstacion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codArea { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codDepartamento { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codPuesto { get; set; }
      public string descPuesto { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public decimal salario { get; set; }
      public string estado { get; set; }
      public string token { get; set; }
      public string descEmpresa { get; set; }
      public string descEstacion { get; set; }
      public string descArea { get; set; }
      public string descDepartamento { get; set; }
      public string detalleXML { get; set; }
      public int anioPresupuesto { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string dpi { get; set; }
      public string afiliacionIgss { get; set; }
      public string nit { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string genero { get; set; }
      public string telefonoMovil { get; set; }
      public string telefonoCasa { get; set; }
      public string email { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime fechaNacimiento { get; set; }
      public string codPaisNacimiento { get; set; }
      public string descPaisNacimiento { get; set; }
      public string codDepartamentoNacimiento { get; set; }
      public string descDepartamentoNacimiento { get; set; }
      public string codMunicipioNacimiento { get; set; }
      public string descMunicipioNacimiento { get; set; }
      public string codPaisDireccion { get; set; }
      public string descPaisDireccion { get; set; }
      public string codDepartamentoDireccion { get; set; }
      public string descDepartamentoDireccion { get; set; }
      public string codMunicipioDireccion { get; set; }
      public string descMunicipioDireccion { get; set; }
      public string direccion { get; set; }
      public int peso { get; set; }
      public int altura { get; set; }
      public string tieneEnfermedadCronica { get; set; }
      public string enfermedadCronica { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime fechaIngreso { get; set; }
      public DateTime fechaBaja { get; set; }
      public string StrfechaBaja { get; set; }
      public string StrfechaNacimiento { get; set; }
      public string nombreCompleto { get; set; }

      public bool bajaEmpleado { get; set; }
      public DateTime fechaTraslado { get; set; }
      public string codTipoBaja { get; set; }
      public string  descTipoBaja { get; set; }
      public string observacion { get; set; }
   }
}
