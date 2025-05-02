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
            List<Slide> slides = new List<Slide>
            {
                new Slide
            {
                Title="NEW PLANT",
                SubTitle="65% OFF",
                Description="Pronia, With 100% Natural, Organic &amp; Plant Shop.",
                Order=2,
                Image="1-1-524x617.png",
                CreatedAt=DateTime.Now
            },
                   new Slide
            {
                Title="NEW PLANT",
                SubTitle="65% OFF",
                Description="Pronia, With 100% Natural, Organic &amp; Plant Shop.",
                Order=3,
                Image="1-2-524x617.png",
                CreatedAt=DateTime.Now
            },
                      new Slide
            {
                Title="NEW PLANT",
                SubTitle="65% OFF",
                Description="Pronia, With 100% Natural, Organic &amp; Plant Shop.",
                Order=1,
                Image="1-3-270x300.jpg",
                CreatedAt=DateTime.Now
            }
            };
            _context.Slides.AddRange(slides);
            _context.SaveChanges();
            HomeVM homeVM= new HomeVM
            {
                Slides=await _context.Slides
                .OrderBy(s=>s.Order)
                .Take(2)
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
