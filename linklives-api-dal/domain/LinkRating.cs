using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LinkRating
    {
        public int ratingId { get; set; }
        public bool rating { get; set; }
        public RatingDescription Description { get; set; }
    }

    public enum RatingDescription
    {
        accurate,
        notaccurate
    }
}
