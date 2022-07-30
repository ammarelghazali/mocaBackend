using Microsoft.Extensions.Configuration;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Events.Repositories;

namespace MOCA.Presistence.Repositories.Events
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly IConfiguration _configuration;

        public IndustryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Industry> GetByID(long? id)
        {
            var industry = new Industry();
            /*   if(id is null)
               {
                   return null;
               }

               try
               {
                   var connectionString = _configuration.GetConnectionString("IdentityConnection");


                   using (SqlConnection connection = new SqlConnection(connectionString))
                   {
                       connection.Open();

                       String sql = $"Select Id, Industry_Name from [db_a6b7be_dev].[dbo].[tb_Industries] where id = {id}";

                       using (SqlCommand command = new SqlCommand(sql, connection))
                       {
                           using (SqlDataReader reader = await command.ExecuteReaderAsync())
                           {
                               while (reader.Read())
                               {
                                   industry = new Industry
                                   {
                                       Id = await reader.Get<int>("Id"),
                                       Industry_Name = await reader.Get<string>("Industry_Name"),
                                   };
                               }
                           }
                       }
                       connection.Close();
                   }
               }
               catch (SqlException e)
               {

               }*/

            return industry != null ? industry : null;
        }
    }
}
