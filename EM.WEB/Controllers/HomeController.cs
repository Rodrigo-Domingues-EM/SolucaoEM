using System.Diagnostics;
using EM.DOMAIN;
using EM.REPOSITORY;
using Microsoft.AspNetCore.Mvc;

namespace EM.WEB.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return RedirectToAction("TabelaAluno", "Aluno");
		}

	}
}
