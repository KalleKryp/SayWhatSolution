using System;
using System.Collections.Generic;

#nullable disable

namespace SayWhat.Models.Entities
{
    public class Lyric
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
        public string Lyric1 { get; set; }
        public double? Rating { get; set; }
        public int? NrOfVotes { get; set; }
    }
}
