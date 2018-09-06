using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace Datos
{
   public class dUsuario : conexionBD
   {
      public eParSalida verificarUsuario(eUsuario usuario)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_verifusuario", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigo = comando.Parameters.Add("parcodigo", MySqlDbType.VarChar, 15);
            prmCodigo.Value = dLibreria.left(usuario.codigo, 15);
            MySqlParameter prmKey = comando.Parameters.Add(new MySqlParameter("paruserkey", MySqlDbType.VarChar, 15));
            prmKey.Value = dLibreria.left(usuario.clave, 15);
            MySqlParameter prmAplicacion = comando.Parameters.Add(new MySqlParameter("paraplicacion", MySqlDbType.VarChar, 50));
            prmAplicacion.Value = dLibreria.left(usuario.aplicacion, 50);
            MySqlParameter prmUsuario = comando.Parameters.Add(new MySqlParameter("parusuario", MySqlDbType.VarChar, 15));
            prmUsuario.Value = dLibreria.left(usuario.usuariologin, 15);
            MySqlParameter prmEquipo = comando.Parameters.Add(new MySqlParameter("parequipo", MySqlDbType.VarChar, 15));
            prmEquipo.Value = dLibreria.left(usuario.equipo, 15);
            MySqlParameter prmIpEquipo = comando.Parameters.Add(new MySqlParameter("paripequipo", MySqlDbType.VarChar, 15));
            prmIpEquipo.Value = dLibreria.left(usuario.ipEquipo, 15);
            MySqlParameter prmIpInternet = comando.Parameters.Add(new MySqlParameter("paripinternet", MySqlDbType.VarChar, 15));
            prmIpInternet.Value = dLibreria.left(usuario.ipInternet, 15);
            //parametros de salida
            comando.Parameters.Add(new MySqlParameter("parretorno", MySqlDbType.Int32, 15));
            comando.Parameters["parretorno"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("parsrvrstate", MySqlDbType.VarChar, 50));
            comando.Parameters["parsrvrstate"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("parerrormessage", MySqlDbType.VarChar, 150));
            comando.Parameters["parerrormessage"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("parnivelusuario", MySqlDbType.Int32, 15));
            comando.Parameters["parnivelusuario"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("partoken", MySqlDbType.VarChar, 64));
            comando.Parameters["partoken"].Direction = ParameterDirection.Output;
            try
            {
               comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
               parSalida.retorno = Convert.ToInt32(ex.HResult);
               parSalida.estado = "";
               parSalida.mensaje = ex.Message.ToString();
               parSalida.nivel = "";
               parSalida.token = "";
               return parSalida;
            }
            finally
            {
               cerrarBD();
            }
            parSalida.retorno = Convert.ToInt32(comando.Parameters["parretorno"].Value.ToString());
            parSalida.estado = comando.Parameters["parsrvrstate"].Value.ToString();
            parSalida.mensaje = comando.Parameters["parerrormessage"].Value.ToString();
            parSalida.nivel = comando.Parameters["parnivelusuario"].Value.ToString();
            parSalida.token = comando.Parameters["partoken"].Value.ToString();
         }

         else
         {
            // parametros de salida                
            parSalida.retorno = -10000;
            parSalida.estado = "";
            parSalida.mensaje = "Fallo de conexión al servidor";
            parSalida.nivel = "";
            parSalida.token = "";
         }
         return parSalida;
      }

      public eParSalida verificarAccesoUsuario(eUsuario usuario)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_verifaccess", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(usuario.token, 64);
            MySqlParameter prmTag = comando.Parameters.Add(new MySqlParameter("partag", MySqlDbType.VarChar, 25));
            prmTag.Value = dLibreria.left(usuario.tag, 25);
            //parametros de salida
            comando.Parameters.Add(new MySqlParameter("parretorno", MySqlDbType.Int32, 15));
            comando.Parameters["parretorno"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("parsrvrstate", MySqlDbType.VarChar, 50));
            comando.Parameters["parsrvrstate"].Direction = ParameterDirection.Output;
            comando.Parameters.Add(new MySqlParameter("parerrormessage", MySqlDbType.VarChar, 150));
            comando.Parameters["parerrormessage"].Direction = ParameterDirection.Output;

            try
            {
               comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
               parSalida.retorno = Convert.ToInt32(ex.HResult);
               parSalida.estado = "";
               parSalida.mensaje = ex.Message.ToString();
               parSalida.nivel = "";
               parSalida.token = "";
               return parSalida;
            }
            finally
            {
               cerrarBD();
            }
            parSalida.retorno = Convert.ToInt32(comando.Parameters["parretorno"].Value.ToString());
            parSalida.estado = comando.Parameters["parsrvrstate"].Value.ToString();
            parSalida.mensaje = comando.Parameters["parerrormessage"].Value.ToString();
            parSalida.nivel = comando.Parameters["parnivelusuario"].Value.ToString();
            parSalida.token = comando.Parameters["partoken"].Value.ToString();
         }

         else
         {
            // parametros de salida                
            parSalida.retorno = -10000;
            parSalida.estado = "";
            parSalida.mensaje = "Fallo de conexión al servidor";
            parSalida.nivel = "";
            parSalida.token = "";
         }
         return parSalida;
      }

      public DataTable verificarTodoAccesoUsuario(eUsuario usuario)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("proc_verifallaccess", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmToken = adaptador.SelectCommand.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
               prmToken.Value = usuario.token;

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
