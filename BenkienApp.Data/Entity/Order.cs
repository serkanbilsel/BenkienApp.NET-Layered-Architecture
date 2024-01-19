using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenkienApp.Data.Entity.BaseEntities;

namespace BenkienApp.Data.Entity
{
    public class Order : BaseAuditEntity, IEntity
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "The {0} field cannot be left blank!")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; } // Kullanıcıya referans vermek için kullanıcı ID'si

        [ForeignKey("UserId")]
        public virtual User User { get; set; } // Kullanıcı nesnesi

        [Required(ErrorMessage = "The {0} field cannot be left blank!")]
        [MaxLength(200, ErrorMessage = "The {0} cannot exceed 200 characters.")]
        public string AdressDetail { get; set; } // Kullanıcının adres bilgisi

        public decimal TotalAmount { get; set; }

        // Diğer özellikler, örneğin sipariş detayları gibi

        public bool IsShipped { get; set; }

        public DateTime? ShippedDate { get; set; }

        public string GenerateOrderNumber()
        {
            // Sipariş numarasını oluşturmak için özel bir mantık eklenebilir
            return $"ORD-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        }
    }


}
