using ProjetoSimples.API.Models;
using ProjetoSimples.API.Services;

namespace ProjetoSimples.Tests.Services
{
    public class ProdutoServiceTests
    {
        [Fact]
        public void Adicionar_DeveInserirProdutoERetornarcomIdGerado()
        {
            // Arrange
            var service = new ProdutoService();
            var produto = new Produto { Nome = "Mouse USB", Preco = 150.00m };

            // Act
            var resultado = service.Adicionar(produto);

            // Assert
            Assert.True(resultado.Id > 0);
            Assert.Equal("Mouse USB", resultado.Nome);
            Assert.Single(service.ObterTodos());
        }

        [Fact]
        public void ObterPorId_DeveRetornarNulo_QuandoProdutoNaoExistir()
        {
            // Arrange
            var service = new ProdutoService();

            // Act
            var resultado = service.ObterPorId(99);

            // Assert
            Assert.Null(resultado);
        }
    }
}
