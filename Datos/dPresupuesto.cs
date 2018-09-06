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
   public class dPresupuesto : conexionBD
   {
      public eParSalida editPresupuesto(ePresupuesto presupuesto)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_presupuestoxdepto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = presupuesto.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = presupuesto.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = presupuesto.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = presupuesto.departamento;
            MySqlParameter prmAnio = comando.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
            prmAnio.Value = presupuesto.anio;
            MySqlParameter prmTipoPresupuesto = comando.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
            prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
            MySqlParameter prmDetalle = comando.Parameters.Add("parxmldetapresto", MySqlDbType.VarChar,21845 );
            prmDetalle.Value = presupuesto.detalleXML;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(presupuesto.token, 64);


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
      public eParSalida editPresupuestoEjecutado(ePresupuesto presupuesto)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_presupuestoejecxdepto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = presupuesto.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = presupuesto.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = presupuesto.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = presupuesto.departamento;
            MySqlParameter prmAnio = comando.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
            prmAnio.Value = presupuesto.anio;
            MySqlParameter prmTipoPresupuesto = comando.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
            prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
            MySqlParameter prmDetalle = comando.Parameters.Add("parxmldetapresto", MySqlDbType.VarChar, 21845);
            prmDetalle.Value = presupuesto.detalleXML;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(presupuesto.token, 64);


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
      public eParSalida editPresupuestoIngreso(ePresupuesto presupuesto)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_prestoingresoxdepto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = presupuesto.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = presupuesto.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = presupuesto.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = presupuesto.departamento;
            MySqlParameter prmAnio = comando.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
            prmAnio.Value = presupuesto.anio;
            MySqlParameter prmTipoPresupuesto = comando.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
            prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
            MySqlParameter prmDetalle = comando.Parameters.Add("parxmldetapresto", MySqlDbType.VarChar, 21845);
            prmDetalle.Value = presupuesto.detalleXML;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(presupuesto.token, 64);


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

      public DataTable getAnioActual()
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_anioactualpresupuesto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;     
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
      public eParSalida calculoPrestacionLaboral(ePresupuesto presupuesto)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_calcprestacionlaboralpresto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = presupuesto.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = presupuesto.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = presupuesto.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = presupuesto.departamento;         
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(presupuesto.token, 64);
            MySqlParameter prmAnio = comando.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
            prmAnio.Value = presupuesto.anioPresupuesto;

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
      public eParSalida setPrecioMargenIngresoxUnidad(ePresupuesto presupuesto)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_setcostmrgentransptoingrretail", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = presupuesto.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = presupuesto.estacion;         
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = presupuesto.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = presupuesto.departamento;            
            MySqlParameter prmAnio = comando.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
            prmAnio.Value = presupuesto.anioPresupuesto;
            MySqlParameter prmTipoPresupuesto = comando.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
            prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
            MySqlParameter prmXmlPresto = comando.Parameters.Add("parxmldatosxprod", MySqlDbType.VarChar, 21845);
                prmXmlPresto.Value = presupuesto.detalleXML;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(presupuesto.token, 64);


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

        public eParSalida setPrecioMargenIngresoDistro(ePresupuesto presupuesto)
        {
            eParSalida parSalida = new eParSalida();
            if (abrirBD())
            {
                MySqlCommand comando = new MySqlCommand("proc_setprecioymrgenptoingrexuneg", Conexion);
                comando.CommandType = CommandType.StoredProcedure;
                //parametros de entrada
                // casting de parametros conforme al procedimentno almacenado
                MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                prmEmpresa.Value = presupuesto.empresa;
                MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
                prmSucursal.Value = presupuesto.estacion;
                MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
                prmToken.Value = dLibreria.left(presupuesto.token, 64);
                MySqlParameter prmAnio = comando.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
                prmAnio.Value = presupuesto.anioPresupuesto;
                MySqlParameter prmPrecio = comando.Parameters.Add("parpreciopublico", MySqlDbType.Decimal);
                prmPrecio.Value = presupuesto.precioPublico;
                MySqlParameter prmMargen = comando.Parameters.Add("parmargen", MySqlDbType.Decimal);
                prmMargen.Value = presupuesto.margen;
                MySqlParameter prmTransporte = comando.Parameters.Add("partransporte", MySqlDbType.Decimal);
                prmTransporte.Value = presupuesto.transporte;
                MySqlParameter prmTipoPresupuesto = comando.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
                prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
                MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
                prmArea.Value = presupuesto.area;
                MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
                prmDepartamento.Value = presupuesto.departamento;
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

        public DataTable getDetallePresupuestoDepartamento(ePresupuesto presupuesto)
        {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detprestoanualxunegxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getDetallePresupuestoEjecutadoDepartamento(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detprestoejecanualxunegxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getDetallePresupuestoIngresoDepartamento(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detprestoingranualxunegxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getDetallePresupuestoIngresoEjecutadoDepartamento(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_detprestoingrejecanualxunegxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getProductosPrestoIngreEmpresaDepartamento(ePresupuesto presupuesto)
        {
            DataTable tblResultado = new DataTable();
            if (abrirBD())
            {
                try
                {
                    MySqlDataAdapter adaptador = new MySqlDataAdapter("getProductosPrestoIngreEmpresaDepartamento", Conexion);
                    adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
                    prmEmpresa.Value = presupuesto.empresa;
                    MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
                    prmSucursal.Value = presupuesto.estacion;
                    MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
                    prmArea.Value = presupuesto.area;
                    MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
                    prmDepartamento.Value = presupuesto.departamento;
                    MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
                    prmAnio.Value = presupuesto.anio;
                    MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
                    prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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


        public DataTable getEncabezadoPresupuestoDepartamento(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_descriprestoanualxdeptoxarea", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getEncabezadoPresupuestoIngresoDepartamento(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_descptoanualingrxdeptoxarea", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getInformePresupuesto(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detprestoanualxunegxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = presupuesto.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getInformePresupuestoxArea(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detprestoanualxunegxarea", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = presupuesto.area;
        
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getInformePresupuestoxUnidad(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detprestoanualxuneg", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;      
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getInformePresupuestoxEmpresa(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_detprestoanualxempre", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;           
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngrexVolumxEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrexvolumxempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngreC1xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec1xempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngreC2xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec2xempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngreFletexEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrefletexempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngreC3xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec3xempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformeTotPrestoIngreAnualxUneg(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_totprestoanualxuneg", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePrestoIngreC4xEmpxSuc(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec4xempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;               
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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

      public DataTable getInformePresupuestoVolumenxUnidad(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrexvolumxempxsuc", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;      
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
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
      public DataTable getInformeIngresoVolumenRetail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrexvolumxretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
     
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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
      public DataTable getInformeIngresoC1Retail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec1xretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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

      public DataTable getInformeIngresoC2Retail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec2xretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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

      public DataTable getInformeIngresoC3Retail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec3xretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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

      public DataTable getInformeIngresoC4Retail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrec4xretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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
      public DataTable getInformeIngresoFleteRetail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_prestoingrefletexretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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

      public DataTable getInformeTotalAnualRetail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_totprestoanualxretailes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;

               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
               MySqlParameter prmTipoPresupuesto = adaptador.SelectCommand.Parameters.Add("partipopresupuesto", MySqlDbType.VarChar, 6);
               prmTipoPresupuesto.Value = presupuesto.tipoPresupuesto;
               MySqlParameter prmGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("pargrupopresupuesto", MySqlDbType.VarChar, 4);
               prmGrupoPresupuesto.Value = presupuesto.grupoPresupuesto;
               MySqlParameter prmSubGrupoPresupuesto = adaptador.SelectCommand.Parameters.Add("parsubgrupopresupuesto", MySqlDbType.VarChar, 2);
               prmSubGrupoPresupuesto.Value = presupuesto.subGrupoPresupuesto;
               MySqlParameter prmDistriRetail = adaptador.SelectCommand.Parameters.Add("pardistribretailuneg", MySqlDbType.VarChar, 1);
               prmDistriRetail.Value = presupuesto.distribRetailUneg;
               MySqlParameter prmMes = adaptador.SelectCommand.Parameters.Add("parmes", MySqlDbType.VarChar, 2);
               prmMes.Value = presupuesto.mes;
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
      public DataTable getInformePrestoVolumenAnualRetail(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("proc_estadisticanualxretail", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
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
      public DataTable getInformePrestoVolumenAnualRetail2(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("proc_estadisticanualxretail2", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
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

      public DataTable getInformePrestoVolumenAnualRetailEmpresa(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("proc_estadisticanualxretailxempre", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
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

      public DataTable getInformePrestoVolumenAnualRetailEmpresaSucursal(ePresupuesto presupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("proc_estadisticanualxretailxemprexes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = presupuesto.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = presupuesto.estacion;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paranio", MySqlDbType.VarChar, 4);
               prmAnio.Value = presupuesto.anio;
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
