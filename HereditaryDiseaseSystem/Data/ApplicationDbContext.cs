using HereditaryDiseaseSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HereditaryDiseaseSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Gene> Genes { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Phenotype> Phenotypes { get; set; }

        public DbSet<GenePhenotype> GenePhenotypes { get; set; }
        public DbSet<GeneDisease> GeneDiseases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GenePhenotype>()
                .HasKey(gp => new { gp.GeneId, gp.PhenotypeId });

            builder.Entity<GeneDisease>()
                .HasKey(gd => new { gd.GeneId, gd.DiseaseId });
        }
    }
}
