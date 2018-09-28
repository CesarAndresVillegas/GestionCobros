namespace GestionCobros.Clientes
{
    partial class CreacionClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreacionClientes));
            this.inputnombre = new System.Windows.Forms.TextBox();
            this.labelnombre = new System.Windows.Forms.Label();
            this.inputdocumento = new System.Windows.Forms.TextBox();
            this.labeldocumento = new System.Windows.Forms.Label();
            this.inputdireccion = new System.Windows.Forms.TextBox();
            this.labeldireccion = new System.Windows.Forms.Label();
            this.inputtelefono = new System.Windows.Forms.TextBox();
            this.labeltelefono = new System.Windows.Forms.Label();
            this.labelcobradores_id = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aLMACENARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mODIFICARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eLIMINARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.eSTADOFINANCIEROToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bUSCARToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputruta_id = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.inputnombre_referencia = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inputdocumento_referencia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.inputdireccion_referencia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.inputtelefono_referencia = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.inputincumplimientos = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // inputnombre
            // 
            this.inputnombre.Location = new System.Drawing.Point(101, 96);
            this.inputnombre.Margin = new System.Windows.Forms.Padding(4);
            this.inputnombre.Name = "inputnombre";
            this.inputnombre.Size = new System.Drawing.Size(319, 24);
            this.inputnombre.TabIndex = 0;
            // 
            // labelnombre
            // 
            this.labelnombre.AutoSize = true;
            this.labelnombre.Location = new System.Drawing.Point(26, 96);
            this.labelnombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelnombre.Name = "labelnombre";
            this.labelnombre.Size = new System.Drawing.Size(75, 17);
            this.labelnombre.TabIndex = 12;
            this.labelnombre.Text = "Nombre:";
            // 
            // inputdocumento
            // 
            this.inputdocumento.Location = new System.Drawing.Point(529, 96);
            this.inputdocumento.Margin = new System.Windows.Forms.Padding(4);
            this.inputdocumento.Name = "inputdocumento";
            this.inputdocumento.Size = new System.Drawing.Size(188, 24);
            this.inputdocumento.TabIndex = 1;
            this.inputdocumento.TextChanged += new System.EventHandler(this.inputdocumento_TextChanged);
            // 
            // labeldocumento
            // 
            this.labeldocumento.AutoSize = true;
            this.labeldocumento.ForeColor = System.Drawing.Color.Red;
            this.labeldocumento.Location = new System.Drawing.Point(426, 96);
            this.labeldocumento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labeldocumento.Name = "labeldocumento";
            this.labeldocumento.Size = new System.Drawing.Size(102, 17);
            this.labeldocumento.TabIndex = 13;
            this.labeldocumento.Text = "Documento:";
            // 
            // inputdireccion
            // 
            this.inputdireccion.Location = new System.Drawing.Point(101, 130);
            this.inputdireccion.Margin = new System.Windows.Forms.Padding(4);
            this.inputdireccion.Name = "inputdireccion";
            this.inputdireccion.Size = new System.Drawing.Size(319, 24);
            this.inputdireccion.TabIndex = 3;
            // 
            // labeldireccion
            // 
            this.labeldireccion.AutoSize = true;
            this.labeldireccion.Location = new System.Drawing.Point(16, 130);
            this.labeldireccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labeldireccion.Name = "labeldireccion";
            this.labeldireccion.Size = new System.Drawing.Size(85, 17);
            this.labeldireccion.TabIndex = 15;
            this.labeldireccion.Text = "Dirección:";
            // 
            // inputtelefono
            // 
            this.inputtelefono.Location = new System.Drawing.Point(816, 96);
            this.inputtelefono.Margin = new System.Windows.Forms.Padding(4);
            this.inputtelefono.Name = "inputtelefono";
            this.inputtelefono.Size = new System.Drawing.Size(184, 24);
            this.inputtelefono.TabIndex = 2;
            // 
            // labeltelefono
            // 
            this.labeltelefono.AutoSize = true;
            this.labeltelefono.Location = new System.Drawing.Point(733, 96);
            this.labeltelefono.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labeltelefono.Name = "labeltelefono";
            this.labeltelefono.Size = new System.Drawing.Size(82, 17);
            this.labeltelefono.TabIndex = 14;
            this.labeltelefono.Text = "Teléfono:";
            // 
            // labelcobradores_id
            // 
            this.labelcobradores_id.AutoSize = true;
            this.labelcobradores_id.Location = new System.Drawing.Point(478, 130);
            this.labelcobradores_id.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelcobradores_id.Name = "labelcobradores_id";
            this.labelcobradores_id.Size = new System.Drawing.Size(50, 17);
            this.labelcobradores_id.TabIndex = 16;
            this.labelcobradores_id.Text = "Ruta:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aLMACENARToolStripMenuItem,
            this.mODIFICARToolStripMenuItem,
            this.eLIMINARToolStripMenuItem,
            this.nuevo,
            this.eSTADOFINANCIEROToolStripMenuItem,
            this.bUSCARToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1008, 26);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aLMACENARToolStripMenuItem
            // 
            this.aLMACENARToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aLMACENARToolStripMenuItem.Image")));
            this.aLMACENARToolStripMenuItem.Name = "aLMACENARToolStripMenuItem";
            this.aLMACENARToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.aLMACENARToolStripMenuItem.Text = "ALMACENAR";
            this.aLMACENARToolStripMenuItem.Click += new System.EventHandler(this.aLMACENARToolStripMenuItem_Click);
            // 
            // mODIFICARToolStripMenuItem
            // 
            this.mODIFICARToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mODIFICARToolStripMenuItem.Image")));
            this.mODIFICARToolStripMenuItem.Name = "mODIFICARToolStripMenuItem";
            this.mODIFICARToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.mODIFICARToolStripMenuItem.Text = "MODIFICAR";
            this.mODIFICARToolStripMenuItem.Click += new System.EventHandler(this.mODIFICARToolStripMenuItem_Click);
            // 
            // eLIMINARToolStripMenuItem
            // 
            this.eLIMINARToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eLIMINARToolStripMenuItem.Image")));
            this.eLIMINARToolStripMenuItem.Name = "eLIMINARToolStripMenuItem";
            this.eLIMINARToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.eLIMINARToolStripMenuItem.Text = "ELIMINAR";
            this.eLIMINARToolStripMenuItem.Click += new System.EventHandler(this.eLIMINARToolStripMenuItem_Click);
            // 
            // nuevo
            // 
            this.nuevo.Image = ((System.Drawing.Image)(resources.GetObject("nuevo.Image")));
            this.nuevo.Name = "nuevo";
            this.nuevo.Size = new System.Drawing.Size(99, 22);
            this.nuevo.Text = "NUEVO";
            this.nuevo.Click += new System.EventHandler(this.nuevo_Click);
            // 
            // eSTADOFINANCIEROToolStripMenuItem
            // 
            this.eSTADOFINANCIEROToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.eSTADOFINANCIEROToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eSTADOFINANCIEROToolStripMenuItem.Image")));
            this.eSTADOFINANCIEROToolStripMenuItem.Name = "eSTADOFINANCIEROToolStripMenuItem";
            this.eSTADOFINANCIEROToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.eSTADOFINANCIEROToolStripMenuItem.Text = "ESTADO FINANCIERO";
            this.eSTADOFINANCIEROToolStripMenuItem.Click += new System.EventHandler(this.eSTADOFINANCIEROToolStripMenuItem_Click);
            // 
            // bUSCARToolStripMenuItem
            // 
            this.bUSCARToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bUSCARToolStripMenuItem.Image")));
            this.bUSCARToolStripMenuItem.Name = "bUSCARToolStripMenuItem";
            this.bUSCARToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.bUSCARToolStripMenuItem.Text = "BUSCAR";
            this.bUSCARToolStripMenuItem.Click += new System.EventHandler(this.bUSCARToolStripMenuItem_Click);
            // 
            // inputruta_id
            // 
            this.inputruta_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.inputruta_id.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.inputruta_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputruta_id.FormattingEnabled = true;
            this.inputruta_id.Location = new System.Drawing.Point(529, 130);
            this.inputruta_id.Name = "inputruta_id";
            this.inputruta_id.Size = new System.Drawing.Size(188, 24);
            this.inputruta_id.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 370);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(950, 277);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(28, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(950, 28);
            this.label2.TabIndex = 23;
            this.label2.Text = "CLIENTES CONFIGURADOS";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(4, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 10);
            this.panel1.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 256);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Nombre:";
            // 
            // inputnombre_referencia
            // 
            this.inputnombre_referencia.Location = new System.Drawing.Point(100, 256);
            this.inputnombre_referencia.Margin = new System.Windows.Forms.Padding(4);
            this.inputnombre_referencia.Name = "inputnombre_referencia";
            this.inputnombre_referencia.Size = new System.Drawing.Size(337, 24);
            this.inputnombre_referencia.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(449, 256);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "Documento:";
            // 
            // inputdocumento_referencia
            // 
            this.inputdocumento_referencia.Location = new System.Drawing.Point(552, 256);
            this.inputdocumento_referencia.Margin = new System.Windows.Forms.Padding(4);
            this.inputdocumento_referencia.Name = "inputdocumento_referencia";
            this.inputdocumento_referencia.Size = new System.Drawing.Size(188, 24);
            this.inputdocumento_referencia.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 290);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Dirección:";
            // 
            // inputdireccion_referencia
            // 
            this.inputdireccion_referencia.Location = new System.Drawing.Point(100, 290);
            this.inputdireccion_referencia.Margin = new System.Windows.Forms.Padding(4);
            this.inputdireccion_referencia.Name = "inputdireccion_referencia";
            this.inputdireccion_referencia.Size = new System.Drawing.Size(337, 24);
            this.inputdireccion_referencia.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(748, 256);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Teléfono:";
            // 
            // inputtelefono_referencia
            // 
            this.inputtelefono_referencia.Location = new System.Drawing.Point(831, 256);
            this.inputtelefono_referencia.Margin = new System.Windows.Forms.Padding(4);
            this.inputtelefono_referencia.Name = "inputtelefono_referencia";
            this.inputtelefono_referencia.Size = new System.Drawing.Size(163, 24);
            this.inputtelefono_referencia.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 13F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label6.Location = new System.Drawing.Point(47, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 22);
            this.label6.TabIndex = 11;
            this.label6.Text = "DATOS DEL CLIENTE:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 13F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))));
            this.label7.Location = new System.Drawing.Point(47, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(258, 22);
            this.label7.TabIndex = 18;
            this.label7.Text = "REFERENCIA PERSONAL:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(550, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(241, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Cantidad de Incumplimientos:";
            // 
            // inputincumplimientos
            // 
            this.inputincumplimientos.Location = new System.Drawing.Point(789, 44);
            this.inputincumplimientos.Name = "inputincumplimientos";
            this.inputincumplimientos.ReadOnly = true;
            this.inputincumplimientos.Size = new System.Drawing.Size(39, 24);
            this.inputincumplimientos.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(834, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 26);
            this.button1.TabIndex = 26;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CreacionClientes
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inputincumplimientos);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputnombre_referencia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputdocumento_referencia);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputdireccion_referencia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inputtelefono_referencia);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputruta_id);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelnombre);
            this.Controls.Add(this.inputnombre);
            this.Controls.Add(this.labeldocumento);
            this.Controls.Add(this.inputdocumento);
            this.Controls.Add(this.labeldireccion);
            this.Controls.Add(this.inputdireccion);
            this.Controls.Add(this.labeltelefono);
            this.Controls.Add(this.inputtelefono);
            this.Controls.Add(this.labelcobradores_id);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 700);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 700);
            this.Name = "CreacionClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INFORMACIÓN DE CLIENTES";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputnombre;
        private System.Windows.Forms.Label labelnombre;
        private System.Windows.Forms.TextBox inputdocumento;
        private System.Windows.Forms.Label labeldocumento;
        private System.Windows.Forms.TextBox inputdireccion;
        private System.Windows.Forms.Label labeldireccion;
        private System.Windows.Forms.TextBox inputtelefono;
        private System.Windows.Forms.Label labeltelefono;
        private System.Windows.Forms.Label labelcobradores_id;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aLMACENARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mODIFICARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eLIMINARToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevo;
        private System.Windows.Forms.ComboBox inputruta_id;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem eSTADOFINANCIEROToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputnombre_referencia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inputdocumento_referencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputdireccion_referencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox inputtelefono_referencia;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox inputincumplimientos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem bUSCARToolStripMenuItem;
    }
}