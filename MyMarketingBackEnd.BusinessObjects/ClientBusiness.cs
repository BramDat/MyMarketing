using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    public class ClientBusiness
    {
        public int ClientId { get; set; }

        public int BizId { get; set; }

        public int BizCategoryId { get; set; }

        public int BizStartHours { get; set; }

        public int BizEndHours { get; set; }

        public int PayPeriodId { get; set; }

        public string BizSubCategoryType { get; set; }

        public string BizSubCategoryNativeType { get; set; }

        [Required(ErrorMessage = "Description Is Mandatory Field")]
        public string BizDescription { get; set; }

        public string BizWebSite { get; set; }

        public string BizGalleryPath { get; set; }

        public string BizLogoPath { get; set; }

        public string BizStartWeekDay { get; set; }

        public string BizEndWeekDay { get; set; }

        [Required(ErrorMessage = "Negotiated Price Is Mandatory Field")]
        public decimal NegotiatedPrice { get; set; }

        public bool IsPremiumBiz { get; set; }

        public bool IsBuldDataReceived { get; set; }

        public bool IsPhonePublic { get; set; }

        public bool IsActive { get; set; }

        public decimal GeoLatitude { get; set; }

        public decimal GeoLongitude { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
