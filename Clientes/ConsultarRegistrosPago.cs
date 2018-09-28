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
    public partial class ConsultarRegistrosPago : Form
    {
        MySqlCommand comando;
        
        public ConsultarRegistrosPago()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputruta.Text))
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT i.id, c.documento, c.nombre, i.valor FROM informacion_pagos i JOIN clientes c ON i.clientes_documento = c.documento WHERE i.fecha = @fecha ORDER BY c.nombre";
                comando.Parameters.AddWithValue("@fecha", inputfecha.Value.ToString("yyyy-MM-dd"));

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
            }
            else
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT i.id, c.documento, c.nombre, i.valor FROM informacion_pagos i JOIN clientes c ON i.clientes_documento = c.documento WHERE i.fecha = @fecha AND c.rutas_id = @rutas_id ORDER BY c.nombre";
                comando.Parameters.AddWithValue("@fecha", inputfecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@rutas_id", inputruta.SelectedValue.ToString());

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
            }

            if (dataGridView1.RowCount>0)
            {
                if (Mensajes.confirmacion("Desea generar un archivo de Excel con la información?"))
                {
                    Datos.ConvertirExcel(dataGridView1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inputruta.SelectedIndex = -1;
        }

        private void inputruta_Enter(object sender, EventArgs e)
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, ruta FROM rutas ORDER BY ruta";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputruta.DataSource = Datos.ejecutarComandoSelect(comando);
            inputruta.DisplayMember = "ruta";
            inputruta.ValueMember = "id";
            inputruta.SelectedIndex = -1;		
        }
    }
}
