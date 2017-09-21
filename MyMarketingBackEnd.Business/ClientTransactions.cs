﻿using MyMarketingBackEnd.BusinessObjects;
using MyMarketingBackEnd.Common;
using MyMarketingBackEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MyMarketingBackEnd.Business
{
    public class ClientTransactions : IClientTransactions
    {
        ClientDataAccess ClientDA = new ClientDataAccess();

        public bool CreateClient(ClientAuth clientObj)
        {
            bool retFlag = default(bool);

            using (TransactionScope transScope = new TransactionScope())
            {
                try
                {
                    if (CreateClientBasicDetails(clientObj))
                    {
                        if (CreateClientAuthDetails(clientObj))
                        {
                            transScope.Complete();
                            retFlag = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transScope.Dispose();
                    throw ex;
                }
                finally
                {
                    transScope.Dispose();
                }
            }

            return retFlag;
        }

        public bool CreateClientBasicDetails(Client clientObj)
        {
            bool retFlag = default(bool);

            StringBuilder insertQuery = new StringBuilder("INSERT INTO [dbo].[Client] ([ClientFirstName],[ClientLastName],[BusinessName],[ClientPrimaryAddress],[PrimaryContactNumber],[AlternateContactNumber],[PrimaryMailId],[FacebookId],[CreatedDate],[IsActive])");
            insertQuery.Append("VALUES(@ClientFirstName,@ClientLastName,@BusinessName,@ClientPrimaryAddress,@PrimaryContactNumber,@AlternateContactNumber,@PrimaryMailId,@FacebookId,@CreatedDate,@IsActive)");
            insertQuery.Append(";SELECT CAST(scope_identity() AS int);");

            clientObj.ClientId = ClientDA.AddNewClient(clientObj, insertQuery.ToString());

            retFlag = (clientObj.ClientId > 0) ? true : false;

            return retFlag;
        }

        public bool CreateClientAuthDetails(ClientAuth clientAuthObj)
        {
            bool retFlag = default(bool);

            StringBuilder insertQuery = new StringBuilder("INSERT INTO [dbo].[ClientAuthentication] (ClientId, ClientUserName, ClientPassword, IsActive, DateOfRegistration, DateOfExpire, LastLoginTime) ");
            insertQuery.Append("VALUES (@ClientId, @ClientUserName, @ClientPassword, @IsActive, @DateOfRegistration, @DateOfExpire, @LastLoginTime);SELECT CAST(scope_identity() AS int);");

            retFlag = (ClientDA.AddClientAuthDetails(clientAuthObj, insertQuery.ToString()) > 0) ? true : false;

            return retFlag;
        }

        public bool UpdateClient(Client clientObj)
        {
            bool retFlag = default(bool);



            return retFlag;
        }

        public ClientAuth GetClientDetails(int clientId)
        {
            ClientAuth clientObj = new ClientAuth();

            StringBuilder sb = new StringBuilder("SELECT CL.*,CLAU.* FROM CLIENT CL JOIN CLIENTAUTHENTICATION CLAU ON CL.CLIENTID=CLAU.CLIENTID WHERE CL.CLIENTID = @ClientId");

            if (ClientDA.GetClientDetails(clientId, sb.ToString(), clientObj))
                return clientObj;
            else
                return null;
        }

        public Dictionary<int, string> GetClientList()
        {
            Dictionary<int, string> clientList = new Dictionary<int, string>();
            StringBuilder sb = new StringBuilder("SELECT ClientId, ClientFirstName, ClientLastName FROM CLIENT");

            if (ClientDA.GetClientList(sb.ToString(), clientList))
                return clientList;
            else
            {
                clientList.Add(0, "No Clients Found");
                return clientList;
            }
        }

    }
}
