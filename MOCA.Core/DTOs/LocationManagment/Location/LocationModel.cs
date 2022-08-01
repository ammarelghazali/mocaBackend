using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "District Id Cannot Be 0")]
        public long DistrictId { get; set; }
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
        [Range(1, 5)]
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
        [Range(1, long.MaxValue, ErrorMessage = "Currency Id Cannot Be 0")]
        public long CurrencyId { get; set; }
        public string Phone { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "LocationType Id Cannot Be 0")]
        public long LocationTypeId { get; set; }
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
        public List<ServiceFeePaymentsDueDateModel> ServiceFeePaymentsDueDates { get; set; }
        public List<LocationContactModel> LocationContacts { get; set; }
        public List<LocationImageModel> LocationImages { get; set; }
        public List<LocationCurrencyModel> LocationCurrencies { get; set; }
        public List<LocationFileModel> LocationFiles { get; set; }
        public List<LocationWorkingHourModel> LocationWorkingHours { get; set; }
        public List<LocationBankAccountModel> LocationBankAccount { get; set; }
        public List<LocationInclusionModel> LocationInclusions { get; set; }
    }
}
