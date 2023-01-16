using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TouchSystems.ViewModel
{
    public class ContactFromVM
    {
        public string Token { get; set; }
        public double Threshold { get; set; }

        [MaxLength(64)]
        [Required]
        public string Name { get; set; }
        [MaxLength(64)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(64)]
        [Required]
        public string PhoneNumber { get; set; }
        [MaxLength(64)]
        [Required]
        public string CompanyName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MaxLength(10000)]
        [Required]
        public string Message { get; set; }
        [MaxLength(64)]
        [Required]
        public string Country { get; set; }
        public string Product { get; set; }
    }
}
