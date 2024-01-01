
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Hospital.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AppointmentHeader> AppointmentHeaders { get; set; }
        public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "aslcn", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Acticmlsaon", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Doctor>().HasData(

                new Doctor
                {
                    Id = 1,
                    DoctorName = "Mohanad",
                    CategoryId = 1,
                    ImageUrl = ""
                },
                   new Doctor
                   {
                       Id = 2,
                       DoctorName = "Mohanad2",
                       CategoryId = 1,
                       ImageUrl = ""
                   }

                );
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
            new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "123", TheName = "Admin", UserName = "Admin", Email = "B201210561@sakarya.edu.tr",
                    NormalizedUserName = "B201210561@sakarya.edu.tr", EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "sau"),City="Sakarya"
                },

            }

                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = "072b142a-c7ef-4503-b22b-984cc00461cf", UserId = "123",
                },

        }
);

        }
    }
}
