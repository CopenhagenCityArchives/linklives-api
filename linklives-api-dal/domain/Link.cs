using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class Link
    {
        public int Link_id { get; set; }
        public string Key { get; set; }
        public int Iteration { get; set; }
        public int Iteration_inner { get; set; }
        public int Method_id { get; set; }
        public int Pa_id1 { get; set; }
        public double Score { get; set; }
        public int Pa_id2 { get; set; }
        public int Source_id1 { get; set; }
        public int Source_id2 { get; set; }
        public string Method_type { get; set; }
        public string Method_subtype1 { get; set; }
        public string Method_description { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
    }
}
