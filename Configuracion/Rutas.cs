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

namespace GestionCobros.Configuracion
{
    public partial class Rutas : Form
    {
        MySqlCommand comando;
        
        public Rutas()
        {
            InitializeComponent();
            llenarGridRutas();
        }

        private void llenarGridRutas()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
        }

        private void aLMACENARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputruta.Text))
            {
                Mensajes.error("Ingrese el nombre de la ruta");
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT cantidad FROM cantidad_cobradores";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable cantidadCobradores = Datos.ejecutarComandoSelect(comando);

            int cobradoresPermitidos = Convert.ToInt32(cantidadCobradores.Rows[0]["cantidad"].ToString());

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT COUNT(id) AS cantidad FROM rutas";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            cantidadCobradores = Datos.ejecutarComandoSelect(comando);

            int cobradoresConfigurados = Convert.ToInt32(cantidadCobradores.Rows[0]["cantidad"].ToString());

            if (cobradoresConfigurados >= cobradoresPermitidos)
            {
                Mensajes.error("La cantidad de Cobradores llego al límite permitido, Imposible Configurar un Nuevo Cobrador");
                Mensajes.informacion("Si desea ampliar la cantidad de cobradores permitidos, póngase en contacto con el distribuidor del software");
                return;
            }

            #endregion

            #region insert

            string ruta = inputruta.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea Insertar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO rutas (ruta) VALUES (@ruta)";

                comando.Parameters.AddWithValue("@ruta", ruta);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("La inserción se ha realizado correctamente.");

                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: ruta = " + ruta + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridRutas();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar realizar la inserción.");
                }
            }
            catch
            {
                Mensajes.error("Ha ocurrido un error al intentar realizar la inserción.");
            }

            #endregion
        }

        private void mODIFICARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputruta.Text))
            {
                Mensajes.error("Ingrese el nombre de la ruta");
                return;
            }

            if (dataGridView1.RowCount==0)
            {
                Mensajes.error("No hay datos para modificar");
                return;
            }

            #endregion

            #region update

            string id = dataGridView1["id",dataGridView1.CurrentRow.Index].Value.ToString();
            string ruta = inputruta.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea modificar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE rutas SET ruta=@ruta WHERE id = @id";

                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@ruta", ruta);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("El registro se ha modificado correctamente.");
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: modificación; LOS DATOS DE LA ACCIÓN SON: id = " + id + ", ruta = " + ruta + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridRutas();
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

            #endregion
        }

        private void eLIMINARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (dataGridView1.RowCount == 0)
            {
                Mensajes.error("No hay datos para modificar");
                return;
            }

            #endregion

            #region delete

            string id = dataGridView1["id",dataGridView1.CurrentRow.Index].Value.ToString();

            if (Mensajes.confirmacion("¿Está seguro de que desea eliminar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "DELETE FROM rutas WHERE id = @id";

                comando.Parameters.AddWithValue("@id", id);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("Se ha eliminado el registro correctamente.");
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: eliminación; LOS DATOS DE LA ACCIÓN SON: id = " + id + ", ruta = " + dataGridView1["ruta",dataGridView1.CurrentRow.Index].Value.ToString() + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridRutas();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar eliminar el registro.");
                }
            }
            catch
            {
                Mensajes.error("Ha ocurrido un error al intentar eliminar el registro.");
            }

            #endregion

        }

        private void nuevo_Click(object sender, EventArgs e)
        {
            inputruta.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount==0)
            {
                return;
            }

            inputruta.Text = dataGridView1["ruta",dataGridView1.CurrentRow.Index].Value.ToString();
        }
    }
}
