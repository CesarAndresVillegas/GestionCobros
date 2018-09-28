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
    public partial class AjustesCaja : Form
    {
        MySqlCommand comando;

        public AjustesCaja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (inputvalor.Value.Equals(0))
            {
                Mensajes.error("Ingrese el valor que desea ajustar");
                return;
            }

            if (String.IsNullOrEmpty(inputmotivo.Text))
            {
                Mensajes.error("Ingrese el motivo del ajuste");
                return;
            }

            if (inputvalor.Value < 0)
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "SELECT valor FROM caja";

                // Al ejecutar la consulta se devuelve un DataTable.
                // -- 
                DataTable caja = Datos.ejecutarComandoSelect(comando);

                if (caja.Rows.Count==0)
                {
                    Mensajes.error("No esta configurada la caja, imposible realizar ajuste");
                    return;
                }

                if (Convert.ToDecimal(caja.Rows[0]["valor"].ToString())<(inputvalor.Value*-1))
                {
                    Mensajes.error("No se puede sacar esa cantidad de la caja, es mayor al saldo actual");
                    return;
                }
            }

            #endregion

            #region transaccion

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
            string valor = inputvalor.Value.ToString();
            string motivo = inputmotivo.Text;
            string users_id = VGlobales.id_usuario;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO ajustes_caja (fecha, valor, motivo, users_id) VALUES (@fecha, @valor, @motivo, @users_id)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@valor", valor);
                comando.Parameters.AddWithValue("@motivo", motivo);
                comando.Parameters.AddWithValue("@users_id", users_id);

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE caja SET valor=valor + @valor";

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
                    string descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: fecha = " + fecha + ", valor = " + valor + ", motivo = " + motivo + ", users_id = " + users_id + "";
                    Datos.crearLOG(formulario, descripcion);

                    descripcion = "ACCIÓN: modificacion en la caja; LOS DATOS DE LA ACCIÓN SON: valor = " + valor + ", fecha = " + fecha + "";
                    Datos.crearLOG(formulario, descripcion);

                    Mensajes.informacion("El ajuste se ha realizado correctamente.");
                }
                else
                {
                    Mensajes.error("Ocurrió un error en el proceso, imposible crear los saldos iniciales");
                }

            }

            #endregion
        }
    }
}
