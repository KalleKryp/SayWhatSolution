﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SayWhat.Models.ViewModels
{
    public class LyricsToplistVM
    {
        //public int Id { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
        public string Lyric1 { get; set; }
        public int? Rating { get; set; }
    }
}
