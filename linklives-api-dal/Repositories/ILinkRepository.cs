﻿using linklives_api_dal.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.Repositories
{
    public interface ILinkRepository
    {
        IEnumerable<Link> GetLinks();
        Link GetLinkByID(string linkKey);
        void InsertLink(Link link);
        void InsertLinks(IEnumerable<Link> links);
        void DeleteLink(string linkKey);
        void Save();
    }
}
