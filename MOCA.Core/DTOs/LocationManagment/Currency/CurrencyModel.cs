using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.Currency
{
    public class CurrencyModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FlagCode { get; set; }
    }
}
