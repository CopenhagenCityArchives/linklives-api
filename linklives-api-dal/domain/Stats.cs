﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linklives_api_dal.domain
{
    public class Stats
    {
        public long EsPersonAppearanceCount { get; set; }
        public long EsLifecourseCount { get; set; }
        public long EsLinkCount { get; set; }
        public long EsSourceCount { get; set; }
        public long DbLifecourseCount { get; set; }
        public long DbLinkCount { get; set; }
        public long DbLinkRatingCount { get; set; }
        public long LifecourseDiff { get { return DbLifecourseCount - EsLifecourseCount; } }
        public long Linkdiff { get { return DbLinkCount - EsLinkCount; } }

    }
}
