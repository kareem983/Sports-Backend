using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class TourmentTeamDTO
    {
        public int TourmentId { get; set; }
        public int TeamId { get; set; }
    }
}
