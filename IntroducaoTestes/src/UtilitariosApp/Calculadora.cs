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
    }
}
