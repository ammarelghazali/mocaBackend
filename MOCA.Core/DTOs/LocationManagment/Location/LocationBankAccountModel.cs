
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationBankAccountModel
    {
        public long Id { get; set; }
        //[Range(1, long.MaxValue, ErrorMessage = "Location Id Cannot Be 0")]
        public long LocationId { get; set; }

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
