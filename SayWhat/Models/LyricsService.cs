using SayWhat.Models.Entities;
using SayWhat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Globalization.CultureInfo;

namespace SayWhat.Models
{
    public class LyricsService
    {
        private MyContext context;

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
            List<char> endings = new List<char> { '.', '?', '!' };
            string tempLyric;
            if (endings.Any(e => viewModel.Lyric1.EndsWith(e)))
                tempLyric = viewModel.Lyric1;
            else
                tempLyric = $"{viewModel.Lyric1}.";

            context.Lyrics.Add(new Lyric
            {
                Artist = viewModel.Artist,
                Song = viewModel.Song,
                Lyric1 = tempLyric
            });
            context.SaveChanges();
        }

        internal LyricsAdminVM[] GetAll()
        {
            return context.Lyrics.Select(l => new LyricsAdminVM
            {
                Id = l.Id,
                Artist = l.Artist,
                Song = l.Song,
                Lyric1 = l.Lyric1,
            }).OrderBy(l => l.Artist).ToArray();
        }

        internal object GetLyricById(int id)
        {
            return context.Lyrics.FirstOrDefault(l => l.Id == id);
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

        internal void EditLyrics(LyricsEditVM viewModel)
        {
            var lyrics = context.Lyrics.FirstOrDefault(l => l.Id == viewModel.Id);
            lyrics.Artist = viewModel.Artist;
            lyrics.Song = viewModel.Song;
            lyrics.Lyric1 = viewModel.Lyric1;
        }


        //string CapitalizeFirst(this string s)
        //{
        //    bool IsNewSentense = true;
        //    var result = new StringBuilder(s.Length);
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        if (IsNewSentense && char.IsLetter(s[i]))
        //        {
        //            result.Append(char.ToUpper(s[i]));
        //            IsNewSentense = false;
        //        }
        //        else
        //            result.Append(s[i]);

        //        if (s[i] == '!' || s[i] == '?' || s[i] == '.')
        //        {
        //            IsNewSentense = true;
        //        }
        //    }

        //    return result.ToString();
        //}
    }
}
