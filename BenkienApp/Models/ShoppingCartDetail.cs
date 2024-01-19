using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenkienApp.Models
{
    [Table("Sepet Detayı")]
    public class ShoppingCartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId {get; set; }
        public int ProductId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

    }
}
