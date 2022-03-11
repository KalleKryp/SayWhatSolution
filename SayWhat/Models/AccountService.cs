using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SayWhat.Models.ViewModels;

namespace SayWhat.Models
{
    public class AccountService
    {
        //readonly UserManager<MyIdentityUser> userManager;
        //readonly SignInManager<MyIdentityUser> signInManager;

        //public AccountService(
        //    UserManager<MyIdentityUser> userManager,
        //    SignInManager<MyIdentityUser> signInManager)
        //{
        //    this.userManager = userManager;
        //    this.signInManager = signInManager;
        //}

        //public async Task<bool> TryRegisterAsync(AccountRegisterVM viewModel)
        //{
        //    var result = await userManager.CreateAsync(
        //        new MyIdentityUser { UserName = viewModel.Username },
        //        viewModel.Password);

        //    return result.Succeeded;
        //}

        public async Task<bool> TryLoginAsync(LyricsLoginVM viewModel)
        {
            //var result = await signInManager.PasswordSignInAsync(
            //    viewModel.Username, viewModel.Password, false, false);

            //return result.Succeeded;
            return viewModel.Username == "admin";
        }
    }
}
