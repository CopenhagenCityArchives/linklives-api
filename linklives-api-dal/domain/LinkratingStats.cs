using linklives_api_dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LinkRatingStats
    {
        public LinkRatingStats()
        {
            HeadingRatings = new Dictionary<string, int>();
        }
        public Dictionary<string, int> HeadingRatings { get; set; }
        public int TotalRatings { get; set; }
    }
}
