using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handee.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
          
        }
      
        public IActionResult Index()
        {
            
            if (HttpContext.Session.GetInt32("role") == 1)
            {

                var id = HttpContext.Session.GetInt32("AdminId");
                var xyz = _context.Userrs.Where(x => x.Userid == id).SingleOrDefault();
                return View(xyz);

            }
            else if (HttpContext.Session.GetInt32("role") == 2)
            {
                var id = HttpContext.Session.GetInt32("CustomerId");
                var xyz = _context.Userrs.Where(x => x.Userid == id).SingleOrDefault();
                return View(xyz);
            }
            else 
            {
                var id = HttpContext.Session.GetInt32("VendorId");
                var xyz = _context.Userrs.Where(x => x.Userid == id).SingleOrDefault();     
                return View(xyz);
            }
           

            var email= HttpContext.Session.GetString("email");
         
                                
       
        }
        public async Task<IActionResult> edit(decimal? id)
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
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userr.Roleid);
            return View(userr);
        }

        // POST: Userrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> edit(decimal id, [Bind("Userid,Fname,Lname,Imagepath,ImageFile,Email,Roleid")] Userr userr, string Usernsme, string Password)
        {
            if (id != userr.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (userr.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "" + userr.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/assets/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userr.ImageFile.CopyToAsync(fileStream);
                        }
                        userr.Imagepath = fileName;
                    }
                 
                    else
                    {
                        userr.Imagepath = _context.Userrs.Where(x => x.Userid == id).AsNoTracking<Userr>().SingleOrDefault().Imagepath;
                    }

                    _context.Update(userr);
                    await _context.SaveChangesAsync();
                    var logininfo = _context.Userlogins.Where(x => x.Userid == userr.Userid).SingleOrDefault();
                    logininfo.Usernsme = Usernsme;
                    logininfo.Password = Password;
                    _context.Update(logininfo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(edit));
                    
                     
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.Roles, "Roleid", "Roleid", userr.Roleid);
            return View(userr);
        }
        private bool UserrExists(decimal id)
        {
            return _context.Userrs.Any(e => e.Userid == id);
        }
        
    }
}
