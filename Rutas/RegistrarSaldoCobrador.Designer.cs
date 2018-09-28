namespace GestionCobros.Rutas
{
    partial class RegistrarSaldoCobrador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarSaldoCobrador));
            this.label2 = new System.Windows.Forms.Label();
            this.inputcobrador_id = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.inputvalor_entregado = new System.Windows.Forms.NumericUpDown();
            this.inputfecha = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputobservaciones = new System.Windows.Forms.RichTextBox();
            this.inputvalor_esperado = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.inputsaldo = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.inputvalor_entregado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputvalor_esperado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputsaldo)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cobrador:";
            // 
            // inputcobrador_id
            // 
            this.inputcobrador_id.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.inputcobrador_id.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.inputcobrador_id.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputcobrador_id.DropDownWidth = 500;
            this.inputcobrador_id.FormattingEnabled = true;
            this.inputcobrador_id.Location = new System.Drawing.Point(484, 87);
            this.inputcobrador_id.Name = "inputcobrador_id";
            this.inputcobrador_id.Size = new System.Drawing.Size(263, 24);
            this.inputcobrador_id.TabIndex = 2;
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
            this.button1.Location = new System.Drawing.Point(276, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(234, 54);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inputvalor_entregado
            // 
            this.inputvalor_entregado.Location = new System.Drawing.Point(174, 134);
            this.inputvalor_entregado.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.inputvalor_entregado.Name = "inputvalor_entregado";
            this.inputvalor_entregado.Size = new System.Drawing.Size(206, 24);
            this.inputvalor_entregado.TabIndex = 3;
            // 
            // inputfecha
            // 
            this.inputfecha.Location = new System.Drawing.Point(174, 41);
            this.inputfecha.Name = "inputfecha";
            this.inputfecha.Size = new System.Drawing.Size(344, 24);
            this.inputfecha.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Fecha:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Recaudo esperado:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Recaudo real:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Observaciones:";
            // 
            // inputobservaciones
            // 
            this.inputobservaciones.Location = new System.Drawing.Point(173, 179);
            this.inputobservaciones.MaxLength = 500;
            this.inputobservaciones.Name = "inputobservaciones";
            this.inputobservaciones.Size = new System.Drawing.Size(574, 46);
            this.inputobservaciones.TabIndex = 4;
            this.inputobservaciones.Text = "";
            // 
            // inputvalor_esperado
            // 
            this.inputvalor_esperado.Enabled = false;
            this.inputvalor_esperado.Location = new System.Drawing.Point(174, 87);
            this.inputvalor_esperado.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.inputvalor_esperado.Name = "inputvalor_esperado";
            this.inputvalor_esperado.Size = new System.Drawing.Size(206, 24);
            this.inputvalor_esperado.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Saldo cobrador:";
            // 
            // inputsaldo
            // 
            this.inputsaldo.Location = new System.Drawing.Point(532, 134);
            this.inputsaldo.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.inputsaldo.Name = "inputsaldo";
            this.inputsaldo.Size = new System.Drawing.Size(215, 24);
            this.inputsaldo.TabIndex = 12;
            // 
            // RegistrarSaldoCobrador
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 348);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.inputsaldo);
            this.Controls.Add(this.inputvalor_esperado);
            this.Controls.Add(this.inputobservaciones);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.inputfecha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.inputvalor_entregado);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inputcobrador_id);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(686, 191);
            this.Name = "RegistrarSaldoCobrador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REGISTRAR SALDO DEL COBRADOR";
            ((System.ComponentModel.ISupportInitialize)(this.inputvalor_entregado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputvalor_esperado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputsaldo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox inputcobrador_id;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown inputvalor_entregado;
        private System.Windows.Forms.DateTimePicker inputfecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox inputobservaciones;
        private System.Windows.Forms.NumericUpDown inputvalor_esperado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown inputsaldo;
    }
}