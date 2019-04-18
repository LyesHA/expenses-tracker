using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrackYourExpensesApp.Data;
using TrackYourExpensesApp.Models;

namespace TrackYourExpensesApp.Controllers
{
    public class FixedExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static ApplicationUser applicationUser;

        public FixedExpensesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: FixedExpenses
        public async Task<IActionResult> Index()
        {
            applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id.Equals(_userManager.GetUserId(User).ToString()));
            var user = _context.Users.Single(b => b.Id == applicationUser.Id);
            var category = _context.Entry(user).Collection(b => b.ListCategories).Query().Include(i => i.Expenses).ToList();
            return View(category);
        }

        // GET: FixedExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedExpenses = await _context.FixedExpenses
                .Include(f => f.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixedExpenses == null)
            {
                return NotFound();
            }

            return View(fixedExpenses);
        }

        // GET: FixedExpenses/Create
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: FixedExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Frequence,ByDate,Id,Title,Amount,TransactionDate,CategoriesId")] FixedExpenses fixedExpenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fixedExpenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", fixedExpenses.CategoriesId);
            return View(fixedExpenses);
        }

        // GET: FixedExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedExpenses = await _context.FixedExpenses.SingleOrDefaultAsync(m => m.Id == id);
            if (fixedExpenses == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", fixedExpenses.CategoriesId);
            return View(fixedExpenses);
        }

        // POST: FixedExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Frequence,ByDate,Id,Title,Amount,TransactionDate,CategoriesId")] FixedExpenses fixedExpenses)
        {
            if (id != fixedExpenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixedExpenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixedExpensesExists(fixedExpenses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", fixedExpenses.CategoriesId);
            return View(fixedExpenses);
        }

        // GET: FixedExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixedExpenses = await _context.FixedExpenses
                .Include(f => f.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixedExpenses == null)
            {
                return NotFound();
            }

            return View(fixedExpenses);
        }

        // POST: FixedExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixedExpenses = await _context.FixedExpenses.SingleOrDefaultAsync(m => m.Id == id);
            _context.FixedExpenses.Remove(fixedExpenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixedExpensesExists(int id)
        {
            return _context.FixedExpenses.Any(e => e.Id == id);
        }
    }
}
