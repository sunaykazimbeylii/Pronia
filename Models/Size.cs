using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Size:BaseEntity
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
