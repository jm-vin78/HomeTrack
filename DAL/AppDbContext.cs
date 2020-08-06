using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<FlatEntity> Flats { get; set; }
        public DbSet<FlatUserEntity> FlatUsers { get; set; } 
        public DbSet<WaterReportEntity> WaterReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlatUserEntity>()
                .HasOne(fe => fe.Flat)
                .WithMany(f => f.FlatUsers)
                .HasForeignKey(fe => fe.FlatId);

            modelBuilder.Entity<FlatUserEntity>()
                .HasOne(fe => fe.User)
                .WithMany(f => f.FlatUsers)
                .HasForeignKey(fe => fe.UserId);

            modelBuilder.Entity<WaterReportEntity>()
                .HasOne(wr => wr.Flat)
                .WithMany(f => f.WaterReports)
                .HasForeignKey(wr => wr.FlatId);
        }
    }
}
