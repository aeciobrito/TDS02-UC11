namespace UtilitariosApp
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Endereco Enderco { get; set; }

        public Cliente(int id, string nome, string email, Endereco enderco)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Enderco = enderco;
        }

        public string ObterInformacoes()
        {
            return $"ID: {Id}\nNome: {Nome}\nEmail: {Email}\n Endereço: {Enderco.FormatarEndereco()}";
        }

        public bool EmailValido()
        {
            return Email.Contains("@") && Email.Contains(".");
        }
    }
}
