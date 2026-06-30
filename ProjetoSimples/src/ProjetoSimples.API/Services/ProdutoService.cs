using ProjetoSimples.API.Models;

namespace ProjetoSimples.API.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly List<Produto> _produtos = new();
        private int _id = 1;

        public Produto Adicionar(Produto produto)
        {
            produto.Id = _id++;
            _produtos.Add(produto);
            return produto;
        }

        public Produto? ObterPorId(int id)
        {
            return _produtos.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Produto> ObterTodos()
        {
            return _produtos;
        }

        public bool Remover(int id)
        {
            var produto = ObterPorId(id);
            if (produto == null) return false;

            _produtos.Remove(produto);
            return true;
        }
    }
}
