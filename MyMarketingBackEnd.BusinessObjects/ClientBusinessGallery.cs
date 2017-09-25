using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    /// <summary>
    /// Shouldn't have inherited ClientBusinessGallery from ClientBusiness
    /// </summary>
    public class ClientBusinessGallery
    {
        public int BusinessId { get; set; }

        public int GalleryId { get; set; }

        public string ImageName { get; set; }

        public DateTime UploadDate { get; set; }

        public bool IsActive { get; set; }
    }
}
