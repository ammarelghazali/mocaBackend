using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment.Base
{
    public class BaseWorkSpaceHourlyPricing : BaseEntity
    {
        [Required]
        public int Hour { get; set; }

        [Required]  
        public decimal PricePerHour { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public decimal? VoucherPercentage { get; set; }

        public decimal? VoucherAmount { get; set; }
    }
}
