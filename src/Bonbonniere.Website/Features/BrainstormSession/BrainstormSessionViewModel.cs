﻿using System;

namespace Bonbonniere.Website.Features.BrainstormSession
{
    public class BrainstormSessionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public int IdeaCount { get; set; }
    }
}
