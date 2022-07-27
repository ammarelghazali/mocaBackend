using System.ComponentModel.DataAnnotations;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Category Display Order Cannot Be 0")]
        public int DisplayOrder { get; set; }

        public List<Faq> Faqs { get; set; }

        public long? LobSpaceTypeId { get; set; }
  //      [ForeignKey("LobSpaceTypeId")]
//        public LobSpaceType LobSpaceType { get; set; }
    }
}
