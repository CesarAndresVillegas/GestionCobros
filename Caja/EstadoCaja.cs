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
    public partial class EstadoCaja : Form
    {
        MySqlCommand comando;
        
        public EstadoCaja()
        {
            InitializeComponent();

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT valor FROM caja";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable SaldoInicial = Datos.ejecutarComandoSelect(comando);

            if (SaldoInicial.Rows.Count>0)
            {
                textBox1.Text = Convert.ToDouble(SaldoInicial.Rows[0]["valor"].ToString()).ToString("C");
            }
            else
            {
                textBox1.Text = "0";
            }            
        }
    }
}
