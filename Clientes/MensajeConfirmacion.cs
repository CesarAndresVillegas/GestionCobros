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
    public partial class MensajeConfirmacion : Form
    {
        string documentoCliente;
        string cuotas;
        string cantidadCuotas;
        string valorPagar;

        MySqlCommand comando;

        public MensajeConfirmacion(string documentoCliente, string cuotas, string cantidadCuotas, string valorPagar)
        {
            InitializeComponent();
            this.documentoCliente = documentoCliente;
            this.cuotas = cuotas;
            this.cantidadCuotas = cantidadCuotas;
            this.valorPagar = valorPagar;

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT nombre FROM clientes WHERE documento = @documento";
            comando.Parameters.AddWithValue("@documento", documentoCliente);

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable cliente = Datos.ejecutarComandoSelect(comando);

            inputnombre_cliente.Text = cliente.Rows[0]["nombre"].ToString();
            inputcobrador.Text = VGlobales.nombre;
            inputdocumento_cliente.Text = documentoCliente;
            inputcuotas.Text = cuotas;
            inputcantidad_cuotas.Text = cantidadCuotas;
            inputvalor.Text = valorPagar;
            inputfecha.Value = DateTime.Now;
        }
    }
}
