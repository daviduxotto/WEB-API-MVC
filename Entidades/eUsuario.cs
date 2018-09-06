using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Entidades
{
   public class eUsuario
   {
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string codigo { get; set; }
      [Required(ErrorMessage = "*Campo obligatorio")]
      public string clave { get; set; }
      public string aplicacion { get; set; }
      public string nombre { get; set; }
      public string apellido { get; set; }
      public System.Int32 nivel { get; set; }
      public string vencimiento { get; set; }
      public DateTime fechaVencimiento { get; set; }
      public string mesesAnteriores { get; set; }
      public string supervisor { get; set; }
      public string existencias { get; set; }
      public string estado { get; set; }
      public DataTable xmlentorno { get; set; }

      public string equipo { get; set; }
      public string ipEquipo { get; set; }
      public string ipInternet { get; set; }
      public string token { get; set; }
      public string usuariologin { get; set; }
      public string tag { get; set; }
      public eUsuario(string parcodigo, string partoken)
      {
         this.usuariologin = parcodigo;
         this.token = partoken;
      }
      public eUsuario()
      { }
   }
   }
