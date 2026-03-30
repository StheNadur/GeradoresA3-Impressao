using System;
using System.IO;
using System.Windows.Forms;
 
namespace GeradoresA3_Impressao
{
    public partial class GeradoresA3 : Form
    {
        private string _caminhoBoleto = string.Empty;
        private string _caminhoCadastro = string.Empty;
        private string _caminhoPresta = string.Empty;

        public GeradoresA3()
        {
            InitializeComponent();
            btnPDFCadastro.Enabled = false;
            btnPDFPrestacao.Enabled = false;
            btnProcessar.Enabled = false;
        }

        private void btnPDFBoleto_Click(object sender, EventArgs e)
        {
            string caminho = AbrirDialogoPDF("Selecione o PDF de Boleto");
            if (string.IsNullOrEmpty(caminho)) return;

            _caminhoBoleto = caminho;
            textBox1.Text = Path.GetFileName(caminho);

            ResetarApartirDe("boleto");

            btnPDFCadastro.Enabled = true;
            lblStatus.Text = "Boleto selecionado. Agora selecione o Cadastro.";
        }

        private void btnPDFCadastro_Click(object sender, EventArgs e)
        {
            string caminho = AbrirDialogoPDF("Selecione o PDF de Cadastro");
            if (string.IsNullOrEmpty(caminho)) return;

            _caminhoCadastro = caminho;
            textBox3.Text = Path.GetFileName(caminho);

            ResetarApartirDe("cadastro");

            btnPDFPrestacao.Enabled = true;
            btnProcessar.Enabled = true;
            lblStatus.Text = "Cadastro selecionado. Selecione a Prestação (opcional) ou clique em Processar.";
        }

        private void btnPDFPrestacao_Click(object sender, EventArgs e)
        {
            string caminho = AbrirDialogoPDF("Selecione o PDF de Prestação");
            if (string.IsNullOrEmpty(caminho)) return;

            _caminhoPresta = caminho;
            textBox2.Text = Path.GetFileName(caminho);

            lblStatus.Text = "Prestação selecionada. Clique em Processar quando estiver pronto.";
        }

        private void btnProcessar_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Processando... aguarde.";
                btnProcessar.Enabled = false;

                var processor = new PdfProcessador();
                string arquivoGerado = processor.Processar(
                    _caminhoBoleto,
                    _caminhoCadastro,
                    string.IsNullOrEmpty(_caminhoPresta) ? null : _caminhoPresta
                );

                lblStatus.Text = $"✔ PDF gerado com sucesso!";
                System.Diagnostics.Process.Start("explorer.exe", Path.GetDirectoryName(arquivoGerado)!);
            }

            catch (Exception ex)
            {
                lblStatus.Text = $"✘ Erro: {ex.Message}";
                MessageBox.Show($"Ocorreu um erro:\n\n{ex.Message}", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnProcessar.Enabled = true;
            }
        }

        private string AbrirDialogoPDF(string titulo)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = titulo,
                Filter = "Arquivos PDF|*.pdf"
            };
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : string.Empty;
        }

        private void ResetarApartirDe(string ponto)
        {
            if (ponto == "boleto")
            {
                _caminhoCadastro = string.Empty;
                textBox3.Text = string.Empty;
                btnPDFCadastro.Enabled = false;

                _caminhoPresta = string.Empty;
                textBox2.Text = string.Empty;
                btnPDFPrestacao.Enabled = false;

                btnProcessar.Enabled = false;
            }

            if (ponto == "cadastro")
            {
                _caminhoPresta = string.Empty;
                textBox2.Text = string.Empty;
                btnPDFPrestacao.Enabled = false;
                btnProcessar.Enabled = false;
            }
        }
    }
}