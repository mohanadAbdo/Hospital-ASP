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

        public void UpdateStatus(int id, string appointmentStatus, string? paymentStatus = null)
        {
            var appointmentFromDb  = _db.AppointmentHeaders.FirstOrDefault(u => u.Id == id);
            if (appointmentFromDb != null)
            {
                appointmentFromDb.ApoointmentStatus = appointmentStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    appointmentFromDb.PaymentStatus = paymentStatus;
                }
            }

        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var appointmentFromDb = _db.AppointmentHeaders.FirstOrDefault(u => u.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {
                appointmentFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                appointmentFromDb.PaymentIntentId = paymentIntentId;
                appointmentFromDb.PaymentDate = DateTime.Now;
            }
        }
    }
}
