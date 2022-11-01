using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Handee.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login([Bind("Usernsme , Password")] Userlogin userLogin)
        {
            var user = _context.Userlogins.Where(x => x.Usernsme == userLogin.Usernsme && x.Password == userLogin.Password).SingleOrDefault();
         

            if (user != null)
            {   var info = _context.Userrs.Where(x => x.Userid == user.Userid).SingleOrDefault();
                switch (user.Roleid)
                {
                    case 1: // Admin
                        HttpContext.Session.SetInt32("AdminId", (int)user.Userid);
                        HttpContext.Session.SetString("email", info.Email);
                        HttpContext.Session.SetString("name", info.Fname);
                        HttpContext.Session.SetString("image", info.Imagepath);
                        HttpContext.Session.SetInt32("role", (int)info.Roleid);
                        return RedirectToAction("tables", "Admin");
                    case 2: //Customer
                        HttpContext.Session.SetInt32("CustomerId", (int)user.Userid);
                        HttpContext.Session.SetString("email", info.Email);
                        HttpContext.Session.SetString("name", info.Fname);
                        HttpContext.Session.SetString("image", info.Imagepath);
                        HttpContext.Session.SetInt32("role", (int)info.Roleid);
                        return RedirectToAction("Index", "Home");
                    case 3: //Vendor
                        HttpContext.Session.SetInt32("VendorId", (int)user.Userid);
                        HttpContext.Session.SetString("email", info.Email);
                        HttpContext.Session.SetString("name", info.Fname);
                        HttpContext.Session.SetString("image", info.Imagepath);
                        HttpContext.Session.SetInt32("role", (int)info.Roleid);
                        return RedirectToAction("ProductTable", "Vendor");
                    case 4: //Not Accepted
                        return RedirectToAction("Wait", "Auth");
                }
            }
            ModelState.AddModelError("", "incorrect user name or password");
            return View();
        }

        public IActionResult reg()
        {
            return View();
        }
        public IActionResult Wait()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> reg([Bind("Userid,Fname,Lname,Imagepath,ImageFile,Email,Roleid")] Userr customer, string username, string password) // customer => fname , lname , imagefile
        {

            if (ModelState.IsValid)
            {
                if (customer.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = System.Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/assets/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await customer.ImageFile.CopyToAsync(filestream);
                    }
                    customer.Imagepath = imageName;
                    customer.Roleid = 2;
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    Userlogin user = new Userlogin
                    {
                        Usernsme = username,
                        Password = password,
                        Roleid = 2,
                        Userid = customer.Userid,

                    };

                    _context.Add(user);
                    await _context.SaveChangesAsync();


                }

                return RedirectToAction("login");
            }

            return View();
        }


        public IActionResult regVendor()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> regVendor([Bind("Userid,Fname,Lname,Imagepath,ImageFile,Email,Roleid")] Userr customer, string username, string password) // customer => fname , lname , imagefile
        {

            if (ModelState.IsValid)
            {
                if (customer.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = System.Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/assets/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await customer.ImageFile.CopyToAsync(filestream);
                    }
                    customer.Imagepath = imageName;
                    customer.Roleid = 4;
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    Userlogin user = new Userlogin
                    {
                        Usernsme = username,
                        Password = password,
                        Roleid = 4,
                        Userid = customer.Userid,

                    };

                    _context.Add(user);
                    await _context.SaveChangesAsync();


                }

                return RedirectToAction("login");
            }

            return View();
        }

    }
}
