using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Location : BaseEntity
    {
        [Required]
        public string LocationName { get; set; }
        [ForeignKey("LocationDistrictID")]
        public long LocationDistrictID { get; set; }
        public virtual District District { get; set; }
        [Required]
        public string LocationAddress { get; set; }
        public int? LocationBuildYear { get; set; }
        [Required]
        public decimal LocationGrossArea { get; set; }
        [Required]//as8ar mn gross
        public decimal LocationNetArea { get; set; }
        [Required]//Link
        public string LocationMapAddress { get; set; }
        public int? LocationContractLength { get; set; }
        public DateTime? LocationContractStartDate { get; set; }
        public DateTime? LocationContractEndDate { get; set; }
        [Range(1,5)]
        public int? LocationPaymentMethods { get; set; }
        public int? LocationPartentershipType { get; set; }
        public int? PartnershipTypeLandlord { get; set; }
        public int? PartnershipTypePercentage { get; set; }
        public decimal LocationMonthlyRentAmount { get; set; }
        public int? LocationPaymentMethod { get; set; }
        public decimal? LocationEstimatedAnnualizedAmount { get; set; }
        public decimal? LocationEstimatedContractAmount { get; set; }
        public decimal? LocationAnnualIncrease { get; set; }
        [Required]
        public int LocationMainCurrency { get; set; }
        public string Phone { get; set; }
        [Required]
        [Range(1, 3)]
        public int LocationType { get; set; }
        public string LandlordLegalName { get; set; }
        public string UploadContract { get; set; }
        public string CommercialRegister { get; set; }
        public string TaxId { get; set; }
        public string CommercialName { get; set; }
        public string BankAccountName { get; set; }
        public long? BankAccountNumber { get; set; }
        public string BankAccountSwift { get; set; }
        public string BankAccountIBAN { get; set; }
        public double? ServiceFeesPriceSqm { get; set; }
        public double? ServiceFeesTotalFee { get; set; }
        public int? ServiceFeesAnnualIncrease { get; set; }
        public string Url360Tour { set; get; }
        public string VenuesBrochureURL { set; get; }
        public string WorkspaceContract { get; set; }
        public string EventspaceContract { get; set; }
        public string About { get; set; }
        public string Terms { set; get; }
        [Required]
        public string Longitude { set; get; }
        [Required]
        public string Latitude { set; get; }
        public decimal? PreOperationFee { get; set; }
        public decimal? GracePeriod { get; set; }
        public decimal? RampUpPeriod { get; set; }
        public decimal? EstimatedRamp { get; set; }
        public decimal? FullOccupancy { get; set; }
        public decimal? MinPaymentPerMonth { get; set; }
        public decimal? MinPaymentPercentage { get; set; }
        public decimal? MonthlyRevenue { get; set; }
        public decimal? DirectCost { get; set; }
        public decimal? Overhead { get; set; }
        public decimal? TotalAfterDeductions { get; set; }
        public decimal? UtilizationPeriod { get; set; }
        public decimal? PricePerMeter { get; set; }
        public string TaxIdNumber { get; set; }
        public string CrNumber { get; set; }
        public bool IsPublish { get; set; }
    }
}
