using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using System.Diagnostics;
using static Org.BouncyCastle.Math.Primes;
using Color = iTextSharp.text.BaseColor;
using Font = iTextSharp.text.Font;

namespace GeradorDeQuestoes.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> gerarQuestoes(string inputGroupSelect01,
                                                               string inputGroupSelect02,
                                                               string inputGroupSelect03,
                                                               string inputGroupSelect04,
                                                               string inputGroupSelect05)

        {
            string materia = inputGroupSelect01;
            string tipoQuestao = inputGroupSelect02;
            string nivelDificuldade = inputGroupSelect03;
            string numeroQuestoes = inputGroupSelect04;
            string outraMateria = inputGroupSelect05;

            if (inputGroupSelect05 != null)
            {
                materia = outraMateria;
            }

            if (materia == null | tipoQuestao == null | nivelDificuldade == null | numeroQuestoes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string prompt = "";

            string _titulo = $"Questoes {tipoQuestao} de {materia} com um nivel de dificuldade {nivelDificuldade}.";


            if (nivelDificuldade == "dificil")
            {
                prompt = $"Gerar {numeroQuestoes} questões com maior precisão e nível difificuldade desafiadoras de {materia} para que eu possa praticar minhas habilidades em um nível avançado, usando o modelo GPT-Maths, com solução completa e bem especificada.";
            }
            else
            {
                prompt = $"Gerar {numeroQuestoes} questões {tipoQuestao} com um nivel de dificuldade {nivelDificuldade} da matéria de {materia}, com sua solução completa e bem detalhada.";
            }

            var token = "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";

            var openAi = new OpenAIAPI(token);

            var completions = openAi.Completions.CreateCompletionAsync(
                prompt: prompt,
                model: "text-davinci-003",
                max_tokens: 2048,
                temperature: 0.2f
            );

            string pathRespostaTxt = @"C:\\Users\amanc\Desktop\Pdf\Resposta.txt";
            string pathRespostaPdf = @"C:\\Users\amanc\Desktop\Pdf\Resposta.pdf";

            using (var respostaTxt = System.IO.File.CreateText(pathRespostaTxt))
            {
                foreach (var completion in completions.Result.Completions)
                {
                    respostaTxt.WriteLine(completion.Text);
                }
            }

            using (Document doc = new Document(PageSize.A4))
            {
                StreamReader rdr = new StreamReader(pathRespostaTxt);

                doc.SetMargins(40, 40, 40, 80);          //estibulando o espaçamento das margens que queremos
                doc.AddCreationDate();                   //adicionando as configuracoes

                PdfWriter.GetInstance(doc, new FileStream(pathRespostaPdf, FileMode.Create));

                doc.Open();

                //criando a variavel para o cabecalho
                iTextSharp.text.Font Fonte = new iTextSharp.text.Font();
                Fonte.Size = 14;
                Fonte.SetStyle(1); //Negrito
                Paragraph cabecalho = new Paragraph("", Fonte);
                cabecalho.Alignment = Element.ALIGN_CENTER;
                cabecalho.Font = Fonte;
                cabecalho.Add("Sistema Gerador de Questões");
                doc.Add(cabecalho);

                //criando a variavel para o paragrafo
                Fonte.Size = 9;
                Paragraph paragrafo = new Paragraph("", Fonte);
                paragrafo.Alignment = Element.ALIGN_CENTER;
                paragrafo.Font = Fonte;
                paragrafo.Add(_titulo);
                doc.Add(paragrafo);

                doc.Add(new Paragraph(rdr.ReadToEnd()));

                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = pathRespostaPdf;
                proc.Start();

                doc.Close();
                doc.Dispose();
            }

        // https://csharp.hotexamples.com/examples/-/iTextSharp.text.pdf.PdfStamper/-/php-itextsharp.text.pdf.pdfstamper-class-examples.html


            return RedirectToAction("Index", "Home");

        }




        public async Task<IActionResult> gerarQuestoes2(string inputGroupSelect01,
                                                               string inputGroupSelect02,
                                                               string inputGroupSelect03,
                                                               string inputGroupSelect04,
                                                               string inputGroupSelect05)

        {
            string materia = inputGroupSelect01;
            string tipoQuestao = inputGroupSelect02;
            string nivelDificuldade = inputGroupSelect03;
            string numeroQuestoes = inputGroupSelect04;
            string outraMateria = inputGroupSelect05;

            if (inputGroupSelect05 != null)
            {
                materia = outraMateria;
            }

            if (materia == null | tipoQuestao == null | nivelDificuldade == null | numeroQuestoes == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string prompt = "";

            string _titulo = $"Questoes {tipoQuestao} de {materia} com um nivel de dificuldade {nivelDificuldade}.";


            if (nivelDificuldade == "dificil")
            {
                prompt = $"Gerar {numeroQuestoes} questões com maior precisão e nível difificuldade desafiadoras de {materia} para que eu possa praticar minhas habilidades em um nível avançado, usando o modelo GPT-Maths, com solução completa e bem especificada.";
            }
            else
            {
                prompt = $"Gerar {numeroQuestoes} questões {tipoQuestao} com um nivel de dificuldade {nivelDificuldade} da matéria de {materia}, com sua solução completa e bem detalhada.";
            }

            var token = "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";

            var openAi = new OpenAIAPI(token);

            var completions = openAi.Completions.CreateCompletionAsync(
                prompt: prompt,
                model: "text-davinci-003",
                max_tokens: 2048,
                temperature: 0.2f
            );

            string pathRespostaTxt = @"C:\\Users\amanc\Desktop\Pdf\Resposta.txt";
            string pathRespostaPdf = @"C:\\Users\amanc\Desktop\Pdf\Resposta.pdf";

            using (var respostaTxt = System.IO.File.CreateText(pathRespostaTxt))
            {
                foreach (var completion in completions.Result.Completions)
                {
                    respostaTxt.WriteLine(completion.Text);
                }
            }

            StreamReader rdr = new StreamReader(@"C:\Users\amanc\Desktop\Pdf\Resposta.txt");

            //Create a New instance on Document Class

            Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
            doc.SetMargins(40, 40, 40, 80);          //estibulando o espaçamento das margens que queremos
            doc.AddCreationDate();                   //adicionando as configuracoes

            PdfWriter.GetInstance(doc, new FileStream(pathRespostaPdf, FileMode.Create));

            doc.Open();

            //criando a variavel para o cabecalho
            Font Fonte = new Font();
            Fonte.Size = 14;
            Fonte.SetStyle(1); //Negrito
            Paragraph cabecalho = new Paragraph("", Fonte);
            cabecalho.Alignment = Element.ALIGN_CENTER;
            cabecalho.Font = Fonte;
            cabecalho.Add("Sistema Gerador de Questões");
            doc.Add(cabecalho);

            //criando a variavel para o paragrafo
            Fonte.Size = 9;
            Paragraph paragrafo = new Paragraph("", Fonte);
            paragrafo.Alignment = Element.ALIGN_CENTER;
            paragrafo.Font = Fonte;
            paragrafo.Add(_titulo);
            doc.Add(paragrafo);

            // Line separator

            Chunk linebreak = new Chunk(new LineSeparator(0.0F, 100.0F, Color.BLUE, Element.ALIGN_CENTER, -1));
            doc.Add(linebreak);

            //Add the content of Text File to PDF File

            doc.Add(new Paragraph(rdr.ReadToEnd()));

            //Close the Document

            doc.Close();
            return RedirectToAction("Index", "Home");

        }

    }





}
// http://www.voidgeeks.com/tutorial/How-to-Use-ChatGPT-in-CApplication/18
