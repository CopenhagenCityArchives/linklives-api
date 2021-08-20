using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace linklives_api_dal.domain
{
    public class LifeCourse : KeyedItem
    {
        public LifeCourse()
        {
            PersonAppearances = new List<dynamic>();
        }
        public int Life_course_id { get; set; }
        public ICollection<Link> Links { get; set; }
        [NotMapped]
        public List<dynamic> PersonAppearances {get; set;}
    }
}
