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
    public partial class ConsultarRutas : Form
    {
        MySqlCommand comando;
        
        public ConsultarRutas()
        {
            InitializeComponent();
            llenarComboRutas();
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

        private void inputruta_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                return;
            }

            llenarGrid();
            ordenarGrid();
        }

        private void llenarGrid()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT * FROM clientes cl JOIN cuotas cu ON cl.documento = cu.clientes_documento WHERE cu.fecha_pago = @fecha_pago AND rutas_id=@rutas_id ORDER BY cl.nombre";
            comando.Parameters.AddWithValue("@fecha_pago", inputfecha.Value.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@rutas_id", inputruta_id.SelectedValue.ToString());

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
        }

        private void ordenarGrid()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }

            dataGridView1.Columns["nombre"].Visible = true;
            dataGridView1.Columns["direccion"].Visible = true;
            dataGridView1.Columns["telefono"].Visible = true;
            dataGridView1.Columns["valor"].Visible = true;
            dataGridView1.Columns["finalizada"].Visible = true;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1["finalizada", i].Value)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void inputfecha_ValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputruta_id.Text))
            {
                return;
            }

            llenarGrid();
            ordenarGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount==0)
            {
                Mensajes.error("No hay datos para exportar");
                return;
            }

            if (Mensajes.confirmacion("Desea generar un archivo de excel con la ruta?"))
            {
                Datos.ConvertirExcel(dataGridView1);
            }
        }
    }
}
