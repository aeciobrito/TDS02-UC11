using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControleEstoque.API.Models
{
    public class FormaPagamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; } = string.Empty; // Ex: "Cartão de Crédito", "Pix", "Dinheiro"

        public bool Ativo { get; set; } = true;

        // Relacionamento: Uma Forma de Pagamento pode ter muitos Pedidos
        // [JsonIgnore] // Evita referência cíclica na serialização do JSON
        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}