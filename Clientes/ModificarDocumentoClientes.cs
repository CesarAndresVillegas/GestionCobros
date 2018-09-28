using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GestionCobros.Clientes
{
    public partial class ModificarDocumentoClientes : Form
    {
        MySqlCommand comando;
        
        public ModificarDocumentoClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(txtDocumentoActual.Text))
            {
                Mensajes.error("Ingrese el documento actual del cliente");
                return;
            }

            if (String.IsNullOrEmpty(txtDocumentoNuevo.Text))
            {
                Mensajes.error("Ingrese el documento nuevo del cliente");
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT documento FROM clientes WHERE documento = @documento";
            comando.Parameters.AddWithValue("@documento", txtDocumentoActual.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable clientes = Datos.ejecutarComandoSelect(comando);
            if (clientes.Rows.Count==0)
            {
                Mensajes.error("El documento ingresado no se encuentra en sistema");
                return;
            }

            #endregion

            string documentoActual = txtDocumentoActual.Text;
            string documentoNuevo = txtDocumentoNuevo.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea modificar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE clientes SET documento=@documento1 WHERE documento = @documento2";

                comando.Parameters.AddWithValue("@documento1", documentoNuevo);
                comando.Parameters.AddWithValue("@documento2", documentoActual);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("El registro se ha modificado correctamente.");
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: modificación; LOS DATOS DE LA ACCIÓN SON: documento anterior = " + documentoActual + " documento nuevo = " + documentoNuevo +"";
                    Datos.crearLOG(formulario, descripcion);
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar modificar el registro.");
                }
            }
            catch
            {
                Mensajes.error("Ha ocurrido un error al intentar modificar el registro.");
            }
        }
    }
}
