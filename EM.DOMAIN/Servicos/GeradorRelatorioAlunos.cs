using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EM.DOMAIN;

namespace EM.DOMAIN.Servicos
{
	public class GeradorRelatorioAluno
	{
		public void GerarRelatorio(List<Aluno> alunos)
		{
			using (FileStream fs = new FileStream("RelatorioAlunos.pdf", FileMode.Create))
			{

				Document document = new Document();
				PdfWriter writer = PdfWriter.GetInstance(document, fs);
				document.Open();

				// Cria uma tabela para o layout
				PdfPTable layoutTable = new PdfPTable(new float[] { 3,6 });
				layoutTable.WidthPercentage = 100;

				// Adiciona o logotipo
				string logoPath = "https://is4-ssl.mzstatic.com/image/thumb/Purple112/v4/33/93/a7/3393a763-f5c2-5b3e-2f11-8f3d70663e5c/AppIcon-0-1x_U007emarketing-0-7-0-0-85-220.png/512x512bb.jpg";  // Substitua pelo caminho do seu logotipo
				Image logo = Image.GetInstance(logoPath);
				logo.ScaleToFit(75, 75);  // Ajusta o tamanho do logotipo
				PdfPCell logoCell = new PdfPCell(logo);
				logoCell.Border = Rectangle.NO_BORDER;
				layoutTable.AddCell(logoCell);

				// Adiciona o título
				Font titleFont = FontFactory.GetFont("Arial", 24, Font.BOLD);
				Paragraph title = new Paragraph("Relatório de Alunos", titleFont);
				title.Alignment = Element.ALIGN_LEFT;
				PdfPCell titleCell = new PdfPCell();
				titleCell.AddElement(title);
				titleCell.Border = Rectangle.NO_BORDER;
				layoutTable.AddCell(titleCell);

				document.Add(layoutTable);


				// Adiciona uma linha de divisão
				Chunk linebreak = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1));
				document.Add(linebreak);

				// Adiciona algum espaço em branco
				document.Add(new Paragraph("\n"));


				// Cria uma tabela com as colunas necessárias
				PdfPTable table = new PdfPTable(new float[] { 4, 6, 3, 3, 2, 3, 1 });
				table.WidthPercentage = 110;

				// Adiciona os cabeçalhos da tabela
				table.AddCell("Matrícula");
				table.AddCell("Nome");
				table.AddCell("Sexo");
				table.AddCell("Data de Nascimento");
				table.AddCell("Idade");
				table.AddCell("Cidade");
				table.AddCell("UF");

				// Adiciona o conteúdo da tabela
				foreach (var aluno in alunos)
				{
					table.AddCell(aluno.Matricula.ToString());
					table.AddCell(aluno.Nome);
					table.AddCell(aluno.Sexo == SexoEnum.Masculino ? "Masculino" : "Feminino");
					table.AddCell(aluno.DataNascimento.HasValue ? aluno.DataNascimento.Value.ToString("dd/MM/yyyy") : "");
					table.AddCell(CalcularIdade(aluno.DataNascimento));
					table.AddCell(aluno.Cidade.Nome);
					table.AddCell(aluno.Cidade.UF);
				}

				document.Add(table);

				document.Close();
			}
		}

		// Método para calcular a idade baseado na data de nascimento
		private string CalcularIdade(DateTime? dataNascimento)
		{
			if (!dataNascimento.HasValue)
			{
				return "Data de nascimento não fornecida";
			}

			DateTime agora = DateTime.Now;
			int anos = agora.Year - dataNascimento.Value.Year;
			int meses = agora.Month - dataNascimento.Value.Month;
			int dias = agora.Day - dataNascimento.Value.Day;

			if (dias < 0)
			{
				meses--;
				dias += DateTime.DaysInMonth(agora.Year, agora.Month);
			}

			if (meses < 0)
			{
				anos--;
				meses += 12;
			}

			return $"{anos}a, {meses}m, {dias}d";
		}
	}
}
