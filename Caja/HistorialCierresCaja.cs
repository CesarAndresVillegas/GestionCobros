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

namespace GestionCobros.Caja
{
    public partial class HistorialCierresCaja : Form
    {
        MySqlCommand comando;
        
        public HistorialCierresCaja()
        {
            InitializeComponent();

            llenarComboCobradores();
        }

        private void llenarComboCobradores()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT nombre, id FROM users WHERE tipo_usuario_id=@tipo_usuario_id;";

            comando.Parameters.AddWithValue("@tipo_usuario_id", "2");

            // Al ejecutar la consulta se devuelve un DataTable.
            // --
            inputcobrador_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputcobrador_id.DisplayMember = "nombre";
            inputcobrador_id.ValueMember = "id";
            inputcobrador_id.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputcobrador_id.Text))
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT c.fecha AS Fecha, c.valor_esperado AS EsperadoSoftware, c.valor_entregado AS TotalRecibido, r.ruta AS Ruta, u.nombre AS Cobrador FROM cierres_caja_diario c JOIN rutas r ON c.rutas_id = r.id JOIN users u ON c.users_id = u.id WHERE c.fecha BETWEEN @fecha1 AND @fecha2";
                comando.Parameters.AddWithValue("@fecha1", fechaInicial.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@fecha2", fechaFinal.Value.ToString("yyyy-MM-dd"));

                // Al ejecutar la consulta se devuelve un DataTable.
                // --
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

                if (dataGridView1.RowCount>1)
                {
                    if (Mensajes.confirmacion("Desea generar un archivo de excel con la información?"))
                    {
                        Datos.ConvertirExcel(dataGridView1);
                    }
                }
            }
            else
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT c.fecha AS Fecha, c.valor_esperado AS EsperadoSoftware, c.valor_entregado AS TotalRecibido, r.ruta AS Ruta, u.nombre AS Cobrador FROM cierres_caja_diario c JOIN rutas r ON c.rutas_id = r.id JOIN users u ON c.users_id = u.id WHERE c.fecha BETWEEN @fecha1 AND @fecha2 AND c.cobrador_id = @cobrador_id";
                comando.Parameters.AddWithValue("@fecha1", fechaInicial.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@fecha2", fechaFinal.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@cobrador_id", inputcobrador_id.SelectedValue.ToString());

                // Al ejecutar la consulta se devuelve un DataTable.
                // --
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

                if (dataGridView1.RowCount > 1)
                {
                    if (Mensajes.confirmacion("Desea generar un archivo de excel con la información?"))
                    {
                        Datos.ConvertirExcel(dataGridView1);
                    }
                }
            }
        }
    }
}
