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
    public partial class Usuarios : Form
    {
        MySqlCommand comando;

        public Usuarios()
        {
            InitializeComponent();
            llenarGridUsuarios();
            llenarComboTipoUsuarios();
            llenarComboRutas();
        }

        private void llenarComboRutas()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputruta.DataSource = Datos.ejecutarComandoSelect(comando);
            inputruta.ValueMember = "id";
            inputruta.DisplayMember = "ruta";
            inputruta.SelectedIndex = -1;
        }

        private void llenarComboTipoUsuarios()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, nombre FROM tipo_usuario ORDER BY nombre";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputtipo_usuario_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputtipo_usuario_id.ValueMember = "id";
            inputtipo_usuario_id.DisplayMember = "nombre";
            inputtipo_usuario_id.SelectedIndex = -1;
        }

        private void aLMACENARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputuser.Text))
            {
                Mensajes.error("Debe Ingresar el USUARIO");
                return;
            }

            if (String.IsNullOrEmpty(inputpass.Text))
            {
                Mensajes.error("Debe Ingresar la CONTRASEÑA");
                return;
            }

            if (String.IsNullOrEmpty(inputtipo_usuario_id.Text))
            {
                Mensajes.error("Debe Seleccionar el TIPO DE USUARIO");
                return;
            }

            if (String.IsNullOrEmpty(inputnombre.Text))
            {
                Mensajes.error("Debe Ingresar el NOMBRE del usuario");
                return;
            }

            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Debe Ingresar el DOCUMENTO del usuario");
                return;
            }

            if (!inputpass.Text.Equals(inputpass2.Text))
            {
                Mensajes.error("La constraseñas no coinciden");
                return;
            }

            if (inputtipo_usuario_id.SelectedValue.Equals(2) && String.IsNullOrEmpty(inputruta.Text))
            {
                Mensajes.error("Debe asignar una ruta al cobrador");
                return;
            }

            if (inputtipo_usuario_id.SelectedValue.Equals(2))
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT cantidad FROM cantidad_cobradores";

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                DataTable cantidadCobradores = Datos.ejecutarComandoSelect(comando);

                int cobradoresPermitidos = Convert.ToInt32(cantidadCobradores.Rows[0]["cantidad"].ToString());

                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT COUNT(id) AS cantidad FROM users WHERE tipo_usuario_id = @tipo_usuario_id AND activo=@activo";
                comando.Parameters.AddWithValue("@tipo_usuario_id", inputtipo_usuario_id.SelectedValue.ToString());
                comando.Parameters.AddWithValue("@activo", "1");

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
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT user FROM users WHERE user = @user";
            comando.Parameters.AddWithValue("@user", inputuser.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable validacion = Datos.ejecutarComandoSelect(comando);

            if (validacion.Rows.Count > 0)
            {
                Mensajes.error("El nombre de usuario ya existe, imposible almacenar");
                return;
            }

            #endregion

            #region insert

            string fecha = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            string user = inputuser.Text;
            string pass = inputpass.Text;
            string tipo_usuario_id = inputtipo_usuario_id.SelectedValue.ToString();
            bool activo = (bool)inputactivo.Checked;
            string nombre = inputnombre.Text;
            string documento = inputdocumento.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea Insertar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO users (fecha, user, pass, tipo_usuario_id, activo, nombre, documento, rutas_id) VALUES (@fecha, @user, @pass, @tipo_usuario_id, @activo, @nombre, @documento, @rutas_id)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@user", user);
                comando.Parameters.AddWithValue("@pass", pass);
                comando.Parameters.AddWithValue("@tipo_usuario_id", tipo_usuario_id);
                comando.Parameters.AddWithValue("@activo", activo);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@documento", documento);
                comando.Parameters.AddWithValue("@rutas_id", inputruta.SelectedValue);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("La inserción se ha realizado correctamente.");

                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: fecha = " + fecha + ", user = " + user + ", pass = " + pass + ", tipo_usuario_id = " + tipo_usuario_id + ", activo = " + activo + ", nombre = " + nombre + ", documento = " + documento + ", rutas = " + inputruta.SelectedValue + " ";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridUsuarios();
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

        private void llenarGridUsuarios()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, nombre, documento, user, activo, tipo_usuario_id, rutas_id FROM users ORDER BY nombre";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
        }

        private void mODIFICARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputuser.Text))
            {
                Mensajes.error("Debe Ingresar el USUARIO");
                return;
            }

            if (String.IsNullOrEmpty(inputpass.Text))
            {
                Mensajes.error("Debe Ingresar la CONTRASEÑA");
                return;
            }

            if (String.IsNullOrEmpty(inputtipo_usuario_id.Text))
            {
                Mensajes.error("Debe Seleccionar el TIPO DE USUARIO");
                return;
            }

            if (String.IsNullOrEmpty(inputnombre.Text))
            {
                Mensajes.error("Debe Ingresar el NOMBRE del usuario");
                return;
            }

            if (String.IsNullOrEmpty(inputdocumento.Text))
            {
                Mensajes.error("Debe Ingresar el DOCUMENTO del usuario");
                return;
            }

            if (!inputpass.Text.Equals(inputpass2.Text))
            {
                Mensajes.error("La constraseñas no coinciden");
                return;
            }

            if (dataGridView1.RowCount == 0)
            {
                Mensajes.error("No hay datos para modificar");
                return;
            }

            if (inputtipo_usuario_id.SelectedValue.Equals(2) && String.IsNullOrEmpty(inputruta.Text))
            {
                Mensajes.error("Debe asignar una ruta al cobrador");
                return;
            }

            if (inputtipo_usuario_id.SelectedValue.Equals(2))
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT cantidad FROM cantidad_cobradores";

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                DataTable cantidadCobradores = Datos.ejecutarComandoSelect(comando);

                int cobradoresPermitidos = Convert.ToInt32(cantidadCobradores.Rows[0]["cantidad"].ToString());

                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT COUNT(id) AS cantidad FROM users WHERE tipo_usuario_id = @tipo_usuario_id AND activo = @activo";
                comando.Parameters.AddWithValue("@tipo_usuario_id", inputtipo_usuario_id.SelectedValue.ToString());
                comando.Parameters.AddWithValue("@activo", "1");

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                cantidadCobradores = Datos.ejecutarComandoSelect(comando);

                int cobradoresConfigurados = Convert.ToInt32(cantidadCobradores.Rows[0]["cantidad"].ToString());

                if (cobradoresConfigurados >= cobradoresPermitidos && !dataGridView1["tipo_usuario_id", dataGridView1.CurrentRow.Index].Value.ToString().Equals("2"))
                {
                    Mensajes.error("La cantidad de Cobradores llego al límite permitido, Imposible Configurar un Nuevo Cobrador");
                    Mensajes.informacion("Si desea ampliar la cantidad de cobradores permitidos, póngase en contacto con el distribuidor del software");
                    return;
                }
            }

            #endregion

            #region update

            string id = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
            string user = inputuser.Text;
            string pass = inputpass.Text;
            string tipo_usuario_id = inputtipo_usuario_id.SelectedValue.ToString();
            bool activo = (bool)inputactivo.Checked;
            string nombre = inputnombre.Text;
            string documento = inputdocumento.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea modificar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE users SET user=@user, pass=@pass, tipo_usuario_id=@tipo_usuario_id, activo=@activo, nombre=@nombre, documento=@documento, rutas_id = @rutas_id WHERE id = @id";

                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@user", user);
                comando.Parameters.AddWithValue("@pass", pass);
                comando.Parameters.AddWithValue("@tipo_usuario_id", tipo_usuario_id);
                comando.Parameters.AddWithValue("@activo", activo);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@documento", documento);
                comando.Parameters.AddWithValue("@rutas_id", inputruta.SelectedValue);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("El registro se ha modificado correctamente.");
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: modificación; LOS DATOS DE LA ACCIÓN SON: id = " + id + ", user = " + user + ", pass = " + pass + ", tipo_usuario_id = " + tipo_usuario_id + ", activo = " + activo + ", nombre = " + nombre + ", documento = " + documento + " , rutas_id = " + inputruta.SelectedValue + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridUsuarios();
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
                Mensajes.error("No hay datos para eliminar");
                return;
            }

            #endregion

            #region delete

            string id = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();

            if (Mensajes.confirmacion("¿Está seguro de que desea eliminar el registro?") == false)
            {
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "DELETE FROM users WHERE id = @id";

                comando.Parameters.AddWithValue("@id", id);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("Se ha eliminado el registro correctamente.");
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: eliminación; LOS DATOS DE LA ACCIÓN SON: id = " + id + ", user = " + dataGridView1["user", dataGridView1.CurrentRow.Index].Value.ToString() + ", tipo_usuario_id = " + dataGridView1["tipo_usuario_id", dataGridView1.CurrentRow.Index].Value.ToString() + ", nombre = " + dataGridView1["nombre", dataGridView1.CurrentRow.Index].Value.ToString() + ", documento = " + dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString() + ", rutas_id = " + dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString() + "";
                    Datos.crearLOG(formulario, descripcion);
                    llenarGridUsuarios();
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
            if (Mensajes.confirmacion("Desea limpiar la pantalla"))
            {
                inputuser.Text = "";
                inputpass.Text = "";
                inputpass2.Text = "";
                inputnombre.Text = "";
                inputdocumento.Text = "";
                inputruta.SelectedIndex = -1;

                inputtipo_usuario_id.SelectedIndex = -1;

                inputactivo.Checked = true;

                labelruta.Visible = false;
                inputruta.Visible = false;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            inputuser.Text = dataGridView1["user", dataGridView1.CurrentRow.Index].Value.ToString();
            inputtipo_usuario_id.SelectedValue = dataGridView1["tipo_usuario_id", dataGridView1.CurrentRow.Index].Value.ToString();
            inputactivo.Checked = (bool)dataGridView1["activo", dataGridView1.CurrentRow.Index].Value;
            inputnombre.Text = dataGridView1["nombre", dataGridView1.CurrentRow.Index].Value.ToString();
            inputdocumento.Text = dataGridView1["documento", dataGridView1.CurrentRow.Index].Value.ToString();
            inputpass.Text = "";
            inputpass2.Text = "";
            try
            {
                inputruta.SelectedValue = dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString();

                if (String.IsNullOrEmpty(dataGridView1["rutas_id", dataGridView1.CurrentRow.Index].Value.ToString()))
                {
                    labelruta.Visible = false;
                    inputruta.Visible = false;
                }
                else
                {
                    labelruta.Visible = true;
                    inputruta.Visible = true;
                }                
            }
            catch (Exception)
            {
                inputruta.SelectedIndex = -1;
                labelruta.Visible = false;
                inputruta.Visible = false;
            }
        }

        private void inputtipo_usuario_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputtipo_usuario_id.Text))
            {
                return;
            }

            if (inputtipo_usuario_id.SelectedValue.Equals(2))
            {
                labelruta.Visible = true;
                inputruta.Visible = true;
            }
            else
            {
                labelruta.Visible = false;
                inputruta.Visible = false;
            }
        }
    }
}
