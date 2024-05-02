using EM.WEB.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EM.DOMAIN;
using EM.REPOSITORY;
using System.Reflection;


namespace EM.WEB.Controllers
{
	public class AlunoController : Controller
	{
		private readonly IRepositorioAluno<Aluno> repositorioAluno;
		private readonly IRepositorioCidade<CidadeModel> repositorioCidade;

		public AlunoController(IRepositorioAluno<Aluno> repositorioAluno, IRepositorioCidade<CidadeModel> repositorioCidade)
		{
			this.repositorioAluno = repositorioAluno;
			this.repositorioCidade = repositorioCidade;
		}

		public IActionResult TabelaAluno()
		{
			IEnumerable<Aluno> listaAlunos = repositorioAluno.GetAll();
			return View(listaAlunos);
		}


		public IActionResult CadastroAluno(long? matricula)
		{
			ViewBag.Cidades = repositorioCidade.GetAll().ToList();

			if (matricula != null)
			{
				var aluno = repositorioAluno.Get(a => a.Matricula == matricula).FirstOrDefault();
				if (aluno == null)
				{
					return NotFound();
				}
				ViewBag.IsEdicao = true;
				return View(aluno);
			}


			ViewBag.IsEdicao = false;
			return View(new Aluno());

		}


		[HttpPost]
		public IActionResult CadastroAluno(Aluno aluno)
		{
			if (ModelState.IsValid)
			{
				if (aluno.Matricula > 0)
				{
					repositorioAluno.Update(aluno);
				}
				else
				{
					repositorioAluno.Add(aluno);
				}
				return RedirectToAction("TabelaAluno");
			}

			ViewBag.IsEdicao = aluno.Matricula > 0;
			ViewBag.Cidades = repositorioCidade.GetAll().ToList();
			return View(aluno);
		}



		[HttpPost]
		public ActionResult Search(string searchTerm, string searchType)
		{
			Console.WriteLine("Search Type: " + searchType); // Adicione esta linha para depuração
			IEnumerable<Aluno> alunos;
			if (searchType == "matricula" && long.TryParse(searchTerm, out long matricula))
			{
				alunos = new List<Aluno>(repositorioAluno.GetByMatricula(matricula));
			}
			else if (searchType == "nome")
			{
				alunos = repositorioAluno.GetByContendoNoNome(searchTerm);
			}
			else
			{
				alunos = new List<Aluno>();
			}
			return View("TabelaAluno", alunos);
		}


		[HttpPost]
		public IActionResult RemoveAluno(Aluno aluno)
		{
			repositorioAluno.Remove(aluno);
			return RedirectToAction("Index", "Home");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
