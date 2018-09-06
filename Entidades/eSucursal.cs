using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
   public class eSucursal
   {
      public string codigoEmpresa { get; set; }
      public string codigoSucursal { get; set; }
      public string codigoUsuario { get; set; }
      public string nombre { get; set; }
      public string descripcion { get; set; }
      public string ventaCredito { get; set; }
      public string ventaPrepago { get; set; }
      public string cargaArchivoEnvoy { get; set; }
      public string cargaArchivoPos { get; set; }
      public string cargaDatosEdgas { get; set; }
      public string codTurnoEdgas { get; set; }
      public string servidorEdgas { get; set; }
      public string baseDeDatosEdgas { get; set; }
      public string puertoEdgas { get; set; }
      public string usuarioEdgas { get; set; }
      public string montoDiferencia { get; set; }
      public string galonDiferencia { get; set; }
      public string estado { get; set; }
      public string token { get; set; }
   }
}
