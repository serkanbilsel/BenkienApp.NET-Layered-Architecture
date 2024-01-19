using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Entity.BaseEntities
{
    public class ProductImageToDelete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageName { get; set; }
    }
}
