using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionCobros
{
    /// <summary>
    /// Clase usada para estandarizar los mensajes dados al usuario.
    /// Developer: Andres Felipe Garcia Ortiz
    /// </summary>
    class Mensajes
    {
        /// <summary>
        /// Metodo que notifica un error al usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public static void error(string mensaje)
        {
            MessageBox.Show(mensaje, "SISTEMA DE INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Metodo que da información al usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public static void informacion(string mensaje)
        {
            MessageBox.Show(mensaje, "SISTEMA DE INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Metodo que da advertencia al usuario.
        /// </summary>
        /// <param name="mensaje"></param>
        public static void advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "SISTEMA DE INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Metodo usado para dirigir una pregunta al usuario
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns> True un si, False un no</returns>
        public static Boolean confirmacion(string mensaje)
        {
            if (MessageBox.Show(mensaje, "SISTEMA DE INFORMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }
    }
}
