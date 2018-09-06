using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
   public class eVerificacion
   {
      public string codEmpresa { get; set; }
      public string codSucursal { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] 
      public DateTime codDia { get; set; }
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime codDia2 { get; set; }
      public string codPantalla { get; set; }
      public string formatoInforme { get; set; }
      public bool informeResumido { get; set; }
    }
}
