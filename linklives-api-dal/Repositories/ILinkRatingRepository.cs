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
        int Count();
        IEnumerable<LinkRating> GetAll();
        LinkRating GetById(int id);
        List<LinkRating> GetbyLinkKey(string linkKey);
        void Insert(LinkRating linkRating);
        void Insert(IEnumerable<LinkRating> linkRatings);
        void Delete(int id);
        void Save();
    }
}
