using BenkienApp.Data.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BenkienApp.Data.Entity
{
    public class Contact : BaseAuditEntity, IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Ad"), Required(ErrorMessage = "{0} alanı Gereklidir")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string? Surname { get; set; }
        [EmailAddress, Required(ErrorMessage = "{0} alanı Gereklidir")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Telefon numarası 10 veya 11 basamaklı olmalıdır.")]
        public string? Phone { get; set; }
        [Display(Name ="Mesaj"), DataType(DataType.MultilineText), Required(ErrorMessage = "{0} alanı Gereklidir")]
        public  string Message { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
