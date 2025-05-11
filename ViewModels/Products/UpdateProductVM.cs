﻿using Pronia.Models;
using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class UpdateProductVM
    {
        public string Name { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public string PrimaryImage { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
