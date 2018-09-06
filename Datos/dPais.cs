

using Entidades;
using MySql.Data.MySqlClient;
using System.Data;

namespace Datos
{
   public  class dPais : conexionBD
   {
      public DataTable getPaises(ePais Pais)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_all_pais", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;       
               adaptador.Fill(tblResultado);
            }
            catch
            {
               return tblResultado;
            }
            finally
            {
               cerrarBD();
            }
         }
         return tblResultado;
      }
   }
}
