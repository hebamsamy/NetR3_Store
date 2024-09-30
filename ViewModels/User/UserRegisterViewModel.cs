using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserRegisterViewModel
    {
        [Required, StringLength(10, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required, StringLength(10, MinimumLength = 8)]
        public string UserName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(10, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(10, MinimumLength = 8)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string NationalID { get; set; } = "00000000000000";

        public string Role { get; set; } = "User";
    }
}
