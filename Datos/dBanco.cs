using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Datos
{
  public class dBanco : conexionBD

   {
      public DataTable getBancosxEmpresa(eBanco banco)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_bancosxempresaweb", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = banco.codEmpresa;

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

      public DataTable getCuentasXBanco(eCuentaBanco cuentaBanco)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_cuentabancoxbancoxesweb", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = cuentaBanco.codEmpresa;
               MySqlParameter prmBanco = adaptador.SelectCommand.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
               prmBanco.Value = cuentaBanco.codBanco;
               MySqlParameter prmSucursal= adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = cuentaBanco.codSucursal;

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
      public DataTable getDoctoBanco(eDoctoBanco doctoBanco)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_doctobcopositivxctanotc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = doctoBanco.codEmpresa;
               MySqlParameter prmBanco = adaptador.SelectCommand.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
               prmBanco.Value = doctoBanco.codBanco;
               MySqlParameter prmDoctoBanco= adaptador.SelectCommand.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
               prmDoctoBanco.Value = doctoBanco.codCuentaBanco;

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
      public DataTable getDoctoBancoNegativos(eDoctoBanco doctoBanco)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_doctobconegativxctanotc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = doctoBanco.codEmpresa;
               MySqlParameter prmBanco = adaptador.SelectCommand.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
               prmBanco.Value = doctoBanco.codBanco;
               MySqlParameter prmDoctoBanco = adaptador.SelectCommand.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
               prmDoctoBanco.Value = doctoBanco.codCuentaBanco;

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
      public DataTable getDoctoBancoTC(eDoctoBanco doctoBanco)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_doctobcopositivxctatc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = doctoBanco.codEmpresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar,4 );
               prmSucursal.Value = doctoBanco.codSucursal;
               MySqlParameter prmEmisorTC = adaptador.SelectCommand.Parameters.Add("parcodemisortarjeta", MySqlDbType.VarChar, 2);
               prmEmisorTC.Value = doctoBanco.codEmisorTC;

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
