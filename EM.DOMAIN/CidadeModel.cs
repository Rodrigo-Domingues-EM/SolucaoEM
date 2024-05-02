using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EM.DOMAIN
{
	public class CidadeModel : IEntidade
    {
		public int Id_cidade { get; set; }
		[Required(ErrorMessage = "O nome da Cidade é obrigatório!")]
		public string? Nome { get; set; }
		[Required(ErrorMessage = "A UF da Cidade é obrigatória!")]
		public string? UF { get; set; }

		public CidadeModel()
		{
			// Inicialize as propriedades conforme necessário
		}

	}
}
