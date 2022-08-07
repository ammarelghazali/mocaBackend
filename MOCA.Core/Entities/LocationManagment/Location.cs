using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.MocaSetting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Location : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public long DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        [Required]
        public string Address { get; set; }
        public int? BuildYear { get; set; }
        [Required]
        public decimal GrossArea { get; set; }
        [Required]
        public decimal NetArea { get; set; }
        [Required]
        public string MapAddress { get; set; }
        [Required]
        public int ContractLength { get; set; }
        [Required]
        public DateTime ContractStartDate { get; set; }
        [Required]
        public DateTime ContractEndDate { get; set; }
        [Required]
        public DateTime LaunchDate { get; set; }
        [Range(1,5)]
        public int? PaymentMethod { get; set; }
        [Range(1, 4)]
        public int? PaymentTerm { get; set; }
        [Required]
        public int PartentershipType { get; set; }
        public decimal? LandlordShares { get; set; }
        public decimal? CopolitanShares { get; set; }
        public decimal? MonthlyRentAmount { get; set; }
        public decimal? EstimatedAnnualizedAmount { get; set; }
        public decimal? EstimatedContractAmount { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public long CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
        public string Phone { get; set; }
        public long LocationTypeId { get; set; }
        [ForeignKey("LocationTypeId")]
        public virtual LocationType LocationType { get; set; }
        [Required]
        public string LandlordLegalName { get; set; }
        public string UploadContract { get; set; }
        public string CommercialRegisterFile { get; set; }
        public string TaxIdFile { get; set; }
        [Required]
        public string CommercialName { get; set; }
        public decimal? ServiceFeesPriceSqm { get; set; }
        public decimal? ServiceFeesTotalFees { get; set; }
        public decimal? ServiceFeesAnnualIncrease { get; set; }
        public string Url360Tour { set; get; }
        public string VenuesBrochureURL { set; get; }
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
        public decimal? FullOccupancyMonthlyPayment { get; set; }
        public decimal? MinPaymentPerMonth { get; set; }
        public decimal? MinPaymentPercentage { get; set; }
        public decimal? MonthlyRevenue { get; set; }
        public decimal? DirectCost { get; set; }
        public decimal? Overhead { get; set; }
        public decimal? TotalAfterDeductions { get; set; }
        public decimal? UtilizationPeriod { get; set; }
        public decimal? ServiceFeesPricePerMeter { get; set; }
        public string TaxIdNumber { get; set; }
        public string CommercialRegisterNumber { get; set; }
        public bool IsPublish { get; set; }
        public int LocationBankAccountType { get; set; }
        public ICollection<EventSpaceBooking> EventSpaceBookings { get; set; }
        public ICollection<IssueReport> IssueReports { get; set; }
        public ICollection<Building> Buildings { get; set; }
        public ICollection<LocationIndustry> LocationIndustries { get; set; }
        public ICollection<LocationContact> LocationContacts { get; set; }
        public ICollection<LocationCurrency> LocationCurrencies { get; set; }
        public ICollection<LocationImage> LocationImages { get; set; }
        public ICollection<LocationInclusion> LocationInclusions { get; set; }
        public ICollection<LocationWorkingHour> LocationWorkingHours { get; set; }
        public ICollection<ServiceFeePaymentsDueDate> ServiceFeePaymentsDueDates { get; set; }
    }
}
