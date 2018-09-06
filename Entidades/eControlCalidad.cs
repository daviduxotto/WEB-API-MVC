

using System;

namespace Entidades
{
   public class eControlCalidad
   {
      public string  codEmpresa { get; set; }
      public string codEstacion { get; set; }
      public DateTime dia { get; set; }
      public string codControlCalidad{ get; set; }
      public string descControlCalidad { get; set; }
      public bool aprobado { get; set; }
      public string observaciones { get; set; }
      public bool observacionesUV { get; set; }
      public string xmlinformesdbr { get; set; }
   }
}
