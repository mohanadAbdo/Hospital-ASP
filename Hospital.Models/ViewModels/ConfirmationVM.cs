
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models.ViewModels
{
    public class ConfirmationVM
    {
        public AppointmentHeader appointmentHeader {  get; set; }
        public IEnumerable<AppointmentDetail> appointmentDetail { get; set; }
    }
}
