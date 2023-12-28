using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class AppointmentDetail
    {
        public int Id { get; set; }
        [Required]
        public int AppointmentHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public AppointmentHeader AppointmentHeader { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [ValidateNever]
        public Doctor Doctor { get; set; }

    }
}
