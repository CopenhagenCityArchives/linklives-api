﻿using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface ILinkRatingRepository
    {
        IEnumerable<LinkRating> GetAll();
        LinkRating GetByKey(int id);
        List<LinkRating> GetbyLinkKey(string linkKey);
        void Insert(LinkRating linkRating);
        void Insert(IEnumerable<LinkRating> linkRatings);
        void Delete(int id);
        void Save();
    }
}
