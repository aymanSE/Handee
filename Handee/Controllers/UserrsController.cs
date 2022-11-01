using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Handee.Models;

namespace Handee.Controllers
{
    public class UserrsController : Controller
    {
        private readonly ModelContext _context;

        public UserrsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userrs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Userrs.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userrs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userr = await _context.Userrs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userr == null)
            {
                return NotFound();
            }

            return View(userr);
        }

        // GET: Userrs/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid");
            return View();
        }

        // POST: Userrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fname,Lname,Imagepath,Email,Roleid")] Userr userr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userr.Roleid);
            return View(userr);
        }

        // GET: Userrs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userr = await _context.Userrs.FindAsync(id);
            if (userr == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", userr.Roleid);
            return View(userr);
        }

        // POST: Userrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fname,Lname,Imagepath,Email,Roleid")] Userr userr)
        {
            
            if (id != userr.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {  
                    var log = _context.Userlogins.Where(x => x.Userid == id).SingleOrDefault();
                    log.Roleid = userr.Roleid;
                    _context.Update(log);
                    _context.Update(userr);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserrExists(userr.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("tables", "Admin");
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Rolename", userr.Roleid);
            return View(userr);
        }

        // GET: Userrs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userr = await _context.Userrs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userr == null)
            {
                return NotFound();
            }

            return View(userr);
        }

        // POST: Userrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userr = await _context.Userrs.FindAsync(id);
            _context.Userrs.Remove(userr);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserrExists(decimal id)
        {
            return _context.Userrs.Any(e => e.Userid == id);
        }
    }
}
