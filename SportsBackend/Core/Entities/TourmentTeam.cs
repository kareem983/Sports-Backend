using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TourmentTeam
    {
        public int Id { get; set; }
        public int TourmentId { get; set; }
        [ForeignKey(nameof(TourmentId))]
        public virtual Tourment? Tourment { get; set; }
        public int TeamId { get; set; }
        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }

    }
}
