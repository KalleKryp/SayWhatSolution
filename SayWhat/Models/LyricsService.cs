using SayWhat.Models.Entities;
using SayWhat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Globalization.CultureInfo;
//using System.Data;
//using System.Web.Services;
//using System.Configuration;
//using System.Data.SqlClient;

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
            var random = context.Lyrics.OrderBy(x => Guid.NewGuid()).First();
            return new LyricsIndexVM
            {
                Id = random.Id,
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

        internal List<ListOfLyricsByLetter> GetAll()
        {
            var listOfLyricsByLetters = new List<ListOfLyricsByLetter>();
            string alphabet = "abcdefghijklmnopqrstuvwxyzåäö";


            var all = context.Lyrics.Select(l => new Lyric()
            {
                Id = l.Id,
                Artist = l.Artist,
                Song = l.Song,
                Lyric1 = l.Lyric1,
                Rating = l.Rating
            }).OrderBy(l => l.Artist).ToList();

            //var balancedLists = all.GroupBy(x => x.Artist[0]).Select(x => x.ToList())
            //    .ToList();

            foreach (char letter in alphabet)
            {
                var selection = all.Where(l => l.Artist.ToLower().StartsWith(letter)).ToList();

                if (selection.Any())
                {
                    all.RemoveAll(l => l.Artist.ToLower().StartsWith(letter));

                    listOfLyricsByLetters.Add(new ListOfLyricsByLetter
                    {
                        Letter = letter.ToString().ToUpper(),
                        LyricsByLetter = selection

                    });
                }
            }

            if (all.Any())
            {
                listOfLyricsByLetters.Add(new ListOfLyricsByLetter
                {
                    Letter = "#",
                    LyricsByLetter = all
                });
            }

            return listOfLyricsByLetters;
        }

        internal LyricsEditVM GetLyricById(int id)
        {
            var temp = context.Lyrics.FirstOrDefault(l => l.Id == id);
            return new LyricsEditVM
            {
                Id = temp.Id,
                Artist = temp.Artist,
                Song = temp.Song,
                Lyric1 = temp.Lyric1,
            };
        }

        internal LyricsToplistVM[] GetToplist()
        {
            return context.Lyrics.Select(l => new LyricsToplistVM
            {
                Artist = l.Artist,
                Song = l.Song,
                Lyric1 = l.Lyric1,
                Rating = l.Rating
            }).OrderByDescending(l => l.Rating).Take(10).ToArray();
        }

        internal void RatingUp(int id)
        {
            var lyrics = context.Lyrics.FirstOrDefault(l => l.Id == id);

            if (lyrics.Rating == null)
                lyrics.Rating = 0;

            lyrics.Rating += 1;

            context.SaveChanges();

        }

        internal void RatingDown(int id)
        {
            var lyrics = context.Lyrics.FirstOrDefault(l => l.Id == id);

            if (lyrics.Rating == null)
                lyrics.Rating = 0;

            lyrics.Rating -= 1;

            context.SaveChanges();

        }

        internal void DeleteEntry(int id)
        {
            var lyricsToDelete = context.Lyrics.FirstOrDefault(l => l.Id == id);
            context.Lyrics.Remove(lyricsToDelete);

            context.SaveChanges();
        }

        internal void EditLyrics(LyricsEditVM viewModel)
        {
            var lyrics = context.Lyrics.FirstOrDefault(l => l.Id == viewModel.Id);
            lyrics.Artist = viewModel.Artist;
            lyrics.Song = viewModel.Song;
            lyrics.Lyric1 = viewModel.Lyric1;

            context.SaveChanges();
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
        public double Rate(int id, int rating)
        {
            var lyrics = context.Lyrics.FirstOrDefault(l => l.Id == id);

            if (lyrics.NrOfVotes == null)
                lyrics.NrOfVotes = 1;
            else
                lyrics.NrOfVotes++;

            if (lyrics.Rating == null)
                lyrics.Rating = rating;
            else
                lyrics.Rating = ((lyrics.Rating * lyrics.NrOfVotes) + rating) / (lyrics.NrOfVotes + 1);

            lyrics.Rating = Math.Round((double)lyrics.Rating, 1, MidpointRounding.ToEven);


            context.SaveChanges();



            return (double)lyrics.Rating;

        }
    }
}
