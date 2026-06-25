namespace UtilitariosApp.Tests
{
    public class UtilitariosTextoTests
    {
        [Theory] // valide todos os casos de borda (edge cases)
        [InlineData("dotnet", 2, 10, true)]
        [InlineData("dotnet", 8, 10, false)]
        [InlineData("pao", 3, 3, true)]
        [InlineData("lata", 1, 3, false)]
        [InlineData("z", 1, 10000, true)]
        [InlineData("", 0, 10, true)]
        [InlineData("", 1, 10, false)]
        [InlineData(null, 5, 10, false)]
        public void ValidarTamanho_DeveRetornarTotalDePalavras(string texto, int min, int max, bool resultado)
        {
            // Arrange
            var utilitario = new UtilitariosTexto();            

            // Act
            bool validacao = utilitario.ValidarTamanho(texto, min, max);

            // Assert
            Assert.Equal(resultado, validacao);
        }

        [Theory]
        [InlineData("Hoje está chovendo", 3)]
        [InlineData("Sim", 1)]
        [InlineData("Até mais, tenho aula hoje", 5)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("  ", 0)]
        [InlineData(" Novo Texto ", 2)]
        [InlineData(" Novo  Texto ", 2)]
        public void ContadorDePlavras_DeveRetornarOTotalDePalavras(string texto, int total)
        {
            // Arrange
            var utilitario = new UtilitariosTexto();

            // Act
            int totalContabilizado = utilitario.ContadorDePlavras(texto);

            //Assert
            Assert.Equal(totalContabilizado, total);
        }
    }
}
