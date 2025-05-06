using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.ViewModels;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;
        public HomeController( AppDbContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            List<Slide> slides = await _context.Slides.ToListAsync();
            
            //_context.Slides.AddRange(slides);
            //_context.SaveChanges();
            HomeVM homeVM= new HomeVM
            {
                Slides=await _context.Slides
                .OrderBy(s=>s.Order)
                .Take(4)
                .ToListAsync(),
                Products=await _context.Products
                .Take(8)
                .Include(p => p.ProductImages.Where(pi=>pi.IsPrimary!=null))
                .ToListAsync(),
            };
            return View(homeVM);
        }
    }
}
