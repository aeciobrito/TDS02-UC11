namespace UtilitariosApp.Tests
{
    public class ClienteTests
    {
        [Fact]
        public void Cliente_DeveTerPropriedaesCorretas()
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

    }
}
