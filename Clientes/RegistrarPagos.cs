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
    public partial class RegistrarPagos : Form
    {
        MySqlCommand comando;
        private string idRuta;

        public RegistrarPagos()
        {
            InitializeComponent();

            llenarComboClientes();

            if (VGlobales.tipo_usuario.Equals("2"))
            {
                comboBox1.Visible = false;
                label3.Visible = false;

                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT rutas_id FROM users WHERE id = @id";
                comando.Parameters.AddWithValue("@id", VGlobales.id_usuario);

                DataTable rutas = Datos.ejecutarComandoSelect(comando); 


                comando.Parameters.Clear();
                comando.CommandText = "SELECT p.id, c.nombre, c.direccion, c.telefono, CONCAT('Valor Prestamo ', p.valor) FROM prestamos p " +
                    " JOIN clientes c ON p.clientes_documento = c.documento " +
                    " JOIN cuotas cu ON p.id = cu.prestamos_id " +
                    " WHERE cu.fecha_pago <= now() AND cu.finalizada=0 AND c.rutas_id=@ruta_id GROUP BY p.id ORDER BY c.nombre;";
                comando.Parameters.AddWithValue("@rutas_id", rutas.Rows[0]["rutas_id"].ToString());

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
                ordenarGrid();
               
            }
            else if (VGlobales.tipo_usuario.Equals("1"))
            {
                dataGridView1.DataSource = null;
            }		
        }

        public RegistrarPagos(string idRuta)
        {
            // TODO: Complete member initialization
            this.idRuta = idRuta;

            InitializeComponent();

            llenarComboClientes();

            comboBox1.SelectedValue = idRuta;

            if (VGlobales.tipo_usuario.Equals("2"))
            {
                comboBox1.Visible = false;
                label3.Visible = false;
            }
            else if (VGlobales.tipo_usuario.Equals("1"))
            {
                comboBox1.Visible = true;
                label3.Visible = true;
            }

            comboBox1_SelectedIndexChanged(null, null);
        }

        private void llenarComboClientes()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            comboBox1.DataSource = Datos.ejecutarComandoSelect(comando);
            comboBox1.DisplayMember = "ruta";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
        }

        private void ordenarGrid()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }

            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].HeaderText = "Cliente";
            dataGridView1.Columns[2].HeaderText = "Dirección";
            dataGridView1.Columns[3].HeaderText = "Teléfono";
            dataGridView1.Columns[4].HeaderText = "Total Prestamo";
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount==0)
            {
                return;
            }

            Clientes.RegistrarPagosDetalle frm = new Clientes.RegistrarPagosDetalle(dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString());
            frm.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT p.id, c.nombre, c.direccion, c.telefono, CONCAT('Valor Prestamo ', p.valor) FROM prestamos p " +
                " JOIN clientes c ON p.clientes_documento = c.documento " +
                " JOIN cuotas cu ON p.id = cu.prestamos_id " +
                " WHERE cu.fecha_pago <= now() AND cu.finalizada=0 AND c.rutas_id=@ruta_id GROUP BY p.id ORDER BY c.nombre;";
            comando.Parameters.AddWithValue("@rutas_id", comboBox1.SelectedValue.ToString());

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
            ordenarGrid();
        }

        private void RegistrarPagos_Load(object sender, EventArgs e)
        {
            //ordenarGrid();
        }    
    }
}
