using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MOCA.Core.DTOs.Events.Account.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Response<AuthenticationResponse>> GetUserById(string userId)
        {
            AuthenticationResponse authenticationResponse = new AuthenticationResponse();
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select * from [db_a6b7be_dev].[Identity].[User] as users WHERE users.Id = '{userId}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                authenticationResponse = new AuthenticationResponse
                                {
                                    //Id = await reader.Get<string>("Id"),
                                    //UserName = await reader.Get<string>("UserName"),
                                    //PassEmail = await reader.Get<string>("PassEmail")
                                };
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {

            }

            return authenticationResponse != null ? new Response<AuthenticationResponse>(authenticationResponse) : null;
        }
    }
}
