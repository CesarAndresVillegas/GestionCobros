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
    public partial class ClientesMora : Form
    {
        MySqlCommand comando;
        
        public ClientesMora()
        {
            InitializeComponent();

            llenarClientesMora();
        }

        private void llenarClientesMora()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT cl.documento, cl.nombre, cl.direccion, cl.telefono, cu.numero_cuota, cu.valor FROM cuotas cu JOIN clientes cl ON cu.clientes_documento = cl.documento WHERE cu.finalizada = @finalizada AND cu.fecha_pago < @fecha_pago ORDER BY cl.nombre";
            comando.Parameters.AddWithValue("@finalizada", "0");
            comando.Parameters.AddWithValue("@fecha_pago", DateTime.Now.ToString("yyyy-MM-dd"));

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
		
        }
    }
}
