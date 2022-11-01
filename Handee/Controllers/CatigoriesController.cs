using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Handee.Controllers
{
    public class CatigoriesController : Controller
    {
        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CatigoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            
            _webHostEnvironment = webHostEnvironment;
        
    }

        // GET: Catigories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catigories.ToListAsync());
        }

        // GET: Catigories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catigory = await _context.Catigories
                .FirstOrDefaultAsync(m => m.Catid == id);
            if (catigory == null)
            {
                return NotFound();
            }

            return View(catigory);
        }

        // GET: Catigories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catigories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Catid,Catname,Imagepath,ImageFile")] Catigory catigory)
        {
            if (ModelState.IsValid)
            {
                if (catigory.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = System.Guid.NewGuid().ToString() + "_" + catigory.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/assets/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await catigory.ImageFile.CopyToAsync(filestream);
                    }
                    catigory.Imagepath = imageName;

                    _context.Add(catigory);
                    await _context.SaveChangesAsync();


                }
                return RedirectToAction(nameof(Index));
            }
           
            ViewData["Catid"] = new SelectList(_context.Catigories, "Catid", "Catid", catigory.Catid);

            return View(catigory);
        }

        // GET: Catigories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catigory = await _context.Catigories.FindAsync(id);
            if (catigory == null)
            {
                return NotFound();
            }
            return View(catigory);
        }

        // POST: Catigories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Catid,Catname")] Catigory catigory)
        {
            if (id != catigory.Catid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catigory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatigoryExists(catigory.Catid))
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
            return View(catigory);
        }

        // GET: Catigories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catigory = await _context.Catigories
                .FirstOrDefaultAsync(m => m.Catid == id);
            if (catigory == null)
            {
                return NotFound();
            }

            return View(catigory);
        }

        // POST: Catigories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var catigory = await _context.Catigories.FindAsync(id);
            _context.Catigories.Remove(catigory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatigoryExists(decimal id)
        {
            return _context.Catigories.Any(e => e.Catid == id);
        }
    }
}
