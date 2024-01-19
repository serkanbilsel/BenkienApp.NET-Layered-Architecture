using System.ComponentModel.DataAnnotations;

namespace BenkienApp.Data.Entity.BaseEntities
{
    public abstract class BaseAuditEntity : BaseEntity, IAuditEntity
    {
        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Bir kere oluşturulduktan sonra sabit kalmasını istiyoruz.
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow; // Her güncellemede son değeri almasını istiyoruz.
        [Display(Name = "Silinme Tarihi")]
        public DateTime? DeletedAt { get; set; }
    }
}
