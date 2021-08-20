using System.Collections.Generic;

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
