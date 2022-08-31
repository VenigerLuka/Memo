using MemoProject.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MemoProject.Models.Memos;
using MemoProject.Models.Setting;

#nullable disable

namespace MemoProject.Data
{
    public partial class MemoDbContext : DbContext
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

                optionsBuilder.UseSqlServer("Name=ConnectionStrings:MemoDbConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      


        }

        public DbSet<MemoViewModel> MemoViewModel { get; set; }

        public DbSet<SettingViewModel> SettingViewModel { get; set; }


    }
}
