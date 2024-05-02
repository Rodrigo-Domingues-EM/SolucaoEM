﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.DOMAIN.Validations;


namespace EM.DOMAIN
{
	public class Aluno : IEntidade
	{
		public long Matricula { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Nome Deve ter no máximo 100 caracteres!")]
		[MinLength(3, ErrorMessage = "Nome Deve ter no mínimo 3 caracteres!")]
		public string Nome { get; set; }
		public SexoEnum? Sexo { get; set; }
		public DateTime? DataNascimento { get; set; }

		[CpfValidation]
		public string? CPF { get; set; }
		public CidadeModel Cidade { get; set; }

	}
}