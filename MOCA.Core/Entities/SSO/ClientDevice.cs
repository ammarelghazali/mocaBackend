using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.SSO
{
    public class ClientDevice : BaseEntity
    {

        public string Model { get; set; }
        public string UniqulyIdentifier { get; set; }
        public string OS { get; set; }
        public string DeviceType { get; set; }
        public string Brand { get; set; }
        public string NotificationToken { get; set; }
        public string OTP { get; set; }
        public DateTime? OTPDate { get; set; }
        public long? BasicUserId { get; set; }
        public BasicUser BasicUser { get; set; }
    }
}
