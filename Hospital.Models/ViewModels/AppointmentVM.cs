using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models.ViewModels
{
    public class AppointmentVM
    {
        public IEnumerable<Appointment> AppointmentList { get; set; }  
        public AppointmentHeader AppointmentHeader { get; set; }
    }
}
