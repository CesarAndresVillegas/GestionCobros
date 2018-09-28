using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestionCobros
{
    class VGlobales
    {
        // Identificación de la aplicación.
        public static string NOMBRE_APLICACION = " - Gestión de Cobros -";

        // Variables constantes para la cadena de conexion de la base de datos.

        //------------------------------------------------------------------------
        // Host Web
        //public static string HOST = "www.publirecargas.com";
        //public static string USUARIO_DB = "ab96474_cobros";
        //public static string PASSWORD_DB = "Cobros@2015";
        //public static string DATABASE = "ab96474_cobros_wiladi";
        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        //// Host Local
        public static string HOST = "localhost";
        public static string USUARIO_DB = "root";
        public static string PASSWORD_DB = "CA@.v1ll3g4s";
        public static string DATABASE = "cobros_wiladi";
        //-------------------------------------------------------------------------
        //variables de sesion
        public static string nombre = "";
        public static string tipo_usuario = "";
        public static string id_usuario = "";
    }
}
