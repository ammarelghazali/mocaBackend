using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MOCA.Core.DTOs.Events.Account.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response<UserResponse>> GetUserByID(string uID)
        {
            var userResponse = new UserResponse();
            IList<RoleResponseDTO> roleResponse = new List<RoleResponseDTO>();
            IList<UserClaimDTO> userClaims = new List<UserClaimDTO>();
            /*
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    userResponse = await GetUserResponseAsync(connection, uID);
                    roleResponse = await GetRoleResponseAsync(connection, uID);
                    userClaims = await GetUserClaimsAsync(connection, uID);

                    connection.Close();
                }
            }
            catch (SqlException e)
            {

            }

            if (userResponse != null)
            {
                userResponse.lstUserClaim = userClaims;
                userResponse.lstUserRoles = roleResponse;

                return new Response<UserResponse>(userResponse);

            }
            */
            return null;
        }

        private async Task<UserResponse?> GetUserResponseAsync(SqlConnection connection, string uID)
        {
            UserResponse userResponse = new UserResponse();
            /*
            string sql = $"Select * from [db_a6b7be_dev].[Identity].[User] as users WHERE users.Id = '{uID}'";
            
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        userResponse = new UserResponse
                        {
                            Id = await reader.Get<string>("Id"),
                            UserName = await reader.Get<string>("UserName"),
                            BirthDate = await reader.Get<DateTime>("BirthDate"),
                            CountryCode = await reader.Get<string>("CountryCode"),
                            DepartmentID = await reader.Get<long>("DepartmentID"),
                            Email = await reader.Get<string>("Email"),
                            FirstName = await reader.Get<string>("FirstName"),
                            Gender = await reader.Get<int>("Gender"),
                            IsActive = await reader.Get<bool>("IsActive"),
                            LastName = await reader.Get<string>("LastName"),
                            PassEmail = await reader.Get<string>("PassEmail"),
                            Password = await reader.Get<string>("PasswordHash"),
                            PhoneNumber = await reader.Get<string>("PhoneNumber"),
                            PositionID = await reader.Get<long>("PositionID"),
                            Profile_Pic_Path = await reader.Get<string>("Profile_Pic_Path"),
                        };
                    }
                }
            }
            */
            return !string.IsNullOrEmpty(userResponse.UserName) ? userResponse : null;
        }

        private async Task<IList<RoleResponseDTO>> GetRoleResponseAsync(SqlConnection connection, string uID)
        {
            var roleResponse = new List<RoleResponseDTO>();
            /*
            string sql = $"Select Roles.Id, Roles.Name from [db_a6b7be_dev].[Identity].[UserRoles] as userRoles, [db_a6b7be_dev].[Identity].[Role] AS Roles WHERE userRoles.UserId = '{uID}' AND Roles.Id = userRoles.RoleId";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        roleResponse.Add(new RoleResponseDTO
                        {
                            RoleId = await reader.Get<string>("Id"),
                            RoleName = await reader.Get<string>("Name")
                        });
                    }
                }
            }
            */
            return roleResponse;
        }

        private async Task<IList<UserClaimDTO>> GetUserClaimsAsync(SqlConnection connection, string uID)
        {
            var userClaims = new List<UserClaimDTO>();
            /*
            string sql = $"Select * from [db_a6b7be_dev].[Identity].[UserClaims] as claims WHERE claims.UserId = '{uID}'";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        userClaims.Add(new UserClaimDTO
                        {
                            ClaimType = await reader.Get<string>("ClaimType"),
                            ClaimValue = await reader.Get<string>("ClaimValue"),
                            Selected = await reader.Get<bool?>("Selected")
                        });
                    }
                }
            }
            */
            return userClaims;
        }
    }
}
