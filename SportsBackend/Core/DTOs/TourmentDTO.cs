using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TourmentDTO
    {
        public TourmentDTO()
        {
            TeamsIds = new List<int>();
        }

        public int? Id { get; set; }
        [Required(ErrorMessage = "please Enter the Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please Enter the Description")]
        public string Description { get; set; }
        public string? Logo { get; set; }
        public List<int>? TeamsIds { get; set; }

    }
}
