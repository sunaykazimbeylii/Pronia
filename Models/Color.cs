using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Color:BaseEntity
    {
        [MaxLength(50)]
        public string Name{ get; set; }
    }
}
