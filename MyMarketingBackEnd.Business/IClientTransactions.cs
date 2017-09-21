using MyMarketingBackEnd.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.Business
{
    public interface IClientTransactions
    {
        bool CreateClient(ClientAuth clientObj);

        bool CreateClientBasicDetails(Client clientObj);

        bool CreateClientAuthDetails(ClientAuth clientAuthObj);

        bool UpdateClient(Client clientObj);

        ClientAuth GetClientDetails(int clientId);

        Dictionary<int, string> GetClientList();
    }
}
