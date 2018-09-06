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
   public class dVerificacion : conexionBD
   {
      public DataTable getInformeVefificadoUV(eVerificacion Verificacion)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detverificacion", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Verificacion.codEmpresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Verificacion.codSucursal;
               MySqlParameter prmDia1= adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia1.Value = Verificacion.codDia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = Verificacion.codDia2;
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

      public DataTable getInformeVefificadoUVHorizontal(eVerificacion Verificacion)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detverificacion_horizontal", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Verificacion.codEmpresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Verificacion.codSucursal;
               MySqlParameter prmDia1 = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia1.Value = Verificacion.codDia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = Verificacion.codDia2;
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
