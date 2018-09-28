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
    public partial class RegistrarPagosDetalle : Form
    {
        private string idPrestamo;
        MySqlCommand comando;
        DataTable clientes;
        
        public RegistrarPagosDetalle(string idPrestamo)
        {
            // TODO: Complete member initialization
            this.idPrestamo = idPrestamo;
            InitializeComponent();

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT c.documento, c.nombre, c.direccion, c.telefono, c.rutas_id FROM prestamos p JOIN clientes c ON p.clientes_documento = c.documento WHERE p.id = @id;";
            comando.Parameters.AddWithValue("@id", idPrestamo);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            clientes = Datos.ejecutarComandoSelect(comando);

            label2.Text = "Documento: " + clientes.Rows[0]["documento"].ToString();
            label3.Text = "Nombre: " + clientes.Rows[0]["nombre"].ToString();
		
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clientes.RegistrarPagoDetalleGrid frm = new Clientes.RegistrarPagoDetalleGrid(idPrestamo, clientes.Rows[0]["nombre"].ToString());
            frm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Mensajes.confirmacion("Desea registrar el incumplimiento"))
            {
                comando = Datos.crearComando();

                comando.Parameters.Clear();
                comando.CommandText = "INSERT INTO incumplimientos (fecha, clientes_documentos) VALUES (@fecha, @clientes_documentos)";
                comando.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@clientes_documentos", clientes.Rows[0]["documento"].ToString());

                // Ejecutar la consulta y decidir
                // True: caso exitoso
                // false: Error.
                if (Datos.ejecutarComando(comando))
                {
                    // TODO: OPERACIÓN A REALIZAR EN CASO DE ÉXITO.
                    Mensajes.informacion("Incumplimiento registrado");
                    this.Close();
                    Clientes.RegistrarPagos frm = new Clientes.RegistrarPagos();
                    frm.Show();
                }
                else
                { Mensajes.error("Ocurrió un error en el procesos, imposible registrar"); }
		
            }
        }
    }
}
