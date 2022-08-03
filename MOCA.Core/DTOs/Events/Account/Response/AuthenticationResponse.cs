using System.Text.Json.Serialization;

namespace MOCA.Core.DTOs.Events.Account.Response
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        //public List<RoleResponseDTO> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        //public IList<UserClaimDTO> AllClaims { get; set; }
        public string PassEmail { get; set; }
    }
}
