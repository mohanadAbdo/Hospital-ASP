using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Repository.IRepository
{
    public interface IAppointmentHeaderRepository : IRepository<AppointmentHeader>
    {
        void Update(AppointmentHeader obj);
        void UpdateStatus(int id, string appointmentStatus, string? paymentStatus = null);

        void UpdateStripePaymentID(int id, string sessionId,string paymentIntentId);
    }
}
