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

namespace GestionCobros.Rutas
{
    public partial class RegistrarSaldoCobrador : Form
    {
        MySqlCommand comando;
        private string ruta;
        private string saldoSistema;
        
        public RegistrarSaldoCobrador(string ruta, string saldoSistema)
        {
            InitializeComponent();
            llenarComboCobradores();
            // TODO: Complete member initialization
            this.ruta = ruta;
            this.saldoSistema = saldoSistema;

            llenarComboCobradores();
        }

        private void llenarComboCobradores()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, nombre FROM users WHERE tipo_usuario_id = @tipo_usuario_id AND activo = @activo ORDER BY nombre";
            comando.Parameters.AddWithValue("@tipo_usuario_id", "2");
            comando.Parameters.AddWithValue("@activo", "1");
            
            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            inputcobrador_id.DataSource = Datos.ejecutarComandoSelect(comando);
            inputcobrador_id.DisplayMember = "nombre";
            inputcobrador_id.ValueMember = "id";
            inputcobrador_id.SelectedIndex = -1;		
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            if (String.IsNullOrEmpty(inputcobrador_id.Text))
            {
                Mensajes.error("Debe seleccionar un cobrador");
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
                Mensajes.error("No esta configurada la caja, imposible realizar el cierre");
                return;
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

            string fecha = inputfecha.Value.ToString("yyyy-MM-dd");
            decimal valor_esperado = inputvalor_esperado.Value;
            decimal valor_entregado = inputvalor_entregado.Value;
            string rutas_id = ruta;
            string users_id = VGlobales.id_usuario;
            string cobrador_id = inputcobrador_id.SelectedValue.ToString();
            string observaciones = inputobservaciones.Text;
            decimal saldo = inputsaldo.Value;

            try
            {
                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO cierres_caja_diario (fecha, valor_esperado, valor_entregado, rutas_id, users_id, cobrador_id, observaciones) VALUES (@fecha, @valor_esperado, @valor_entregado, @rutas_id, @users_id, @cobrador_id, @observaciones)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@valor_esperado", valor_esperado);
                comando.Parameters.AddWithValue("@valor_entregado", valor_entregado);
                comando.Parameters.AddWithValue("@rutas_id", rutas_id);
                comando.Parameters.AddWithValue("@users_id", users_id);
                comando.Parameters.AddWithValue("@cobrador_id", cobrador_id);
                comando.Parameters.AddWithValue("@observaciones", observaciones);

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                comando.CommandText = "UPDATE caja SET valor=valor + @valor";

                comando.Parameters.AddWithValue("@valor", valor_entregado);

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO saldos_cobradores (fecha, saldo, observaciones, cobrador_id) VALUES (@fecha, @saldo, @observaciones, @cobrador_id)";

                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@saldo", saldo);
                comando.Parameters.AddWithValue("@observaciones", observaciones);
                comando.Parameters.AddWithValue("@cobrador_id", cobrador_id);

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
                    string descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: fecha = " + fecha + ", valor_esperado = " + valor_esperado + ", valor_entregado = " + valor_entregado + ", rutas_id = " + rutas_id + ", users_id = " + users_id + ", cobrador_id = " + cobrador_id + ", observaciones = " + observaciones + "";
                    Datos.crearLOG(formulario, descripcion);

                    descripcion = "ACCIÓN: modificacion en la caja; LOS DATOS DE LA ACCIÓN SON: valor = " + valor_esperado + ", fecha = " + fecha + "";
                    Datos.crearLOG(formulario, descripcion);

                    descripcion = "ACCIÓN: inserción; LOS DATOS DE LA ACCIÓN SON: fecha = " + fecha + ", saldo = " + saldo + ", observaciones = " + observaciones + ", cobrador_id = " + cobrador_id + "";
                    Datos.crearLOG(formulario, descripcion);

                    Mensajes.informacion("El ajuste se ha realizado correctamente.");
                    this.Close();
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
