using Hospital.DataAccess.Data;
using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Repository
{
    public class AppointmentHeaderRepository : Repository<AppointmentHeader>, IAppointmentHeaderRepository
    {
        private ApplicationDbContext _db;
        public AppointmentHeaderRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Update(AppointmentHeader obj)
        {
            _db.AppointmentHeaders.Update(obj);
        }
    }
}
