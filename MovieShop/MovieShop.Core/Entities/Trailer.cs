﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MovieShop.Core.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        public Movie Movie { get; set; }
    }
}
