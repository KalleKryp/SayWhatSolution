using Microsoft.AspNetCore.Mvc;
using SayWhat.Models;
using SayWhat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SayWhat.Controllers
{
    public class LyricsController : Controller
    {
        readonly LyricsService lyricsService;
        private readonly AccountService accountService;
        private readonly string title;

        public LyricsController(LyricsService LyricsService, AccountService accountService)
        {
            this.lyricsService = LyricsService;
            this.accountService = accountService;
            title = "Say What?! - The worlds weirdest lyrics ranked";
        }


        [Route("")]
        [Route("/index")]
        public IActionResult Index()
        {
            ViewBag.Title = title;
            var random = lyricsService.GetRandom();
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

            lyricsService.SubmitLyrics(lyrics);

            return RedirectToAction(nameof(Index));
        }


        [Route("/toplist")]
        public IActionResult Toplist()
        {
            ViewBag.Title = "Top list | " + title;

            var toplist = lyricsService.GetToplist();
            return View(toplist);
        }

        [Authorize]
        [Route("/admin")]
        public IActionResult Admin()
        {
            ViewBag.Title = "Admin | " + title;

            var all = lyricsService.GetAll();
            var model = new LyricsAdminVM
            {
                ListOfLyricsByLetters = all
            };
            return View(model);
        }


        [HttpGet]
        [Route("/login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.Title = "Login | " + title;

            return View(new LyricsLoginVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> LoginAsync(LyricsLoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            // Check if credentials is valid (and set auth cookie)
            var success = await accountService.TryLoginAsync(viewModel);
            if (!success)
            {
                // Show error
                ModelState.AddModelError(nameof(LyricsLoginVM.Username), "Login failed");
                return View(viewModel);
            }
            
            return RedirectToAction(nameof(Admin));

        }




        [Authorize]
        [Route("/edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit | " + title;

            var lyrics = lyricsService.GetLyricById(id);
            return View(lyrics);
        }


        [Authorize]
        [Route("/edit/{id}")]
        [HttpPost]
        public IActionResult Edit(LyricsEditVM lyrics)
        {
            if (!ModelState.IsValid)
                return View(lyrics);

            lyricsService.EditLyrics(lyrics);

            return RedirectToAction("Admin");
        }

        [Authorize]
        [Route("/delete")]
        public IActionResult Delete(int id)
        {
            lyricsService.DeleteEntry(id);
            return RedirectToAction("Admin");
        }


        [Route("/rate")]
        public async Task<IActionResult> RateAsync(int id, int rating)
        {
            var newRating = lyricsService.Rate(id, rating);
            return Content(newRating.ToString());
        }

        [Route("/lyrics-box")]
        public IActionResult LyricsBox()
        {
            var random = lyricsService.GetRandom();
            return PartialView("_LyricsBox", random);
        }


        [Route("/test")]
        public IActionResult Test()
        {
            return View();
        }


    }
}
