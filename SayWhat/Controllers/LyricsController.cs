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

        public LyricsController(LyricsService service)
        {
            this.service = service;
        }


        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            var random = service.GetRandom();
            return View(random);
        }

        [Route("/submit")]
        [HttpGet]
        public IActionResult Submit()
        {
            return View();
        }


        //En action - svarar på ett HTTP-anrop
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
            var toplist = service.GetToplist();
            return View(toplist);
        }


        [Route("/admin")]
        public IActionResult Admin()
        {
            var all = service.GetAll();
            return View(all);
        }



        [Route("/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lyrics = service.GetLyricById(id);
            return View(lyrics);
        }


        //En action - svarar på ett HTTP-anrop
        [Route("/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(LyricsEditVM lyrics)
        {
            if (!ModelState.IsValid)
                return View(lyrics);

            service.EditLyrics(lyrics);

            return RedirectToAction("Admin");
        }

        [Route("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            service.DeleteEntry(id);
            return RedirectToAction("Admin");
        }


        [Route("/ratingup/{id}")]
        public IActionResult RatingUp(int id)
        {
            service.RatingUp(id);
            return RedirectToAction(nameof(Index));
        }


        [Route("/ratingdown/{id}")]
        public IActionResult RatingDown(int id)
        {
            service.RatingDown(id);
            return RedirectToAction(nameof(Index));
        }


        [Route("/lyrics-box")]
        public IActionResult LyricsBox()
        {
            var random = service.GetRandom();
            return PartialView("_LyricsBox", random);
        }

    }
}
