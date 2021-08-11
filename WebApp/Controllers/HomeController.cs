using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult GerarRelatorioFibonacci(int id = 20)
		{
			var fibonacciService = new FibonacciService();
			return View("GerarRelatorioFibonacci", fibonacciService.Gerar(id));
		}

		public IActionResult GerarRelatorioFibonacciBackGround(int id = 20)
		{
			var fibonacciService = new FibonacciService();
			
			if (!fibonacciService.IsRunning)
				Task.Factory.StartNew(() => fibonacciService.Gerar(id));

			if (fibonacciService.Count() < id)
				return View("GerarRelatorioFibonacciBackGround", $"{fibonacciService.Count() * 100 / id} %");
			else
				return View("GerarRelatorioFibonacci", fibonacciService.Gerar(id));
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
