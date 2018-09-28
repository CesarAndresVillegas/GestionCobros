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
    public partial class MovimientosDeCaja : Form
    {
        MySqlCommand comando;
        
        public MovimientosDeCaja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region validaciones

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "SELECT valor FROM caja";

            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            DataTable caja = Datos.ejecutarComandoSelect(comando);

            if (caja.Rows.Count == 0)
            {
                Mensajes.error("No esta configurada la caja, imposible realizar consultas");
                return;
            }

            #endregion

            #region calculos

            comando = Datos.crearComando();

            comando.Parameters.Clear();
            comando.CommandText = "(SELECT c.fecha, 'RECAUDO' AS motivo, CONCAT(' Cobrador: ', u.nombre, ' - Ruta: ', r.ruta, ' - Observaciones: ', c.observaciones) AS detalle, c.valor_entregado AS valor FROM cierres_caja_diario c JOIN rutas r ON c.rutas_id = r.id JOIN users u ON u.id = c.cobrador_id WHERE c.fecha BETWEEN @fecha1 AND @fecha2) " +
                                    " UNION " +
                                    " (SELECT fecha, 'PRESTAMO' AS motivo, CONCAT(' Documento del cliente: ', clientes_documento, ' -  Observación: ',observacion) AS detalle, valor*-1 AS valor FROM prestamos WHERE fecha BETWEEN @fecha1 AND @fecha2) " +
                                    " UNION " +
                                    " (SELECT fecha, 'AJUSTES', CONCAT(cast(motivo as char),' ') AS detalle, valor FROM ajustes_caja WHERE fecha BETWEEN @fecha1 AND @fecha2) " +
                                    " ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1",fechaInicial.Value.ToString("yyyy-MM-dd"));
            comando.Parameters.AddWithValue("@fecha2", fechaFinal.Value.ToString("yyyy-MM-dd"));
            
            // Al ejecutar la consulta se devuelve un DataTable.
            // -- 
            dataGridView1.DataSource = Datos.ejecutarComandoSelect(comando);

            dataGridView1.Columns[0].HeaderText = "Fecha";
            dataGridView1.Columns[1].HeaderText = "Motivo";
            dataGridView1.Columns[2].HeaderText = "Detalles";
            dataGridView1.Columns[3].HeaderText = "Valor";

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            #endregion

            if (dataGridView1.RowCount>0)
            {
                if (Mensajes.confirmacion("Desea generar un archivo de excel con la información?"))
                {
                    Datos.ConvertirExcel(dataGridView1);
                }
            }
        }
    }
}
