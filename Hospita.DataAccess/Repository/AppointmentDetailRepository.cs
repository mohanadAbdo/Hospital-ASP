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
    public class AppointmentDetailRepository : Repository<AppointmentDetail>, IAppointmentDetailRepository
    {
        private ApplicationDbContext _db;
        public AppointmentDetailRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Update(AppointmentDetail obj)
        {
            _db.AppointmentDetails.Update(obj);
        }
    }
}
