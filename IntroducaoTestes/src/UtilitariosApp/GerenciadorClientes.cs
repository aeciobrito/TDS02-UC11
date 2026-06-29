namespace UtilitariosApp
{
    public class GerenciadorClientes
    {
        private List<Cliente> clientes = new();

        public void AdicionarCliente(Cliente cliente)
        {
            if( cliente == null )
                throw new ArgumentNullException(nameof(cliente));

            clientes.Add(cliente);
        }

        public List<Cliente> ObterTodosClientes() => clientes;

        public int ContarClientes() => clientes.Count;

        public Cliente? ProcurarPorId(int id)
        {
            foreach (var cliente in clientes)
            {
                if (cliente.Id == id) return cliente;
            }
            return null;
        }

        public bool RemvoerPorId(int id)
        {
            var cliente = ProcurarPorId(id);
            if(cliente != null)
            {
                clientes.Remove(cliente);
                return true;
            }
            return false;
        }
    }
}
