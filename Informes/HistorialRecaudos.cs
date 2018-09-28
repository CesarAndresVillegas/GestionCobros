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
    public partial class HistorialRecaudos : Form
    {
        MySqlCommand comando;

        public HistorialRecaudos()
        {
            InitializeComponent();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            llenarComboCobrador();
        }

        private void llenarComboCobrador()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, nombre FROM users WHERE tipo_usuario_id = @tipo_usuario_id AND activo = @activo ORDER BY nombre";
            comando.Parameters.AddWithValue("@tipo_usuario_id", "2");
            comando.Parameters.AddWithValue("@activo", "1");

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            comboBox1.DataSource = Datos.ejecutarComandoSelect(comando);
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int valor = 0;
            
            if (radioButton1.Checked)
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT i.fecha, i.clientes_documento, c.nombre, i.cuotas, i.valor, u.nombre AS cobrador FROM informacion_pagos i JOIN clientes c ON i.clientes_documento = c.documento JOIN users u ON i.users_id = u.id WHERE i.fecha >= @fecha1 AND i.fecha<=@fecha2 ORDER BY i.fecha, u.nombre;";
                comando.Parameters.AddWithValue("@fecha1", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@fecha2", dateTimePicker2.Value.ToString("yyyy-MM-dd"));

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);                
            }
            else if (radioButton2.Checked)
            {
                if (String.IsNullOrEmpty(comboBox1.Text))
                {
                    Mensajes.error("Seleccione un cobrador");
                    return;
                }

                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT i.fecha, i.clientes_documento, c.nombre, i.cuotas, i.valor, u.nombre AS cobrador FROM informacion_pagos i JOIN clientes c ON i.clientes_documento = c.documento JOIN users u ON i.users_id = u.id WHERE i.fecha >= @fecha1 AND i.fecha<=@fecha2 AND i.users_id = @users_id ORDER BY i.fecha, u.nombre";
                comando.Parameters.AddWithValue("@fecha1", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@fecha2", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@users_id", comboBox1.SelectedValue.ToString());

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
            }
            else
            {
                Mensajes.error("Seleccione el tipo de reporte que desea");
                return;
            }

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                valor = valor + Convert.ToInt32(dataGridView1["valor", i].Value.ToString());
            }

            textBox1.Text = valor.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label1.Visible = true;
                comboBox1.Visible = true;
            }
        }
    }
}
