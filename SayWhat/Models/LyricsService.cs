using SayWhat.Models.Entities;
using SayWhat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SayWhat.Models
{
    public class LyricsService
    {
        private MyContext context;
        Random random = new Random();

        public LyricsService(MyContext context)
        {
            this.context = context;
        }

        public LyricsIndexVM GetRandom()
        {
            var random = context.Lyrics.OrderBy(x => Guid.NewGuid()).Take(1).First();
            return new LyricsIndexVM
            {
                Artist = random.Artist,
                Song = random.Song,
                Lyric1 = random.Lyric1,
                Rating = random.Rating
            };
        }

        internal void SubmitLyrics(LyricsSubmitVM viewModel)
        {
            context.Lyrics.Add(new Lyric
            {
                Artist = viewModel.Artist,
                Song = viewModel.Song,
                Lyric1 = viewModel.Lyric1

            });
            context.SaveChanges();
        }

        internal LyricsToplistVM[] GetToplist()
        {
            return context.Lyrics.Select(l => new LyricsToplistVM
            {
                Artist = l.Artist,
                Song = l.Song,
                Lyric1 = l.Lyric1,
                Rating = l.Rating
            }).OrderBy(l => l.Rating).ToArray();
        }
    }
}
