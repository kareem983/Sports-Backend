using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Please Enter the Email")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage ="Please enter valid email formate")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter the Password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please Enter the Confirmed Password")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

    }
}
