using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Controllers
{
   public class libreria
   {
      public static string genXMLinformesDBR(List<eInformeDBR> informes)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var informe in informes)
            {
               strXMLReturn = strXMLReturn + "<informesdbr>";
               strXMLReturn = strXMLReturn + "<codinforme>" + informe.codInforme +"</codinforme>";
               strXMLReturn = strXMLReturn + "<recibido>" + boolToChar(informe.recibido)  + "</recibido>";
               strXMLReturn = strXMLReturn + "<noaplica>" + boolToChar(informe.noAplica) + "</noaplica>";
               strXMLReturn = strXMLReturn + "<observaciones>" + informe.observaciones + "</observaciones>";
               strXMLReturn = strXMLReturn + "</informesdbr>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }

      public static string genXMLinformesCtrol(List<eControlCalidad> informes)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var informe in informes)
            {
               strXMLReturn = strXMLReturn + "<informesctrl>";
               strXMLReturn = strXMLReturn + "<codinforme>" + informe.codControlCalidad+ "</codinforme>";
               strXMLReturn = strXMLReturn + "<aprobado>" + boolToChar(informe.aprobado) + "</aprobado>";
               strXMLReturn = strXMLReturn + "<observaciones>" + informe.observaciones + "</observaciones>";
               strXMLReturn = strXMLReturn + "</informesctrl>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }

      public static string genXMLPrsupuesto(List<ePresupuesto> presupuestos)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var presupuesto in presupuestos)
            {
               strXMLReturn = strXMLReturn + "<detpresto>";
               strXMLReturn = strXMLReturn + "<cuenta>" + presupuesto.CodcuentaContable + "</cuenta>";
               strXMLReturn = strXMLReturn + "<p01>" + presupuesto.pres01 + "</p01>";
               strXMLReturn = strXMLReturn + "<p02>" + presupuesto.pres02 + "</p02>";
               strXMLReturn = strXMLReturn + "<p03>" + presupuesto.pres03 + "</p03>";
               strXMLReturn = strXMLReturn + "<p04>" + presupuesto.pres04 + "</p04>";
               strXMLReturn = strXMLReturn + "<p05>" + presupuesto.pres05 + "</p05>";
               strXMLReturn = strXMLReturn + "<p06>" + presupuesto.pres06 + "</p06>";
               strXMLReturn = strXMLReturn + "<p07>" + presupuesto.pres07 + "</p07>";
               strXMLReturn = strXMLReturn + "<p08>" + presupuesto.pres08 + "</p08>";
               strXMLReturn = strXMLReturn + "<p09>" + presupuesto.pres09 + "</p09>";
               strXMLReturn = strXMLReturn + "<p10>" + presupuesto.pres10 + "</p10>";
               strXMLReturn = strXMLReturn + "<p11>" + presupuesto.pres11 + "</p11>";
               strXMLReturn = strXMLReturn + "<p12>" + presupuesto.pres12 + "</p12>";
               strXMLReturn = strXMLReturn + "</detpresto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }

      public static string genXMLPresupuestoOpexEjecutado(List<ePresupuesto> presupuestos)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var presupuesto in presupuestos)
            {
               strXMLReturn = strXMLReturn + "<detpresto>";
               strXMLReturn = strXMLReturn + "<cuenta>" + presupuesto.CodcuentaContable + "</cuenta>";
               strXMLReturn = strXMLReturn + "<p01>" + presupuesto.ejec01 + "</p01>";
               strXMLReturn = strXMLReturn + "<p02>" + presupuesto.ejec02 + "</p02>";
               strXMLReturn = strXMLReturn + "<p03>" + presupuesto.ejec03 + "</p03>";
               strXMLReturn = strXMLReturn + "<p04>" + presupuesto.ejec04 + "</p04>";
               strXMLReturn = strXMLReturn + "<p05>" + presupuesto.ejec05 + "</p05>";
               strXMLReturn = strXMLReturn + "<p06>" + presupuesto.ejec06 + "</p06>";
               strXMLReturn = strXMLReturn + "<p07>" + presupuesto.ejec07 + "</p07>";
               strXMLReturn = strXMLReturn + "<p08>" + presupuesto.ejec08 + "</p08>";
               strXMLReturn = strXMLReturn + "<p09>" + presupuesto.ejec09 + "</p09>";
               strXMLReturn = strXMLReturn + "<p10>" + presupuesto.ejec10 + "</p10>";
               strXMLReturn = strXMLReturn + "<p11>" + presupuesto.ejec11 + "</p11>";
               strXMLReturn = strXMLReturn + "<p12>" + presupuesto.ejec12 + "</p12>";
               strXMLReturn = strXMLReturn + "</detpresto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }
      public static string genXMLPrsupuestoIngreso(List<ePresupuesto> presupuestos)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var presupuesto in presupuestos)
            {
               strXMLReturn = strXMLReturn + "<detpresto>";
               strXMLReturn = strXMLReturn + "<clase>" + presupuesto.claseProducto + "</clase>";
               strXMLReturn = strXMLReturn + "<familia>" + presupuesto.familiaProducto + "</familia>";
               strXMLReturn = strXMLReturn + "<producto>" + presupuesto.codProducto + "</producto>";
               strXMLReturn = strXMLReturn + "<p01>" + presupuesto.pres01 + "</p01>";
               strXMLReturn = strXMLReturn + "<p02>" + presupuesto.pres02 + "</p02>";
               strXMLReturn = strXMLReturn + "<p03>" + presupuesto.pres03 + "</p03>";
               strXMLReturn = strXMLReturn + "<p04>" + presupuesto.pres04 + "</p04>";
               strXMLReturn = strXMLReturn + "<p05>" + presupuesto.pres05 + "</p05>";
               strXMLReturn = strXMLReturn + "<p06>" + presupuesto.pres06 + "</p06>";
               strXMLReturn = strXMLReturn + "<p07>" + presupuesto.pres07 + "</p07>";
               strXMLReturn = strXMLReturn + "<p08>" + presupuesto.pres08 + "</p08>";
               strXMLReturn = strXMLReturn + "<p09>" + presupuesto.pres09 + "</p09>";
               strXMLReturn = strXMLReturn + "<p10>" + presupuesto.pres10 + "</p10>";
               strXMLReturn = strXMLReturn + "<p11>" + presupuesto.pres11 + "</p11>";
               strXMLReturn = strXMLReturn + "<p12>" + presupuesto.pres12 + "</p12>";
               strXMLReturn = strXMLReturn + "</detpresto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }

      public static string genXMLSetCostMrgenTransptoIngRetail(List<ePresupuesto> presupuestos)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var presupuesto in presupuestos)
            {
               strXMLReturn = strXMLReturn + "<presupuesto>";
               strXMLReturn = strXMLReturn + "<clase>" + presupuesto.claseProducto + "</clase>";
               strXMLReturn = strXMLReturn + "<familia>" + presupuesto.familiaProducto + "</familia>";
               strXMLReturn = strXMLReturn + "<producto>" + presupuesto.codProducto + "</producto>";
               strXMLReturn = strXMLReturn + "<preciopublico>" + presupuesto.precioPublico + "</preciopublico>";
               strXMLReturn = strXMLReturn + "<margen>" + presupuesto.margenUtilidad + "</margen>";
               strXMLReturn = strXMLReturn + "<transporte>" + presupuesto.precioTransporte + "</transporte>";
               strXMLReturn = strXMLReturn + "</presupuesto>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }
      public static string genXMLSalariosEmpleados(List<eEmpleado> Empleados)
      {
         string strXMLReturn = "";
         try
         {
            strXMLReturn = "<xmlroot>";
            foreach (var Empleado in Empleados)
            {
               strXMLReturn = strXMLReturn + "<detsalarios>";
               strXMLReturn = strXMLReturn + "<codigo>" + Empleado.codigoCorp + "</codigo>";
               strXMLReturn = strXMLReturn + "<salario>" + Empleado.salario + "</salario>";        
               strXMLReturn = strXMLReturn + "</detsalarios>";
            }
            strXMLReturn = strXMLReturn + "</xmlroot>";
            return strXMLReturn;
         }
         catch { return ""; }
      }
      public static char boolToChar(bool valor)
      {
         if (valor)
         {
            return 'S';
         }
         else
         {
            return 'N';
         }
      }

      public static bool chartoBool(char valor)
      {
         if (valor=='S')
         {
            return true;
         }
         else
         {
            return false;
         }
      }
   }
}