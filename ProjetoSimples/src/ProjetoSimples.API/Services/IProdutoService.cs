using ProjetoSimples.API.Models;

namespace ProjetoSimples.API.Services
{
    public interface IProdutoService
    {
        IEnumerable<Produto> ObterTodos();
        Produto? ObterPorId(int id);
        Produto Adicionar(Produto produto);
        bool Remover(int id);
    }
}
