using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.ViewModels.Products;

namespace Pronia.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<GetProductVM> productVMs = await _context.Products.Select(p => new GetProductVM
            {
                Id = p.Id,
                Name = p.Name,
                SKU = p.SKU,
                Price= p.Price,
                CategoryName=p.Category.Name,
                MainImage=p.ProductImages.FirstOrDefault(pi=>pi.IsPrimary==true).Image
            }).ToListAsync();
            return View(productVMs);
        }
    }
}
