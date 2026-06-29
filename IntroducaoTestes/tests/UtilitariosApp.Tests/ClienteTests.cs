namespace UtilitariosApp.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_DeveTerPropriedadesCorretas()
        {
            // Arrange
            var endereco = new Endereco("Rua Tito", 54, "São Paulo", "SP");
            int id = 87;
            string nome = "Souza Santos";
            string email = "souza.santos@mail.com";

            // Act
            var cliente = new Cliente(id, nome, email, endereco);

            // Assert
            Assert.NotNull(cliente);
            Assert.Equal(id, cliente.Id);
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(email, cliente.Email);
            Assert.NotNull(endereco);
        }

        [Fact]
        public void Cliente_DeveContribuirEnderecoCorreto()
        {
            // Arrange
            var endereco = new Endereco("Rua Tito", 54, "São Paulo", "SP");
            int id = 87;
            string nome = "Souza Santos";
            string email = "souza.santos@mail.com";

            // Act
            var cliente = new Cliente(id, nome, email, endereco);
            var enderecoFormatado = cliente.Enderco.FormatarEndereco();

            // Assert
            Assert.Equal("Rua Tito, 54, São Paulo, SP", enderecoFormatado);
        }

        [Fact]
        public void EmailValido_ComEmailCorreto_DeveRetornarVerdadeiro()
        {
            // Arrange
            var endereco = new Endereco("Rua Tito", 54, "São Paulo", "SP");
            int id = 87;
            string nome = "Souza Santos";
            string email = "souza.santos@mail.com";

            // Act
            var cliente = new Cliente(id, nome, email, endereco);

            // Assert
            Assert.True(cliente.EmailValido());
        }

        [Fact]
        public void EmailValido_SemArroba_DeveRetornarFalso()
        {
            // Arrange
            var endereco = new Endereco("Rua Tito", 54, "São Paulo", "SP");
            int id = 87;
            string nome = "Souza Santos";
            string email = "souza.santos.mail.com";

            // Act
            var cliente = new Cliente(id, nome, email, endereco);

            // Assert
            Assert.False(cliente.EmailValido());
        }

        [Fact]
        public void EmailValido_SemPonto_DeveRetornarFalso()
        {
            // Arrange
            var endereco = new Endereco("Rua Tito", 54, "São Paulo", "SP");
            int id = 87;
            string nome = "Souza Santos";
            string email = "souzasantos@mail-com";

            // Act
            var cliente = new Cliente(id, nome, email, endereco);

            // Assert
            Assert.False(cliente.EmailValido());
        }
    }
}
