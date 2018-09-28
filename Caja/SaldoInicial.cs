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
    public partial class SaldoInicial : Form
    {
        MySqlCommand comando;

        public SaldoInicial()
        {
            InitializeComponent();

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT fecha, valor FROM saldo_inicial";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable SaldoInicial = Datos.ejecutarComandoSelect(comando);

            if (SaldoInicial.Rows.Count>0)
            {
                inputvalor.Value = Convert.ToDecimal(SaldoInicial.Rows[0]["valor"].ToString());
                inputvalor.Enabled = false;
                label2.Text = "Fecha del saldo: " + SaldoInicial.Rows[0]["fecha"].ToString();
            }
            else
            {
                label2.Text = "--";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (inputvalor.Value.Equals(0))
            {
                Mensajes.error("Ingrese un valor de saldo inicial");
                return;
            }

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT fecha, valor FROM saldo_inicial";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable SaldoInicial = Datos.ejecutarComandoSelect(comando);

            if (SaldoInicial.Rows.Count > 0)
            {
                Mensajes.error("El saldo inicial ya se encuentra configurado");
                return;
            }

            #endregion

            if (Mensajes.confirmacion("¿Está seguro de que desea Insertar el registro?") == false)
            {
                return;
            }

            comando = Datos.crearComando();
            comando.Connection.Open();
            MySqlTransaction transaccion = comando.Connection.BeginTransaction();
            comando.Transaction = transaccion;
            bool operacionCorrecta = false;

            string fecha = DateTime.Now.ToString("yyyy-MM-dd");
            string valor = inputvalor.Text;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO saldo_inicial (fecha, valor) VALUES (@fecha, @valor)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@valor", valor);

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO caja (valor) VALUES (@valor)";

                comando.Parameters.AddWithValue("@valor", valor);

                comando.ExecuteNonQuery();

                comando.Transaction.Commit();

                operacionCorrecta = true;
            }

            catch
            {
                comando.Transaction.Rollback();
            }
            finally
            {
                comando.Connection.Close();
                
                if (operacionCorrecta)
                {
                    string formulario = this.Name;
                    string descripcion = "ACCIÓN: creación de saldos iniciales; LOS DATOS DE LA ACCIÓN SON: fecha = " + fecha + ", valor = " + valor + "";
                    Datos.crearLOG(formulario, descripcion);

                    descripcion = "ACCIÓN: inserción en la caja; LOS DATOS DE LA ACCIÓN SON: valor = " + valor + "";
                    Datos.crearLOG(formulario, descripcion);

                    Mensajes.informacion("La inserción se ha realizado correctamente.");

                    inputvalor.Enabled = false;
                    label2.Text = "Fecha del saldo: " + DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    Mensajes.error("Ocurrió un error en el proceso, imposible crear los saldos iniciales");
                }
                
            }
        }
    }
}
