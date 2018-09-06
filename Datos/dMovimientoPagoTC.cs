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
    public class dMovimientoPagoTC:conexionBD
    {
        public DataTable getMovPagoTCXFecha(eMovimientoPagoTC movimiento)
        {
            DataTable tblResultado = new DataTable();
            if (abrirBD())
            {
                try
                {
                    MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detmovbcopagoclientetcsxfch", Conexion);
                    adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                    prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
                    MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
                    prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
                    MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
                    prmDia.Value = movimiento.dia;
                    MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
                    prmDia2.Value = movimiento.dia;
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
      public DataTable getMovPagoTCXFechaDetalle(eMovimientoPagoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detmovbcopagoclientetcxesxfch_det", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = movimiento.dia;
               MySqlParameter prmBanco = adaptador.SelectCommand.Parameters.Add("parcodbanco", MySqlDbType.VarChar, 2);
               prmBanco.Value = dLibreria.left(movimiento.codBanco, 2);
               MySqlParameter prmCuenta = adaptador.SelectCommand.Parameters.Add("parcodcuenta", MySqlDbType.VarChar, 15);
               prmCuenta.Value = dLibreria.left(movimiento.codCuenta, 15);
               MySqlParameter prmDocto = adaptador.SelectCommand.Parameters.Add("parcoddoctobanco", MySqlDbType.VarChar, 6);
               prmDocto.Value = dLibreria.left(movimiento.codDoctoBanco, 6);
               MySqlParameter prmDocumento = adaptador.SelectCommand.Parameters.Add("parcoddocumento", MySqlDbType.VarChar, 15);
               prmDocumento.Value = dLibreria.left(movimiento.codDocumento, 15);
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

      public eParSalida conciliar(eMovimientoPagoTC MovimientoEfectivo)
        {
            eParSalida parSalida = new eParSalida();
            if (abrirBD())
            {
                MySqlCommand comando = new MySqlCommand("proc_conciliadoctobancariopagocliente", Conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //parametros de entrada
                // casting de parametros conforme al procedimentno almacenado
                MySqlParameter prmBanco = comando.Parameters.Add("parcodbanco", MySqlDbType.VarChar, 2);
                prmBanco.Value = dLibreria.left(MovimientoEfectivo.codBanco, 2);
                MySqlParameter prmCuenta = comando.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
                prmCuenta.Value = dLibreria.left(MovimientoEfectivo.codCuenta, 15);
                MySqlParameter prmDoctoBanco = comando.Parameters.Add("partipodoctobanco", MySqlDbType.VarChar, 6);
                prmDoctoBanco.Value = dLibreria.left(MovimientoEfectivo.codDoctoBanco, 6);
                MySqlParameter prmDocumento = comando.Parameters.Add("parnodocumento", MySqlDbType.VarChar, 15);
                prmDocumento.Value = dLibreria.left(MovimientoEfectivo.codDocumento, 15);
                MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
                MySqlParameter prmFechaBanco = comando.Parameters.Add("parfechabanco", MySqlDbType.Date);
                prmFechaBanco.Value = MovimientoEfectivo.fechaBanco;
                MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
                prmPantalla.Value = "06";
                 MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
                prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
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
        public DataTable getLiquidacionPagoClienteXempresa(eMovimientoPagoTC movimiento)
        {
            DataTable tblResultado = new DataTable();
            if (abrirBD())
            {
                try
                {
                    MySqlDataAdapter adaptador = new MySqlDataAdapter("get_liquidacionpagoclientexemprexes", Conexion);
                    adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                    prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
                    MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
                    prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
                    MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfecha", MySqlDbType.Date);
                    prmDia.Value = movimiento.fecha;
                    MySqlParameter prmTipoPago = adaptador.SelectCommand.Parameters.Add("parpagotc", MySqlDbType.VarChar, 1);
                    prmTipoPago.Value = dLibreria.left(movimiento.tipoPago, 1);
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
        public eParSalida cambioCodigo(eMovimientoPagoTC MovimientoPagoCC)
        {
            eParSalida parSalida = new eParSalida();
            if (abrirBD())
            {
                MySqlCommand comando = new MySqlCommand("proc_chgcoddoctobcopagoclientees", Conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //parametros de entrada
                // casting de parametros conforme al procedimentno almacenado
                MySqlParameter prmBanco = comando.Parameters.Add("parbanconew", MySqlDbType.VarChar, 2);
                prmBanco.Value = dLibreria.left(MovimientoPagoCC.codBanco, 2);
                MySqlParameter prmCuenta = comando.Parameters.Add("parcuentanew", MySqlDbType.VarChar, 15);
                prmCuenta.Value = dLibreria.left(MovimientoPagoCC.codCuenta, 15);
                MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobconew", MySqlDbType.VarChar, 6);
                prmDoctoBanco.Value = dLibreria.left(MovimientoPagoCC.codDoctoBanco, 6);
                MySqlParameter prmDocumento = comando.Parameters.Add("parnodoctonew", MySqlDbType.VarChar, 15);
                prmDocumento.Value = dLibreria.leftRelleno(MovimientoPagoCC.codDocumento, 15);
                MySqlParameter prmBancoA = comando.Parameters.Add("parbancoant", MySqlDbType.VarChar, 2);
                prmBancoA.Value = dLibreria.left(MovimientoPagoCC.codBancoAnterior, 2);
                MySqlParameter prmCuentaA = comando.Parameters.Add("parcuentaant", MySqlDbType.VarChar, 15);
                prmCuentaA.Value = dLibreria.left(MovimientoPagoCC.codCuentaAnterior, 15);
                MySqlParameter prmDoctoBancoA = comando.Parameters.Add("pardoctobcoant", MySqlDbType.VarChar, 6);
                prmDoctoBancoA.Value = dLibreria.left(MovimientoPagoCC.codDoctoBancoAnterior, 6);
                MySqlParameter prmDocumentoA = comando.Parameters.Add("parnodoctoant", MySqlDbType.VarChar, 15);
                prmDocumentoA.Value = dLibreria.left(MovimientoPagoCC.codDocumentoAnterior, 15);
                MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                prmEmpresa.Value = dLibreria.left(MovimientoPagoCC.codEmpresa, 5);
                MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
                prmToken.Value = dLibreria.left(MovimientoPagoCC.token, 64);
                MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
                 prmPantalla.Value = "06";

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

      public DataTable infoMovClienteTCXFecha(eMovimientoPagoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_deptoclientetcconcixes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = movimiento.dia2;
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

      public DataTable infoClientetcVsConciliado(eMovimientoPagoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_clientetcvsconci", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = movimiento.dia2;
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
      public DataTable infoClientetcVsConciliadoGrafico(eMovimientoPagoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_clientetcvsconci_grafico", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = movimiento.dia2;
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
      public eParSalida getEstadoPantalla(eMovimientoPagoTC Movimiento)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("get_estado_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(Movimiento.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(Movimiento.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = Movimiento.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "06";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Movimiento.token, 64);
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
      public eParSalida abrirPantallaUV(eMovimientoPagoTC Movimiento)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_abrir_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(Movimiento.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(Movimiento.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = Movimiento.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "06";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Movimiento.token, 64);
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
      public eParSalida cerrarPantallaUV(eMovimientoPagoTC Movimiento)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_cerrar_pantalla_pagoclientetc_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(Movimiento.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(Movimiento.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmDia.Value = Movimiento.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "06";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Movimiento.token, 64);
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
