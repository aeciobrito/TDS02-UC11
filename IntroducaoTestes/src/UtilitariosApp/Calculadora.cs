using System.Reflection.Metadata.Ecma335;

namespace UtilitariosApp
{
    public class Calculadora
    {
        public int Somar(int a, int b)
        {
            return a + b;
        }

        public int Multiplicar(int a, int b)
        {
            return a * b;
        }

        public int Dividir(double a, double b)
        {
            if (b == 0) return (int)a;

            var resultado = a / b;
            return (int)Math.Ceiling(resultado);
        }

        public int SubtrairPositivoOuZero(int a, int b)
        {
            return b > a ? 0 : a - b;
        }

        public double PotenciaDeUmNumero(int @base, int expoente)
        {
            return Math.Pow(@base, expoente);
        }

        // Crie 2 testes: um fato, e uma teoria
        public double Raiz(int a)
        {
            if(a < 0)
            {
                throw new ArgumentException("Não é possível calcular raiz de 0 ou negativo!");
            }

            return Math.Sqrt(a);
        }
    }
}
