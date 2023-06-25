using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Reservation>()
        //         .HasOne(r => r.User)
        //         .WithMany(u => u.Reservations)
        //         .HasForeignKey(u => u.User)
        //         .IsRequired(false);

        //     modelBuilder.Entity<Vehicle>()
        //         .Property(v => v.Id)
        //         .ValueGeneratedOnAdd();

        //     modelBuilder.Entity<Reservation>()
        //         .HasOne(r => r.Vehicle)
        //         .WithOne(v => v.Reservation)
        //         .HasForeignKey<Vehicle>(v => v.Id)
        //         .IsRequired(false);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}