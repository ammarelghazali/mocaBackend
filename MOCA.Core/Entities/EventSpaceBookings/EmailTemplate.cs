
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EmailTemplate : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
        public int EmailTemplateTypeID { get; set; }

        [ForeignKey("EmailTemplateTypeID")]
        public EmailTemplateType EmailTemplateType { get; set; }
    }
}
