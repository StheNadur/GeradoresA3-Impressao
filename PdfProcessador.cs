using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace GeradoresA3_Impressao
{
    public class PdfProcessador
    {      
        public string Processar(string caminhoBoleto, string caminhoCadastro, string? caminhoPresta = null)
        {
            PdfDocument pdfBoleto = PdfReader.Open(caminhoBoleto, PdfDocumentOpenMode.Import);
            PdfDocument pdfCadastro = PdfReader.Open(caminhoCadastro, PdfDocumentOpenMode.Import);

            if (pdfBoleto.PageCount != pdfCadastro.PageCount)
                throw new Exception("Boleto e Cadastro têm número de páginas diferentes!");

            int totalPaginas = pdfBoleto.PageCount;

            int paginasPresta = 0;
            PdfDocument? pdfPresta = null;

            if (!string.IsNullOrEmpty(caminhoPresta))
            {
                pdfPresta = PdfReader.Open(caminhoPresta, PdfDocumentOpenMode.Import);
                paginasPresta = pdfPresta.PageCount;

                if (paginasPresta < 1 || paginasPresta > 2)
                    throw new Exception("PDF de Prestação deve ter 1 ou 2 páginas!");
            }

            PdfDocument pdfSaida = new PdfDocument();

            if (pdfPresta == null)
            {
                for (int i = 0; i < totalPaginas; i++)
                {
                    AdicionarPaginaA4(pdfSaida, caminhoBoleto, i);
                    AdicionarPaginaA4(pdfSaida, caminhoCadastro, i);
                }
            }
            else
            {
                for (int i = 0; i < totalPaginas; i++)
                {
                    AdicionarPaginaA3(
                        pdfSaida,
                        caminhoEsquerdo: caminhoBoleto, indiceEsquerdo: i,
                        caminhoDireito: caminhoPresta!, indiceDireito: 0);

                    if (paginasPresta == 2)
                    {
                        AdicionarPaginaA3(
                            pdfSaida,
                            caminhoEsquerdo: caminhoPresta!, indiceEsquerdo: 1,
                            caminhoDireito: caminhoCadastro, indiceDireito: i);
                    }
                    else
                    {
                        AdicionarPaginaA3Direita(
                            pdfSaida,
                            caminhoDireito: caminhoCadastro, indiceDireito: i);
                    }
                }
            }

            string pastaPai = Path.GetDirectoryName(Path.GetDirectoryName(caminhoBoleto))!;
            string pastaOutput = Path.Combine(pastaPai, "PDF_A3");

            if (!Directory.Exists(pastaOutput))
                Directory.CreateDirectory(pastaOutput);

            string nomeArquivo = $"PDF_A3_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string caminhoFinal = Path.Combine(pastaOutput, nomeArquivo);

            pdfSaida.Save(caminhoFinal);
            return caminhoFinal;
        }

        private void AdicionarPaginaA3(PdfDocument destino,
                                        string caminhoEsquerdo, int indiceEsquerdo,
                                        string caminhoDireito, int indiceDireito)
        {
            PdfPage pagina = destino.AddPage();
            pagina.Width = XUnit.FromMillimeter(420);
            pagina.Height = XUnit.FromMillimeter(297);

            using XGraphics gfx = XGraphics.FromPdfPage(pagina);

            double metade = pagina.Width.Point / 2;
            double altura = pagina.Height.Point;
            double largura = metade;

            XPdfForm formEsq = XPdfForm.FromFile(caminhoEsquerdo);
            formEsq.PageIndex = indiceEsquerdo;
            gfx.DrawImage(formEsq, new XRect(0, 0, largura, altura));

            XPdfForm formDir = XPdfForm.FromFile(caminhoDireito);
            formDir.PageIndex = indiceDireito;
            gfx.DrawImage(formDir, new XRect(metade, 0, largura, altura));
        }

        private void AdicionarPaginaA3Direita(PdfDocument destino,
                                               string caminhoDireito, int indiceDireito)
        {
            PdfPage pagina = destino.AddPage();
            pagina.Width = XUnit.FromMillimeter(420);
            pagina.Height = XUnit.FromMillimeter(297);

            using XGraphics gfx = XGraphics.FromPdfPage(pagina);

            double metade = pagina.Width.Point / 2;
            double altura = pagina.Height.Point;

            XPdfForm formDir = XPdfForm.FromFile(caminhoDireito);
            formDir.PageIndex = indiceDireito;
            gfx.DrawImage(formDir, new XRect(metade, 0, metade, altura));
        }

        private void AdicionarPaginaA4(PdfDocument destino, string caminho, int indice)
        {
            PdfDocument origem = PdfReader.Open(caminho, PdfDocumentOpenMode.Import);

            PdfPage pagina = destino.AddPage();
            pagina.Width = origem.Pages[indice].Width;
            pagina.Height = origem.Pages[indice].Height;

            using XGraphics gfx = XGraphics.FromPdfPage(pagina);

            XPdfForm form = XPdfForm.FromFile(caminho);
            form.PageIndex = indice;
            gfx.DrawImage(form, new XRect(0, 0, pagina.Width.Point, pagina.Height.Point));
        }
    }
}