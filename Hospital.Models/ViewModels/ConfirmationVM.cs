
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models.ViewModels
{
    public class ConfirmationVM
    {
        public AppointmentHeader AppointmentHeader {  get; set; }
        public IEnumerable<AppointmentDetail> AppointmentDetail { get; set; }
    }
}
