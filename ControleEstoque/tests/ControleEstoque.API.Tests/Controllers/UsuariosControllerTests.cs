using ControleEstoque.API.Controllers;
using ControleEstoque.API.DTOs;
using ControleEstoque.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControleEstoque.API.Tests.Controllers
{
    public class UsuariosControllerTests
    {
        [Fact]
        public async Task Autenticar_QuandoCredencialInvalida_RetornaUnauthorized()
        {
            // Arrange
            var login = new LoginDto { Email = "teste@mail.com", Senha = "senha" };
            var loginCorreto = new LoginDto { Email = "teste@mail.com", Senha = "senha123" };
            var service = new Mock<IUsuarioService>();
            service.Setup(s => s.AutenticarAsync(loginCorreto)).ReturnsAsync((TokenDto?)null);
            var controller = new UsuariosController(service.Object);

            // Act
            var result = await controller.Autenticar(login);

            // Assert
            var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Contains("ERROR:", unauthorized.Value?.ToString());
        }

        [Fact]
        public async Task RegistrarCliente_QuandoSucesso_DeveRetornarCreatedAtAction()
        {

        }
    }
}
