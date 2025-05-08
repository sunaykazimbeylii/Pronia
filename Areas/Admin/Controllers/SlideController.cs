using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;
using Pronia.Utilities.Enums;
using Pronia.Utilities.Extensions;
using Pronia.ViewModels;
using Pronia.ViewModels.Slides;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller

    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlideController( AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetSlideVM> slideVMs = await _context.Slides.Select(s=>
         
                new GetSlideVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    Image = s.Image,
                    CreatedAt = s.CreatedAt,
                    Order = s.Order,

                }
            ).ToListAsync();

            return View(slideVMs);
        }
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlideVM slideVM)
        {
            if (!slideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File type is incorrect");
                return View();
            }
            if (!slideVM.Photo.ValidateSize(FileSize.MB,2))
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Photo), "File size sould be less than 2MB");
                return View();
            }
            bool result = await _context.Slides.AnyAsync(s => s.Order == slideVM.Order);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateSlideVM.Order), $"{slideVM.Order} This order value already exists");
                return View();
            }
           string fileName =   await slideVM.Photo.CreateFileAsync(_env.WebRootPath,"assets","images","website-images");
            Slide slide = new Slide
            {
                Title=slideVM.Title,
                SubTitle=slideVM.SubTitle,
                Description=slideVM.Description,
                Order=slideVM.Order,
                Image=fileName,
                CreatedAt=DateTime.Now
            };

            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slide? slide= await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null) return NotFound();
            slide.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            _context.Remove(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();
            Slide? slide = await _context.Slides.FirstOrDefaultAsync(s => s.Id == id);
            if (slide is null) return NotFound();
           UpdateSlideVM slideVM = new UpdateSlideVM
           {
               Description = slide.Description,
               Title = slide.Title,
               Order = slide.Order,
               SubTitle = slide.SubTitle,
               Image= slide.Image,
           };
            return View(slideVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,UpdateSlideVM slideVM)
        {
            if (!ModelState.IsValid) return View(slideVM);
            Slide? existed=await _context.Slides.FirstOrDefaultAsync(s=>s.Id==id);
            if (existed is null) return NotFound();
            if(slideVM.Photo is not null) { 
            if (!slideVM.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File type is incorrect");
                return View(slideVM);
            }
            if (!slideVM.Photo.ValidateSize(FileSize.MB,2))
            {
                ModelState.AddModelError(nameof(UpdateSlideVM.Photo), "File size must be less than 2MB");
                return View(slideVM);

            }
            string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images", "website-images");
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "images", "website-images");
            existed.Image = fileName;
            }
            existed.Title=slideVM.Title;
            existed.SubTitle=slideVM.SubTitle;  
            existed.Description=slideVM.Description;
            existed.Order=slideVM.Order;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
