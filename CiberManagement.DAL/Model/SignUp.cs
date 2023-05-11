using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberManagement.DAL.Model
{
    public class SignUp
    {
        [Required(ErrorMessage = "The field is required")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "The field is required")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "The field is required"), EmailAddress]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage ="The field is required")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "The field is required")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
