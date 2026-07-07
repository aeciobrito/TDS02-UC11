using ControleEstoque.API.Controllers;
using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleEstoque.API.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        [Fact]
        public async Task GetById_ProdutoNaoEncontrado_DeveRetornarNotFound()
        {
            // Arrange
            var serviceMock = new Mock<IProdutoService>();
            serviceMock.Setup(sm => sm.ObterPorIdAsync(23))
                .ReturnsAsync((ProdutoDto?)null);
            var controller = new ProdutosController(serviceMock.Object);

            // Act
            var result = await controller.GetById(23);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ProdutoCriado_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var produtoDto = new CriarProdutoDto
            {
                Nome = "Teclado",
                Preco = 99.90m,
                FornecedorId = 1,
                QuantidadeEstoque = 30
            };

            var produtoRetornadoDaService = new ProdutoDto
            {
                Id = 23,
                Nome = "Teclado",
                Preco = 599.90m,
                FornecedorId = 1,
                QuantidadeEstoque = 30
            };

            var serviceMock = new Mock<IProdutoService>();

            serviceMock.Setup(s => s.CriarAsync(produtoDto)).ReturnsAsync(produtoRetornadoDaService);

            var controller = new ProdutosController(serviceMock.Object);

            // Act
            var result = await controller.Create(produtoDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(23, ((ProdutoDto)createdResult.Value!).Id);
            Assert.Equal("Teclado", ((ProdutoDto)createdResult.Value!).Nome);
        }

        [Fact]
        public async Task Update_IdDiferente_DeveRetornarBadRequest()
        {
            // Arrange
            var serviceMock = new Mock<IProdutoService>();
            var produtoParaAtualizar = new AtualizarProdutoDto
            {
                Id = 23,
                Nome = "Teclado Mecanico RGB",
                Preco = 599.90m,
                FornecedorId = 1,
                QuantidadeEstoque = 30
            };

            var controller = new ProdutosController(serviceMock.Object);

            // Act
            var result = await controller.Update(3, produtoParaAtualizar);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Ação incorreta", badRequest.Value?.ToString());
        }

        [Fact]
        public async Task Delete_QuandoServicoCompleta_DeveRetornarNoContent()
        {
            // Arrange
            var mock = new Mock<IProdutoService>();
            mock.Setup(x => x.RemoverAsync(90)).Returns(Task.CompletedTask);
            var controller = new ProdutosController(mock.Object);

            // Act
            var result = await controller.Delete(90);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
