using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Category:BaseEntity
    {
        [MinLength(3,ErrorMessage ="sehvsen")]
        [MaxLength(30)]
        public string Name { get; set; }
        public List<Product>? Products{ get; set;}
    }
}
