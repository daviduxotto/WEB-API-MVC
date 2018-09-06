using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eFacturaCajaChica
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      public string codProveedor { get; set; }
      public string descProveedor { get; set; }
      public string codDoctoCxP { get; set; }
      public string descDoctoCxP { get; set; }
      public string serie { get; set; }
      public string numero { get; set; }
      public DateTime fecha { get; set; }
      public string justificacion { get; set; }
      public decimal montoBien { get; set; }
      public decimal montoServicio { get; set; }
      public decimal montoTotal { get; set; }
      public bool autorizacion { get; set; }
      public bool validado { get; set; }
      public string token { get; set; }
      public string codProveedorAnterior { get; set; }
      public string codDoctoCxPAnterior { get; set; }
      public string serieNuevo { get; set; }
      public string numeroNuevo { get; set; }

   }
}
