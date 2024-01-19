using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BenkienApp.Data.Entity.BaseEntities;

namespace BenkienApp.Data.Entity
{
    public class User : BaseAuditEntity, IEntity
    {
        [Display(Name = "Kullanıcı ID")]
        public int Id { get; set; }


        public int? RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role? Role { get; set; }



        [Display(Name = "Ad"), Required(ErrorMessage = "{0} alanı Gereklidir")]
        public string Name { get; set; }
        [Display(Name = "Soyad")]
        public string? Surname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} alanı Gereklidir")]
        public string Password { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string? UserName { get; set; }
        [Display(Name = "Avatar")]

        public string? Image { get; set; }
        [Display(Name = "Açık Adres")]
        public string? AdressDetail { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [ScaffoldColumn(false)]
        public Guid? UserGuid { get; set; } // Bu guid i session veya cookie de saklayarak kullanıcıyı tanımak için kullanırız

    }
}
