using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.LocationManagment.FavouriteLocation
{
    public class FavouriteLocationModel
    {
        public long Id { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Location Id Cannot Be 0")]
        public long LocationId { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = "Basic User Id Cannot Be 0")]
        public long BasicUserId { get; set; }
    }
}
