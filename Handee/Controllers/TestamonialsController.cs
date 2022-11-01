using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Handee.Models;
using Microsoft.AspNetCore.Http;

namespace Handee.Controllers
{
    public class TestamonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestamonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Testamonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Testamonials.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Testamonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testamonial = await _context.Testamonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Tid == id);
            if (testamonial == null)
            {
                return NotFound();
            }

            return View(testamonial);
        }

        // GET: Testamonials/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Fname");
            return View();
        }

        // POST: Testamonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Note,Tid")] Testamonial testamonial)
        {
            if (ModelState.IsValid)
            {
                testamonial.Userid = HttpContext.Session.GetInt32("CustomerId");
                _context.Add(testamonial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Fname", testamonial.Userid);
            return View(testamonial);
        }

        // GET: Testamonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testamonial = await _context.Testamonials.FindAsync(id);
            if (testamonial == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Userid", testamonial.Userid);
            return View(testamonial);
        }

        // POST: Testamonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Note,Tid,Userid")] Testamonial testamonial)
        {
            if (id != testamonial.Tid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testamonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestamonialExists(testamonial.Tid))
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
            ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Userid", testamonial.Userid);
            return View(testamonial);
        }

        // GET: Testamonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testamonial = await _context.Testamonials
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Tid == id);
            if (testamonial == null)
            {
                return NotFound();
            }

            return View(testamonial);
        }

        // POST: Testamonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var testamonial = await _context.Testamonials.FindAsync(id);
            _context.Testamonials.Remove(testamonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestamonialExists(decimal id)
        {
            return _context.Testamonials.Any(e => e.Tid == id);
        }
    }
}
