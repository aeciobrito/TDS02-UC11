namespace UtilitariosApp
{
    public class FormatadorDados
    {
        public string FormatarNumeroTelefone(double numero)
        {
            string t = numero.ToString();
            string resposta = $"({t[0]}{t[1]}{t[2]}) {t[3]}{t[4]}{t[5]}-{t[6]}{t[7]}{t[8]}{t[9]}";
            return resposta;
        }
    }
}
