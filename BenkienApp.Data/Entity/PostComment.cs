using BenkienApp.Data.Entity.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenkienApp.Data.Entity
{
    public class PostComment : BaseAuditEntity, IEntity
    {




        [ForeignKey(nameof(PostId))]
        public int? PostId { get; set; }
        public Post? Post { get; set; }


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public User? User { get; set; }


        [Column(TypeName = "text"), DataType(DataType.Text)]
        public string Comment { get; set; }

        public bool IsActive { get; set; }

    }
}
