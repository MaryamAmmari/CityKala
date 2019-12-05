using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShop.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="نام کاربری")]
        public string UserName { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
    }
}
