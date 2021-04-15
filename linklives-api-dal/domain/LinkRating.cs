using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LinkRating
    {
        public int Id { get; set; }
        public bool Rating { get; set; }
        public RatingDescription Description { get; set; }
        public string LinkKey { get; set; }
        public virtual Link Link { get; set; }
    }

    public enum RatingDescription
    {
        accurate,
        notaccurate
    }
}
