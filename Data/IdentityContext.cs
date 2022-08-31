using MemoProject.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemoProject.Data
{
    public partial class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole<string>, string>
    {
        public IdentityContext()
        {
        }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Name=ConnectionStrings:IdentityDbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            base.OnModelCreating(modelBuilder);


        }


    }
}

