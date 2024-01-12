using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage ="Please Enter the UserName")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Please Enter the Password")]
        public string? Password { get; set; }

    }
}
