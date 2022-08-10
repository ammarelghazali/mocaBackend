using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.Shared
{
    public class MemberType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<WorkSpaceBundleMemberType> WorkSpaceBundleMemberships { get; set; }
        public ICollection<CoworkingSpaceBundleMemberType> CoworkingSpaceBundleMemberTypes { get; set; }
    }
}
