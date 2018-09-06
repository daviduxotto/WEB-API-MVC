using MySql.Data.MySqlClient;
using System.Configuration;

namespace Datos
{
   public class conexionBD
   {
      public MySqlConnection Conexion = new MySqlConnection();
      public bool abrirBD()
      {
         try
         {
            Conexion.ConnectionString = ConfigurationManager.ConnectionStrings["cnx"].ToString();
            Conexion.Open();
            return true;
         }
         catch {return false; }
      }

      public bool cerrarBD()
      {
         try
         {
            Conexion.Close();
            return true;
         }
         catch { return false; }
      }

   }
}
  