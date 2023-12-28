using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class AppointmentHeader
    {
        public int Id { get; set; }
        
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? ApoointmentStatus { get; set; }

        [Required]
        public int Name { get; set; }
        [Required]
        public string TheName { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Region { get; set; }
        [Required]
        public string? Number { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        public string? PaymentStatus { get; set; }



    }
}
