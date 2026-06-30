namespace UtilitariosApp.Tests
{
    public class GerenciadorClientesTests
    {
        [Fact]
        public void AdicionarCliente_AoAdicionarCliente_DeveAumentarQuantidade()
        {
            // Arrange
            var gerenciadorClientes = new GerenciadorClientes();
            var endereco = new Endereco("Rua B", 123, "São Paulo", "SP");
            var cliente = new Cliente(2, "Marcio", "marcio@mail.com", endereco);

            // Act
            gerenciadorClientes.AdicionarCliente(cliente);

            // Assert
            Assert.Equal(1, gerenciadorClientes.ContarClientes());
        }

        [Fact]
        public void AdicionarCliente_ComClienteNulo_DeveLancarArgumentNullException()
        {
            // Arrange
            var gerenciador = new GerenciadorClientes();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => gerenciador.AdicionarCliente(null));
        }

        [Fact]
        public void AdicionarCliente_VariosClientesValidos_DeveAdicionarTodos()
        {
            // Arrange
            var gerenciador = new GerenciadorClientes(); 

            var endereco1 = new Endereco("Rua B", 123, "São Paulo", "SP");
            var endereco2 = new Endereco("Travessa X", 544, "São Paulo", "SP");

            var cliente1 = new Cliente(1, "Silva", "jsilva@mail.com", endereco1);
            var cliente2 = new Cliente(2, "Marcio", "marcio@mail.com", endereco2);
            var cliente3 = new Cliente(3, "Luana", "luana@mail.com", endereco2);

            // Act
            gerenciador.AdicionarCliente(cliente1);
            gerenciador.AdicionarCliente(cliente2);
            gerenciador.AdicionarCliente(cliente3);

            // Assert
            Assert.Equal(3, gerenciador.ContarClientes());
        }

        [Fact]
        public void ProcurarPorId_ComIdExistente_DeveRertornarCliente()
        {
            // Arrange
            var gerenciadorClientes = new GerenciadorClientes();
            var endereco = new Endereco("Rua B", 123, "São Paulo", "SP");
            var cliente = new Cliente(2, "Marcio", "marcio@mail.com", endereco);
            gerenciadorClientes.AdicionarCliente(cliente);

            // Act
            var clienteEncontrado = gerenciadorClientes.ProcurarPorId(2);

            // Assert
            Assert.NotNull(clienteEncontrado);
            Assert.Equal(2, clienteEncontrado.Id);
            Assert.Equal("Marcio", clienteEncontrado.Nome);
        }

        [Fact]
        public void ProcurarPorId_ComIdInexistente_DeveRertornarNull()
        {
            // Arrange
            var gerenciadorClientes = new GerenciadorClientes();    

            // Act
            var clienteEncontrado = gerenciadorClientes.ProcurarPorId(2);

            // Assert
            Assert.Null(clienteEncontrado);
        }
    }
}
