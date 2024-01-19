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
    public class MatchDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "please Enter the Match Date")]
        public DateTime MatchDate { get; set; }
        [Required(ErrorMessage = "please Enter the Match Result")]
        public string Result { get; set; }
        [Required(ErrorMessage = "please Enter the Assigned Tourment Id")]
        public int TourmentId { get; set; }
        [Required(ErrorMessage = "please Enter the Home Team Id")]
        public int HomeTeamId { get; set; }
        [Required(ErrorMessage = "please Enter the Hosted Team Id")]
        public int HostedTeamId { get; set; }
    }
}
