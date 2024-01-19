using BenkienApp.Data.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BenkienApp.Data.Entity
{
    public class Slider : BaseAuditEntity, IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        public string? Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Slider adresi")]
        public string? Link { get; set; }


    }


}
