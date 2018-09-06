using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Entidades;

namespace Datos
{
   public class dSystem : conexionBD
   {
      public string getFechaSistema(eToken partoken)
      {
         string varfecha = "";
         if (abrirBD())
         {
            try
            {
               MySqlCommand comando = new MySqlCommand("get_systemdate", Conexion);
               comando.CommandType = CommandType.StoredProcedure;
               MySqlParameter parToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
               parToken.Value = dLibreria.left(partoken.token, 64);
               comando.Parameters.Add(new MySqlParameter("parfechasistema", MySqlDbType.Date));
               comando.Parameters["parfechasistema"].Direction = ParameterDirection.Output;
               comando.ExecuteScalar();
               varfecha = comando.Parameters["parfechasistema"].Value.ToString();
            }
            catch
            {
               return varfecha;
            }
            finally
            {
               cerrarBD();
            }
         }
         return varfecha;
      }

      public string getFechaHoraSistema(eToken partoken)
      {
         string varfecha = "";
         if (abrirBD())
         {
            try
            {
               MySqlCommand comando = new MySqlCommand("get_systemdatetime", Conexion);
               comando.CommandType = CommandType.StoredProcedure;
               MySqlParameter parToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
               parToken.Value = dLibreria.left(partoken.token, 64);
               comando.Parameters.Add(new MySqlParameter("parfechasistema", MySqlDbType.Date));
               comando.Parameters["parfechasistema"].Direction = ParameterDirection.Output;
               comando.ExecuteScalar();
               varfecha = comando.Parameters["parfechasistema"].Value.ToString();
            }
            catch
            {
               return varfecha;
            }
            finally
            {
               cerrarBD();
            }
         }
         return varfecha;
      }

      public string getLogoEmpresa(eEmpresa empresa)
      {
         string varlogo = "";
         if (abrirBD())
         {
            try
            {
               MySqlCommand comando = new MySqlCommand("get_emprepathlogo", Conexion);
               comando.CommandType = CommandType.StoredProcedure;
               MySqlParameter parempresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               parempresa.Value = dLibreria.left(empresa.codigo,5);
               comando.Parameters.Add(new MySqlParameter("parpathlogo", MySqlDbType.VarChar,100));
               comando.Parameters["parpathlogo"].Direction = ParameterDirection.Output;
               comando.ExecuteScalar();
               varlogo = comando.Parameters["parpathlogo"].Value.ToString();
            }
            catch
            {
               return varlogo;
            }
            finally
            {
               cerrarBD();
            }
         }
         return varlogo;
      }
   }
}
