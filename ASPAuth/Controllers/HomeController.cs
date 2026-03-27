using ASPAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPAuth.Controllers;

public class HomeController(
	ILogger<HomeController> logger,
	UserManager<IdentityUser> um,
	SignInManager<IdentityUser> sim,
	RoleManager<IdentityRole> rm) : Controller
{
	public IActionResult Index()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		um.FindByNameAsync("Admin");

		if (HttpContext.User.Claims.Any(e => e.Value == "Privacy"))
			return View();
		else
			return Forbid();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
