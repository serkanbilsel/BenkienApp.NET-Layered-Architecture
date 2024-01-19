using BenkienApp.Data.Entity.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace BenkienApp.Data.Entity
{
    public class Role : BaseEntity, IEntity
    {
        [MaxLength(50, ErrorMessage = "The {0} cannot exceed 100 characters"), Display(Name = "Role Name")]
        public string RoleName { get; set; }
        //public int RoleId { get; set; }
    }

}
