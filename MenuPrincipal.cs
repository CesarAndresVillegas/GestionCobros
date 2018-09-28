using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionCobros
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            label1.Text = DateTime.Now.ToString("dddd - dd MMMM - yyyy");
            label2.Text = VGlobales.nombre;
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1"))
            {
                Configuracion.Usuarios frm = new Configuracion.Usuarios();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }            
        }

        private void rutasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1"))
            {
                Configuracion.Rutas frm = new Configuracion.Rutas();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }            
        }

        private void manejoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Clientes.CreacionClientes frm = new Clientes.CreacionClientes();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void registrarPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("2"))
            {
                Clientes.RegistrarPagos frm = new Clientes.RegistrarPagos();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void verPagosAnterioresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Clientes.ConsultarRegistrosPago frm = new Clientes.ConsultarRegistrosPago();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void consultarRutasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Rutas.ConsultarRutas frm = new Rutas.ConsultarRutas();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void cierreDelDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Rutas.RegistrarCumplimiento frm = new Rutas.RegistrarCumplimiento();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void visualizarRecorridoEnMapaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mensajes.informacion("Esta opción aún no está disponible en esta versión...");
            return;
        }

        private void historialDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Informes.HistorialClientes frm = new Informes.HistorialClientes();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void historialDeRecaudosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Informes.HistorialRecaudos frm = new Informes.HistorialRecaudos();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }              
        }

        private void clientesEnMoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Informes.ClientesMora frm = new Informes.ClientesMora();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }
        }

        private void saldosPendientesDeCobradoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1") || VGlobales.tipo_usuario.Equals("3"))
            {
                Rutas.SaldosCobradores frm = new Rutas.SaldosCobradores();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }            
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nash-ti.com/");
        }

        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cambioDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VGlobales.tipo_usuario.Equals("1"))
            {
                Clientes.ModificarDocumentoClientes frm = new Clientes.ModificarDocumentoClientes();
                frm.Show();
            }
            else
            {
                Mensajes.error("El usuario no tiene autorización para acceder a esta opción");
                return;
            }
        }

        private void saldoInicialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caja.SaldoInicial frm = new Caja.SaldoInicial();
            frm.Show();
        }

        private void estadoDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caja.EstadoCaja frm = new Caja.EstadoCaja();
            frm.Show();
        }

        private void ajustesDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caja.AjustesCaja frm = new Caja.AjustesCaja();
            frm.Show();
        }

        private void movimientosDeCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caja.MovimientosDeCaja frm = new Caja.MovimientosDeCaja();
            frm.Show();
        }

        private void generarArchivoConLasRutasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
