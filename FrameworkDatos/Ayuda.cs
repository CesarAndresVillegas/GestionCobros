using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionCobros
{
    /// <summary>
    /// Framework para Obtener Tooltips.
    /// Developer: César A. Villegas.
    /// </summary>
    class Ayuda
    {
        /// <summary>
        /// General ToolTip
        /// </summary>
        /// <param name="Componente"> Objeto que contenga el evento que convoca el Tooltip</param>
        /// <param name="Titulo"> Titulo del ToolTip</param>
        /// <param name="Texto"> Contenido del ToolTip, si desea separar renglones envie debe incluir el caracter "\n" despues de cada palabra</param>
        public static void CrearTooltip(object Componente, string Titulo, string Texto)
        {
            if (Componente is Control)
            {
                Control x = Componente as Control;
                ToolTip y = new ToolTip();
                y.ToolTipTitle = Titulo;
                y.UseFading = true;
                y.UseAnimation = true;
                y.IsBalloon = true;
                y.ToolTipIcon = ToolTipIcon.Info;
                y.AutoPopDelay = 1000000;
                y.SetToolTip(x, Texto);
            }
        }
    }
}