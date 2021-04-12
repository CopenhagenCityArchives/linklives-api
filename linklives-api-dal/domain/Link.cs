using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class Link
    {
        public int link_id { get; set; }
        public string link_key { get; set; }
        public int iteration { get; set; }
        public int iteration_inner { get; set; }
        public int method_id { get; set; }
        public int pa_id1 { get; set; }
        public double score { get; set; }
        public int pa_id2 { get; set; }
        public int source_id1 { get; set; }
        public int source_id2 { get; set; }
        public string method_type { get; set; }
        public string method_subtype1 { get; set; }
        public string method_description { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
    }
}
