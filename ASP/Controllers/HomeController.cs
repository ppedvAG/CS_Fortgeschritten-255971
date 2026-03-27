using ASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.Controllers;

public class HomeController(ILogger<HomeController> logger, CounterService cs, NorthwindContext db) : Controller
{
	public IActionResult Index()
	{
		//WICHTIG: ToList() hier sehr gefährlich; jede Iterationsanweisung lädt die Daten von der Datenbank
		//List<Customers> customers = db.Customers.ToList();

		//IQueryable: Die Anleitung zum Laden der Daten
		//IQueryable: Unterinterface von IEnumerable
		IQueryable<Customers> customers = db.Customers.Where(e => e.Country == "UK");

		cs.Counter++;
		return View(customers);
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
