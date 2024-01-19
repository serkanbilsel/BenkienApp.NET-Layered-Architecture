using BenkienApp.Data.Entity.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Entity
{
    public class Setting : BaseAuditEntity, IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Site Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Site Açıklama")]
        public string? Description { get; set; }

        public string? Email { get; set; }
        [Display(Name = "Telefon")]
        public string? Phone { get; set; }
        [Display(Name = "Mail Sunucusu")]
        public string? MailServer { get; set; }
        [Display(Name = "Port")]
        public int Port { get; set; }
        [Display(Name = "Mail Kullanıcı Adı")]
        public String UserName { get; set; }
        [Display(Name = "Mail Şifresi")]
        public String Password { get; set; }

        public String Favicon { get; set; }
        [Display(Name = "Site Logo")]
        public String Logo { get; set; }
        [Display(Name = "Firma Adresi"), DataType(DataType.MultilineText)]
        public String Address { get; set; }  
        [Display(Name = "Firma Harita Kodu"), DataType(DataType.MultilineText)]
        public String MapCode { get; set; } 

    }
}
