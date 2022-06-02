using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BD_26.Models
{
    public class Login
    {
        [Display(Name = "email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        public string email { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}