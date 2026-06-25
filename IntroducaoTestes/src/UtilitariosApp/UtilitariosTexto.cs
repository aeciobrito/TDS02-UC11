using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitariosApp
{
    public class UtilitariosTexto
    {
        public bool ValidarTamanho(string texto, int min, int max)
        {
            if (texto == null) return false;
            return texto.Length >= min && texto.Length <= max;
        }

        public int ContadorDePlavras(string texto)
        {
            // me digam, onde eu errei?
            if (string.IsNullOrEmpty(texto)) 
                return 0;

            var palavras = texto.Split(' ');
            return palavras.Length;
        }
    }
}
