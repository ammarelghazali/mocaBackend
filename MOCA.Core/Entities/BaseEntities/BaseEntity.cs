using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.BaseEntities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public virtual DateTime? LastModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
