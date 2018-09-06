using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class ePresupuesto
   {
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string empresa { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string estacion { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string  area { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string  departamento { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, short.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public int anio { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string tipoPresupuesto { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string CodcuentaContable { get; set; }
      public string DesccuentaContable { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres01 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec01 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald01 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres02 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec02 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald02 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres03 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec03 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald03 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres04 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec04 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald04 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres05 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec05 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald05 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres06 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec06 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald06 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres07 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec07 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald07 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres08 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec08 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald08 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres09 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec09 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald09 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres10 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec10 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald10 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres11 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec11 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald11 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double pres12 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejec12 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double sald12 { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double presTotal { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double ejecTotal { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      [Range(0, Double.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
      public double saldTotal { get; set; }
      public string token { get; set; }
      public string  detalleXML { get; set; }
      public string  descEmpresa { get; set; }
      public string descEstacion { get; set; }
      public string descArea { get; set; }
      public string descDepartamento { get; set; }
      public string claseProducto  { get; set; }
      public string familiaProducto { get; set; }
      public string  codProducto { get; set; }
      public string descProducto { get; set; }
      public int anioPresupuesto { get; set; }
      public decimal precioPublico { get; set; }
      public decimal margen { get; set; }
      public decimal transporte { get; set; }

        //campos para distribuidora
      public decimal totalTransporte { get; set; }
        
      public decimal precioCompraSuper { get; set; }
      public decimal precioCompraRegular { get; set; }
      public decimal precioCompraDiesel { get; set; }          

      public decimal transporteSuper { get; set; }
      public decimal transporteRegular { get; set; }
      public decimal transporteDiesel { get; set; }

      public decimal margenSuper { get; set; }
      public decimal margenRegular { get; set; }
      public decimal margenDiesel { get; set; }


      public decimal margenUtilidad { get; set; }
      public decimal precioTransporte { get; set; }
      
        // filtrar cuenta corriente vacía
      public bool cuentaMovimientoVacia { get; set; }
      public string grupoPresupuesto { get; set; }
      public string subGrupoPresupuesto { get; set; }
      public string distribRetailUneg { get; set; }
      public string mes { get; set; }
      public string formatoInforme { get; set; }


    }
}
