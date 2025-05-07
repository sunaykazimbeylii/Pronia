using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class CreateSlideVM
    {

        [MaxLength(100, ErrorMessage = "slide Title must be 100 characters or fewer")]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        [MaxLength(300, ErrorMessage = "slide description must be 300 characters or fewer")]
        public string Description { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "Order 1den az ola bilmez")]
        public int Order { get; set; }
        public IFormFile Photo { get; set; }
    }
}
