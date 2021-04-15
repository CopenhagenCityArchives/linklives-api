using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LifeCourse
    {
        public int Life_course_id { get; set; }
        public string Key { get; set; }
        public virtual ICollection<Link> Links { get; set; }
    }
}
