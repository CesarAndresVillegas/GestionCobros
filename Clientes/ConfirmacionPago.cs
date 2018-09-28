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
    public partial class ConfirmacionPago : Form
    {
        private Int64 idPago;
        private string idCliente;
        private string cuotas;
        private int cantidadCuotas;
        private int valor;
        DataTable cliente;

        MySqlCommand comando;

        public ConfirmacionPago(Int64 idPago, string idCliente, string cuotas, int cantidadCuotas, int valor)
        {
            // TODO: Complete member initialization
            this.idPago = idPago;
            this.idCliente = idCliente;
            this.cuotas = cuotas;
            this.cantidadCuotas = cantidadCuotas;
            this.valor = valor;

            InitializeComponent();

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT nombre, rutas_id FROM clientes WHERE documento = @documento";
            comando.Parameters.AddWithValue("@documento", idCliente);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            cliente = Datos.ejecutarComandoSelect(comando);


            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT nombre FROM users WHERE id = @id";
            comando.Parameters.AddWithValue("@id", VGlobales.id_usuario);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable usuarios = Datos.ejecutarComandoSelect(comando);

            inputregistro.Text = idPago.ToString();
            inputfecha.Value = DateTime.Now;
            inputnombre_cliente.Text = cliente.Rows[0]["nombre"].ToString();
            inputdocumento_cliente.Text = idCliente;
            inputcobrador.Text = usuarios.Rows[0]["nombre"].ToString();
            inputcuotas.Text = cuotas;
            inputcantidad_cuotas.Text = cantidadCuotas.ToString();
            inputvalor.Text = valor.ToString();            
        }

        private void ConfirmacionPago_FormClosed(object sender, FormClosedEventArgs e)
        {
            Clientes.RegistrarPagos frm = new Clientes.RegistrarPagos(cliente.Rows[0]["rutas_id"].ToString());
            frm.Show();
        }        
    }
}
