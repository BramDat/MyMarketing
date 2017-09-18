using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMarketingBackEnd.DataAccess;
using MyMarketingBackEnd.BusinessObjects;

namespace MyMarketingBackEnd.Business
{
    public class BusinessTransactions : IBusinessTransactions, IDisposable
    {
        private ClientDataAccess clientDA;
        private GenericDataReader genericDRSingleton;

        public BusinessTransactions()
        {
            clientDA = new ClientDataAccess();
            genericDRSingleton = GenericDataReader.GetInstance();
        }

        public Dictionary<int, string> GetBusinessCategory()
        {
            Dictionary<int, string> bizCatList = new Dictionary<int, string>();
            bizCatList = genericDRSingleton.BusinessCategoryList;
            return bizCatList;
        }

        public Dictionary<int, string> GetPayPeriods()
        {
            Dictionary<int, string> payPeriodList = new Dictionary<int, string>();
            payPeriodList = genericDRSingleton.PayPeriodList;
            return payPeriodList;
        }

        public bool SaveBusinessDetails(ClientBusiness clientBixObj)
        {
            bool returnFlag = default(bool);


            try
            {
                StringBuilder insertQuery = new StringBuilder("INSERT INTO [dbo].[ClientBusinessDetails]([ClientId],[BusinessCategoryTypeId],[BusinessSubCategoryType],[PayPeriodTypeId],[BusinessHours],[IsPremiumCustomer],[NegotiatedPrice],[IsBulkDataReceived],[IsMobileNumberToBePublicAccess],[LocationLongitude],[LocationLattitude],[CreatedDate],[IsActive],[BusinessDescription],[BusinessGalleryPath],[BusinessWebSite],[BusinessLogoPath],[BusinessSubCategoryNativeType])");
                insertQuery.Append("VALUES(@ClientId,@BusinessCategoryTypeId,@BusinessSubCategoryType,@PayPeriodTypeId,@BusinessHours,@IsPremiumCustomer,@NegotiatedPrice,@IsBulkDataReceived,@IsMobileNumberToBePublicAccess,@LocationLongitude,@LocationLattitude,@CreatedDate,@IsActive,@BusinessDescription,@BusinessGalleryPath,@BusinessWebSite,@BusinessLogoPath,@BusinessSubCategoryNativeType);");
                insertQuery.Append("SELECT CAST(scope_identity() AS int);");

                returnFlag = (clientDA.AddClientBizDetails(clientBixObj, insertQuery.ToString()) > 0) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnFlag;
        }

        public bool UpdateBusinessDetails()
        {
            throw new NotImplementedException();
        }

        public bool UploadLogo()
        {
            throw new NotImplementedException();
        }

        public bool UploadGallery()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
