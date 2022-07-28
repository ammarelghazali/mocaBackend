using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.SSO
{
    public class MemberShipCategories : BaseEntity
    {
        public string Name { get; set; }
        public long MainCategoryId { get; set; }
        public MemberShipMainCategories MainCategory { get; set; }
        public long BenefitTypeId { get; set; }
        public MemberShipBenefitsTypes BenefitType { get; set; }
        public bool IsStatic { get; set; }
    }
}
