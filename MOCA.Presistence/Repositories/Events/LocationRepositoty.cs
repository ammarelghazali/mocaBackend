using Microsoft.Extensions.Configuration;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class LocationRepositoty : ILocationRepositoty
    {
        private readonly IConfiguration _configuration;

        public LocationRepositoty(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Location> GetLocationByID(long id)
        {
            var location = new Location();
            /*
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select id, loc_name from [db_a6b7be_dev].[dbo].[tb_Locations] where id = {id}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                location = new Location
                                {
                                    Id = await reader.Get<long>("Id"),
                                    Loc_Name = await reader.Get<string>("Loc_Name"),
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
            */
            return location != null ? location : null;
        }
    }
}
