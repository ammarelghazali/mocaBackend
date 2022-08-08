using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.SSO
{
    public class BasicUser : BaseEntity
    {
       
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public long CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string ProfilePhoto { get; set; }

        public DateTime? DateOfBirth { get; set; }
        //public DateTime? CreateAt { get; set; }


        public long? MembershipCategoryId { get; set; }
        public MemberShipCategories MembershipCategory { get; set; }

        public long? MembershipTypeId { get; set; }
        public MemberShipTypes MemberShipTypes { get; set; }

        public bool? IsQRVerifiedByAdmin { get; set; }

        public bool? IsQRVerifiedByClient { get; set; }

        [Required]
        public bool IsVerified { get; set; }

        [Required]
        public bool? Accept { get; set; }

        [Required]
        public bool? StatusUser { get; set; }

        public string ActivationToken { get; set; }
        public string Company { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal WalletBalance { get; set; }

        public string MemberID { get; set; }

        public DateTime? MembershipActivationDate { get; set; }

        [Required]
        public string Status { get; set; }

        public long GenderId { get; set; }
        public Gender Gender { get; set; }
        //public string Gender { get; set; }

        public long? UserDeviceId { get; set; }

        [ForeignKey("UserDeviceId")]
        public ClientDevice UserDevice { get; set; }

        public ICollection<ReservationDetail> ReservationDetails { get; set; }
        public ICollection<WorkSpaceReservationHourly> WorkSpaceHourlyReservations { get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceTailoredReservations { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceBundleReservations { get; set; }
        public ICollection<WorkSpaceHourlyTopUp> WorkSpaceHourlyTopUps { get; set; }
        public ICollection<WorkSpaceTailoredTopUp> WorkSpaceTailoredTopUps { get; set; }
        public ICollection<MeetingReservation> MeetingReservations { get; set; }
        public ICollection<MeetingReservationTopUp> MeetingReservationTopUps { get; set; }
    }
}
