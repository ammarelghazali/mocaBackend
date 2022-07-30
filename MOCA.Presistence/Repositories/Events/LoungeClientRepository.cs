using Microsoft.Extensions.Configuration;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class LoungeClientRepository : ILoungeClientRepository
    {
        private readonly IConfiguration _configuration;

        public LoungeClientRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetClientNameById(long Client_Id)
        {
            var First_Name = "";
            var Last_Name = "";/*
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = $"Select First_Name, Last_Name from [db_a6b7be_dev].[dbo].[tb_Lounge_Clients] WHERE Id = {Client_Id}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                First_Name = await reader.Get<string>("First_Name");
                                Last_Name = await reader.Get<string>("Last_Name");
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {

            }
            
            if(string.IsNullOrEmpty(First_Name) && string.IsNullOrEmpty(Last_Name))
            {
                return null;
            }
            */
            return First_Name + " " + Last_Name;
        }
    }
}
