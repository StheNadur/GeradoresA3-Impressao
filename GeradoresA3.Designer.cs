namespace GeradoresA3_Impressao
{
    partial class GeradoresA3
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
            btnPDFBoleto = new Button();
            btnPDFPrestacao = new Button();
            btnPDFCadastro = new Button();
            btnProcessar = new Button();
            lblBoleto = new Label();
            lblPrestacao = new Label();
            lblCadastro = new Label();
            lblStatus = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            SuspendLayout();
            // 
            // btnPDFBoleto
            // 
            btnPDFBoleto.Location = new Point(512, 46);
            btnPDFBoleto.Name = "btnPDFBoleto";
            btnPDFBoleto.Size = new Size(105, 23);
            btnPDFBoleto.TabIndex = 1;
            btnPDFBoleto.Text = "PDF Boleto";
            btnPDFBoleto.UseVisualStyleBackColor = true;
            // 
            // btnPDFPrestacao
            // 
            btnPDFPrestacao.Enabled = false;
            btnPDFPrestacao.Location = new Point(512, 161);
            btnPDFPrestacao.Name = "btnPDFPrestacao";
            btnPDFPrestacao.Size = new Size(105, 23);
            btnPDFPrestacao.TabIndex = 2;
            btnPDFPrestacao.Text = "PDF Prestação";
            btnPDFPrestacao.UseVisualStyleBackColor = true;
            // 
            // btnPDFCadastro
            // 
            btnPDFCadastro.Enabled = false;
            btnPDFCadastro.Location = new Point(512, 100);
            btnPDFCadastro.Name = "btnPDFCadastro";
            btnPDFCadastro.Size = new Size(105, 23);
            btnPDFCadastro.TabIndex = 3;
            btnPDFCadastro.Text = "PDF Cadastro";
            btnPDFCadastro.UseVisualStyleBackColor = true;
            // 
            // btnProcessar
            // 
            btnProcessar.Enabled = false;
            btnProcessar.Location = new Point(512, 225);
            btnProcessar.Name = "btnProcessar";
            btnProcessar.Size = new Size(105, 23);
            btnProcessar.TabIndex = 4;
            btnProcessar.Text = "Processar";
            btnProcessar.UseVisualStyleBackColor = true;
            // 
            // lblBoleto
            // 
            lblBoleto.AutoSize = true;
            lblBoleto.Location = new Point(19, 50);
            lblBoleto.Name = "lblBoleto";
            lblBoleto.Size = new Size(41, 15);
            lblBoleto.TabIndex = 5;
            lblBoleto.Text = "Boleto";
            lblBoleto.Click += this.lblBoleto_Click;
            // 
            // lblPrestacao
            // 
            lblPrestacao.AutoSize = true;
            lblPrestacao.Location = new Point(16, 165);
            lblPrestacao.Name = "lblPrestacao";
            lblPrestacao.Size = new Size(58, 15);
            lblPrestacao.TabIndex = 6;
            lblPrestacao.Text = "Prestação";
            lblPrestacao.Click += this.lblPrestacao_Click;
            // 
            // lblCadastro
            // 
            lblCadastro.AutoSize = true;
            lblCadastro.Location = new Point(17, 105);
            lblCadastro.Name = "lblCadastro";
            lblCadastro.Size = new Size(54, 15);
            lblCadastro.TabIndex = 7;
            lblCadastro.Text = "Cadastro";
            lblCadastro.Click += this.lblCadastro_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(512, 262);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(106, 15);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status do Processo";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(84, 46);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(404, 23);
            textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(84, 161);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(404, 23);
            textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(84, 101);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(404, 23);
            textBox3.TabIndex = 11;
            // 
            // GeradoresA3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 304);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(lblStatus);
            Controls.Add(lblCadastro);
            Controls.Add(lblPrestacao);
            Controls.Add(lblBoleto);
            Controls.Add(btnProcessar);
            Controls.Add(btnPDFCadastro);
            Controls.Add(btnPDFPrestacao);
            Controls.Add(btnPDFBoleto);
            Name = "GeradoresA3";
            Text = "Gerador A3 - Impressão";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPDFBoleto;
        private Button btnPDFPrestacao;
        private Button btnPDFCadastro;
        private Button btnProcessar;
        private Label lblBoleto;
        private Label lblPrestacao;
        private Label lblCadastro;
        private Label lblStatus;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
    }
}