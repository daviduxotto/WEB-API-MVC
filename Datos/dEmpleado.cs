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
  public  class dEmpleado : conexionBD
   {
     
      public eParSalida newEmpleado(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_empleado", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;
            MySqlParameter prmPrimerNombre = comando.Parameters.Add("parprimernombre", MySqlDbType.VarChar, 25);
            prmPrimerNombre.Value = Empleado.primerNombre;
            MySqlParameter prmSegundoNombre = comando.Parameters.Add("parsegundonombre", MySqlDbType.VarChar, 25);
            prmSegundoNombre.Value = Empleado.segundoNombre;
            MySqlParameter prmTercerNombre = comando.Parameters.Add("partercernombre", MySqlDbType.VarChar, 25);
            prmTercerNombre.Value = Empleado.tercerNombre;

            MySqlParameter prmPrimerApellido = comando.Parameters.Add("parprimerapellido", MySqlDbType.VarChar, 35);
            prmPrimerApellido.Value = Empleado.primerApellido;
            MySqlParameter prmSegundoApellido= comando.Parameters.Add("parsegundoapellido", MySqlDbType.VarChar, 35);
            prmSegundoApellido.Value = Empleado.segundoApellido;
            MySqlParameter prmApellidoCasada = comando.Parameters.Add("parapellidocasada", MySqlDbType.VarChar, 35);
            prmApellidoCasada.Value = Empleado.apellidoCasada;

            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = Empleado.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = Empleado.departamento;
            // -- otros datos
            MySqlParameter prmDpi = comando.Parameters.Add("pardpi", MySqlDbType.VarChar, 15);
            prmDpi.Value = dLibreria.left(Empleado.dpi, 15);
            MySqlParameter prmIgss = comando.Parameters.Add("parafiliacionigss", MySqlDbType.VarChar, 15);
            prmIgss.Value = dLibreria.left(Empleado.afiliacionIgss, 15);
            MySqlParameter prmNit = comando.Parameters.Add("parnit", MySqlDbType.VarChar, 15);
            prmNit.Value = dLibreria.left(Empleado.nit, 15);
            MySqlParameter prmGenero = comando.Parameters.Add("pargenero", MySqlDbType.VarChar, 1);
            prmGenero.Value = dLibreria.left(Empleado.genero, 1);
            MySqlParameter prmTelefonoMovil = comando.Parameters.Add("partelefonomovil", MySqlDbType.VarChar, 20);
            prmTelefonoMovil.Value = dLibreria.left(Empleado.telefonoMovil, 20);
            MySqlParameter prmTelefonoCasa = comando.Parameters.Add("partelefonocasa", MySqlDbType.VarChar, 20);
            prmTelefonoCasa.Value = dLibreria.left(Empleado.telefonoCasa, 20);
            MySqlParameter prmEmail= comando.Parameters.Add("parcorreoelectronico", MySqlDbType.VarChar, 60);
            prmEmail.Value = dLibreria.left(Empleado.email, 60);
            MySqlParameter prmFechaNacimiento = comando.Parameters.Add("parfechanacimiento", MySqlDbType.Date);
            prmFechaNacimiento.Value = Empleado.fechaNacimiento;
            MySqlParameter prmPaisNacimiento = comando.Parameters.Add("parcodpaisnacimiento", MySqlDbType.VarChar, 3);
            prmPaisNacimiento.Value = dLibreria.left(Empleado.codPaisNacimiento, 3);
            MySqlParameter prmDepartamentoNacimiento = comando.Parameters.Add("parcoddeptonacimiento", MySqlDbType.VarChar, 3);
            prmDepartamentoNacimiento.Value = dLibreria.left(Empleado.codDepartamentoNacimiento, 3);
            MySqlParameter prmMunicipioNacimiento = comando.Parameters.Add("parcodmunicipionacimiento", MySqlDbType.VarChar, 3);
            prmMunicipioNacimiento.Value = dLibreria.left(Empleado.codMunicipioNacimiento, 3);

            MySqlParameter prmPaisDireccion = comando.Parameters.Add("parcodpaisdireccion", MySqlDbType.VarChar, 3);
            prmPaisDireccion.Value = dLibreria.left(Empleado.codPaisDireccion, 3);
            MySqlParameter prmDepartamentoDireccion = comando.Parameters.Add("parcoddeptoDireccion", MySqlDbType.VarChar, 3);
            prmDepartamentoDireccion.Value = dLibreria.left(Empleado.codDepartamentoDireccion, 3);
            MySqlParameter prmMunicipioDireccion = comando.Parameters.Add("parcodmunicipioDireccion", MySqlDbType.VarChar, 3);
            prmMunicipioDireccion.Value = dLibreria.left(Empleado.codMunicipioDireccion, 3);
            MySqlParameter prmDireccion = comando.Parameters.Add("parDireccion", MySqlDbType.VarChar, 150);
            prmDireccion.Value = dLibreria.left(Empleado.direccion, 150);
            MySqlParameter prmPeso = comando.Parameters.Add("parpesolibras", MySqlDbType.Int32);
            prmPeso.Value =Empleado.peso;
            MySqlParameter prmAltura = comando.Parameters.Add("paralturacm", MySqlDbType.Int32);
            prmAltura.Value = Empleado.altura;
            MySqlParameter prmTieneEnfermedad = comando.Parameters.Add("partieneenfermedadcronica", MySqlDbType.VarChar, 1);
            prmTieneEnfermedad.Value = dLibreria.left(Empleado.tieneEnfermedadCronica, 1);
            MySqlParameter prmEnfermedad = comando.Parameters.Add("parenfermedadcronica", MySqlDbType.VarChar, 60);
            prmEnfermedad.Value = dLibreria.left(Empleado.enfermedadCronica, 60);
            MySqlParameter prmFechaIngreso = comando.Parameters.Add("parfechaingreso", MySqlDbType.Date);
            prmFechaIngreso.Value = Empleado.fechaIngreso;

            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);


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
      public eParSalida editEmpleado(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_empleado", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
            prmCodigoCorp.Value = Empleado.codigoCorp;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;
            MySqlParameter prmPrimerNombre = comando.Parameters.Add("parprimernombre", MySqlDbType.VarChar, 25);
            prmPrimerNombre.Value = Empleado.primerNombre;
            MySqlParameter prmSegundoNombre = comando.Parameters.Add("parsegundonombre", MySqlDbType.VarChar, 25);
            prmSegundoNombre.Value = Empleado.segundoNombre;
            MySqlParameter prmTercerNombre = comando.Parameters.Add("partercernombre", MySqlDbType.VarChar, 25);
            prmTercerNombre.Value = Empleado.tercerNombre;

            MySqlParameter prmPrimerApellido = comando.Parameters.Add("parprimerapellido", MySqlDbType.VarChar, 35);
            prmPrimerApellido.Value = Empleado.primerApellido;
            MySqlParameter prmSegundoApellido = comando.Parameters.Add("parsegundoapellido", MySqlDbType.VarChar, 35);
            prmSegundoApellido.Value = Empleado.segundoApellido;
            MySqlParameter prmApellidoCasada = comando.Parameters.Add("parapellidocasada", MySqlDbType.VarChar, 35);
            prmApellidoCasada.Value = Empleado.apellidoCasada;

            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = Empleado.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = Empleado.departamento;
            // -- otros datos
            MySqlParameter prmDpi = comando.Parameters.Add("pardpi", MySqlDbType.VarChar, 15);
            prmDpi.Value = dLibreria.left(Empleado.dpi, 15);
            MySqlParameter prmIgss = comando.Parameters.Add("parafiliacionigss", MySqlDbType.VarChar, 15);
            prmIgss.Value = dLibreria.left(Empleado.afiliacionIgss, 15);
            MySqlParameter prmNit = comando.Parameters.Add("parnit", MySqlDbType.VarChar, 15);
            prmNit.Value = dLibreria.left(Empleado.nit, 15);
            MySqlParameter prmGenero = comando.Parameters.Add("pargenero", MySqlDbType.VarChar, 1);
            prmGenero.Value = dLibreria.left(Empleado.genero, 1);
            MySqlParameter prmTelefonoMovil = comando.Parameters.Add("partelefonomovil", MySqlDbType.VarChar, 20);
            prmTelefonoMovil.Value = dLibreria.left(Empleado.telefonoMovil, 20);
            MySqlParameter prmTelefonoCasa = comando.Parameters.Add("partelefonocasa", MySqlDbType.VarChar, 20);
            prmTelefonoCasa.Value = dLibreria.left(Empleado.telefonoCasa, 20);
            MySqlParameter prmEmail = comando.Parameters.Add("parcorreoelectronico", MySqlDbType.VarChar, 60);
            prmEmail.Value = dLibreria.left(Empleado.email, 60);
            MySqlParameter prmFechaNacimiento = comando.Parameters.Add("parfechanacimiento", MySqlDbType.Date);
            prmFechaNacimiento.Value = Empleado.fechaNacimiento;
            MySqlParameter prmPaisNacimiento = comando.Parameters.Add("parcodpaisnacimiento", MySqlDbType.VarChar, 3);
            prmPaisNacimiento.Value = dLibreria.left(Empleado.codPaisNacimiento, 3);
            MySqlParameter prmDepartamentoNacimiento = comando.Parameters.Add("parcoddeptonacimiento", MySqlDbType.VarChar, 3);
            prmDepartamentoNacimiento.Value = dLibreria.left(Empleado.codDepartamentoNacimiento, 3);
            MySqlParameter prmMunicipioNacimiento = comando.Parameters.Add("parcodmunicipionacimiento", MySqlDbType.VarChar, 3);
            prmMunicipioNacimiento.Value = dLibreria.left(Empleado.codMunicipioNacimiento, 3);

            MySqlParameter prmPaisDireccion = comando.Parameters.Add("parcodpaisdireccion", MySqlDbType.VarChar, 3);
            prmPaisDireccion.Value = dLibreria.left(Empleado.codPaisDireccion, 3);
            MySqlParameter prmDepartamentoDireccion = comando.Parameters.Add("parcoddeptoDireccion", MySqlDbType.VarChar, 3);
            prmDepartamentoDireccion.Value = dLibreria.left(Empleado.codDepartamentoDireccion, 3);
            MySqlParameter prmMunicipioDireccion = comando.Parameters.Add("parcodmunicipioDireccion", MySqlDbType.VarChar, 3);
            prmMunicipioDireccion.Value = dLibreria.left(Empleado.codMunicipioDireccion, 3);
            MySqlParameter prmDireccion = comando.Parameters.Add("parDireccion", MySqlDbType.VarChar, 150);
            prmDireccion.Value = dLibreria.left(Empleado.direccion, 150);
            MySqlParameter prmPeso = comando.Parameters.Add("parpesolibras", MySqlDbType.Int32);
            prmPeso.Value = Empleado.peso;
            MySqlParameter prmAltura = comando.Parameters.Add("paralturacm", MySqlDbType.Int32);
            prmAltura.Value = Empleado.altura;
            MySqlParameter prmTieneEnfermedad = comando.Parameters.Add("partieneenfermedadcronica", MySqlDbType.VarChar, 1);
            prmTieneEnfermedad.Value = dLibreria.left(Empleado.tieneEnfermedadCronica, 1);
            MySqlParameter prmEnfermedad = comando.Parameters.Add("parenfermedadcronica", MySqlDbType.VarChar, 60);
            prmEnfermedad.Value = dLibreria.left(Empleado.enfermedadCronica, 60);
            MySqlParameter prmFechaIngreso = comando.Parameters.Add("parfechaIngreso", MySqlDbType.Date);
            prmFechaIngreso.Value = Empleado.fechaIngreso;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
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
    
      public DataTable getEmpleadoxEmpresaxAreaXDepartamento(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_emplexemprexesxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Empleado.departamento;
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
      public DataTable infoAllEmpleados(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_all_emple", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Empleado.departamento;
               adaptador.Fill(tblResultado);
            }
            catch (Exception e)
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
      public DataTable getEmpleadoxEmpresaxUnidad(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_emplexemprexes", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
      
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
      public eParSalida delEmpleado(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("baja_empleado", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.VarChar, 5);
            prmCodigoCorp.Value = Empleado.codigoCorp;
            MySqlParameter prmFechaBaja = comando.Parameters.Add("parfechabaja", MySqlDbType.Date);
            prmFechaBaja.Value = Empleado.fechaBaja;
            MySqlParameter prmTipoBaja = comando.Parameters.Add("partipobaja", MySqlDbType.Int32);
            prmTipoBaja.Value = Empleado.codTipoBaja;
            MySqlParameter prmObservacion = comando.Parameters.Add("parobservacion", MySqlDbType.VarChar, 250);
            prmObservacion.Value = Empleado.observacion;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);


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

      public DataTable getOneEmpleado(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_one_empleado", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;         
               MySqlParameter prmCodigoCorp = adaptador.SelectCommand.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
               prmCodigoCorp.Value = Empleado.codigoCorp;
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

      public DataTable getBajaEmpleado(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_empleado_baja", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigoCorp = adaptador.SelectCommand.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
               prmCodigoCorp.Value = Empleado.codigoCorp;
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

      public DataTable getEntorno(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_descemprexsucxarexdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Empleado.departamento;
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
      public eParSalida trasladarEmpleado(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_traslado_empleado", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
            prmCodigoCorp.Value = Empleado.codigoCorp;
            MySqlParameter prmCodigoEmpresa = comando.Parameters.Add("parcodigoempresa", MySqlDbType.Int32);
            prmCodigoEmpresa.Value = Empleado.codigo;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;   
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = Empleado.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = Empleado.departamento;
            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmFechaTraslado = comando.Parameters.Add("parfechatraslado", MySqlDbType.Date);
            prmFechaTraslado.Value = Empleado.fechaTraslado;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
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
      public eParSalida trasladarEmpleadoEmpresa(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("proc_traslado_empleado_empre", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
            prmCodigoCorp.Value = Empleado.codigoCorp;
            MySqlParameter prmCodigoEmpresa = comando.Parameters.Add("parcodigoempresa", MySqlDbType.Int32);
            prmCodigoEmpresa.Value = Empleado.codigo;
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = Empleado.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = Empleado.departamento;
            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmFechaTraslado = comando.Parameters.Add("parfechatraslado", MySqlDbType.Date);
            prmFechaTraslado.Value = Empleado.fechaTraslado;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
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
      // Metodos para el modulo de presupuesto
      public eParSalida newEmpleadoPresu(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("new_empleado_presto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;
            MySqlParameter prmPrimerNombre = comando.Parameters.Add("parprimernombre", MySqlDbType.VarChar, 25);
            prmPrimerNombre.Value = Empleado.primerNombre;
            MySqlParameter prmSegundoNombre = comando.Parameters.Add("parsegundonombre", MySqlDbType.VarChar, 25);
            prmSegundoNombre.Value = Empleado.segundoNombre;
            MySqlParameter prmTercerNombre = comando.Parameters.Add("partercernombre", MySqlDbType.VarChar, 25);
            prmTercerNombre.Value = Empleado.tercerNombre;

            MySqlParameter prmPrimerApellido = comando.Parameters.Add("parprimerapellido", MySqlDbType.VarChar, 35);
            prmPrimerApellido.Value = Empleado.primerApellido;
            MySqlParameter prmSegundoApellido = comando.Parameters.Add("parsegundoapellido", MySqlDbType.VarChar, 35);
            prmSegundoApellido.Value = Empleado.segundoApellido;
            MySqlParameter prmApellidoCasada = comando.Parameters.Add("parapellidocasada", MySqlDbType.VarChar, 35);
            prmApellidoCasada.Value = Empleado.apellidoCasada;

            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmArea = comando.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
            prmArea.Value = Empleado.area;
            MySqlParameter prmDepartamento = comando.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
            prmDepartamento.Value = Empleado.departamento;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
            MySqlParameter prmAnio = comando.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
            prmAnio.Value = Empleado.anioPresupuesto;

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
      public eParSalida editEmpleadoPresu(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_empleado_presto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
            prmCodigoCorp.Value = Empleado.codigoCorp;
            MySqlParameter prmPrimerNombre = comando.Parameters.Add("parprimernombre", MySqlDbType.VarChar, 25);
            prmPrimerNombre.Value = Empleado.primerNombre;
            MySqlParameter prmSegundoNombre = comando.Parameters.Add("parsegundonombre", MySqlDbType.VarChar, 25);
            prmSegundoNombre.Value = Empleado.segundoNombre;
            MySqlParameter prmTercerNombre = comando.Parameters.Add("partercernombre", MySqlDbType.VarChar, 25);
            prmTercerNombre.Value = Empleado.tercerNombre;
            MySqlParameter prmPrimerApellido = comando.Parameters.Add("parprimerapellido", MySqlDbType.VarChar, 35);
            prmPrimerApellido.Value = Empleado.primerApellido;
            MySqlParameter prmSegundoApellido = comando.Parameters.Add("parsegundoapellido", MySqlDbType.VarChar, 35);
            prmSegundoApellido.Value = Empleado.segundoApellido;
            MySqlParameter prmApellidoCasada = comando.Parameters.Add("parapellidocasada", MySqlDbType.VarChar, 35);
            prmApellidoCasada.Value = Empleado.apellidoCasada;

            MySqlParameter prmPuesto = comando.Parameters.Add("parpuesto", MySqlDbType.Int32, 32);
            prmPuesto.Value = Empleado.codPuesto;
            MySqlParameter prmSalario = comando.Parameters.Add("parsalariordinario", MySqlDbType.Decimal, 10);
            prmSalario.Value = Empleado.salario;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
            MySqlParameter prmAnio = comando.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
            prmAnio.Value = Empleado.anioPresupuesto;
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
      public eParSalida delEmpleadoPresu(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("baja_empleado_presto", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmCodigoCorp = comando.Parameters.Add("parcodigocorp", MySqlDbType.VarChar, 5);
            prmCodigoCorp.Value = Empleado.codigoCorp;

            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
            MySqlParameter prmAnio = comando.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
            prmAnio.Value = Empleado.anioPresupuesto;

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
      public DataTable getOneEmpleadoPresu(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_one_empleado_presto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmCodigoCorp = adaptador.SelectCommand.Parameters.Add("parcodigocorp", MySqlDbType.Int32);
               prmCodigoCorp.Value = Empleado.codigoCorp;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
               prmAnio.Value = Empleado.anioPresupuesto;
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
      public DataTable getEmpleadoxEmpresaxAreaXDepartamentoPresu(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_empleprestoxemprexunxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Empleado.departamento;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
               prmAnio.Value = Empleado.anioPresupuesto;
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
      public DataTable getEmpleadoxEmpresaxUnidadPresu(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_empleprestoxemprexun", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmAnio = adaptador.SelectCommand.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
               prmAnio.Value = Empleado.anioPresupuesto;
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
      public eParSalida editSalariosEmpleados(eEmpleado Empleado)
      {
         eParSalida parSalida = new eParSalida();
         if (abrirBD())
         {
            MySqlCommand comando = new MySqlCommand("edit_presupsalarioemplexemprexsuc", Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            //parametros de entrada
            // casting de parametros conforme al procedimentno almacenado
            // casting de parametros conforme al procedimentno almacenado
            MySqlParameter prmEmpresa = comando.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
            prmEmpresa.Value = Empleado.empresa;
            MySqlParameter prmSucursal = comando.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
            prmSucursal.Value = Empleado.estacion;
            MySqlParameter prmDetalle = comando.Parameters.Add("parxmldetasalarios", MySqlDbType.VarChar, 21845);
            prmDetalle.Value = Empleado.detalleXML;
            MySqlParameter prmToken = comando.Parameters.Add("partoken", MySqlDbType.VarChar, 64);
            prmToken.Value = dLibreria.left(Empleado.token, 64);
            MySqlParameter prmAnio = comando.Parameters.Add("paraniopresto", MySqlDbType.VarChar, 4);
            prmAnio.Value = Empleado.anioPresupuesto;
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
      // ----------------- informes para empleados de RRHH
      public DataTable getInformeEmpleadoxDepartamento(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_emplexemprexesxareaxdepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("pardepartamento", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Empleado.departamento;    
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
      public DataTable getInformeEmpleadoxArea(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_ ", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;
               MySqlParameter prmArea = adaptador.SelectCommand.Parameters.Add("pararea", MySqlDbType.VarChar, 3);
               prmArea.Value = Empleado.area;
       
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
      public DataTable getInformeEmpleadoxUnidad(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_ ", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;
               MySqlParameter prmSucursal = adaptador.SelectCommand.Parameters.Add("parsucursal", MySqlDbType.VarChar, 4);
               prmSucursal.Value = Empleado.estacion;  
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
      public DataTable getInformeEmpleadoxEmpresa(eEmpleado Empleado)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("info_ ", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
               prmEmpresa.Value = Empleado.empresa;         
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
