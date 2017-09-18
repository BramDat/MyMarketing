using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.BusinessObjects
{
    public class ClientBusinessGallery : ClientBusiness
    {
        public int GalleryId { get; set; }

        public string ImageName { get; set; }

        public DateTime UploadDate { get; set; }

        public bool IsActive { get; set; }
    }
}
