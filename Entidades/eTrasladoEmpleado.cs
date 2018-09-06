using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eTrasladoEmpleado
   {
      public int codigoCorp { get; set; }
      public string nombre { get; set; }
      public string  empresaAnterior { get; set; }
      public string  sucursalAnterior { get; set; }
      public string  empresa { get; set; }
      public string  sucursal { get; set; }
      public string  fechaTraslado { get; set; }
      public string  accion { get; set; }
   }
}
