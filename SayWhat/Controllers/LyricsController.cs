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
        LyricsService service;

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

    }
}
