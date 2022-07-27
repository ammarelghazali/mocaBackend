﻿using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.SSO
{
    public class BasicUser 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string JobTitle { get; set; }

        [Required]
        public string Email { get; set; }

                                    [Required]
                                    public string CountryCode { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public string ProfilePhoto { get; set; }

        public DateTime? DateOfBirth { get; set; }
        //public DateTime? CreateAt { get; set; }


        public long? MembershipCategoryId { get; set; }
        public MemberShip_Categories MembershipCategory { get; set; }

        public long? MembershipTypeId { get; set; }
        public MemberShip_Types MemberShipTypes { get; set; }

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

        public DateTime? Membership_Activation_Date { get; set; }

        [Required]
        public string Status { get; set; }

        public long GenderId { get; set; }
        public Gender Gender { get; set; }
        //public string Gender { get; set; }

        public long? UserDeviceId { get; set; }
        public Client_Device UserDevice { get; set; }
    }
}
