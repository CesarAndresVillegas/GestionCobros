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
    public partial class RegistrarPagoDetalleGrid : Form
    {
        private string idPrestamo;
        private string idCliente;
        MySqlCommand comando;

        public RegistrarPagoDetalleGrid(string idPrestamo, string idCliente)
        {
            // TODO: Complete member initialization
            this.idPrestamo = idPrestamo;
            InitializeComponent();

            inicializarGrid();

            calcularSaldo();
        }

        private void inicializarGrid()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, fecha_pago, numero_cuota, valor, abono, finalizada FROM cuotas WHERE prestamos_id = @prestamos_id AND finalizada=@finalizada ORDER BY fecha_pago ASC";
            comando.Parameters.AddWithValue("@prestamos_id", idPrestamo);
            comando.Parameters.AddWithValue("@finalizada", "0");

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

            foreach (DataGridViewColumn c in dataGridView1.Columns)
            {
                c.ReadOnly = true;
            }

            dataGridView1.Columns["abono"].ReadOnly = false;

            dataGridView1.Columns["abono"].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void calcularSaldo()
        {
            int saldo = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                saldo = saldo + Convert.ToInt32(dataGridView1["valor", i].Value.ToString());
            }

            textBox1.Text = saldo.ToString();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((bool)dataGridView1["finalizada", dataGridView1.CurrentRow.Index].Value)
            {
                try
                {
                    int i = dataGridView1.CurrentRow.Index - 1;

                    if (!(bool)dataGridView1["finalizada", i].Value)
                    {
                        dataGridView1["finalizada", dataGridView1.CurrentRow.Index].Value = false;
                        return;
                    }
                }
                catch {}

                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightSalmon;
            }
            else
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cuotas ="";
            int cantidadCuotas =0;;
            int valor =0;
            
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if ((bool)dataGridView1["finalizada", i].Value)
                {
                    cuotas = cuotas + " - " + dataGridView1["numero_cuota", i].Value.ToString();
                    cantidadCuotas = cantidadCuotas + 1;
                    valor = valor + Convert.ToInt32(dataGridView1["valor", i].Value.ToString());
                 }
            }

            MensajeConfirmacion msjConf = new MensajeConfirmacion(idCliente, cuotas.ToString(), cantidadCuotas.ToString(), valor.ToString());
            msjConf.ShowDialog();

            if (msjConf.DialogResult.Equals(DialogResult.Yes))
            {
                bool pagado = false;
                Int64 idPago = -1;

                comando = Datos.crearComando();
                comando.Connection.Open();
                MySqlTransaction transaccion = comando.Connection.BeginTransaction();
                comando.Transaction = transaccion;
                bool operacionCorrecta = false;

                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if ((bool)dataGridView1["finalizada", i].Value)
                        {
                            comando.Parameters.Clear();
                            comando.CommandText = "UPDATE cuotas SET finalizada = @finalizada, fecha_registro=@fecha_registro WHERE id = @id";
                            comando.Parameters.AddWithValue("@finalizada", "1");
                            comando.Parameters.AddWithValue("@fecha_registro", DateTime.Now.ToString("yyyy-MM-dd"));
                            comando.Parameters.AddWithValue("@id", dataGridView1["id", i].Value.ToString());

                            comando.ExecuteNonQuery();

                            // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                            pagado = true;
                            continue;
                        }
                    }

                    if (pagado)
                    {
                        comando = Datos.crearComando();

                        comando.Parameters.Clear();
                        comando.CommandText = "INSERT INTO informacion_pagos (fecha, valor, clientes_documento, cuotas, users_id) VALUES (@fecha, @valor, @clientes_documento, @cuotas, @users_id)";
                        comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                        comando.Parameters.AddWithValue("@valor", valor);
                        comando.Parameters.AddWithValue("@clientes_documento", idCliente);
                        comando.Parameters.AddWithValue("@cuotas", cuotas);
                        comando.Parameters.AddWithValue("@users_id", VGlobales.id_usuario);

                        // Ejecutar la consulta y decidir
                        // True: caso exitoso
                        // false: Error. 
                        comando.ExecuteNonQuery();

                        idPago = unchecked((int)comando.LastInsertedId);                       
                    }

                    comando.Transaction.Commit();
                    operacionCorrecta = true;
                }
                catch
                {
                    operacionCorrecta = false;
                    comando.Transaction.Rollback();
                }
                finally
                {
                    comando.Connection.Close();
                    if (operacionCorrecta)
                    {
                        inicializarGrid();
                        calcularSaldo();

                        Clientes.ConfirmacionPago frm = new Clientes.ConfirmacionPago(idPago, idCliente, cuotas, cantidadCuotas, valor);
                        frm.Show();
                        this.Close();
                    }
                    else
                    {
                        Mensajes.error("Ha ocurrido un error.");
                    }
                }   
            }
            else
            {
                Mensajes.informacion("Pago NO realizado"); 
            }
        }
    }
}
