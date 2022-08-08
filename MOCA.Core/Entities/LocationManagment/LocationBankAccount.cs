using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationBankAccount : BaseEntity
    {
        public long LocationId { get; set; }
        public virtual Location Location { get; set; }
        public string LandlordBankAccountName { get; set; }
        public long? LandlordBankAccountNumber { get; set; }
        public string LandlordBankAccountSwift { get; set; }
        public string LandlordBankAccountIBAN { get; set; }
        public string MocaBankAccountName { get; set; }
        public long? MocaBankAccountNumber { get; set; }
        public string MocaBankAccountSwift { get; set; }
        public string MocaBankAccountIBAN { get; set; }
        public string SharedBankAccountName { get; set; }
        public long? SharedBankAccountNumber { get; set; }
        public string SharedBankAccountSwift { get; set; }
        public string SharedBankAccountIBAN { get; set; }
    }
}
