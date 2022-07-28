using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Faq : BaseEntity
    {
        public long? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Question { get; set; }
       
        [Required]
        public string Answer { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "FAQ Display Order Cannot Be 0")]
        
        public int DisplayOrder { get; set; }
        
        public long? LobSpaceTypeId { get; set; }
        //[ForeignKey("LobSpaceTypeId")]
        //public LobSpaceType LobSpaceType { get; set; }
    }
}
