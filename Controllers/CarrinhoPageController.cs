using Microsoft.AspNetCore.Mvc;
using CarrinhoProjeto.Repositories;

// Acabei criando esse aquivo a parte do CarrinhoController para 
// gerenciar a página web, ao meu ver fica mais organizado separar 
// as rotas das telas.

namespace CarrinhoProjeto.Controllers
{
    public class CarrinhoPageController : Controller
    {
        private readonly CarrinhoRepository _repository;

        public CarrinhoPageController(CarrinhoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var carrinho = await _repository.GetCarrinhoAsync(1);
            return View(carrinho);
        }
    }
}
