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
   public class dFacturaCajaChica : conexionBD
   {
      public DataTable getFacturaCajaChicaesxEstacionxFecha(eFacturaCajaChica FacturaCajaChica)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_factcxpcajachxempxesxdia_uv", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(FacturaCajaChica.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(FacturaCajaChica.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
               prmDia.Value = FacturaCajaChica.fecha;
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
      public DataTable getLiquidacionCajaChicaesxEstacionxFecha(eFacturaCajaChica FacturaCajaChica)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_liquidacioncajachicadiaxemprexes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = dLibreria.left(FacturaCajaChica.codEmpresa, 5);
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = dLibreria.left(FacturaCajaChica.codSucursal, 4);
               MySqlParameter prmDia = adaptador.SelectCommand.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
               prmDia.Value = FacturaCajaChica.fecha;
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

       public eParSalida validarFacturaFacturaCajaChica(eFacturaCajaChica FacturaCajaChica)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_validafactucajachica", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = FacturaCajaChica.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = FacturaCajaChica.codSucursal;
            MySqlParameter prmDocto = comando.Parameters.Add("pardoctopos", MySqlDbType.VarChar, 6);
            prmDocto.Value = FacturaCajaChica.codDoctoCxP;
            MySqlParameter prmSerie = comando.Parameters.Add("parnoserie", MySqlDbType.VarChar, 20);
            prmSerie.Value = FacturaCajaChica.serie;
            MySqlParameter prmFactura = comando.Parameters.Add("parfactura", MySqlDbType.VarChar, 15);
            prmFactura.Value = FacturaCajaChica.numero;
            MySqlParameter prmProveedor = comando.Parameters.Add("parproveedor", MySqlDbType.VarChar, 6);
            prmProveedor.Value = FacturaCajaChica.codProveedor;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "07";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(FacturaCajaChica.token, 64);


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

      public eParSalida getEstadoPantalla(eFacturaCajaChica FacturaCajaChica)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("get_estado_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(FacturaCajaChica.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(FacturaCajaChica.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = FacturaCajaChica.fecha;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "07";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(FacturaCajaChica.token, 64);
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
      public eParSalida abrirPantallaUV(eFacturaCajaChica FacturaCajaChica)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_abrir_pantalla_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(FacturaCajaChica.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(FacturaCajaChica.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = FacturaCajaChica.fecha;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "07";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(FacturaCajaChica.token, 64);
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

      public eParSalida cerrarPantallaUV(eFacturaCajaChica FacturaCajaChica)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_cerrar_pantalla_cajach_uv", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado

            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = dLibreria.left(FacturaCajaChica.codEmpresa, 5);
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = dLibreria.left(FacturaCajaChica.codSucursal, 4);
            MySqlParameter prmDia = comando.Parameters.Add("parfechatrabajo", MySqlDbType.Date);
            prmDia.Value = FacturaCajaChica.fecha;
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "07";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(FacturaCajaChica.token, 64);
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

      public eParSalida editCodigoFacturaCajaChica(eFacturaCajaChica FacturaCajaChica)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_codigofactucajachica", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = FacturaCajaChica.codEmpresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = FacturaCajaChica.codSucursal;
            MySqlParameter prmDocto = comando.Parameters.Add("pardoctopos", MySqlDbType.VarChar, 6);
            prmDocto.Value = FacturaCajaChica.codDoctoCxP;
            MySqlParameter prmSerie = comando.Parameters.Add("parnoserie", MySqlDbType.VarChar, 20);
            prmSerie.Value = FacturaCajaChica.serie;
            MySqlParameter prmFactura = comando.Parameters.Add("parfactura", MySqlDbType.VarChar, 15);
            prmFactura.Value = FacturaCajaChica.numero;
            MySqlParameter prmProveedor = comando.Parameters.Add("parproveedor", MySqlDbType.VarChar, 6);
            prmProveedor.Value = FacturaCajaChica.codProveedor; 
            MySqlParameter prmSerieNuevo = comando.Parameters.Add("parnoserienuevo", MySqlDbType.VarChar, 20);
            prmSerieNuevo.Value = FacturaCajaChica.serieNuevo;
            MySqlParameter prmFacturaNuevo = comando.Parameters.Add("parfacturanuevo", MySqlDbType.VarChar, 15);
            prmFacturaNuevo.Value = FacturaCajaChica.numeroNuevo;      
            MySqlParameter prmPantalla = comando.Parameters.Add("parpantalla", MySqlDbType.VarChar, 2);
            prmPantalla.Value = "07";
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(FacturaCajaChica.token, 64);


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
