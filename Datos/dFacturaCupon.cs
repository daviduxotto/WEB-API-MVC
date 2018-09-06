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
  public  class dFacturaCupon : conexionBD
   {
      public DataTable getFactuCuponesxEstacionxFecha(eFacturaCupon facturaCupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {

               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_factcuponxestacionxrgofecha", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(facturaCupon.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(facturaCupon.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = facturaCupon.fecha;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = facturaCupon.fecha;
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

      public DataTable getLiquidacionFacturaCupones(eFacturaCupon Cupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_liquidafactcuponxemprexesxdia", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(Cupon.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(Cupon.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfecha", MySqlDbType.Date);
               prmDia.Value = Cupon.fecha;

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

      public eParSalida validarFacturaCupon(eFacturaCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_validafacturacuponcxc", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmDocto = comando.Parameters.Add("pardoctopos", MySqlDbType.VarChar, 6);
            prmDocto.Value = Cupon.codDoctoPos;
            MySqlParameter prmSerie = comando.Parameters.Add("parnoserie", MySqlDbType.VarChar, 20);
            prmSerie.Value = Cupon.noSerie;
            MySqlParameter prmFactura = comando.Parameters.Add("parfactura", MySqlDbType.VarChar, 15);
            prmFactura.Value = Cupon.noFactura;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "04";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Cupon.token, 64);


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
   }
}
