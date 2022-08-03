using Microsoft.Extensions.Configuration;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class LocationsMemberShipsRepository : ILocationsMemberShipsRepository
    {
        private readonly IConfiguration _configuration;

        public LocationsMemberShipsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Location> GetLocationByID(long? id)
        {
            var location = new Location();
/*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select Id, Loc_Name from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE loc.Id = {id} AND loc.IsPublish = {1}";

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
            return !string.IsNullOrEmpty(location.Name) ? location : null;
        }

        public async Task<Location> GetLocationByIDAndLocType(long id, int LocType)
        {
            var location = new Location();
            /*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql;

                    if (LocType == 2)
                    {
                        sql = $"Select Id, Loc_Name from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE loc.Id = {id} AND loc.IsPublish = {1} AND loc.LOB_Location_Type_Id != {3}";
                    }
                    else
                    {
                        sql = $"Select Id, Loc_Name from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE loc.Id = {id} AND loc.IsPublish = {1} AND loc.LOB_Location_Type_Id = {3}";
                    }

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
            return !string.IsNullOrEmpty(location.Name) ? location : null;
        }

        public async Task<List<Location>> GetLocationmoca()
        {
            var locations = new List<Location>();
            /*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select Id, Loc_Name from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE loc.LOB_Location_Type_Id = {3} AND loc.IsPublish = {1}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                locations.Add(new Location
                                {
                                    Id = await reader.Get<long>("Id"),
                                    Loc_Name = await reader.Get<string>("Loc_Name"),
                                });
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
            return locations;
        }

        public async Task<List<Location>> GetLocationNotmoca()
        {
            var locations = new List<Location>();
            /*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select Id, Loc_Name from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE loc.LOB_Location_Type_Id != {3} AND loc.IsPublish = {1}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                locations.Add(new Location
                                {
                                    Id = await reader.Get<long>("Id"),
                                    Loc_Name = await reader.Get<string>("Loc_Name"),
                                });
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            */
            return locations;
        }

        public async Task<int> GetLocationTypeByID(long id)
        {
            int locationType = 0;
            /*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select LOB_Location_Type_Id from [db_a6b7be_dev].[dbo].[tb_Locations] as loc WHERE  loc.id = {id}";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                locationType = await reader.Get<int>("LOB_Location_Type_Id");
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            */
            return locationType;
        }

        public async Task<bool> LocationTypeExists(int LocType)
        {
            bool result = false;
            /*
            try
            {

                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"Select Id from [db_a6b7be_dev].[dbo].[tb_LOB_Location_Types] as loc WHERE loc.Id = {LocType} ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            */
            return result;
        }
    }
}
