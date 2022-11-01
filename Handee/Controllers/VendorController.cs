using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handee.Controllers
{
    public class VendorController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VendorController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult ProductTable()
           { 
        // var xyz1 = _context.Userrs.Where(x=>x.Userid == HttpContext.Session.GetInt32("VendorId")).SingleOrDefault();
           var xyz = _context.Products.Where(x => x.Userid == HttpContext.Session.GetInt32("VendorId")).ToList();
         //  var all = Tuple.Create<Userr, IEnumerable<Handee.Models.Product>>(xyz1, xyz);
            return View(xyz);
            }
         public IActionResult CreateProduct()
        {
            //var xyz1 = _context.Userrs.Where(x=>x.Userid == HttpContext.Session.GetInt32("VendorId")).SingleOrDefault();

            ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Userid");
            ViewData["Catid"] = new SelectList(_context.Catigories, "Catid", "Catname");


            return View();
        }

      
        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateProduct([Bind("Productid,Name,Price,Imagepath,ImageFile,Catid")] Product product)
        {

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = System.Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/assets/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(filestream);
                    }
                    product.Imagepath = imageName;
                    product.Userid= HttpContext.Session.GetInt32("VendorId");
                    product.Isavilable = true;
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    

                }
                return RedirectToAction(nameof(CreateProduct));
                }
                ViewData["Userid"] = new SelectList(_context.Userrs, "Userid", "Userid", product.Userid);
                ViewData["Catid"] = new SelectList(_context.Catigories, "Catid", "Catname", product.Catid);
            
            return View(product);
            }
        

    }
}
