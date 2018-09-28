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

namespace GestionCobros.Rutas
{
    public partial class SaldosCobradores : Form
    {
        MySqlCommand comando;
        decimal valorInicial = 0;
        
        public SaldosCobradores()
        {
            InitializeComponent();
            llenarComboCobradores();
        }

        private void llenarComboCobradores()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, nombre FROM users WHERE tipo_usuario_id = @tipo_usuario_id AND activo = @activo ORDER BY nombre";
            comando.Parameters.AddWithValue("@tipo_usuario_id", "2");
            comando.Parameters.AddWithValue("@activo", "1");

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputcobrador_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputcobrador_id.DisplayMember = "nombre";
            inputcobrador_id.ValueMember = "id";
            inputcobrador_id.SelectedIndex = -1;
        }

        private void llenarGrid()
        {
            this.Cursor = Cursors.WaitCursor;
            
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, fecha, pago, saldo, observaciones, cobrador_id FROM saldos_cobradores WHERE cobrador_id=@cobrador_id ORDER BY fecha DESC";
            comando.Parameters.AddWithValue("@cobrador_id", inputcobrador_id.SelectedValue.ToString());

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1["pago", i].Value.ToString().Equals(dataGridView1["saldo", i].Value.ToString()))
                {
                    dataGridView1.Rows[i].Visible = false;
                    continue;
                }
            }

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["cobrador_id"].Visible = false;

            dataGridView1.Columns["fecha"].HeaderText = "Fecha";

            dataGridView1.Columns["pago"].HeaderText = "Pago Realizado";
            dataGridView1.Columns["pago"].ReadOnly = false;
            dataGridView1.Columns["pago"].DefaultCellStyle.BackColor = Color.LightBlue;

            dataGridView1.Columns["saldo"].HeaderText = "Saldo Total";
            dataGridView1.Columns["saldo"].DefaultCellStyle.BackColor = Color.LawnGreen;

            this.Cursor = Cursors.Default;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToDecimal(dataGridView1["pago", dataGridView1.CurrentRow.Index].Value.ToString()) > Convert.ToDecimal(dataGridView1["saldo", dataGridView1.CurrentRow.Index].Value.ToString()))
            {
                Mensajes.error("El pago no puede ser mayor al saldo");
                dataGridView1["pago",dataGridView1.CurrentRow.Index].Value = valorInicial;
                return;
            }

            if (Convert.ToDecimal(dataGridView1["pago", dataGridView1.CurrentRow.Index].Value.ToString()) < valorInicial)
            {
                Mensajes.error("El pago no puede ser menor al valor inicial");
                dataGridView1["pago", dataGridView1.CurrentRow.Index].Value = valorInicial;
                return;
            }

            #region update

            string id = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
            decimal pago = Convert.ToDecimal(dataGridView1["pago", dataGridView1.CurrentRow.Index].Value.ToString());

            if (Mensajes.confirmacion("¿Está seguro de que desea modificar el registro?") == false)
            {
                dataGridView1["pago", dataGridView1.CurrentRow.Index].Value = valorInicial;
                return;
            }

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE saldos_cobradores SET pago=@pago WHERE id = @id";

                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@pago", pago);

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    Mensajes.informacion("Datos modificados con éxito");
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar modificar el registro.");
                    dataGridView1["pago", dataGridView1.CurrentRow.Index].Value = valorInicial;
                }
            }
            catch
            {
                Mensajes.error("Ha ocurrido un error al intentar modificar el registro.");
                dataGridView1["pago", dataGridView1.CurrentRow.Index].Value = valorInicial;
            }

            #endregion
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                valorInicial = Convert.ToDecimal(dataGridView1["pago", dataGridView1.CurrentRow.Index].Value.ToString());
            }
            catch
            {
                valorInicial = 0;
            }            
        }

        private void inputcobrador_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputcobrador_id.Text))
            {
                return;
            }

            llenarGrid();
        }
    }
}
