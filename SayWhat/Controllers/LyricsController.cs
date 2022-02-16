using Microsoft.AspNetCore.Mvc;
using SayWhat.Models;
using SayWhat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SayWhat.Controllers
{
    public class LyricsController : Controller
    {
        readonly LyricsService service;
        private readonly string title;

        public LyricsController(LyricsService service)
        {
            this.service = service;
            title = "Say What?! - The worlds weirdest lyrics ranked";
        }


        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            ViewBag.Title = title;
            var random = service.GetRandom();
            return View(random);
        }

        [Route("/submit")]
        [HttpGet]
        public IActionResult Submit()
        {
            ViewBag.Title = "Submit | " + title;

            return View();
        }


        [Route("/submit")]
        [HttpPost]
        public IActionResult Submit(LyricsSubmitVM lyrics)
        {
            if (!ModelState.IsValid)
                return View(lyrics);

            service.SubmitLyrics(lyrics);

            return RedirectToAction(nameof(Index));
        }


        [Route("/toplist")]
        public IActionResult Toplist()
        {
            ViewBag.Title = "Top list | " + title;

            var toplist = service.GetToplist();
            return View(toplist);
        }


        [Route("/admin")]
        public IActionResult Admin()
        {
            ViewBag.Title = "Admin | " + title;

            var all = service.GetAll();
            var model = new LyricsAdminVM
            {
                ListOfLyricsByLetters = all
            };
            return View(model);
        }



        [Route("/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit | " + title;

            var lyrics = service.GetLyricById(id);
            return View(lyrics);
        }


        [Route("/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(LyricsEditVM lyrics)
        {
            if (!ModelState.IsValid)
                return View(lyrics);

            service.EditLyrics(lyrics);

            return RedirectToAction("Admin");
        }

        [Route("/delete")]
        public IActionResult Delete(int id)
        {
            service.DeleteEntry(id);
            return RedirectToAction("Admin");
        }


        [Route("/rate")]
        public async Task<IActionResult> RateAsync(int id, int rating)
        {
            var newRating = service.Rate(id, rating);
            return Content(newRating.ToString());
        }

        [Route("/lyrics-box")]
        public IActionResult LyricsBox()
        {
            var random = service.GetRandom();
            return PartialView("_LyricsBox", random);
        }


        [Route("/test")]
        public IActionResult Test()
        {
            return View();
        }


    }
}
