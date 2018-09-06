using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
namespace Datos
{
   public class dSucursal : conexionBD
   {
       public DataTable getSucursalXUserXEmpre(eSucursal parsucursal)
      {        
         DataTable tblResultado = new DataTable();       
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_estacionxuser", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigoUsuario = adaptador.SelectCommand.Parameters.Add("parusuario", MySqlDbType.VarChar, 15);
               prmCodigoUsuario.Value = dLibreria.left(parsucursal.codigoUsuario,15);
               MySqlParameter prmCodigoEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmCodigoEmpresa.Value = dLibreria.left(parsucursal.codigoEmpresa,5);      
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
