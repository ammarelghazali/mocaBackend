using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Location
{
    public class LocationModel
    {
        public long Id { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "LocationType Id Cannot Be 0")]
        public long LocationTypeId { get; set; }
        public string Name { get; set; }
        public int? BuildYear { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Country Id Cannot Be 0")]
        public long CountryId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "City Id Cannot Be 0")]
        public long CityId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "District Id Cannot Be 0")]
        public long DistrictId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string MapAddress { get; set; }
        [Required]
        public string Longitude { set; get; }
        [Required]
        public string Latitude { set; get; }
        [Range(1, long.MaxValue, ErrorMessage = "Currency Id Cannot Be 0")]
        public long CurrencyId { get; set; }
        [Required]
        public decimal GrossArea { get; set; }
        [Required]
        public decimal NetArea { get; set; }
        public string Phone { get; set; }
        [Required]
        public DateTime LaunchDate { get; set; }
        [Required]
        public string LandlordCommercialName { get; set; }
        [Required]
        public string LandlordLegalName { get; set; }
        [Required]
        public int ContractLength { get; set; }
        [Required]
        public DateTime ContractStartDate { get; set; }
        [Required]
        public DateTime ContractEndDate { get; set; }
        [Required]
        public int PartentershipType { get; set; }
        public decimal? PreOperationFee { get; set; }
        public decimal? GracePeriod { get; set; }
        public decimal? RampUpPeriod { get; set; }
        public decimal? UtilizationPercentage { get; set; }
        public decimal? MinPaymentPerMonth { get; set; }
        public decimal? MinPaymentPercentage { get; set; }
        public decimal? EstimatedRampUpAmount { get; set; }
        public decimal? FullRampUpRevenue { get; set; }
        public decimal? FullOccupancyMonthlyPayment { get; set; }
        public decimal? EstimatedAnnualizedAmount { get; set; }
        public decimal? EstimatedContractAmount { get; set; }
        public decimal? LandlordShares { get; set; }
        public decimal? CopolitanShares { get; set; }
        [Range(1, 5)]
        public int? PaymentMethod { get; set; }
        [Range(1, 4)]
        public int? PaymentTerm { get; set; }
        public decimal? MonthlyRentAmount { get; set; }
        public decimal? PricePerMeter { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public decimal? LandlordAdditionalRevenue { get; set; }
        public decimal? MonthlyRevenue { get; set; }
        public decimal? DirectCost { get; set; }
        public decimal? Overhead { get; set; }
        public decimal? TotalAfterDeductions { get; set; }
        public decimal? ServiceFeesPriceSqm { get; set; }
        public decimal? ServiceFeesTotalFees { get; set; }
        public decimal? ServiceFeesAnnualIncrease { get; set; }
        public string? EventspaceLeaseContract { get; set; }
        public string? CommercialRegisterFile { get; set; }
        public string? CommercialRegisterNumber { get; set; }
        public string? TaxIdFile { get; set; }
        public string? TaxIdNumber { get; set; }
        public string? About { get; set; }
        public string? Terms { set; get; }
        public bool IsPublish { get; set; }
        public int LocationBankAccountType { get; set; }
        public List<ServiceFeePaymentsDueDateModel> ServiceFeePaymentsDueDates { get; set; }
        public List<LocationContactModel> LocationContacts { get; set; }
        public List<LocationImageModel> LocationImages { get; set; }
        public List<LocationCurrencyModel> LocationCurrencies { get; set; }
        public List<LocationFileModel> LocationFiles { get; set; }
        public List<LocationWorkingHourModel> LocationWorkingHours { get; set; }
        public LocationBankAccountModel LocationBankAccount { get; set; }
        public List<LocationInclusionModel> LocationInclusions { get; set; }
    }
}
