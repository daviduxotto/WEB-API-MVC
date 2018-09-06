

using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
   public class eDashboard
   {
      [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      public DateTime fecha { get; set; }
   }
}
