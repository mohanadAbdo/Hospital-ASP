using Hospital.DataAccess.Data;
using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Repository
{
    public class UintOfWork : IUnitOfWork
    {
        public IAppointmentRepository Appointment { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IDoctorRepository Doctor { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        private ApplicationDbContext _db;
        public UintOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Category = new CategoryRepository(_db);
            Doctor = new DoctorRepository(_db);
            Appointment = new AppointmentRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
