

using System;

namespace Entidades
{
   public class eInformeDBR
   {

      public string codEmpresa { get; set; }
      public string descEmpresa { get; set; }
      public string codEstacion { get; set; }
      public string descEstacion { get; set; }
      public DateTime dia { get; set; }
      public string codGrupo { get; set; }
      public string descGrupo { get; set; }
      public string codInforme { get; set; }
      public string descInforme { get; set; }
      public bool obligatorio { get; set; }
      public bool recibido { get; set; }
      public bool noAplica { get; set; }
      public bool observacionesUV { get; set; }
      public string observaciones { get; set; }
      public string xmlinformesdbr { get; set; }
      public string xmlinformectrl { get; set; }
      public string token { get; set; }
    }
}
