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
        IEnumerable<LinkRating> GetLinkRatings();
        LinkRating GetLinkRatingByID(int linkRatingId);
        void InsertLinkRating(LinkRating linkRating);
        void DeleteLinkRating(int linkRatingId);
        void Save();
    }
}