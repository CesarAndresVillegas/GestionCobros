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
    public partial class incumplimientosDetalles : Form
    {
        private string idCliente;
        private string nombreCliente;
        MySqlCommand comando;

        public incumplimientosDetalles(string idCliente, string nombreCliente)
        {
            // TODO: Complete member initialization
            this.idCliente = idCliente;
            this.nombreCliente = nombreCliente;
            InitializeComponent();
            label1.Text = "Documento: " + idCliente;
            label2.Text = "Nombre: " + nombreCliente;
            llenarGridClientes();
        }

        private void llenarGridClientes()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, fecha FROM incumplimientos WHERE clientes_documentos = @clientes_documentos ORDER BY fecha DESC";
            comando.Parameters.AddWithValue("@clientes_documentos", idCliente);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);		
        }        
    }
}
