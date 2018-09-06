using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
   public class dTrasladoEmpleado : conexionBD
   {
      public DataTable getTrasladoEmpleado(eTrasladoEmpleado TrasladoEmpleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_empleado_hst", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigoCorp = adaptador.SelectCommand.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
               prmCodigoCorp.Value = TrasladoEmpleado.codigoCorp;
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
