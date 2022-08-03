namespace MOCA.Core.DTOs.Events.Account.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public long DepartmentID { get; set; }
        public string Profile_Pic_Path { get; set; }
        public long PositionID { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string CountryCode { get; set; }
        public IList<RoleResponseDTO> lstUserRoles { get; set; }
        public IList<UserClaimDTO> lstUserClaim { get; set; }
        public string PassEmail { get; set; }
    }
}
