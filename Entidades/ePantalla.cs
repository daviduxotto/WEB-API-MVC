using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ePantalla
    {
        public string codEmpresa { get; set; }
        public string codSucursal { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime codDia { get; set; }
        public string codPantalla { get; set; }
        public string descPantalla { get; set; }
        public string token { get; set; }
   }
}
