using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public string? Website { get; set; }
        public DateTime? FoundationDate { get; set; }

        //public virtual List<Player> Players { get; set; }

    }
}
