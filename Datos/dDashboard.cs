using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data.MySqlClient;

namespace Datos
{
   public class dDashboard : conexionBD
   {
      public DataTable infoVentavsProyeccion(eDashboard dashboard)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_estadisticovtasvrspresupxes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmFecha = adaptador.SelectCommand.Parameters.Add("parfechacalculo", MySqlDbType.Date);
               prmFecha.Value = dashboard.fecha; 
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
