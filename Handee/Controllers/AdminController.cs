using Handee.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handee.Controllers
{//ayman
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var xyz = _context.Userrs.OrderByDescending(x=>x.Roleid).ToList();
            var xyz1 = _context.Roles.ToList();
            var xyz3 = _context.Contracts.ToList();
            var all =Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Role>, IEnumerable<Handee.Models.Contract>>(xyz, xyz1, xyz3);
            return View(all);
        }
        [HttpPost]
        public async Task<IActionResult> tables(DateTime? startDate, DateTime? endDate)
        {
            //_context.Products.ToListAsync();
            var result = _context.Sales;
            if (startDate == null && endDate == null)

            {
                var res = await result.ToListAsync();
                var xyz = _context.Userrs.ToList();
                var xyz1 = _context.Products.ToList();
                var all = Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Product>, IEnumerable<Handee.Models.Sale>>(xyz, xyz1, res);

                return View(all);
            }
            else if (startDate == null && endDate != null)
            {
                var xyz = _context.Userrs.ToList();
                var xyz1 = _context.Products.ToList();
                var res = await result.Where(x => x.Datesold == endDate).ToListAsync();
                var all = Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Product>, IEnumerable<Handee.Models.Sale>>(xyz, xyz1, res);

                return View(all);

            }
            else if (startDate != null && endDate == null)
            {
                var xyz = _context.Userrs.ToList();
                var xyz1 = _context.Products.ToList();
                var res = await result.Where(x => x.Datesold.Value.Date == startDate).ToListAsync();
                var all = Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Product>, IEnumerable<Handee.Models.Sale>>(xyz, xyz1, res);
                return View(all);

            }
            else
            {
                var xyz = _context.Userrs.ToList();
                var xyz1 = _context.Products.ToList();
                
                var res = await result.Where(x => x.Datesold   >= startDate && x.Datesold <= endDate).ToListAsync();
                var date = res.FirstOrDefault().Datesold;
                var all = Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Product>, IEnumerable<Handee.Models.Sale>>(xyz, xyz1, res);
                return View(all);

            }
        }

        public IActionResult tables()
        {   
            var xyz = _context.Userrs.ToList();
            var xyz1 = _context.Products.ToList();
            var xyz2 = _context.Sales.ToList();
          

            var all = Tuple.Create<IEnumerable<Handee.Models.Userr>, IEnumerable<Handee.Models.Product>, IEnumerable<Handee.Models.Sale>>(xyz, xyz1,xyz2);




            return View(all);

        }

    }
}
