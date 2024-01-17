using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MatchDTO
    {
        public int? Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string Result { get; set; }
        public int TourmentId { get; set; }
        public int HomeTeamId { get; set; }
        public int HostedTeamId { get; set; }
    }
}
