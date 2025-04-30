namespace Pronia.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        List<Category> Categories { get; set;}
    }
}
