namespace UtilitariosApp.Tests
{
    public class FormatadorDadosTests
    {
        [Theory]
        [InlineData(1234567890, "(123) 456-7890")]
        [InlineData(1111111111, "(111) 111-1111")]
        [InlineData(1211111111, "(121) 111-1111")]
        [InlineData(9999999999, "(999) 999-9999")]
        public void FormatarNumeroTelefone_AoReceberNumeroValido_DeveRetornarFormatado(double numero, string formatado)
        {
            // Arrange
            var formatador = new FormatadorDados();

            // Act
            var resultado = formatador.FormatarNumeroTelefone(numero);

            // Assert
            Assert.Equal(formatado, resultado);
        }
    }
}
