using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mercedes.Models;
using mercedes.Models.Entities;

namespace mercedes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private NorthwindContext context;

    public HomeController(NorthwindContext _context)
    {
        context = _context;
    }

    public IActionResult Index()
    {
        List<Region> list =  context.Regions.ToList();
 
        return View(list);
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
