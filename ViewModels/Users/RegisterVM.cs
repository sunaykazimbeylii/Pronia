﻿using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class RegisterVM
    {
        [MinLength(3)]
        [MaxLength(50)]
        public  string Name { get; set; }
        public  string Surname { get; set; }
        [MaxLength(100)]
        public  string UserName { get; set; }
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email  { get; set; }
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
