using MyMarketingBackEnd.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.Business
{
    public interface IBusinessTransactions
    {
        Dictionary<int, string> GetBusinessCategory();

        bool SaveBusinessDetails(ClientBusiness clientBixObj);

        bool UpdateBusinessDetails();

        bool UploadGallery();

        bool UploadLogo(ClientBusiness cbObject);

        List<ClientBusiness> GetBusinessDetails(int clientId);

    }
}
