using UtilitariosApp;

namespace UtilitariosApp.Tests
{
    
    public class CalculadoraTests
    {
        [Fact]
        public void Somar_DeveRotornarASomaCorreta()
        {
            var calculadora = new Calculadora();
            var resultado = calculadora.Somar(5, 7);
            var resultadoZerado = calculadora.Somar(0, 0);

            Assert.Equal(12, resultado);
            Assert.Equal(0, resultadoZerado);
        }

        [Fact]
        public void Multiplicar_DeveRetornarOProdutoCorreto()
        {
            var calculadora = new Calculadora();
            var resultado = calculadora.Multiplicar(3, 3);

            Assert.Equal(9, resultado);
        }

        [Fact]
        public void Dividir_DeveRetornarOResultadoInteiroDaDivisaoArredondadoParaCima()
        {
            var calculadora = new Calculadora();
            var resultado = calculadora.Dividir(5, 3);

            Assert.Equal(2, resultado);
        }

        [Fact]
        public void Dividir_AoDividirPorZero_DeveRetornarODividendo()
        {
            var calculadora = new Calculadora();
            var resultado = calculadora.Dividir(5, 0);

            Assert.Equal(5, resultado);
        }

        [Fact]
        public void Subtrair_DeveRetornarASubtracao()
        {
            Calculadora c1 = new Calculadora();
            var resultado = c1.SubtrairPositivoOuZero(6, 2);

            Assert.Equal(4, resultado);
        }

        [Fact]
        public void Subtrair_AoSubtrairValorMaiorDeMenor_ResultadoEhZero()
        {
            Calculadora c1 = new Calculadora();
            var resultado = c1.SubtrairPositivoOuZero(2, 10);

            Assert.Equal(0, resultado);
        }

        [Theory]
        [InlineData(10, 3, 1000)]
        [InlineData(10, 2, 100)]
        [InlineData(5, 2, 25)]
        [InlineData(5, 3, 125)]
        [InlineData(2, 5, 32)]
        [InlineData(2, -2, 0.25)]
        [InlineData(2, 0, 1)]
        [InlineData(-32, 0, 1)]
        [InlineData(15, 1, 15)]
        [InlineData(-15, 1, -15)]
        public void PotenciaDeUmNumero_DeveRetornarAPotencia(int a, int b, double pot)
        {
            Calculadora c1 = new Calculadora();
            var resultado = c1.PotenciaDeUmNumero(a, b);

            Assert.Equal(pot, resultado);
        }
    }
}
