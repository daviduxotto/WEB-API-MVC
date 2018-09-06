using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;
namespace Datos
{
   public class dEmpresa: conexionBD
   {
      public DataTable getEmpresasXUsuario(eUsuario usuario)
      {
         DataTable tblResultado = new DataTable();       
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_emprexuser", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigo = adaptador.SelectCommand.Parameters.Add("parusuario", MySqlDbType.VarChar, 15);
               prmCodigo.Value = usuario.usuariologin;
    
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
      public DataTable get_empreconfigcodprod(eEmpresa empresa)
      {
         DataTable tblResultado = new DataTable();
       
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_empreconfigcodprod", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigo = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmCodigo.Value = empresa.codigo;
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
