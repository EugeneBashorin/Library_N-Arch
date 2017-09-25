using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Important field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage ="Important field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}