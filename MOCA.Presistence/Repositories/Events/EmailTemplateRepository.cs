using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Presistence.Repositories.Events
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public EmailTemplateRepository(IConfiguration configuration,
                                              IAuthenticatedUserService authenticatedUser,
                                              IDateTimeService dateTime)
        {
            _configuration = configuration;
            _authenticatedUser = authenticatedUser;
            _dateTime = dateTime;
        }

        public async Task<EmailTemplate> AddAsync(EmailTemplate emailTemplate)
        {
            emailTemplate.CreatedAt = _dateTime.NowUtc;
            emailTemplate.UserId = _authenticatedUser.UserId;

            var madeEmailTemplate = new EmailTemplate();
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = $"insert into [db_a6b7be_dev].[dbo].[tb_EmailTemplate] (Subject, Body, UserId, ImagePath, EmailTemplateTypeID, CreatedAt) OUTPUT INSERTED.Id VALUES ('{emailTemplate.Subject}', '{emailTemplate.Body}', '{emailTemplate.UserId}', '{emailTemplate.ImagePath}', {emailTemplate.EmailTemplateTypeID}, '{emailTemplate.CreatedAt}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var reader = await command.ExecuteScalarAsync();

                        madeEmailTemplate = emailTemplate;
                        madeEmailTemplate.Id = (long)reader;

                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                return null;
            }

            return string.IsNullOrEmpty(madeEmailTemplate.UserId) ? null : madeEmailTemplate;
        }

        public async Task<EmailTemplate> GetLatestEmailTemplate(int emailTypeID)
        {
            var emailTemplate = new EmailTemplate();
            try
            {
                var connectionString = _configuration.GetConnectionString("IdentityConnection");


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"Select TOP 1 * from [db_a6b7be_dev].[dbo].[tb_EmailTemplate] as EMAIL WHERE EMAIL.EmailTemplateTypeID = {emailTypeID} ORDER BY CreatedAt desc";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                emailTemplate = new EmailTemplate()
                                {/*
                                    Id = await reader.Get<long>("Id"),
                                    Body = await reader.Get<string>("Body"),
                                    CreatedAt = await reader.Get<DateTime>("CreatedAt"),
                                    EmailTemplateTypeID = await reader.Get<int>("EmailTemplateTypeID"),
                                    ImagePath = await reader.Get<string>("ImagePath"),
                                    Subject = await reader.Get<string>("Subject"),
                                    UserId = await reader.Get<string>("UserId"),
                                */
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

            return string.IsNullOrEmpty(emailTemplate.UserId) ? null : emailTemplate;
        }
    }
}
