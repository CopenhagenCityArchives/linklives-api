using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class LinkRating
    {
        public int Id { get; set; }
        [DataMember, Required(AllowEmptyStrings = false, ErrorMessage = "Must have valid rating id")]
        public int RatingId { get; set; }
        [DataMember()]
        public virtual RatingOption Rating { get; set; }
        [DataMember, Required(AllowEmptyStrings = false, ErrorMessage = "Must have valid link key")]
        public string LinkKey { get; set; }
        [DataMember, Required(AllowEmptyStrings = false, ErrorMessage = "Must have valid user id")]
        public string User { get; set; }
    }
}
