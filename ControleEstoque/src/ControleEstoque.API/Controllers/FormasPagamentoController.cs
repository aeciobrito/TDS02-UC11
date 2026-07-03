using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleEstoque.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Exige estar autenticado para QUALQUER endpoint deste controller
    public class FormasPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoService _service;

        public FormasPagamentoController(IFormaPagamentoService service)
        {
            _service = service;
        }

        // 1. Visualização: Clientes, Caixas e Gerentes podem ver todas
        [HttpGet]
        [Authorize(Roles = "Cliente,Caixa,Gerente")]
        public async Task<IActionResult> ObterTodas()
        {
            var formas = await _service.ObterTodasAsync();
            return Ok(formas);
        }

        // 2. Visualização por ID: Clientes, Caixas e Gerentes
        [HttpGet("{id}")]
        [Authorize(Roles = "Cliente,Caixa,Gerente")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var forma = await _service.ObterPorIdAsync(id);
            if (forma == null) return NotFound(new { message = "Forma de pagamento não encontrada." });
            return Ok(forma);
        }

        // 3. Cadastro: APENAS Gerentes podem cadastrar
        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Criar([FromBody] CriarFormaPagamentoDto dto)
        {
            var novaForma = await _service.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novaForma.Id }, novaForma);
        }

        // 4. Alteração: APENAS Gerentes podem alterar
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CriarFormaPagamentoDto dto)
        {
            var atualizada = await _service.AtualizarAsync(id, dto);
            if (atualizada == null) return NotFound(new { message = "Forma de pagamento não encontrada." });
            return Ok(atualizada);
        }

        // 5. Exclusão: APENAS Gerentes podem deletar
        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Excluir(int id)
        {
            var excluido = await _service.ExcluirAsync(id);
            if (!excluido) return NotFound(new { message = "Forma de pagamento não encontrada." });
            return NoContent();
        }
    }
}