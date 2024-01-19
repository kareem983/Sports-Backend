using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class PlayerDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "please Enter the First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "please Enter the Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "please Enter the Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "please Enter the Assigned Team Id")]
        public int TeamId { get; set; }
    }
}
