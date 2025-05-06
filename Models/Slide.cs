using Pronia.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia.Models
{
    public class Slide:BaseEntity
    {
        [MaxLength(100, ErrorMessage = "slide Title must be 100 characters or fewer")]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        [MaxLength(300,ErrorMessage ="slide description must be 300 characters or fewer")]
        public string Description { get; set; }
        public string Image{ get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Order 1den az ola bilmez")]
        public int Order {  get; set; }
        [NotMapped]
        public IFormFile Photo {  get; set; }

    }

}
