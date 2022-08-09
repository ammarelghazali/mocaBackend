using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.DynamicLists
{
    public class WorkSpaceTypeModel
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }


        public long WorkSpaceCategoryId { get; set; }

      //  public WorkSpaceCategory WorkSpaceCategory { get; set; }

       // public ICollection<WorkSpace> WorkSpaces { get; set; }

    }
}
