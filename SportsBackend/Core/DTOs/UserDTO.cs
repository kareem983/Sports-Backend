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
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter the Unique UserName")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Please Enter the Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter the Password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please Enter the Confirmed Password")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

    }
}
