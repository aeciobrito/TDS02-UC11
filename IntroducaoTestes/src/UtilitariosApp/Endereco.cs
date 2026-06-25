namespace UtilitariosApp
{
    public class Endereco
    {
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Endereco(string rua, int numero, string cidade, string estado)
        {
            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
        }

        public string FormatarEndereco()
        {
            return $"{Rua}, {Numero}, {Cidade}, {Estado}";
        }

    }
}
