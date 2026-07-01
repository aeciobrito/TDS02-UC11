using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoSimples.API.Models;
using ProjetoSimples.API.Services;

namespace ProjetoSimples.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        // A injeção de dependência ocorre aqui, via interface
        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_produtoService.ObterTodos());
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var produto = _produtoService.ObterPorId(id);

            if (produto == null) return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public IActionResult Post(Produto produto)
        {
            _produtoService.Adicionar(produto);
            return CreatedAtAction(nameof(Post), produto);
        }
    }
}
