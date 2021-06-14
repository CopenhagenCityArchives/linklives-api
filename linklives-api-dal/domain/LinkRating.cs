using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LinkRating : KeyedItem
    {
        public int RatingId { get; set; }
        public virtual RatingOption Rating { get; set; }
        public string LinkKey { get; set; }
    }
}
