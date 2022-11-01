using Aspose.Pdf;
using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Handee.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

      /*  public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
*/
        public IActionResult Index()
        {
            var catigories = _context.Catigories.ToList();
            var product = _context.Products.ToList();
            var tests = _context.Testamonials.OrderByDescending(x=> x.Tid).Take(3).ToList();
            var user =_context.Userrs.ToList();
            var all = Tuple.Create<IEnumerable<Catigory>,IEnumerable<Product>, IEnumerable<Testamonial>, IEnumerable<Userr>>(catigories, product,tests,user);
            return View(all);
            //
        }

        public IActionResult Privacy()
        {
            return View();

        }
        public IActionResult Product(int id, string name)
        {   if (id != 0 && name == null)
            {
                var xyz = _context.Products.Where(x => x.Catid == id&&x.Isavilable==true).ToList();
                return View(xyz);
            }
           else if (id != 0 && name != null)
            {
                var xyz = _context.Products.Where(x => x.Catid == id&& x.Name.Contains(name) && x.Isavilable == true).ToList();
                return View(xyz);
            }
            else if (id == 0&&name!=null) { 
                var xyz = _context.Products.Where(x => x.Name.Contains(name) && x.Isavilable == true).ToList();
            return View(xyz);
            }

            else
            {
                var xyz = _context.Products.Where(x=>  x.Isavilable == true).ToList();
                return View(xyz);
            }

          
        }
        public IActionResult Pay(int id)
        {
            var xyz = _context.Products.Where(x => x.Productid==id).SingleOrDefault();
            return View(xyz);

           

        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult aboutcshtml()
        {
            var xyz = _context.Abouts.FirstOrDefault();

            return View(xyz);
        }
        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Note,Hid,Imagepath")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contract);
        }

        public IActionResult Transction (int idproduct,string pin ,string pass ,string expire)
        {
            string time = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");
            
            var chickVisa = _context.Visas.Where(x => x.Pin == pin && x.Pass == pass && x.Exp == expire).FirstOrDefault();

            if (chickVisa != null)
            {
                var price = _context.Products.Where(x => x.Productid == idproduct).SingleOrDefault().Price;

                //update balance from price

                chickVisa.Balance = chickVisa.Balance - price;
               
                Document document = new Document(); 
                     Page page = document.Pages.Add();
              // v 22.10.10
                     Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                     table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Purple));
                     // Add text to new page
                     table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Green));
                     //var invoice = _context.OrdersdoneFps.Where(x => x.UserFk == custId).ToList();
                     var card1 = _context.Products.Where(x => x.Productid == idproduct).SingleOrDefault();
                var sale = new Sale
                {
                    Datesold = DateTime.Now.Date,
                    Price= card1?.Price,
                    Amount=1,
                    Productid=idproduct,
                    Userid = HttpContext.Session.GetInt32("CustomerId")
                };
                  page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Handee"));
                     page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                     page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("------------------------------------------------------------------------------------------------------------------------"));
                     page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
                       page.ArtBox.Center();
                
 
                    {
                         // Add row to table
                         Aspose.Pdf.Row row = table.Rows.Add();
                         // Add table cells
                         row.Cells.Add("The Date Of Purchase");
                         row.Cells.Add("Product Name");
                         row.Cells.Add("Product Price");
                       

                     }

                //for multible products
                {
                    Aspose.Pdf.Row row = table.Rows.Add();
                    // Add table cells
                    row.Cells.Add(DateTime.Now.ToString());
                    row.Cells.Add(card1.Name.ToString());
                    row.Cells.Add("$"+card1.Price.ToString());
                    
                }

                    page.Paragraphs.Add(table);
                     //var sum = _context.OrdersFps.Where(x => x.UserFk == custId).Sum(x => x.HandcraftFkNavigation.Price);

                     page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("------------------------------------------------------------------------------------------------------------------------"));
                 
                     page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Thank you for shopping, Handee "));
                //num == rand to save docu 
                     document.Save(time + " document.pdf");
                     SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
                     string email=  HttpContext.Session.GetString("email");
                     MailMessage mail = new MailMessage();
                     SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com", 587);
                     mail.From = new MailAddress("");
                     mail.To.Add(email);
                     mail.Subject = "Purchase Invoice";
                 mail.Body = "Ayman";
                     smtp.Credentials = new NetworkCredential("****", "***");
                     Attachment data = new Attachment(time + " document.pdf");
                     smtp.EnableSsl = true;
   
                     mail.Attachments.Add(data);
                     smtp.Send(mail);

                   
                     {      //remove item product
                    card1.Isavilable = false;
                    _context.Update(card1);
                         _context.SaveChangesAsync();
                    _context.Add(sale);
                    _context.SaveChangesAsync();
                }
                /* */
                return RedirectToAction(nameof(Product));
            }

             return RedirectToAction(nameof(Product));


        }

     



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
