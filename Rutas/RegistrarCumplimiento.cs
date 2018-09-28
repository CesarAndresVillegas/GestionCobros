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
    public partial class RegistrarCumplimiento : Form
    {
        MySqlCommand comando;
        
        public RegistrarCumplimiento()
        {
            InitializeComponent();
            llenarComboRutas();
            llenarDatosPago();
        }

        private void llenarComboRutas()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputruta_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputruta_id.ValueMember = "id";
            inputruta_id.DisplayMember = "ruta";
            inputruta_id.SelectedIndex = -1;
        }

        private void llenarDatosPago()
        {
            if (!String.IsNullOrEmpty(inputruta_id.Text))
            {
                int valorEsperado = 0;
                int valorRecibido = 0;

                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT consulta.documento, consulta.nombre, consulta.direccion, consulta.numero_cuota, consulta.valor, consulta.finalizada FROM (SELECT * FROM clientes cl JOIN cuotas cu ON cl.documento = cu.clientes_documento WHERE cl.rutas_id=@rutas_id AND cu.fecha_registro=@fecha_pago " +
                                        " UNION " +
                                        " SELECT * FROM clientes cl JOIN cuotas cu ON cl.documento = cu.clientes_documento WHERE cl.rutas_id=@rutas_id AND cu.fecha_pago<=@fecha_pago AND finalizada = 0 " +
                                        " ) consulta " +
                                        " ORDER BY consulta.documento,consulta.numero_cuota;";
                comando.Parameters.AddWithValue("@rutas_id", inputruta_id.SelectedValue.ToString());
                comando.Parameters.AddWithValue("@fecha_pago", inputfecha.Value.ToString("yyyy-MM-dd"));

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);                

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    valorEsperado = valorEsperado + Convert.ToInt32(dataGridView1["valor", i].Value.ToString());

                    if (dataGridView1["finalizada", i].Value.ToString().Equals("1"))
                    {
                        valorRecibido = valorRecibido + Convert.ToInt32(dataGridView1["valor", i].Value.ToString());
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                }

                textBox1.Text = valorEsperado.ToString();
                textBox2.Text = valorRecibido.ToString();
            }
        }
                
        private void inputfecha_ValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                return;
            }
            llenarDatosPago();
        }

        private void inputruta_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                return;
            }
            llenarDatosPago();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount==0)
            {
                Mensajes.error("No hay datos para cerrar el día");
                return;
            }

            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                Mensajes.error("Debe Seleccionar una ruta");
                return;
            }

            Rutas.RegistrarSaldoCobrador frm = new Rutas.RegistrarSaldoCobrador(inputruta_id.SelectedValue.ToString(), (Convert.ToInt32(textBox1.Text) - Convert.ToInt32(textBox2.Text)).ToString());
            frm.Show();
        }
    }
}
