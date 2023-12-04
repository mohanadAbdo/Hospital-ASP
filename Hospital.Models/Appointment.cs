using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]

        public string ApllicationUserID { get; set; }
        [ForeignKey("ApllicationUserID")]
        [ValidateNever]
        public ApplicationUser ApllicationUser { get; set; }

    }
}
