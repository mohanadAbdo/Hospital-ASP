using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Hospital.Models
{
    public  class ApplicationUser:IdentityUser {

        public string TheName {  get; set; }
        public string? Number { get; set; }
        public string? Region {  get; set; }
        public string? City { get; set; }
        
        

    
    } 
}
