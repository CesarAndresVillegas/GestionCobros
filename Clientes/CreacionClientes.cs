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
    public partial class CreacionClientes : Form
    {
        MySqlCommand comando;

        public CreacionClientes()
        {
            InitializeComponent();
            llenarGridClientes();
            llenarComboRuta();
        }

        private void llenarComboRuta()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputruta_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputruta_id.DisplayMember = "ruta";
            inputruta_id.ValueMember = "id";
            inputruta_id.SelectedIndex = -1;
        }

        private void llenarGridClientes()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT * FROM clientes c LEFT JOIN referencias r ON c.documento = r.clientes_documento JOIN rutas ru ON c.rutas_id = ru.id ORDER BY c.nombre";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }

            dataGridView1.Columns["nombre"].Visible = true;
            dataGridView1.Columns["nombre"].HeaderText = "Cliente";

            dataGridView1.Columns["documento"].Visible = true;
            dataGridView1.Columns["documento"].HeaderText = "Documento";

            dataGridView1.Columns["nombre1"].Visible = true;
            dataGridView1.Columns["nombre1"].HeaderText = "Referencia";

            dataGridView1.Columns["ruta"].Visible = true;
            dataGridView1.Columns["ruta"].HeaderText = "Ruta";
        }

        private void aLMACENARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputnombre.Text))
            {
                Mensajes.error("Ingrese el nombre del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Ingrese el documento del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputdireccion.Text))
            {
                Mensajes.error("Ingrese la dirección del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputtelefono.Text))
            {
                Mensajes.error("Ingrese el teléfono del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                Mensajes.error("Seleccione la ruta a la que pertenece el cliente");
                return;
            }

            if (dataGridView1.RowCount>0)
            {
                Mensajes.error("Este número de documento ya se encuentra registrado");
                Mensajes.informacion("A continuación se mostrará la información del paciente registrado");

                inputnombre.Text = dataGridView1["nombre", dataGridView1.CurrentRow.Index].Value.ToString();
                inputdocumento.Text = dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString();
                inputdireccion.Text = dataGridView1["direccion", dataGridView1.CurrentRow.Index].Value.ToString();
                inputtelefono.Text = dataGridView1["telefono", dataGridView1.CurrentRow.Index].Value.ToString();
                inputruta_id.SelectedValue = dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString();

                inputnombre_referencia.Text = dataGridView1["nombre1", dataGridView1.CurrentRow.Index].Value.ToString();
                inputdocumento_referencia.Text = dataGridView1["documento1", dataGridView1.CurrentRow.Index].Value.ToString();
                inputdireccion_referencia.Text = dataGridView1["direccion1", dataGridView1.CurrentRow.Index].Value.ToString();
                inputtelefono_referencia.Text = dataGridView1["telefono1", dataGridView1.CurrentRow.Index].Value.ToString();

                calcularIncumplimientos(inputdocumento.Text);

                return;
            }

            #endregion

            #region transaccion

            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            string nombre = inputnombre.Text;
            string documento = inputdocumento.Text;
            string direccion = inputdireccion.Text;
            string telefono = inputtelefono.Text;
            string ruta_id = inputruta_id.SelectedValue.ToString();
            int idCliente = 0;

            string nombreReferencia = inputnombre_referencia.Text;
            string documentoReferencia = inputdocumento_referencia.Text;
            string direccionReferencia = inputdireccion_referencia.Text;
            string telefonoReferencia = inputtelefono_referencia.Text;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO clientes (nombre, documento, direccion, telefono, rutas_id) VALUES (@nombre, @documento, @direccion, @telefono, @rutas_id)";

                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@documento", documento);
                comando.Parameters.AddWithValue("@direccion", direccion);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@rutas_id", ruta_id);

                comando.ExecuteNonQuery();

                idCliente = unchecked((int)comando.LastInsertedId);

                comando.Parameters.Clear();

                if (!String.IsNullOrEmpty(documentoReferencia))
                {
                    comando.CommandText = "INSERT INTO referencias (nombre, documento, direccion, telefono, clientes_documento) VALUES (@nombre, @documento, @direccion, @telefono, @clientes_documento)";

                    comando.Parameters.AddWithValue("@nombre", nombreReferencia);
                    comando.Parameters.AddWithValue("@documento", documentoReferencia);
                    comando.Parameters.AddWithValue("@direccion", direccionReferencia);
                    comando.Parameters.AddWithValue("@telefono", telefonoReferencia);
                    comando.Parameters.AddWithValue("@clientes_documento", documento);

                    comando.ExecuteNonQuery();                    
                }

                comando.Transaction.Commit();

                operacionCorrecta = true;
            }
            catch
            {
                comando.Transaction.Rollback();
            }
            finally
            {
                comando.Connection.Close();

                if (operacionCorrecta)
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: nombre = " + nombre + ", documento = " + documento + ", direccion = " + direccion + ", telefono = " + telefono + ", rutas_id = " + ruta_id + " , nombreReferencia = " + nombreReferencia + ", documentoReferencia = " + documentoReferencia + ", direccionReferencia = " + direccionReferencia + ", telefonoReferencia = " + telefonoReferencia + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridClientes();
                    Mensajes.informacion("Proceso finalizado con éxito");
                    limpiarPantalla();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar eliminar el registro, probablemente el número de documento o referencia ya se encuetren registrados");
                }
            }

            #endregion
        }

        private void mODIFICARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputnombre.Text))
            {
                Mensajes.error("Ingrese el nombre del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Ingrese el documento del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputdireccion.Text))
            {
                Mensajes.error("Ingrese la dirección del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputtelefono.Text))
            {
                Mensajes.error("Ingrese el teléfono del cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                Mensajes.error("Seleccione la ruta a la que pertenece el cliente");
                return;
            }

            if (dataGridView1.RowCount == 0)
            {
                Mensajes.error("No hay datos para modificar");
                return;
            }

            #endregion

            if (Mensajes.confirmacion("¿Está seguro de que desea modificar el registro?") == false)
            {
                return;
            }

            #region transaccion

            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            string nombreReferencia = inputnombre_referencia.Text;
            string documentoReferencia = inputdocumento_referencia.Text;
            string direccionReferencia = inputdireccion_referencia.Text;
            string telefonoReferencia = inputtelefono_referencia.Text;

            string nombre = inputnombre.Text;
            string documentoAnterior = dataGridView1["Documento", dataGridView1.CurrentRow.Index].Value.ToString();
            string direccion = inputdireccion.Text;
            string telefono = inputtelefono.Text;
            string ruta_id = inputruta_id.SelectedValue.ToString();
            string documentoNuevo = inputdocumento.Text;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "UPDATE referencias SET nombre=@nombre, documento=@documento, direccion=@direccion, telefono=@telefono WHERE clientes_documento = @clientes_idAnterior";

                comando.Parameters.AddWithValue("@nombre", nombreReferencia);
                comando.Parameters.AddWithValue("@documento", documentoReferencia);
                comando.Parameters.AddWithValue("@direccion", direccionReferencia);
                comando.Parameters.AddWithValue("@telefono", telefonoReferencia);
                comando.Parameters.AddWithValue("@clientes_idAnterior", documentoAnterior);
                
                comando.ExecuteNonQuery();                

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE clientes SET nombre=@nombre, direccion=@direccion, telefono=@telefono, rutas_id=@rutas_id WHERE documento = @documentoAnterior";

                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@documentoAnterior", documentoAnterior);
                comando.Parameters.AddWithValue("@direccion", direccion);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@rutas_id", ruta_id);

                comando.ExecuteNonQuery();

                comando.Transaction.Commit();

                operacionCorrecta = true;

            }
            catch
            {
                comando.Transaction.Rollback();
            }
            finally
            {
                comando.Connection.Close();

                if (operacionCorrecta)
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: modificación; LOS DATOS DE LA ACCIÓN SON: documento anterior = " + documentoAnterior + ", nombre = " + nombre + ", documento nuevo = " + documentoNuevo + ", direccion = " + direccion + ", telefono = " + telefono + ", rutas_id = " + ruta_id + ", documento = " + documentoReferencia + ", nombre = " + nombreReferencia + ", documento nuevo cliente = " + inputdocumento.Text + ", direccion = " + direccionReferencia + ", telefono = " + telefonoReferencia + ", cliente anterior = " + dataGridView1["Documento", dataGridView1.CurrentRow.Index].Value.ToString() + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridClientes();
                    Mensajes.informacion("Proceso finalizado con éxito");
                    limpiarPantalla();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar eliminar el registro.");
                } 
            }

            #endregion
        }

        private void eLIMINARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (dataGridView1.RowCount == 0)
            {
                Mensajes.error("No hay datos para elimnar");
                return;
            }

            #endregion

            if (Mensajes.confirmacion("¿Está seguro de que desea eliminar el registro?") == false)
            {
                return;
            }

            #region Transacción Delete
            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando.Parameters.Clear();
                comando.CommandText = "DELETE FROM referencias WHERE clientes_documento = @clientes_documento";
                comando.Parameters.AddWithValue("@clientes_documento", dataGridView1["Documento", dataGridView1.CurrentRow.Index].Value.ToString());

                comando.ExecuteNonQuery();

                string documento = dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString();

                //Se realiza la inserción de los datos en la base de datos
                comando.Parameters.Clear();
                comando.CommandText = "DELETE FROM clientes WHERE documento = @documento";

                comando.Parameters.AddWithValue("@documento", documento);

                comando.ExecuteNonQuery();
                
                comando.Transaction.Commit();                

                operacionCorrecta = true;
            }
            catch
            {
                comando.Transaction.Rollback();
            }
            finally
            {
                comando.Connection.Close();

                if (operacionCorrecta)
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: eliminación; LOS DATOS DE LA ACCIÓN SON: nombre = " + dataGridView1["nombre", dataGridView1.CurrentRow.Index].Value.ToString() + ", documento = " + dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString() + ", direccion = " + dataGridView1["direccion", dataGridView1.CurrentRow.Index].Value.ToString() + ", telefono = " + dataGridView1["telefono", dataGridView1.CurrentRow.Index].Value.ToString() + ", rutas_id = " + dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString() + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridClientes();
                    Mensajes.informacion("Proceso finalizado con éxito");
                    limpiarPantalla();                    
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar eliminar el registro.");
                }
            }

            #endregion
        }

        private void nuevo_Click(object sender, EventArgs e)
        {
            if (Mensajes.confirmacion("Desea limpiar la pantalla?"))
            {
                limpiarPantalla();
                llenarGridClientes();
            }
        }

        private void limpiarPantalla()
        {
            inputnombre.Text = "";
            inputdocumento.Text = "";
            inputdireccion.Text = "";
            inputtelefono.Text = "";
            inputruta_id.SelectedIndex = -1;

            inputnombre_referencia.Text = "";
            inputdocumento_referencia.Text = "";
            inputdireccion_referencia.Text = "";
            inputtelefono_referencia.Text = "";
        }

        private void calcularIncumplimientos(string idCliente)
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT COUNT(id) AS cantidad FROM incumplimientos WHERE clientes_documentos=@clientes_documentos";
            comando.Parameters.AddWithValue("@clientes_documentos", idCliente);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable incumplimientos = Datos.ejecutarComandoSelect(comando);

            inputincumplimientos.Text = incumplimientos.Rows[0]["cantidad"].ToString();

            if (Convert.ToInt32(inputincumplimientos.Text) > 0)
            {
                label8.ForeColor = Color.Red;
                inputincumplimientos.ForeColor = Color.Red;
            }
            else
            {
                label8.ForeColor = Color.Black;
                inputincumplimientos.ForeColor = Color.Black;
            }
        }

        private void eSTADOFINANCIEROToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1"))
            {
                try
                {
                    //Clientes.AsignacionCupos frm = new Clientes.AsignacionCupos(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
                    Clientes.AsignacionCupos frm = new Clientes.AsignacionCupos(inputdocumento.Text);
                    frm.Show();
                }
                catch
                {
                    Mensajes.error("Seleccione un cliente");
                    return;
                }
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Seleccione un cliente");
                return;
            }

            if (String.IsNullOrEmpty(inputincumplimientos.Text))
            {
                Mensajes.error("Seleccione un cliente");
                return;
            }
            else if (inputincumplimientos.Text.Equals("0"))
            {
                Mensajes.error("El cliente no tiene incumplimientos");
                return;
            }

            Clientes.incumplimientosDetalles frm = new Clientes.incumplimientosDetalles(inputdocumento.Text, inputnombre.Text);
            frm.Show();
        }

        private void bUSCARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Debe ingresar el documento a buscar");
                return;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1["Documento", i].Value.ToString().Equals(inputdocumento.Text))
                {
                    dataGridView1.Rows[i].Selected = true;
                    break;
                }
            }            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Mensajes.confirmacion("Desea visualizar los datos del cliente?"))
            {
                return;
            }
            
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            inputnombre.Text = dataGridView1["nombre", dataGridView1.CurrentRow.Index].Value.ToString();
            inputdocumento.Text = dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString();
            inputdireccion.Text = dataGridView1["direccion", dataGridView1.CurrentRow.Index].Value.ToString();
            inputtelefono.Text = dataGridView1["telefono", dataGridView1.CurrentRow.Index].Value.ToString();
            inputruta_id.SelectedValue = dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString();
            
            inputnombre_referencia.Text = dataGridView1["nombre1", dataGridView1.CurrentRow.Index].Value.ToString();
            inputdocumento_referencia.Text = dataGridView1["documento1", dataGridView1.CurrentRow.Index].Value.ToString();
            inputdireccion_referencia.Text = dataGridView1["direccion1", dataGridView1.CurrentRow.Index].Value.ToString();
            inputtelefono_referencia.Text = dataGridView1["telefono1", dataGridView1.CurrentRow.Index].Value.ToString();

            calcularIncumplimientos(inputdocumento.Text);
        }

        private void inputdocumento_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Documento LIKE '%{0}%'", inputdocumento.Text);
        }
    }
}
