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
   public class dMovimientoEfectivo : conexionBD
   {
      public DataTable getMovEfectivoXFecha(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detmovbcovtadiaefectivesxfch", Conexion);
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


      public eParSalida cambiarMonto(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_chgmontodepositovtadiaefectes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoEfectivo.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentabanco", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoEfectivo.codCuenta,15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoEfectivo.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnumerodocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoEfectivo.codDocumento, 15);
            MySqlParameter prmMonto = comando.Parameters.Add("parnuevomonto", MySqlDbType.Decimal);
            prmMonto.Value = MovimientoEfectivo.monto;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";


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
      public eParSalida cambiarCodigo(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_chgcoddepositovtadiaefectes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanconew", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoEfectivo.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuentanew", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoEfectivo.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobconew", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoEfectivo.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("parnodoctonew", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.leftRelleno(MovimientoEfectivo.codDocumento, 15);
            MySqlParameter prmBancoA = comando.Parameters.Add("parbancoant", MySqlDbType.VarChar, 2);
            prmBancoA.Value = dLibreria.left(MovimientoEfectivo.codBancoAnterior, 2);
            MySqlParameter prmCuentaA = comando.Parameters.Add("parcuentaant", MySqlDbType.VarChar, 15);
            prmCuentaA.Value = dLibreria.left(MovimientoEfectivo.codCuentaAnterior, 15);
            MySqlParameter prmDoctoBancoA = comando.Parameters.Add("pardoctobcoant", MySqlDbType.VarChar, 6);
            prmDoctoBancoA.Value = dLibreria.left(MovimientoEfectivo.codDoctoBancoAnterior, 6);
            MySqlParameter prmDocumentoA = comando.Parameters.Add("parnodoctoant", MySqlDbType.VarChar, 15);
            prmDocumentoA.Value = dLibreria.left(MovimientoEfectivo.codDocumentoAnterior, 15);
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";


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

      public eParSalida nuevo(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_doctovtadiariaefectivoxes", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoEfectivo.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuenta", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoEfectivo.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoEfectivo.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("pardocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.leftRelleno(MovimientoEfectivo.codDocumento, 15);
          
            MySqlParameter prmbolsa = comando.Parameters.Add("parunidadbolsa", MySqlDbType.VarChar, 15);
            prmbolsa.Value = dLibreria.left(MovimientoEfectivo.codUnidadBolsa, 15);
            MySqlParameter prmfecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmfecha.Value = MovimientoEfectivo.fecha;
            MySqlParameter prmConcepto = comando.Parameters.Add("parconcepto", MySqlDbType.VarChar, 250);
            prmConcepto.Value = dLibreria.left(MovimientoEfectivo.concepto, 250);
            MySqlParameter prmMonto = comando.Parameters.Add("parmontopago", MySqlDbType.Decimal);
            prmMonto.Value = MovimientoEfectivo.monto;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoEfectivo.codSucursal, 4);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";

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
      public eParSalida del(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("del_vtadiariaesefectivo", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmBanco = comando.Parameters.Add("parbanco", MySqlDbType.VarChar, 2);
            prmBanco.Value = dLibreria.left(MovimientoEfectivo.codBanco, 2);
            MySqlParameter prmCuenta = comando.Parameters.Add("parcuenta", MySqlDbType.VarChar, 15);
            prmCuenta.Value = dLibreria.left(MovimientoEfectivo.codCuenta, 15);
            MySqlParameter prmDoctoBanco = comando.Parameters.Add("pardoctobanco", MySqlDbType.VarChar, 6);
            prmDoctoBanco.Value = dLibreria.left(MovimientoEfectivo.codDoctoBanco, 6);
            MySqlParameter prmDocumento = comando.Parameters.Add("pardocumento", MySqlDbType.VarChar, 15);
            prmDocumento.Value = dLibreria.left(MovimientoEfectivo.codDocumento, 15);         
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoEfectivo.codSucursal, 4);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";

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
      public eParSalida conciliar(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_conciliadoctobancariovtadia", Conexion);
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
            prmPantalla.Value = "02";
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
      public DataTable getLiquidacionMonetariaVentaDiariaXempresa(eMovimientoEfectivoDet movimiento)
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
               MySqlParameter prmTipoPago= adaptador.SelectCommand.Parameters.Add("parpagotc", MySqlDbType.VarChar,1);
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
      public DataTable infoMovEfectivoXFecha(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_deptoconcixes", Conexion);
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
      public DataTable infoVentaDiariaVSConciliadoXFecha(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_vtadiaefecvsconci", Conexion);
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
       public DataTable infoVentaDiariaVSConciliadoXFechaGrafico(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_vtadiaefecvsconci_grafico", Conexion);
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
      public DataTable infoVentaDiariaVSConciliadoXFechaxEstacion(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_vtadiaefecvsconcixes", Conexion);
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
      public DataTable infoFlujoEfectivoxMes(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_flujoefectivomes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;      
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
      public DataTable infoFlujoEfectivoxMes2(eMovimientoEfectivo movimiento)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_flujoefectivomes2", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(movimiento.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(movimiento.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = movimiento.dia;
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
      public eParSalida revertir(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_reversiondoctobancariovtadiaria", Conexion);
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
            prmDocumento.Value = dLibreria.leftRelleno(MovimientoEfectivo.codDocumento, 15);           
            MySqlParameter prmConcepto = comando.Parameters.Add("parconcepto", MySqlDbType.VarChar, 250);
            prmConcepto.Value = dLibreria.left(MovimientoEfectivo.concepto, 250);
            MySqlParameter prmMonto = comando.Parameters.Add("parmonto_documento_reversado", MySqlDbType.Decimal);
            prmMonto.Value = MovimientoEfectivo.montoReversion;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmDoctoBancoReversion = comando.Parameters.Add("partipodoctobanco_reversado", MySqlDbType.VarChar, 6);
            prmDoctoBancoReversion.Value = dLibreria.left(MovimientoEfectivo.codDoctoBancoReversion, 6);
            MySqlParameter prmDocumentoReversion = comando.Parameters.Add("parnodocumento_reversado", MySqlDbType.VarChar, 15);
            prmDocumentoReversion.Value = dLibreria.leftRelleno(MovimientoEfectivo.codDocumentoReversion, 15);
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(MovimientoEfectivo.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";


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

      public eParSalida abrirPantallaUV(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_abrir_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
          
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);      
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoEfectivo.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = MovimientoEfectivo.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";
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
      public eParSalida getEstadoPantalla(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("get_estado_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoEfectivo.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = MovimientoEfectivo.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";
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
      public eParSalida cerrarPantallaUV(eMovimientoEfectivoDet MovimientoEfectivo)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_cerrar_pantalla_vtadiaefec_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(MovimientoEfectivo.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(MovimientoEfectivo.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmDia.Value = MovimientoEfectivo.dia;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "02";
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
   }
}
