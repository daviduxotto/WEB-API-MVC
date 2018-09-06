using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eFacturaCupon
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      public string descEmpresa { get; set; }
      public string descSucursal { get; set; }
      public DateTime fecha { get; set; }
      public string codEmisor { get; set; }
      public string descEmisor { get; set; }
      public string codDoctoPos { get; set; }
      public string descTipoDocto{ get; set; }
      public string noSerie { get; set; }

      public string noFactura { get; set; }
      public string codCliente { get; set; }
      public string descCliente { get; set; }

      public bool aplicaDescuento { get; set; }
      public decimal porcentajeDescuento { get; set; }
      public bool validado { get; set; }
      public decimal montoDescuento{ get; set; }
      public decimal montoTotal { get; set; }
      public string token { get; set; }
   }
}
