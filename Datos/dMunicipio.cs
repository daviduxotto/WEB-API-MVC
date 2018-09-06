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
   public class dMunicipio : conexionBD
   {

      public DataTable getMunicipioxDepartamento(eMunicipio Municipio)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_municipioxestadodepto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               MySqlParameter prmPais = adaptador.SelectCommand.Parameters.Add("parcodpais", MySqlDbType.VarChar, 3);
               prmPais.Value = Municipio.codPais;
               MySqlParameter prmDepartamento = adaptador.SelectCommand.Parameters.Add("parestadodepto", MySqlDbType.VarChar, 3);
               prmDepartamento.Value = Municipio.codDepartamento;

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
