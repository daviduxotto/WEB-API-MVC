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
   public class dMovimientoTC : conexionBD
   {
      public DataTable getMovTCXFecha(eMovimientoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detmovbcovtadiatcesxfch", Conexion);
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


      public eParSalida cambiarMonto(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_chgmontodepositovtadiaefectes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnumerodocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoTC.codDocumento, 15);
            MySqlParameter prmMonto = comando.Parameters.Add("parnuevomonto", MySqlDbType.Decimal);
            prmMonto.Value = MovimientoTC.monto;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);

            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";
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
               parSalida.retorno = Convert.ToInt16(ex.HResult);
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
      public eParSalida cambiarCodigo(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_chgcoddepositovtadiaefectes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanconew", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentanew", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobconew", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnodoctonew", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.leftRelleno(MovimientoTC.codDocumento, 15);
            MySqlParameter prmBancoA = comando.Parameters.Add("parbancoant", MySqlDbType.VarChar, 2);
            prmBancoA.Value = dLibreria.left(MovimientoTC.codBancoAnterior, 2);
            MySqlParameter prmCuentaA = comando.Parameters.Add("parcuentaant", MySqlDbType.VarChar, 15);
            prmCuentaA.Value = dLibreria.left(MovimientoTC.codCuentaAnterior, 15);
            MySqlParameter prmDoctoBancoA = comando.Parameters.Add("pardoctobcoant", MySqlDbType.VarChar, 6);
            prmDoctoBancoA.Value = dLibreria.left(MovimientoTC.codDoctoBancoAnterior, 6);
            MySqlParameter prmDocumentoA = comando.Parameters.Add("parnodoctoant", MySqlDbType.VarChar, 15);
            prmDocumentoA.Value = dLibreria.left(MovimientoTC.codDocumentoAnterior, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";

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

      public eParSalida nuevo(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_doctovtadiariaTCxes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuenta", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("pardocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.leftRelleno(MovimientoTC.codDocumento, 15);

            MySqlParameter prmbolsa = comando.Parameters.Add("parunidadbolsa", MySqlDbType.VarChar, 15);
            prmbolsa.Value = dLibreria.left(MovimientoTC.codUnidadBolsa, 15);
            MySqlParameter prmfecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmfecha.Value = MovimientoTC.fecha;
            MySqlParameter prmConcepto = comando.Parameters.Add("parconcepto", MySqlDbType.VarChar, 250);
            prmConcepto.Value = dLibreria.left(MovimientoTC.concepto, 250);
            MySqlParameter prmMonto = comando.Parameters.Add("parmontopago", MySqlDbType.Decimal);
            prmMonto.Value = MovimientoTC.monto;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoTC.codSucursal, 4);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";

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
      public eParSalida del(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("del_vtadiariaesTC", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuenta", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("pardocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoTC.codDocumento, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoTC.codSucursal, 4);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";
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
      public eParSalida conciliar(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_conciliadoctobancariovtadia", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parcodbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("partipodoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnodocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoTC.codDocumento, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmFechaBanco = comando.Parameters.Add("parfechabanco", MySqlDbType.Date);
            prmFechaBanco.Value = MovimientoTC.fechaBanco;

            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);
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
      public DataTable getLiquidacionMonetariaVentaDiariaXempresa(eMovimientoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_liquidacionmonetariadiaxemprexes", Conexion);
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
      public DataTable infoMovTCXFecha(eMovimientoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_deptotcconcixes", Conexion);
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
      public DataTable infoVentaDiariaTCvsConciliado(eMovimientoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_vtadiatcvsconci", Conexion);
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
      public DataTable infoVentaDiariaTCvsConciliadoGrafico(eMovimientoTC movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_vtadiatcvsconci_grafico", Conexion);
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

      public eParSalida newMovimientoTC(eMovimientoTC Movimiento)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_detmovbco_vtadiariaestc_web", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parcodbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(Movimiento.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(Movimiento.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("partipodoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(Movimiento.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnodocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(Movimiento.codDocumento, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(Movimiento.codEmpresa, 5);
            MySqlParameter prmEstacion = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmEstacion.Value = dLibreria.left(Movimiento.codSucursal, 4);
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Movimiento.dia;
            MySqlParameter prmConcepto = comando.Parameters.Add("parconcepto", MySqlDbType.VarChar, 250);
            prmConcepto.Value = dLibreria.left(Movimiento.concepto, 250);
            MySqlParameter prmEmisorTC = comando.Parameters.Add("parcodemisortarjeta", MySqlDbType.VarChar, 2);
            prmEmisorTC.Value = dLibreria.left(Movimiento.codEmisor, 2);
            MySqlParameter prmMonto = comando.Parameters.Add("parmontopago", MySqlDbType.Decimal);
            prmMonto.Value = Movimiento.monto;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Movimiento.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";
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

      public eParSalida delMovimientoTC(eMovimientoTC MovimientoTC)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("del_vtadiariaestc", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoTC.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuenta", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoTC.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoTC.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("pardocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoTC.codDocumento, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoTC.codEmpresa, 5);
            MySqlParameter prmEstacion = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmEstacion.Value = dLibreria.left(MovimientoTC.codSucursal, 4);
            MySqlParameter prmFechaBanco = comando.Parameters.Add("parfechabanco", MySqlDbType.Date);
            prmFechaBanco.Value = MovimientoTC.fechaBanco;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "03";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoTC.token, 64);
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
      public eParSalida getEstadoPantalla(eMovimientoTC Movimiento)
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
            prmPantalla.Value = "03";
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
      public eParSalida abrirPantallaUV(eMovimientoTC Movimiento)
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
            prmPantalla.Value = "03";
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
      public eParSalida cerrarPantallaUV(eMovimientoTC Movimiento)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_cerrar_pantalla_vtadiatc_uv", Conexion);
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
            prmPantalla.Value = "03";
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
