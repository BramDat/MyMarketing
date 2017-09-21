using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyMarketingBackEnd.WebApp.Models;
using MyMarketingBackEnd.BusinessObjects;

namespace MyMarketingBackEnd.WebApp.App_Code
{
    public class ViewModelManager
    {
        public static ClientVM ConvertClientToClientVM(ClientAuth clientObj)
        {
            ClientVM clientVMObj = new ClientVM();
            Mapper.CreateMap<ClientAuth, ClientVM>();
            clientVMObj = Mapper.Map<ClientVM>(clientObj);
            return clientVMObj;
        }
    }
}