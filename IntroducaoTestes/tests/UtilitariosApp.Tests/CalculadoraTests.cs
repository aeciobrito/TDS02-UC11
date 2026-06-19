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
    }
}
