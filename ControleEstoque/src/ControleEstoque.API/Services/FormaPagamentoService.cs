using ControleEstoque.API.Data;
using ControleEstoque.API.DTOs;
using ControleEstoque.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.API.Services
{
    public interface IFormaPagamentoService
    {
        Task<IEnumerable<FormaPagamentoDto>> ObterTodasAsync();
        Task<FormaPagamentoDto?> ObterPorIdAsync(int id);
        Task<FormaPagamentoDto> CriarAsync(CriarFormaPagamentoDto dto);
        Task<FormaPagamentoDto?> AtualizarAsync(int id, CriarFormaPagamentoDto dto);
        Task<bool> ExcluirAsync(int id);
    }

    public class FormaPagamentoService : IFormaPagamentoService
    {
        private readonly AppDbContext _context;

        public FormaPagamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormaPagamentoDto>> ObterTodasAsync()
        {
            return await _context.FormasPagamento
                .Select(f => new FormaPagamentoDto { Id = f.Id, Nome = f.Nome, Ativo = f.Ativo })
                .ToListAsync();
        }

        public async Task<FormaPagamentoDto?> ObterPorIdAsync(int id)
        {
            var forma = await _context.FormasPagamento.FindAsync(id);
            if (forma == null) return null;

            return new FormaPagamentoDto { Id = forma.Id, Nome = forma.Nome, Ativo = forma.Ativo };
        }

        public async Task<FormaPagamentoDto> CriarAsync(CriarFormaPagamentoDto dto)
        {
            var forma = new FormaPagamento { Nome = dto.Nome, Ativo = dto.Ativo };
            _context.FormasPagamento.Add(forma);
            await _context.SaveChangesAsync();

            return new FormaPagamentoDto { Id = forma.Id, Nome = forma.Nome, Ativo = forma.Ativo };
        }

        public async Task<FormaPagamentoDto?> AtualizarAsync(int id, CriarFormaPagamentoDto dto)
        {
            var forma = await _context.FormasPagamento.FindAsync(id);
            if (forma == null) return null;

            forma.Nome = dto.Nome;
            forma.Ativo = dto.Ativo;
            await _context.SaveChangesAsync();

            return new FormaPagamentoDto { Id = forma.Id, Nome = forma.Nome, Ativo = forma.Ativo };
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var forma = await _context.FormasPagamento.FindAsync(id);
            if (forma == null) return false;

            _context.FormasPagamento.Remove(forma);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}