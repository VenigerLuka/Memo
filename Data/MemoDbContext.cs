using MemoProject.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MemoProject.Models.Memo;

#nullable disable

namespace MemoProject.Data
{
    public partial class MemoDbContext : IdentityDbContext<IdentityUser, IdentityRole<string>, string>
    {
        public MemoDbContext()
        {
        }

        public MemoDbContext(DbContextOptions<MemoDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Memo> Memo { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            base.OnModelCreating(modelBuilder);


        }

        public DbSet<MemoProject.Models.Memo.MemoViewModel> MemoViewModel { get; set; }


    }
}
