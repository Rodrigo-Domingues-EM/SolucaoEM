using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EM.DOMAIN; // Certifique-se de que este namespace é onde sua classe Aluno está localizada

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

				Paragraph header = new Paragraph("Relatório de Alunos");
				header.Font.Size = 20;

				document.Add(header);

				// Cria uma tabela com as colunas necessárias
				PdfPTable table = new PdfPTable(new float[] { 2, 3, 2, 2, 1, 2, 1 });
				table.WidthPercentage = 100;

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
					table.AddCell(CalcularIdade(aluno.DataNascimento).ToString());
					table.AddCell(aluno.Cidade.Nome);
					table.AddCell(aluno.Cidade.UF);
				}

				document.Add(table);

				document.Close();
			}
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
