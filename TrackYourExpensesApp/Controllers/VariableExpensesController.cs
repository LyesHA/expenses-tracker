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
    public class VariableExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private static ApplicationUser applicationUser;

        public VariableExpensesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VariableExpenses
        public async Task<IActionResult> Index()
        {
            applicationUser = await _context.Users.SingleOrDefaultAsync(m => m.Id.Equals(_userManager.GetUserId(User).ToString()));
            var user = _context.Users.Single(b => b.Id == applicationUser.Id);
            var category = _context.Entry(user).Collection(b => b.ListCategories).Query().Include(i => i.Expenses).ToList();
            return View(category);
        }

        // GET: VariableExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variableExpenses = await _context.VariableExpenses
                .Include(v => v.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (variableExpenses == null)
            {
                return NotFound();
            }

            return View(variableExpenses);
        }

        // GET: VariableExpenses/Create
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: VariableExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Amount,TransactionDate,CategoriesId")] VariableExpenses variableExpenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variableExpenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", variableExpenses.CategoriesId);
            return View(variableExpenses);
        }

        // GET: VariableExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variableExpenses = await _context.VariableExpenses.SingleOrDefaultAsync(m => m.Id == id);
            if (variableExpenses == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", variableExpenses.CategoriesId);
            return View(variableExpenses);
        }

        // POST: VariableExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Amount,TransactionDate,CategoriesId")] VariableExpenses variableExpenses)
        {
            if (id != variableExpenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variableExpenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariableExpensesExists(variableExpenses.Id))
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
            ViewData["CategoriesId"] = new SelectList(_context.Categories, "Id", "Name", variableExpenses.CategoriesId);
            return View(variableExpenses);
        }

        // GET: VariableExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variableExpenses = await _context.VariableExpenses
                .Include(v => v.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (variableExpenses == null)
            {
                return NotFound();
            }

            return View(variableExpenses);
        }

        // POST: VariableExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variableExpenses = await _context.VariableExpenses.SingleOrDefaultAsync(m => m.Id == id);
            _context.VariableExpenses.Remove(variableExpenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariableExpensesExists(int id)
        {
            return _context.VariableExpenses.Any(e => e.Id == id);
        }
    }
}
