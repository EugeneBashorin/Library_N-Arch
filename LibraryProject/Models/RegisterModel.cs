﻿using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Models
{
    public class RegisterModel
    {
        //[Required]
        //public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        //[DataType(DataType.Password)]
        //public string PasswordConfirm { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        //[Required]
        //public string Address { get; set; }
        [Required]
        public string Name { get; set; }
    }
}