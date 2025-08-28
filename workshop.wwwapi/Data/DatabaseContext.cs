using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public sealed class DatabaseContext : DbContext
    {
        private readonly string _connectionString;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection")!;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setup tables
            modelBuilder.Entity<Doctor>().ToTable("doctors");
            modelBuilder.Entity<Patient>().ToTable("patients");
            modelBuilder.Entity<Appointment>().ToTable("appointments");
            
            // Setup keys
            modelBuilder.Entity<Appointment>()
                .HasKey(appointment => new { appointment.DoctorId, appointment.PatientId });
            
            // Setup relations
            modelBuilder.Entity<Doctor>().HasMany(doctor => doctor.Appointments)
                .WithOne(appointment => appointment.Doctor)
                .HasForeignKey(doctor => doctor.DoctorId);
            
            modelBuilder.Entity<Patient>().HasMany(patient => patient.Appointments)
                .WithOne(appointment => appointment.Patient)
                .HasForeignKey(patient => patient.PatientId);
            
            // Setup seed
            var seeder = new Seeder();
            modelBuilder.Entity<Doctor>().HasData(seeder.Doctors);
            modelBuilder.Entity<Patient>().HasData(seeder.Patients);
            modelBuilder.Entity<Appointment>().HasData(seeder.Appointments);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}