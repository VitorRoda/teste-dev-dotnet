using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using teste_dev_dotnet.Models;

namespace teste_dev_dotnet.Controllers;

// Mantive as telas bases de quando criamos a aplicação, para acessar atividade va até a rota http://localhost:5042/CarrinhoPage

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
