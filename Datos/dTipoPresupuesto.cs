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
   public class dTipoPresupuesto : conexionBD
   {
      public DataTable getTipoPresupuesto(eTipoPresupuesto TipoPresupuesto)
      {
         DataTable tblResultado = new DataTable();
         if (abrirBD())
         {
            try
            {
               MySqlDataAdapter adaptador = new MySqlDataAdapter("get_tipopresupuesto", Conexion);
               adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
               //MySqlParameter prmEmpresa = adaptador.SelectCommand.Parameters.Add("parempresa", MySqlDbType.VarChar, 5);
              // prmEmpresa.Value = TipoPresupuesto.codigo;

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
