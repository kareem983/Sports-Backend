using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SportsContext : IdentityDbContext<ApplicationUser>
    {
        public SportsContext(DbContextOptions<SportsContext> options): base(options)
        {
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TourmentTeam>()
                .HasKey(p => new { p.TourmentId, p.TeamId});
        }

        public virtual DbSet<Tourment> Tourments { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TourmentTeam> TourmentTeams { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Match> Matches { get; set; }

    }
}
