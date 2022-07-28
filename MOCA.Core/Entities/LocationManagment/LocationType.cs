using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.MocaSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<Faq> Faqs { get; set; }

        public ICollection<IssueCaseStage> IssueCaseStages { get; set; }

        public ICollection<IssueReport> IssueReports { get; set; }

        public ICollection<Plan> Plans { get; set; }
        public ICollection<Policy> Plocies { get; set; }
        public ICollection<TopUp> TopUps { get; set; }
        public ICollection<Wifi> Wifi { get; set; }

    }
}
