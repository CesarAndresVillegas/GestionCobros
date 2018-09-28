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
    public partial class ListarCuotas : Form
    {
        MySqlCommand comando;
        private object dataSource;

        public ListarCuotas(object dataSource)
        {
            InitializeComponent();
            this.dataSource = dataSource;

            dataGridView1.DataSource = dataSource;
        }
    }
}
