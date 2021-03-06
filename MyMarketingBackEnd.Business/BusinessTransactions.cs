﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMarketingBackEnd.DataAccess;
using MyMarketingBackEnd.BusinessObjects;
using System.Transactions;

namespace MyMarketingBackEnd.Business
{
    public class BusinessTransactions : IBusinessTransactions
    {
        private ClientDataAccess clientDA;
        private GenericDataReader genericDRSingleton;

        //public delegate bool DeletePhysicalImage(int clientId, string imageName);

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

        public List<ClientBusiness> GetBusinessDetails(int clientId)
        {
            List<ClientBusiness> cbObj = new List<ClientBusiness>();

            StringBuilder sb = new StringBuilder("SELECT B.ClientBusinessDetailId, B.ClientId,B.BusinessCategoryTypeId,B.BusinessSubCategoryType,B.PayPeriodTypeId,B.BusinessHours,B.IsPremiumCustomer,");
            sb.Append("B.NegotiatedPrice,B.IsBulkDataReceived,B.IsMobileNumberToBePublicAccess,B.LocationLongitude,B.LocationLattitude,B.CreatedDate,");
            sb.Append("B.IsActive,B.BusinessDescription,B.BusinessGalleryPath,B.BusinessWebSite,B.BusinessLogoPath,B.BusinessSubCategoryNativeType,");
            sb.Append("BG.GalleryId, BG.ImageName, BG.UploadedDate FROM ClientBusinessDetails B LEFT OUTER JOIN ClientBusinessGallery BG ON ");
            sb.Append("B.ClientBusinessDetailId=BG.ClientBusinessDetailId WHERE B.ClientId=@ClientId");

            if (clientDA.GetFullBusinessData(clientId, sb.ToString(), cbObj))
                return cbObj;
            else
                return null;
        }

        public Dictionary<int, string> GetBusinessList()
        {
            Dictionary<int, string> bizList = new Dictionary<int, string>();

            StringBuilder sb = new StringBuilder("SELECT CB.ClientBusinessDetailId, C.BusinessName FROM Client C JOIN ClientBusinessDetails CB ON C.ClientId = CB.ClientId");

            bizList = clientDA.GetBusinessList(sb.ToString());

            return bizList;
        }

        public Dictionary<int, string> GetBusinessList(int ClientId)
        {
            Dictionary<int, string> bizList = new Dictionary<int, string>();

            StringBuilder sb = new StringBuilder("SELECT CB.ClientBusinessDetailId, C.BusinessName FROM Client C JOIN ClientBusinessDetails CB ON C.ClientId = CB.ClientId  WHERE C.ClientId = @ClientId");

            bizList = clientDA.GetBusinessList(ClientId, sb.ToString());

            return bizList;
        }

        public bool SaveBusinessDetails(ClientBusiness clientBixObj)
        {
            bool returnFlag = default(bool);


            try
            {
                StringBuilder insertQuery = new StringBuilder("INSERT INTO [dbo].[ClientBusinessDetails]([ClientId],[BusinessCategoryTypeId],[BusinessSubCategoryType],[PayPeriodTypeId],[BusinessHours],[IsPremiumCustomer],[NegotiatedPrice],[IsBulkDataReceived],[IsMobileNumberToBePublicAccess],[LocationLongitude],[LocationLattitude],[CreatedDate],[IsActive],[BusinessDescription],[BusinessGalleryPath],[BusinessWebSite],[BusinessLogoPath],[BusinessSubCategoryNativeType])");
                insertQuery.Append("VALUES(@ClientId,@BusinessCategoryTypeId,@BusinessSubCategoryType,@PayPeriodTypeId,@BusinessHours,@IsPremiumCustomer,@NegotiatedPrice,@IsBulkDataReceived,@IsMobileNumberToBePublicAccess,@LocationLongitude,@LocationLattitude,@CreatedDate,@IsActive,@BusinessDescription,@BusinessGalleryPath,@BusinessWebSite,@BusinessLogoPath,@BusinessSubCategoryNativeType);");
                insertQuery.Append("SELECT CAST(scope_identity() AS int);");

                clientBixObj.BizId = clientDA.AddClientBizDetails(clientBixObj, insertQuery.ToString());

                returnFlag = (clientBixObj.BizId > 0) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnFlag;
        }

        public bool UpdateBusinessDetails(ClientBusiness clientBizObj)
        {
            try
            {
                StringBuilder updateQuery = new StringBuilder("UPDATE [dbo].[ClientBusinessDetails] SET [ClientId] = @ClientId, [BusinessCategoryTypeId] = @BusinessCategoryTypeId, ");
                updateQuery.Append("[BusinessSubCategoryType] = @BusinessSubCategoryType, [PayPeriodTypeId] = @PayPeriodTypeId, ");
                updateQuery.Append("[BusinessHours] = @BusinessHours, [IsPremiumCustomer] = @IsPremiumCustomer, [NegotiatedPrice] = @NegotiatedPrice, ");
                updateQuery.Append("[IsBulkDataReceived] = @IsBulkDataReceived, [IsMobileNumberToBePublicAccess] = @IsMobileNumberToBePublicAccess, ");
                updateQuery.Append("[LocationLongitude] = @LocationLongitude, [LocationLattitude] = @LocationLattitude, [CreatedDate] = @CreatedDate, ");
                updateQuery.Append("[IsActive] = @IsActive, [BusinessDescription] = @BusinessDescription, [BusinessGalleryPath] = @BusinessGalleryPath, ");
                updateQuery.Append("[BusinessWebSite] = @BusinessWebSite, [BusinessLogoPath] = @BusinessLogoPath, ");
                updateQuery.Append("[BusinessSubCategoryNativeType] = @BusinessSubCategoryNativeType WHERE ClientBusinessDetailId = @ClientBusinessDetailId");

                if (clientDA.UpdateBusinessDetails(clientBizObj, updateQuery.ToString()))
                    return true;
                else
                    throw new Exception("Some error occured while saving business data to DB");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UploadLogo(ClientBusiness cbObject)
        {

            try
            {
                StringBuilder updateQuery = new StringBuilder("UPDATE ClientBusinessDetails SET BusinessLogoPath=@LogoPath WHERE ClientBusinessDetailId=@ClientBusinessDetailId");
                if (clientDA.AddLogoDetails(cbObject, updateQuery.ToString()))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UploadGallery(ClientBusiness cbObject, string[] fileList)
        {
            StringBuilder sb = new StringBuilder("INSERT INTO [dbo].[ClientBusinessGallery] ([ClientBusinessDetailId],[ImageName],[UploadedDate],[IsActive])");
            sb.Append("VALUES (@ClientBusinessDetailId,@ImageName,@UploadedDate,@IsActive)");

            if (clientDA.AddGalleryDetails(cbObject, fileList, sb.ToString()))
                return true;
            else
                return false;
        }

        public void GetBusinessDetails(int businessId, ref ClientBusiness cbObj)
        {
            StringBuilder sb = new StringBuilder("SELECT B.ClientBusinessDetailId, B.ClientId,B.BusinessCategoryTypeId,B.BusinessSubCategoryType,B.PayPeriodTypeId,B.BusinessHours,B.IsPremiumCustomer,");
            sb.Append("B.NegotiatedPrice,B.IsBulkDataReceived,B.IsMobileNumberToBePublicAccess,B.LocationLongitude,B.LocationLattitude,B.CreatedDate,");
            sb.Append("B.IsActive,B.BusinessDescription,B.BusinessGalleryPath,B.BusinessWebSite,B.BusinessLogoPath,B.BusinessSubCategoryNativeType,");
            sb.Append("BG.GalleryId, BG.ImageName, BG.UploadedDate FROM ClientBusinessDetails B LEFT OUTER JOIN ClientBusinessGallery BG ON ");
            sb.Append("B.ClientBusinessDetailId=BG.ClientBusinessDetailId WHERE B.ClientBusinessDetailId=@BusinessId");

            try
            {
                clientDA.GetSpecificBusinessData(businessId, sb.ToString(), ref cbObj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveGalleryImage(int galleryId, int clientId, string imageName, DeletePhysicalImage deleteImage)
        {
            bool retFlag = default(bool);
            StringBuilder deleteQuery = new StringBuilder("DELETE FROM ClientBusinessGallery WHERE GalleryId = @GalleryId");
            // delete file
            //  if success, remove entry from DB .. wat if data remove fails?
            //  HENCE deleting data from DB first.. den remove physical file.. if file deletion fails, rollback data removal!

            using (TransactionScope transScope = new TransactionScope())
            {
                try
                {
                    if (clientDA.RemoveGalleryPic(galleryId, deleteQuery.ToString()))
                    {
                        if (deleteImage.Invoke(clientId, imageName))
                        // using delegate to execute the delete image function from Utility class in WebApp
                        // feeling so lazy to move the utility generic functionalities to 'Common' project
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

    }
}
