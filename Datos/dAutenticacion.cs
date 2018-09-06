using System;
using Entidades;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace Datos
{
    public class dAutenticacion : conexionBD
   {
      public  int validartokenUsuario(eUsuario usuario)
      {
         conexionBD cnxbd = new conexionBD();
         if (cnxbd.abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("fn_validatoken", cnxbd.Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            MySqlParameter parUsuario = comando.Parameters.Add("parcodusuario", MySqlDbType.VarChar, 15);
            parUsuario.Value = dLibreria.left(usuario.codigo, 15);
            MySqlParameter parToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            parToken.Value = dLibreria.left(usuario.token, 64);
            MySqlParameter parReturn = comando.Parameters.Add("return", MySqlDbType.Int32, 50);
            parReturn.Direction = ParameterDirection.ReturnValue;
            try
            {
               comando.CommandTimeout = 10;
               comando.ExecuteScalar();
               return Convert.ToInt32(comando.Parameters["return"].Value.ToString());
            }
            catch
            {
               return -9000001;
            }
            finally
            {
               cnxbd.cerrarBD();
            }

         }
         else
         {
            return -900001;
         }

      }

   }


}
