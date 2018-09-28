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
    public partial class AsignacionCupos : Form
    {
        MySqlCommand comando;

        private string clienteId;

        public AsignacionCupos(string clienteId)
        {
            // TODO: Complete member initialization
            this.clienteId = clienteId;
            InitializeComponent();

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT nombre, documento FROM clientes WHERE documento = @documento";
            comando.Parameters.AddWithValue("@documento", clienteId);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable datosCliente = Datos.ejecutarComandoSelect(comando);

            label1.Text = "Nombre: " + datosCliente.Rows[0]["nombre"].ToString();
            label2.Text = "Documento: " + datosCliente.Rows[0]["documento"].ToString();

            llenarHistorialPrestamos();
            cargarComboFrecuencia();
            limpiarPantalla();
        }

        private void limpiarPantalla()
        {
            inputfecha.Value = DateTime.Now;
            inputdias.Value = 1;
            inputvalor.Value = 1;
            inputinteres.Text = "0";
            inputfrecuencia_cobro_id.SelectedIndex = -1;
            inputobservacion.Text = "";
            inputnumero_comprobante.Text = "";
        }

        private void cargarComboFrecuencia()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT * FROM frecuencia_cobro ORDER BY nombre";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputfrecuencia_cobro_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputfrecuencia_cobro_id.ValueMember = "id";
            inputfrecuencia_cobro_id.DisplayMember = "nombre";
            inputfrecuencia_cobro_id.SelectedIndex = -1;
        }

        private void llenarGridCuotas(string idPrestamo)
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT numero_cuota, valor, fecha_pago, finalizada FROM cuotas WHERE prestamos_id = @prestamos_id ORDER BY fecha_pago DESC";
            comando.Parameters.AddWithValue("@prestamos_id", idPrestamo);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView2.DataSource = Datos.ejecutarComandoSelect(comando);

            int saldoPendiente = 0;

            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if ((bool)dataGridView2["finalizada", i].Value)
                {
                    dataGridView2.BackgroundColor = Color.Peru;
                }
                else
                {
                    dataGridView2.BackgroundColor = Color.White;
                    saldoPendiente += Convert.ToInt32(dataGridView2["valor", i].Value.ToString()); 
                }
            }

            inputsaldo_pendiente.Text = saldoPendiente.ToString();
        }

        private void llenarHistorialPrestamos()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT * FROM prestamos WHERE clientes_documento = @clientes_documento";
            comando.Parameters.AddWithValue("@clientes_documento", clienteId);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (inputvalor.Value.Equals("0"))
            {
                Mensajes.error("El valor del prestamo no puede ser igual a 0");
                return;
            }

            if (inputdias.Value.Equals("0"))
            {
                Mensajes.error("El préstamo no puede ser a 0 días");
                return;
            }

            if (inputinteres.Text.Equals("0"))
            {
                Mensajes.error("El interes no puede ser igual a 0%");
                return;
            }

            if (inputvalorCobro.Value.Equals("0"))
            {
                Mensajes.error("El valor del prestamo no puede ser igual a 0");
                return;
            }

            if (String.IsNullOrEmpty(inputfrecuencia_cobro_id.Text))
            {
                Mensajes.error("Seleccione la frecuencia de los cobros");
                return;
            }

            if (String.IsNullOrEmpty(inputnumero_comprobante.Text))
            {
                Mensajes.error("Escriba el número del comprobante");
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT valor FROM caja";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable caja = Datos.ejecutarComandoSelect(comando);

            if (caja.Rows.Count == 0)
            {
                Mensajes.error("No esta configurada la caja, imposible realizar ajuste");
                return;
            }

            if (Convert.ToDecimal(caja.Rows[0]["valor"].ToString()) < inputvalor.Value)
            {
                Mensajes.error("No se puede sacar esa cantidad de la caja, es mayor al saldo actual");
                return;
            }

            #endregion

            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;

            #region agregar prestamo

            string fecha = inputfecha.Value.ToString("yyyy-MM-dd");
            string dias = inputdias.Text;
            string valor = inputvalor.Value.ToString();
            string valorCobro = inputvalorCobro.Value.ToString();
            string interes = inputinteres.Value.ToString().Replace(",",".");
            string observacion = inputobservacion.Text;
            string frecuencia_cobro_id = inputfrecuencia_cobro_id.SelectedValue.ToString();
            string clientes_documento = clienteId;
            string numero_comprobante = inputnumero_comprobante.Text;
            int idPrestamo = 0;

            if (Mensajes.confirmacion("¿Está seguro de que desea Insertar el registro?") == false)
            {
                this.Cursor = Cursors.Default;
                button1.Enabled = true;
                return;
            }

            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            try
            {
                //Se realiza la inserción de los datos en la base de datos
                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO prestamos (fecha, dias, valor, interes, observacion, frecuencia_cobro_id, clientes_documento, numero_comprobante) VALUES (@fecha, @dias, @valor, @interes, @observacion, @frecuencia_cobro_id, @clientes_documento, @numero_comprobante)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@dias", dias);
                comando.Parameters.AddWithValue("@valor", valor);
                comando.Parameters.AddWithValue("@interes", interes);
                comando.Parameters.AddWithValue("@observacion", observacion);
                comando.Parameters.AddWithValue("@frecuencia_cobro_id", frecuencia_cobro_id);
                comando.Parameters.AddWithValue("@clientes_documento", clientes_documento);
                comando.Parameters.AddWithValue("@numero_comprobante", numero_comprobante);

                comando.ExecuteNonQuery();

                idPrestamo = unchecked((int)comando.LastInsertedId);

                int periodoPago = 0;

                if (inputfrecuencia_cobro_id.SelectedValue.Equals(1)) // diario
                {
                    periodoPago = 1;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(2)) // semanal
                {
                    periodoPago = 7;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(3)) // quincenal
                {
                    periodoPago = 15;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(4)) // mensual
                {
                    periodoPago = 30;
                }

                for (int i = 0; i < Convert.ToInt32(dias); i++)
                {
                    //Se realiza la inserción de los datos en la base de datos
                    comando.Parameters.Clear();
                    comando.CommandText = "INSERT INTO cuotas (numero_cuota, valor, prestamos_id, clientes_documento, fecha_pago) VALUES (@numero_cuota, @valor, @prestamos_id, @clientes_documento, @fecha_pago)";

                    comando.Parameters.AddWithValue("@numero_cuota", i + 1);
                    comando.Parameters.AddWithValue("@valor", valorCobro);
                    comando.Parameters.AddWithValue("@prestamos_id", idPrestamo);
                    comando.Parameters.AddWithValue("@clientes_documento", clienteId);
                    comando.Parameters.AddWithValue("@fecha_pago", Convert.ToDateTime(inputfecha.Value.AddDays((i * periodoPago)+1)).ToString("yyyy-MM-dd"));

                    comando.ExecuteNonQuery();

                }

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE caja SET valor=valor - @valor";

                comando.Parameters.AddWithValue("@valor", valor);

                comando.ExecuteNonQuery();

                comando.Transaction.Commit();
                operacionCorrecta = true;
            }
            catch
            {
                comando.Transaction.Rollback();               
                operacionCorrecta = false;               
            }
            finally
            {
                comando.Connection.Close();

                if (operacionCorrecta)
                {
                    llenarGridCuotas(idPrestamo.ToString());
                    llenarHistorialPrestamos(); 
                    Mensajes.informacion("Datos agregados con éxito");
                    limpiarPantalla();

                    this.Cursor = Cursors.Default;
                    button1.Enabled = true;
                    Clientes.ListarCuotas ListarFrm = new Clientes.ListarCuotas(dataGridView2.DataSource);
                    ListarFrm.ShowDialog();
                    this.Close();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar realizar el proceso, revise su conexión a Internet.");
                    this.Cursor = Cursors.Default;
                    button1.Enabled = true;
                }                
            }
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (inputvalor.Value.Equals("0"))
            {
                Mensajes.error("El valor del prestamo no puede ser igual a 0");
                return;
            }

            if (inputdias.Value.Equals("0"))
            {
                Mensajes.error("El préstamo no puede ser a 0 días");
                return;
            }

            if (inputinteres.Text.Equals("0"))
            {
                Mensajes.error("El interes no puede ser igual a 0%");
                return;
            }

            if (inputvalorCobro.Value.Equals("0"))
            {
                Mensajes.error("El valor del prestamo no puede ser igual a 0");
                return;
            }

            if (String.IsNullOrEmpty(inputfrecuencia_cobro_id.Text))
            {
                Mensajes.error("Seleccione la frecuencia de los cobros");
                return;
            }

            string idPrestamo = "";

            try
            {
                idPrestamo = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch
            {
                Mensajes.error("Debe Seleccionar un Préstamo");
                return;
            }

            if (String.IsNullOrEmpty(inputnumero_comprobante.Text))
            {
                Mensajes.error("Debe ingresar el número del comprobante");
                return;
            }

            #endregion

            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;

            #region agregar prestamo

            string fecha = inputfecha.Value.ToString("yyyy-MM-dd");
            string dias = inputdias.Text;
            int valor = Convert.ToInt32(inputvalor.Value);
            int valorCobro = Convert.ToInt32(inputvalorCobro.Value);
            string interes = inputinteres.Value.ToString().Replace(",",".");
            string observacion = inputobservacion.Text;
            string frecuencia_cobro_id = inputfrecuencia_cobro_id.SelectedValue.ToString();
            string clientes_documento = clienteId;
            string numero_comprobante = inputnumero_comprobante.Text;

            if (Mensajes.confirmacion("¿Está seguro de que desea Insertar el registro?") == false)
            {
                this.Cursor = Cursors.Default;
                button1.Enabled = true;
                return;
            }

            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "DELETE FROM cuotas WHERE prestamos_id = @prestamos_id AND finalizada = @finalizada";

                comando.Parameters.AddWithValue("@prestamos_id", idPrestamo);
                comando.Parameters.AddWithValue("@finalizada", "0");

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE prestamos SET dias=@dias, interes=@interes, observacion=@observacion, frecuencia_cobro_id=@frecuencia_cobro_id, valor=@valor, numero_comprobante=@numero_comprobante WHERE id = @id";
                comando.Parameters.AddWithValue("@dias", dias);
                comando.Parameters.AddWithValue("@interes", interes);
                comando.Parameters.AddWithValue("@observacion", observacion);
                comando.Parameters.AddWithValue("@frecuencia_cobro_id", frecuencia_cobro_id);
                comando.Parameters.AddWithValue("@id", idPrestamo);
                comando.Parameters.AddWithValue("@valor", valor);
                comando.Parameters.AddWithValue("@numero_comprobante", numero_comprobante);

                comando.ExecuteNonQuery();

                idPrestamo = dataGridView1["id",dataGridView1.CurrentRow.Index].Value.ToString();

                int periodoPago = 0;

                if (inputfrecuencia_cobro_id.SelectedValue.Equals(1)) // diario
                {
                    periodoPago = 1;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(2)) // semanal
                {
                    periodoPago = 7;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(3)) // quincenal
                {
                    periodoPago = 15;
                }
                else if (inputfrecuencia_cobro_id.SelectedValue.Equals(4)) // mensual
                {
                    periodoPago = 30;
                }

                for (int i = 0; i < Convert.ToInt32(dias); i++)
                {
                    //Se realiza la inserción de los datos en la base de datos
                    comando.Parameters.Clear();
                    comando.CommandText = "INSERT INTO cuotas (numero_cuota, valor, prestamos_id, clientes_documento, fecha_pago) VALUES (@numero_cuota, @valor, @prestamos_id, @clientes_documento, @fecha_pago)";

                    comando.Parameters.AddWithValue("@numero_cuota", i + 1);
                    comando.Parameters.AddWithValue("@valor", valorCobro);
                    comando.Parameters.AddWithValue("@prestamos_id", idPrestamo);
                    comando.Parameters.AddWithValue("@clientes_documento", clienteId);
                    comando.Parameters.AddWithValue("@fecha_pago", Convert.ToDateTime(inputfecha.Value.AddDays(i * periodoPago)).ToString("yyyy-MM-dd"));

                    comando.ExecuteNonQuery();

                }

                comando.Transaction.Commit();
                operacionCorrecta = true;
            }
            catch
            {
                comando.Transaction.Rollback();
                operacionCorrecta = false;
            }
            finally
            {
                comando.Connection.Close();

                if (operacionCorrecta)
                {
                    llenarGridCuotas(idPrestamo.ToString());
                    llenarHistorialPrestamos();
                    Mensajes.informacion("Datos agregados con éxito");
                    limpiarPantalla();
                }
                else
                {
                    Mensajes.error("Ha ocurrido un error al intentar realizar el proceso, revise su conexión a Internet.");
                }

                this.Cursor = Cursors.Default;
                button1.Enabled = true; ;
            }

            #endregion
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Mensajes.confirmacion("Desea visualizar los datos del crédito?"))
            {
                return;
            }
            
            #region validaciones

            string idPrestamo;

            if (dataGridView1.RowCount == 0)
            {
                Mensajes.error("No hay créditos para visualizar");
                return;
            }

            try
            {
                idPrestamo = dataGridView1["id", dataGridView1.CurrentRow.Index].Value.ToString();
            }
            catch
            {
                Mensajes.error("Debe Seleccionar un crédito");
                return;
            }

            #endregion

            #region llenar datos del préstamo

            inputfecha.Value = Convert.ToDateTime(dataGridView1["fecha", dataGridView1.CurrentRow.Index].Value.ToString());
            inputvalor.Value = Convert.ToInt32(dataGridView1["valor", dataGridView1.CurrentRow.Index].Value.ToString());
            inputdias.Value = Convert.ToInt32(dataGridView1["dias", dataGridView1.CurrentRow.Index].Value.ToString());
            inputinteres.Text = Convert.ToDouble(dataGridView1["interes", dataGridView1.CurrentRow.Index].Value).ToString();
            inputfrecuencia_cobro_id.SelectedValue = dataGridView1["frecuencia_cobro_id", dataGridView1.CurrentRow.Index].Value.ToString();
            inputobservacion.Text = dataGridView1["observacion", dataGridView1.CurrentRow.Index].Value.ToString();
            inputnumero_comprobante.Text = dataGridView1["numero_comprobante", dataGridView1.CurrentRow.Index].Value.ToString();
            
            #endregion

            llenarGridCuotas(idPrestamo);
        }

        private void inputvalorCobro_ValueChanged(object sender, EventArgs e)
        {
            if (!inputvalorCobro.Value.Equals(1))
            {
                calculaInteres(1);
            }
        }

        private void calculaInteres(int operacion)
        {
            try
            {
                if (operacion.Equals(1))
                {
                    double totalCobrar = Convert.ToDouble(inputdias.Value) * Convert.ToDouble(inputvalorCobro.Value);
                    double interesCalculado = (totalCobrar / Convert.ToDouble(inputvalor.Value)) - 1;
                    inputinteres.Value = Convert.ToDecimal(Math.Round(interesCalculado * 100, 2));

                    inputrentabilidad.Text = (totalCobrar - Convert.ToDouble(inputvalor.Value)).ToString();
                }

                else if (operacion.Equals(2))
                {
                    double interes = Convert.ToDouble(inputinteres.Value.ToString()) / 100;
                    double valorTotal = Convert.ToDouble(inputvalor.Value.ToString());
                    double dias = Convert.ToDouble(inputdias.Value.ToString());

                    valorTotal = (valorTotal * interes) + valorTotal;

                    inputvalorCobro.Value = Convert.ToDecimal(valorTotal / dias);

                    inputrentabilidad.Text = (valorTotal - Convert.ToDouble(inputvalor.Value.ToString())).ToString();
                }
            }
            catch
            {
                return;
            }
            
        }

        private void inputdias_ValueChanged(object sender, EventArgs e)
        {
            if (!inputdias.Value.Equals(1))
            {
                if (!inputvalor.Value.Equals(1))
                {
                    calculaInteres(1);
                }
                else if (!inputinteres.Value.Equals(1))
                {
                    calculaInteres(2);
                }
                else
                {
                    return;
                }
            }
        }

        private void inputinteres_ValueChanged(object sender, EventArgs e)
        {
            if (!inputinteres.Value.Equals(1))
            {
                calculaInteres(2);
            }
        }

        private void inputvalor_ValueChanged(object sender, EventArgs e)
        {
            inputvalorCobro.Value = 1;
            inputdias.Value = 1;
            inputinteres.Value = 1;
        }
    }
}
