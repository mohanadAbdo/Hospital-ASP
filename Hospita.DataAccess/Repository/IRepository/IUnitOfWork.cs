using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IDoctorRepository Doctor { get; }
        IAppointmentRepository Appointment { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
