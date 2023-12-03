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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private ApplicationDbContext _db;
        public DoctorRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public void Update(Doctor obj)
        {
            var objFromDb = _db.Doctors.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.DoctorName = obj.DoctorName;
                objFromDb.CategoryId = obj.CategoryId;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
