using BenkienApp.Data.Entity.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Entity
{
    public class Brand : BaseAuditEntity, IEntity
    {
        public int Id { get; set; }
        

        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Marka Logosu")]
        public string? Logo { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        // Brand sınıfında Products koleksiyonu tanımlandı
        public List<Product>? Products { get; set; }
    }


}
