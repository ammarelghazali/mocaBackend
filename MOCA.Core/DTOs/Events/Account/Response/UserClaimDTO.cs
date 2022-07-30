namespace MOCA.Core.DTOs.Events.Account.Response
{
    public class UserClaimDTO
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool? Selected { get; set; }
    }
}
