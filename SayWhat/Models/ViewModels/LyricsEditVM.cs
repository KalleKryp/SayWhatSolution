using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SayWhat.Models.ViewModels
{
    public class LyricsEditVM
    {
        public int Id { get; set; }

        [Display(Name = "Name of artist or group")]
        [Required(ErrorMessage = "Must enter an artist or group.")]
        public string Artist { get; set; }

        [Display(Name = "Name of song")]
        public string Song { get; set; }


        [Display(Name = "Weird, bad or confusing lyrics")]
        [Required(ErrorMessage = "Must enter lyrics!")]
        public string Lyric1 { get; set; }

        [Display(Name = "What is 2 +2?")]
        [Required(ErrorMessage = "Answer required.")]
        [Range(4, 4)]
        public int BotCheck { get; set; }
    }
}
