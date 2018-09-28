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

namespace GestionCobros.Informes
{
    public partial class HistorialClientes : Form
    {
        MySqlCommand comando;
        
        public HistorialClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region prestamos

            if (String.IsNullOrEmpty(textBox1.Text))
            {
                Mensajes.error("Ingrese un número de documento");
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT fecha, valor, interes, observacion, finalizado FROM prestamos WHERE clientes_documento = @clientes_documento ORDER BY fecha DESC";
            comando.Parameters.AddWithValue("@clientes_documento", textBox1.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1["finalizado",i].Value)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }

            #endregion

            #region pagos

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT fecha_pago, numero_cuota, valor, finalizada FROM cuotas WHERE clientes_documento = @clientes_documento ORDER BY fecha_pago DESC";
            comando.Parameters.AddWithValue("@clientes_documento", textBox1.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView2.DataSource = Datos.ejecutarComandoSelect(comando);

            int deuda = 0;

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {                
                if ((bool)dataGridView2["finalizada", i].Value)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    continue;
                }
                else if (Convert.ToDateTime(dataGridView2["fecha_pago", i].Value.ToString())<DateTime.Now)
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                deuda = deuda + Convert.ToInt32(dataGridView2["valor", i].Value.ToString());
            }

            textBox2.Text = deuda.ToString();

            #endregion

            #region incumplimientos

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT documento, nombre, fecha, direccion, telefono, ruta FROM incumplimientos i JOIN clientes c ON i.clientes_documentos=c.documento JOIN rutas r ON c.rutas_id = r.id WHERE c.documento = @documento ORDER BY documento, fecha";
            comando.Parameters.AddWithValue("@documento", textBox1.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView3.DataSource = Datos.ejecutarComandoSelect(comando);		

            #endregion
        }
    }
}
