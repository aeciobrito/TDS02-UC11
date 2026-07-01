using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjetoSimples.API.Controllers;
using ProjetoSimples.API.Models;
using ProjetoSimples.API.Services;

namespace ProjetoSimples.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        [Fact]
        public void GetById_DeveRetornarOk_QuantoProdutoExistir()
        {
            // Arrange
            /// 1. Criamos um MOCK (imitador, dublê) da ProdutoService
            var mockService = new Mock<IProdutoService>();

            /// 2. Ensinamos ao mock, como respodner chamadas por ID
            mockService.Setup(s => s.ObterPorId(40))
                .Returns(new Produto { Nome = "Mouse USB", Preco = 150.00m });

            /// 3. Instanciamos a controller, dependendo do mock, e não da service real
            var controller = new ProdutosController(mockService.Object);

            // Act
            var result = controller.GetById(40);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var produtoRetornado = Assert.IsType<Produto>(okResult.Value);
            Assert.Equal("Mouse USB", produtoRetornado.Nome);
        }
    }
}
