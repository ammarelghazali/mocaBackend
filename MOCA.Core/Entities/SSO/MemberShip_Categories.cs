using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.SSO
{
    public class MemberShip_Categories : BaseEntity
    {
        public string Name { get; set; }
        public long MainCategoryId { get; set; }
        public MemberShip_Main_Categories MainCategory { get; set; }
        public long BenefitTypeId { get; set; }
        public MemberShip_Benefits_Types BenefitType { get; set; }
        public bool IsStatic { get; set; }
    }
}
