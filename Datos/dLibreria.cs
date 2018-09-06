using System;
using System.Data;
using System.Linq;


namespace Datos
{
   public class dLibreria
   {
      public static string left(string parCadena, int parLongitud)
      {

         try
         {
            int longitud = Convert.ToInt32(parCadena.Length);
            if (longitud <= parLongitud)
            {
               return parCadena;
            }
            else
            {
               return parCadena.Substring(0, parLongitud);
            }
         }
         catch
         {
            return "";
         }

      }

      public static string leftRelleno(string parCadena, int parLongitud)
      {

         try
         {
            int longitud = Convert.ToInt32(parCadena.Length);
            if (longitud <= parLongitud)
            {
               int diferencia = parLongitud - longitud;
               string relleno = "";
               for (int i = 1; i <= diferencia; i++)
               {
                  relleno += "0";
               }
               return relleno +parCadena;
            }
            else
            {
               return parCadena.Substring(0, parLongitud);
            }
         }
         catch
         {
            return "";
         }

      }

      public static string genXMLntorno(DataTable parTblDetalle)
      {
         string strXMLReturn = "";
         Int32 intRegistro = 0;
         try
         {
            strXMLReturn = "<xmlroot>";
            for (intRegistro = 0; intRegistro < parTblDetalle.Rows.Count; intRegistro++)
            {
               strXMLReturn = strXMLReturn + "<acceso>";
               strXMLReturn = strXMLReturn + "<empresa>" + parTblDetalle.Rows[intRegistro]["cod_empresa"].ToString() + "</empresa>";
               strXMLReturn = strXMLReturn + "<sucursal>" + parTblDetalle.Rows[intRegistro]["cod_sucursal"].ToString() + "</sucursal>";
               strXMLReturn = strXMLReturn + "</acceso>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch
         {
            return "";
         }
      }

      public static string genXMLDetalleNivel(DataTable parTblDetalle)
      {
         string strXMLReturn = "";
         Int32 intRegistro = 0;
         try
         {
            strXMLReturn = "<xmlroot>";
            for (intRegistro = 0; intRegistro < parTblDetalle.Rows.Count; intRegistro++)
            {
               strXMLReturn = strXMLReturn + "<menu>";
               strXMLReturn = strXMLReturn + "<menukey>" + parTblDetalle.Rows[intRegistro]["cod_menu"].ToString() + "</menukey>";
               strXMLReturn = strXMLReturn + "</menu>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch
         {
            return "";
         }
      }

      public static string genXMLContactos(DataTable parTblDetalle)
      {
         string strXMLReturn = "";
         Int32 intRegistro = 0;
         try
         {
            strXMLReturn = "<xmlroot>";
            for (intRegistro = 0; intRegistro < parTblDetalle.Rows.Count; intRegistro++)
            {
               strXMLReturn = strXMLReturn + "<contacto>";
               strXMLReturn = strXMLReturn + "<nombrecompleto>" + parTblDetalle.Rows[intRegistro]["nombrecompleto"].ToString() + "</nombrecompleto>";
               strXMLReturn = strXMLReturn + "<puesto>" + parTblDetalle.Rows[intRegistro]["puesto"].ToString() + "</puesto>";
               strXMLReturn = strXMLReturn + "<telefonofijo>" + parTblDetalle.Rows[intRegistro]["telefonofijo"].ToString() + "</telefonofijo>";
               strXMLReturn = strXMLReturn + "<telefonomovil>" + parTblDetalle.Rows[intRegistro]["telefonomovil"].ToString() + "</telefonomovil>";
               strXMLReturn = strXMLReturn + "<email>" + parTblDetalle.Rows[intRegistro]["email"].ToString() + "</email>";
               strXMLReturn = strXMLReturn + "</contacto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch
         {
            return "";
         }
      }

      public static string genXMLDetalleProducto(DataTable parTblDetalle)
      {
         string strXMLReturn = "";
         Int32 intRegistro = 0;
         try
         {
            strXMLReturn = "<xmlroot>";
            for (intRegistro = 0; intRegistro < parTblDetalle.Rows.Count; intRegistro++)
            {
               strXMLReturn = strXMLReturn + "<detalleproducto>";
               strXMLReturn = strXMLReturn + "<sucursal>" + parTblDetalle.Rows[intRegistro]["sucursal"].ToString() + "</sucursal>";
               strXMLReturn = strXMLReturn + "<bodega>" + parTblDetalle.Rows[intRegistro]["bodega"].ToString() + "</bodega>";
               strXMLReturn = strXMLReturn + "<seccion>" + parTblDetalle.Rows[intRegistro]["seccion"].ToString() + "</seccion>";
               strXMLReturn = strXMLReturn + "<estanteria>" + parTblDetalle.Rows[intRegistro]["estanteria"].ToString() + "</estanteria>";
               strXMLReturn = strXMLReturn + "<nivel>" + parTblDetalle.Rows[intRegistro]["nivel"].ToString() + "</nivel>";
               strXMLReturn = strXMLReturn + "<existenciaminima>" + parTblDetalle.Rows[intRegistro]["existenciaminima"].ToString() + "</existenciaminima>";
               strXMLReturn = strXMLReturn + "<existenciamaxima>" + parTblDetalle.Rows[intRegistro]["existenciamaxima"].ToString() + "</existenciamaxima>";
               strXMLReturn = strXMLReturn + "<habilitadocompra>" + parTblDetalle.Rows[intRegistro]["habilitadocompra"].ToString() + "</habilitadocompra>";
               strXMLReturn = strXMLReturn + "<habilitadoventa>" + parTblDetalle.Rows[intRegistro]["habilitadoventa"].ToString() + "</habilitadoventa>";
               strXMLReturn = strXMLReturn + "<habilitadoventapos>" + parTblDetalle.Rows[intRegistro]["habilitadoventapos"].ToString() + "</habilitadoventapos>";
               strXMLReturn = strXMLReturn + "</detalleproducto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch
         {
            return "";
         }
      }

      public static DataTable tbError()
      {
         DataTable tblResultado = new DataTable();
         DataColumn retorno = tblResultado.Columns.Add("parretorno", typeof(Int32));
         DataRow fila = tblResultado.NewRow();
         fila["parretorno"] = -9000002;
         tblResultado.Rows.Add(fila);
         return tblResultado;
      }

        public static string genXMLDetallePrestoIngrRetail(DataTable parTblDetalle)
        {
            string strXMLReturn = "";
            Int32 intRegistro = 0;
            try
            {
                strXMLReturn = "<xmlroot>";
                for (intRegistro = 0; intRegistro < parTblDetalle.Rows.Count; intRegistro++)
                {
                    strXMLReturn = strXMLReturn + "<detalleproducto>";
                    strXMLReturn = strXMLReturn + "<sucursal>" + parTblDetalle.Rows[intRegistro]["sucursal"].ToString() + "</sucursal>";
                    strXMLReturn = strXMLReturn + "<bodega>" + parTblDetalle.Rows[intRegistro]["bodega"].ToString() + "</bodega>";
                    strXMLReturn = strXMLReturn + "<seccion>" + parTblDetalle.Rows[intRegistro]["seccion"].ToString() + "</seccion>";
                    strXMLReturn = strXMLReturn + "<estanteria>" + parTblDetalle.Rows[intRegistro]["estanteria"].ToString() + "</estanteria>";
                    strXMLReturn = strXMLReturn + "<nivel>" + parTblDetalle.Rows[intRegistro]["nivel"].ToString() + "</nivel>";
                    strXMLReturn = strXMLReturn + "<existenciaminima>" + parTblDetalle.Rows[intRegistro]["existenciaminima"].ToString() + "</existenciaminima>";
                    strXMLReturn = strXMLReturn + "<existenciamaxima>" + parTblDetalle.Rows[intRegistro]["existenciamaxima"].ToString() + "</existenciamaxima>";
                    strXMLReturn = strXMLReturn + "<habilitadocompra>" + parTblDetalle.Rows[intRegistro]["habilitadocompra"].ToString() + "</habilitadocompra>";
                    strXMLReturn = strXMLReturn + "<habilitadoventa>" + parTblDetalle.Rows[intRegistro]["habilitadoventa"].ToString() + "</habilitadoventa>";
                    strXMLReturn = strXMLReturn + "<habilitadoventapos>" + parTblDetalle.Rows[intRegistro]["habilitadoventapos"].ToString() + "</habilitadoventapos>";
                    strXMLReturn = strXMLReturn + "</detalleproducto>";
                }
                strXMLReturn = strXMLReturn + "</xmlroot>";
                return strXMLReturn;
            }
            catch
            {
                return "";
            }
        }
    }
}
