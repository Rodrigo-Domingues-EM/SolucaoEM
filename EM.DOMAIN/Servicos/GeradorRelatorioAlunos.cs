using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using EM.DOMAIN; // Certifique-se de que este namespace é onde sua classe Aluno está localizada
namespace EM.DOMAIN.Servicos
{
	public class GeradorRelatorioAluno
	{
		public void GerarRelatorio(List<Aluno> alunos)
		{
			var writer = new PdfWriter(new FileStream("RelatorioAlunos.pdf", FileMode.Create));
			var pdf = new PdfDocument(writer);
			var document = new Document(pdf);

			Paragraph header = new Paragraph("Relatório de Alunos")
			   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
			   .SetFontSize(20);

			document.Add(header);

			// Cria uma tabela com as colunas necessárias
			Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 1, 2, 1, 2, 1 }))
				.UseAllAvailableWidth();

			// Adiciona os cabeçalhos da tabela
			table.AddHeaderCell("Matrícula");
			table.AddHeaderCell("Nome");
			table.AddHeaderCell("Sexo");
			table.AddHeaderCell("Data de Nascimento");
			table.AddHeaderCell("Idade");
			table.AddHeaderCell("Cidade");
			table.AddHeaderCell("UF");

			// Adiciona o conteúdo da tabela
			foreach (var aluno in alunos)
			{
				table.AddCell(aluno.Matricula.ToString());
				table.AddCell(aluno.Nome);
				table.AddCell(aluno.Sexo == SexoEnum.Masculino ? "Masculino" : "Feminino");
				table.AddCell(aluno.DataNascimento.HasValue ? aluno.DataNascimento.Value.ToString("dd/MM/yyyy") : "");
				table.AddCell(CalcularIdade(aluno.DataNascimento).ToString());
				table.AddCell(aluno.Cidade.Nome);
				table.AddCell(aluno.Cidade.UF);
			}

			document.Add(table);

			document.Close();
		}

		// Método para calcular a idade baseado na data de nascimento
		private int CalcularIdade(DateTime? dataNascimento)
		{
			if (!dataNascimento.HasValue)
			{
				return 0;
			}

			var idade = DateTime.Now.Year - dataNascimento.Value.Year;
			if (DateTime.Now.Month < dataNascimento.Value.Month || (DateTime.Now.Month == dataNascimento.Value.Month && DateTime.Now.Day < dataNascimento.Value.Day))
			{
				idade--;
			}

			return idade;
		}
	}
}
