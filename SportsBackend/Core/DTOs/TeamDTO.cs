using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public  class TeamDTO
    {
        [Required(ErrorMessage ="please Enter the Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please Enter the Description")]
        public string Description { get; set; }
        public string? Logo { get; set; }
        public string? Website { get; set; }
        [Required(ErrorMessage = "please Enter the Foundation Date")]
        public DateTime FoundationDate { get; set; }
    }
}
