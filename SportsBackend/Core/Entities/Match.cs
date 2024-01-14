using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime? MatchDate { get; set; }
        public string Result { get; set; }
        public int TourmentId { get; set; }
        [ForeignKey(nameof(TourmentId))]
        public virtual Tourment Tourment { get; set; }
        public int HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; }
        public int HostedTeamId { get; set; }
        [ForeignKey(nameof(HostedTeamId))]
        public virtual Team HostedTeam { get; set; }

    }
}
