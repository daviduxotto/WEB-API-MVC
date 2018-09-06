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
   public class dEstadoDepartamento : conexionBD
   {
      public DataTable getDepartamentoxPais(eEstadoDepartamento EstadoDepartamento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_estadodeptoxpais", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmPais = adaptador.SelectCommand.Parameters.Add("parcodpais", MySqlDbType.VarChar, 3);
               prmPais.Value = EstadoDepartamento.codPais;

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
