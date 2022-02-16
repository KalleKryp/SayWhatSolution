using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SayWhat.Models.Entities;

namespace SayWhat.Models.ViewModels
{
    public class LyricsAdminVM
    { 
        //public int Id { get; set; }
        //public string Artist { get; set; }
        //public string Song { get; set; }
        //public string Lyric1 { get; set; }
        //public double? Rating { get; set; }

        public List<ListOfLyricsByLetter> ListOfLyricsByLetters { get; set; }

    }

    public class ListOfLyricsByLetter
    {
        public string Letter { get; set; }
        public List<Lyric> LyricsByLetter { get; set; }
    }
}
