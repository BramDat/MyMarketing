using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "FirstName is Mandatory Field")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Mandatory Field")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "BusinessName is Mandatory Field")]
        [MaxLength(200)]
        public string BusinessName { get; set; }

        [Required(ErrorMessage = "Address is Mandatory Field")]
        [MaxLength(400)]
        public string PrimaryAddress { get; set; }

        [Required(ErrorMessage = "Primary Phone number is Mandatory Field")]
        public long PrimaryPhoneNum { get; set; }

        public long AltPhoneNum { get; set; }

        [Required(ErrorMessage = "Primary eMail Address is Mandatory Field")]
        public string PrimaryMail { get; set; }

        public string FacebookId { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
