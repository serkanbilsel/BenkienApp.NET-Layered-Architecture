using BenkienApp.Data.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BenkienApp.Data.Entity
{
    public class ProductImage : BaseAuditEntity, IEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string? ImageName { get; set; }

        public string? ImageUrl { get; set; }
        [JsonIgnore]

        public Product? Product { get; set; }
    }


}
