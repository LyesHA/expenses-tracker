using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackYourExpensesApp.Data;
using TrackYourExpensesApp.Models;

namespace TrackYourExpensesApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static ApplicationUser applicationUser;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id.Equals(_userManager.GetUserId(User).ToString()));
            var user = _context.Users.Single(b => b.Id == applicationUser.Id);
            var category = _context.Entry(user).Collection(b => b.ListCategories).Query().Include(i => i.Expenses).ToList();
            // user.ListCategories = category;   

            if(applicationUser.ImageUrl!=null) HttpContext.Session.SetString("UserImg", applicationUser.ImageUrl.ToString());
            else HttpContext.Session.SetString("UserImg", "default.png");

            if(applicationUser.FirstName != null) HttpContext.Session.SetString("Name", applicationUser.FirstName);
            else HttpContext.Session.SetString("Name", "User");


            return View(category);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
