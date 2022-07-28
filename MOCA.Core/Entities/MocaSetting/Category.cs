using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Enums.LocationManagment;

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
        [ForeignKey("LobSpaceTypeId")]
        public LocationType LobSpaceType { get; set; }
    }
}
