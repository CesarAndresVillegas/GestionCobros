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

namespace GestionCobros
{
    public partial class Main : Form
    {
        MySqlCommand comando;

        public Main()
        {
            InitializeComponent();
            ping();
        }

        private void ping()
        {
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT 1+1";
            if (!Datos.ejecutarComando(comando))
            {
                Mensajes.error("No Se Logro Conexión Con La Base de Datos, Imposible Continuar");
                Mensajes.informacion("El Programa se Cerrará para Evitar Errores");
                Environment.Exit(0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Validaciones

            if (String.IsNullOrEmpty(inputuser.Text))
            {
                Mensajes.error("Debe Ingresar un Nombre de Usuario");
                return;
            }

            if (String.IsNullOrEmpty(inputpass.Text))
            {
                Mensajes.error("Debe Ingresar una Contraseña");
                return;
            }

            #endregion

            #region Ingreso

            #region Busca el usuario en tabla users y guarda el resultado de la consulta en TablaUsuarios

            DataTable TablaUsuarios = new DataTable();
            //datos de sesion
            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT id, user, pass, tipo_usuario_id, nombre, rutas_id FROM users WHERE user = @user AND pass=@pass";
            comando.Parameters.AddWithValue("@user", inputuser.Text);
            comando.Parameters.AddWithValue("@pass", inputpass.Text);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            TablaUsuarios = Datos.ejecutarComandoSelect(comando);

            #endregion

            if (TablaUsuarios.Rows.Count > 0)
            {
                VGlobales.nombre = TablaUsuarios.Rows[0]["nombre"].ToString();
                VGlobales.id_usuario = TablaUsuarios.Rows[0]["id"].ToString();
                VGlobales.tipo_usuario = TablaUsuarios.Rows[0]["tipo_usuario_id"].ToString();
                
                MenuPrincipal frm = new MenuPrincipal();
                frm.Show();
                this.Hide();
            }
            else
            {
                Mensajes.error("El nombre de usuario o la contraseña son incorrectas, favor intentelo nuevamente");
                inputuser.Focus();
                return;
            }

            #endregion
        }
    }
}
