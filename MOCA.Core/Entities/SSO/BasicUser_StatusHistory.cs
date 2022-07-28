using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.SSO
{
    public class BasicUser_StatusHistory : BaseEntity
    {

        [Required]
        public long BasicUserId { get; set; }
        public BasicUser BasicUser { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }


    }
}
