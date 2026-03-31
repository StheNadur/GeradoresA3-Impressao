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
            string caminho = AbrirDialogoPDF("Selecione o PDF do Boleto.");
            if (string.IsNullOrEmpty(caminho)) return;

            if (!IsPdfValido(caminho))
            {
                MessageBox.Show("O arquivo selecionado não é um PDF válido.",
                                "Arquivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _caminhoBoleto = caminho;
            textBox1.Text = Path.GetFileName(caminho);
            ResetarApartirDe("boleto");
            btnPDFCadastro.Enabled = true;
            lblStatus.Text = "Boleto selecionado. Agora selecione o PDF de Cadastro.";
        }

        private void btnPDFCadastro_Click(object sender, EventArgs e)
        {
            string caminho = AbrirDialogoPDF("Selecione o PDF de Cadastro.");
            if (string.IsNullOrEmpty(caminho)) return;

            if (!IsPdfValido(caminho))
            {
                MessageBox.Show("O arquivo selecionado não é um PDF válido.",
                                "Arquivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (caminho == _caminhoBoleto)
            {
                MessageBox.Show("Este arquivo já foi selecionado como Boleto.\nEscolha um arquivo diferente.",
                                "Arquivo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _caminhoCadastro = caminho;
            textBox3.Text = Path.GetFileName(caminho);
            ResetarApartirDe("cadastro");
            btnPDFPrestacao.Enabled = true;
            btnProcessar.Enabled = true;
            lblStatus.Text = "Cadastro selecionado. Selecione a Prestação (opcional)\nou clique em Processar.";
        }
        private void btnPDFPrestacao_Click(object sender, EventArgs e)
        {
            string caminho = AbrirDialogoPDF("Selecione o PDF de Prestação");
            if (string.IsNullOrEmpty(caminho)) return;

            if (!IsPdfValido(caminho))
            {
                MessageBox.Show("O arquivo selecionado não é um PDF válido.",
                                "Arquivo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (caminho == _caminhoBoleto || caminho == _caminhoCadastro)
            {
                MessageBox.Show("Este arquivo já foi selecionado em outro campo.\nEscolha um arquivo diferente.",
                                "Arquivo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _caminhoPresta = caminho;
            textBox2.Text = Path.GetFileName(caminho);
            lblStatus.Text = "Prestação selecionada.\nConfira os arquivos e clique em 'Processar'.";
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
                ResetarTudo();
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
        private void ResetarTudo()
        {
            _caminhoBoleto = string.Empty;
            _caminhoCadastro = string.Empty;
            _caminhoPresta = string.Empty;

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;

            btnPDFCadastro.Enabled = false;
            btnPDFPrestacao.Enabled = false;
            btnProcessar.Enabled = false;
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
        private bool IsPdfValido(string caminho)
        {
            try
            {
                byte[] header = new byte[4];
                using FileStream fs = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                fs.Read(header, 0, 4);
                return header[0] == 0x25 && header[1] == 0x50 &&
                       header[2] == 0x44 && header[3] == 0x46;
            }
            catch
            {
                return false;
            }
        }
    }
}