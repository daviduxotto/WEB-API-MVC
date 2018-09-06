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
   public  class dCupon : conexionBD
   {
      public DataTable getCuponesxEstacionxFecha(eCupon Cupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detcuponestaciondia_det", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(Cupon.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(Cupon.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechainicial", MySqlDbType.Date);
               prmDia.Value = Cupon.dia;
               MySqlParameter prmDia2 = adaptador.SelectCommand.Parameters.Add("parfechafinal", MySqlDbType.Date);
               prmDia2.Value = Cupon.dia;
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
      public DataTable getDenominacionCupon(eCupon Cupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_denominacuponxemprexemisorxalta", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(Cupon.codEmpresa, 5);
               MySqlParameter prmEmisor= adaptador.SelectCommand.Parameters.Add("paremisor", MySqlDbType.VarChar, 2);
               prmEmisor.Value = dLibreria.left(Cupon.codEmisor, 2);
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
      public DataTable getEmisorCupon(eCupon Cupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_emisorcuponxempre", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(Cupon.codEmpresa, 5);         
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
      public eParSalida validarCupon (eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_validacuponesindividuales", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
            MySqlParameter prmEmisor = comando.Parameters.Add("paremisorcupon", MySqlDbType.VarChar, 2);
            prmEmisor.Value = Cupon.codEmisor;
            MySqlParameter prmDenominacion = comando.Parameters.Add("pardenominacioncupon", MySqlDbType.VarChar, 2);
            prmDenominacion.Value = Cupon.codDenominacion;
            MySqlParameter prmNoCupon = comando.Parameters.Add("parnocupon", MySqlDbType.Int32);
            prmNoCupon.Value = Cupon.numero;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Cupon.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "04";

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
      public eParSalida nuevoCupon(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_cuponesindividualesweb", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
            MySqlParameter prmEmisor = comando.Parameters.Add("paremisorcupon", MySqlDbType.VarChar, 2);
            prmEmisor.Value = Cupon.codEmisor;
            MySqlParameter prmDenominacion = comando.Parameters.Add("pardenominacioncupon", MySqlDbType.VarChar, 2);
            prmDenominacion.Value = Cupon.codDenominacion;
            MySqlParameter prmNumero= comando.Parameters.Add("parnocupon", MySqlDbType.Int32);
            prmNumero.Value = Cupon.numero;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Cupon.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "04";

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
      public eParSalida editarCupon(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_cuponesindividualesweb", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
            MySqlParameter prmEmisor = comando.Parameters.Add("paremisorcupon", MySqlDbType.VarChar, 2);
            prmEmisor.Value = Cupon.codEmisor;
            MySqlParameter prmDenominacion = comando.Parameters.Add("pardenominacioncupon", MySqlDbType.VarChar, 2);
            prmDenominacion.Value = Cupon.codDenominacion;
            MySqlParameter prmNumero = comando.Parameters.Add("parnocupon", MySqlDbType.Int32);
            prmNumero.Value = Cupon.numero;
            MySqlParameter prmNumero2 = comando.Parameters.Add("parnocuponant", MySqlDbType.Int32);
            prmNumero2.Value = Cupon.numeroAnterior;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Cupon.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "04";

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
      public eParSalida eliminarCupon(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("del_cuponesindividualesweb", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
            MySqlParameter prmEmisor = comando.Parameters.Add("paremisorcupon", MySqlDbType.VarChar, 2);
            prmEmisor.Value = Cupon.codEmisor;
            MySqlParameter prmDenominacion = comando.Parameters.Add("pardenominacioncupon", MySqlDbType.VarChar, 2);
            prmDenominacion.Value = Cupon.codDenominacion;
            MySqlParameter prmNumero = comando.Parameters.Add("parnocupon", MySqlDbType.Int32);
            prmNumero.Value = Cupon.numero;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Cupon.token, 64);
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "04";

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
      public DataTable getLiquidacionCupones(eCupon Cupon)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_liquidacioncupondiaxemprexes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(Cupon.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(Cupon.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfecha", MySqlDbType.Date);
               prmDia.Value = Cupon.dia;
       
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

      public eParSalida getEstadoPantalla(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("get_estado_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
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
      public eParSalida abrirPantallaUV(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_abrir_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
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
      public eParSalida cerrarPantallaUV(eCupon Cupon)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_cerrar_pantalla_cupones_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Cupon.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Cupon.codSucursal;
            MySqlParameter prmFecha = comando.Parameters.Add("parfecha", MySqlDbType.Date);
            prmFecha.Value = Cupon.dia;
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
