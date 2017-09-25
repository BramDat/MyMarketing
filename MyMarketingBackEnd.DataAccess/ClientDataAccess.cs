using MyMarketingBackEnd.BusinessObjects;
using MyMarketingBackEnd.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.DataAccess
{
    public class ClientDataAccess
    {
        private static string ConnectionStr = ConfigurationManager.ConnectionStrings["MyMarketDBConn"].ToString();

        #region READ GENERIC DATA

        public void GetBusinessCategoryList(ref Dictionary<int, string> bizCategoryList)
        {
            try
            {
                bizCategoryList = new Dictionary<int, string>();

                string SqlCommandString = "SELECT BusinessCategoryTypeId, BusinessCategoryType FROM BusinessCategoryTypes";

                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SqlCommandString, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            bizCategoryList.Add(Convert.ToInt32(dr["BusinessCategoryTypeId"].ToString()), dr["BusinessCategoryType"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetPayPeriods(ref Dictionary<int, string> bizCategoryList)
        {
            try
            {
                bizCategoryList = new Dictionary<int, string>();

                string SqlCommandString = "select * from PayPeriodTypes";

                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(SqlCommandString, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            bizCategoryList.Add(Convert.ToInt32(dr["PayPeriodTypeId"].ToString()), dr["PayPeriodType"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DML COMMANDS

        public int AddNewClient(Client clientObj, string insertQuery)
        {
            try
            {
                int ClientID;
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    clientObj.CreatedDate = DateTime.Now;

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                    {
                        List<SqlParameter> sParamList = new List<SqlParameter>()
                        {
                            new SqlParameter(){ParameterName="@ClientFirstName",SqlDbType=SqlDbType.VarChar,Value=clientObj.FirstName, Size=100},
                            new SqlParameter(){ParameterName="@ClientLastName",SqlDbType=SqlDbType.VarChar,Value=clientObj.LastName, Size=100},
                            new SqlParameter(){ParameterName="@BusinessName",SqlDbType=SqlDbType.VarChar,Value=clientObj.BusinessName, Size=200},
                            new SqlParameter(){ParameterName="@ClientPrimaryAddress",SqlDbType=SqlDbType.VarChar,Value=clientObj.PrimaryAddress, Size=400},
                            new SqlParameter(){ParameterName="@PrimaryContactNumber",SqlDbType=SqlDbType.Decimal,Value=clientObj.PrimaryPhoneNum, Precision=10, Scale=0},
                            new SqlParameter(){ParameterName="@AlternateContactNumber",SqlDbType=SqlDbType.Decimal,Value=DBHelper.DefaultForDBNullValues(clientObj.AltPhoneNum),  Precision=10, Scale=0},
                            new SqlParameter(){ParameterName="@PrimaryMailId",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.PrimaryMail),  Size=200},
                            new SqlParameter(){ParameterName="@FacebookId",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.FacebookId), Size=200},
                            new SqlParameter(){ParameterName="@CreatedDate",SqlDbType=SqlDbType.DateTime,Value=clientObj.CreatedDate},
                            new SqlParameter(){ParameterName="@IsActive",SqlDbType=SqlDbType.Bit,Value=clientObj.IsActive}
                        };
                        insertCommand.Parameters.AddRange(sParamList.ToArray());

                        con.Open();
                        ClientID = (int)insertCommand.ExecuteScalar();
                    }
                }
                return ClientID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddClientAuthDetails(ClientAuth clientObj, string insertQuery)
        {
            try
            {
                int ClientAuthID = default(int);

                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(insertQuery, con);

                    List<SqlParameter> sParamList = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@ClientId",SqlDbType=SqlDbType.Int,Value=clientObj.ClientId},
                        new SqlParameter(){ParameterName="@ClientUserName",SqlDbType=SqlDbType.VarChar,Value=clientObj.UserName, Size=300},
                        new SqlParameter(){ParameterName="@ClientPassword",SqlDbType=SqlDbType.VarChar,Value=clientObj.Password, Size=300},
                        new SqlParameter(){ParameterName="@IsActive",SqlDbType=SqlDbType.Bit,Value=clientObj.IsActive},
                        new SqlParameter(){ParameterName="@DateOfRegistration",SqlDbType=SqlDbType.DateTime,Value=clientObj.CreatedDate},
                        // Adding 1 year for expiry
                        new SqlParameter(){ParameterName="@DateOfExpire",SqlDbType=SqlDbType.DateTime,Value=DBHelper.DefaultForDBNullValues(clientObj.CreatedDate.AddYears(1))},
                        // Last login time must be null
                        new SqlParameter(){ParameterName="@LastLoginTime",SqlDbType=SqlDbType.DateTime,Value=DBNull.Value}
                    };

                    cmd.Parameters.AddRange(sParamList.ToArray());

                    ClientAuthID = (int)cmd.ExecuteScalar();
                }

                return ClientAuthID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int AddClientBizDetails(ClientBusiness clientObj, string insertQuery)
        {
            try
            {
                int bizID = default(int);

                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(insertQuery, con);

                    string bizHours = Common.StringHelper.FormatBusinessHours(clientObj.BizStartHours, clientObj.BizEndHours, clientObj.BizStartWeekDay, clientObj.BizEndWeekDay);

                    List<SqlParameter> sParamList = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@ClientId",SqlDbType=SqlDbType.Int,Value=clientObj.ClientId},
                        new SqlParameter(){ParameterName="@BusinessCategoryTypeId",SqlDbType=SqlDbType.Int,Value=clientObj.BizCategoryId},
                        new SqlParameter(){ParameterName="@BusinessSubCategoryType",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.BizSubCategoryType), Size=40},
                        new SqlParameter(){ParameterName="@PayPeriodTypeId",SqlDbType=SqlDbType.Int,Value=clientObj.PayPeriodId},
                        new SqlParameter(){ParameterName="@BusinessHours",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(bizHours), Size=200},
                        new SqlParameter(){ParameterName="@IsPremiumCustomer",SqlDbType=SqlDbType.Bit,Value=clientObj.IsPremiumBiz},
                        new SqlParameter(){ParameterName="@NegotiatedPrice",SqlDbType=SqlDbType.Decimal,Value=clientObj.NegotiatedPrice},
                        new SqlParameter(){ParameterName="@IsBulkDataReceived",SqlDbType=SqlDbType.Bit,Value=clientObj.IsBuldDataReceived},
                        new SqlParameter(){ParameterName="@IsMobileNumberToBePublicAccess",SqlDbType=SqlDbType.Bit,Value=clientObj.IsPhonePublic},
                        new SqlParameter(){ParameterName="@LocationLongitude",SqlDbType=SqlDbType.Decimal,Value=DBHelper.DefaultForDBNullValues(clientObj.GeoLatitude), Precision=8, Scale=6},
                        new SqlParameter(){ParameterName="@LocationLattitude",SqlDbType=SqlDbType.Decimal,Value=DBHelper.DefaultForDBNullValues(clientObj.GeoLongitude), Precision=8, Scale=6},
                        new SqlParameter(){ParameterName="@CreatedDate",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                        new SqlParameter(){ParameterName="@IsActive",SqlDbType=SqlDbType.Bit,Value=clientObj.IsActive},
                        new SqlParameter(){ParameterName="@BusinessDescription",SqlDbType=SqlDbType.VarChar,Value=clientObj.BizDescription, Size=750},
                        new SqlParameter(){ParameterName="@BusinessGalleryPath",SqlDbType=SqlDbType.VarChar,Value=clientObj.BizGalleryPath, Size=750},
                        new SqlParameter(){ParameterName="@BusinessWebSite",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.BizWebSite), Size=200},
                        new SqlParameter(){ParameterName="@BusinessLogoPath",SqlDbType=SqlDbType.VarChar,Value=DBNull.Value, Size=750},
                        new SqlParameter(){ParameterName="@BusinessSubCategoryNativeType",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.BizSubCategoryNativeType), Size=80}
                    };

                    cmd.Parameters.AddRange(sParamList.ToArray());

                    bizID = (int)cmd.ExecuteScalar();
                }

                return bizID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddLogoDetails(ClientBusiness clientObj, string updateQuery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ClientBusinessDetailId", SqlDbType = SqlDbType.Int, Value = clientObj.BizId });
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@LogoPath", SqlDbType = SqlDbType.VarChar, Value = clientObj.BizLogoPath, Size = 750 });

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateClientDetails(Client clientObj, string updateQuery)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(updateQuery, con);

                    List<SqlParameter> sParamList = new List<SqlParameter>()
                    {
                            new SqlParameter(){ParameterName="@ClientId",SqlDbType=SqlDbType.Int,Value=clientObj.ClientId},
                            new SqlParameter(){ParameterName="@ClientFirstName",SqlDbType=SqlDbType.VarChar,Value=clientObj.FirstName, Size=100},
                            new SqlParameter(){ParameterName="@ClientLastName",SqlDbType=SqlDbType.VarChar,Value=clientObj.LastName, Size=100},
                            new SqlParameter(){ParameterName="@BusinessName",SqlDbType=SqlDbType.VarChar,Value=clientObj.BusinessName, Size=200},
                            new SqlParameter(){ParameterName="@ClientPrimaryAddress",SqlDbType=SqlDbType.VarChar,Value=clientObj.PrimaryAddress, Size=400},
                            new SqlParameter(){ParameterName="@PrimaryContactNumber",SqlDbType=SqlDbType.Decimal,Value=clientObj.PrimaryPhoneNum, Precision=10, Scale=0},
                            new SqlParameter(){ParameterName="@AlternateContactNumber",SqlDbType=SqlDbType.Decimal,Value=DBHelper.DefaultForDBNullValues(clientObj.AltPhoneNum),  Precision=10, Scale=0},
                            new SqlParameter(){ParameterName="@PrimaryMailId",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.PrimaryMail),  Size=200},
                            new SqlParameter(){ParameterName="@FacebookId",SqlDbType=SqlDbType.VarChar,Value=DBHelper.DefaultForDBNullValues(clientObj.FacebookId), Size=200},
                            new SqlParameter(){ParameterName="@IsActive",SqlDbType=SqlDbType.Bit,Value=clientObj.IsActive}
                        };

                    cmd.Parameters.AddRange(sParamList.ToArray());

                    int retCount = (int)cmd.ExecuteNonQuery();

                    if (retCount > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddGalleryDetails(ClientBusiness clientObj, string[] fileNames, string insertQuery)
        {
            try
            {
                SqlTransaction objTrans = null;
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    objTrans = con.BeginTransaction();
                    try
                    {
                        foreach (var item in fileNames)
                        {
                            SqlCommand cmd = new SqlCommand(insertQuery, con);
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ClientBusinessDetailId", SqlDbType = SqlDbType.Int, Value = clientObj.BizId });
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ImageName", SqlDbType = SqlDbType.VarChar, Value = item, Size = 40 });
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UploadedDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now });
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@IsActive", SqlDbType = SqlDbType.Bit, Value = true });
                            cmd.ExecuteNonQuery();
                        }
                        objTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        objTrans.Rollback();
                        con.Close();
                        throw ex;
                    }
                    return true;
                }
            }
        }


        #endregion

        #region READ CLIENT OR BUSINESS SPECIFIC DATA

        public bool GetClientDetails(int clientId, string selectQuery, ClientAuth obj)
        {
            if (obj == null)
                obj = new ClientAuth();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(selectQuery, con);
                    cmd.Parameters.Add(new SqlParameter() { ParameterName = "@ClientId", SqlDbType = SqlDbType.Int, Value = clientId });

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            obj.ClientId = Convert.ToInt32(dr["ClientId"]);
                            obj.FirstName = Convert.ToString(dr["ClientFirstName"]);
                            obj.LastName = Convert.ToString(dr["ClientLastName"]);
                            obj.BusinessName = Convert.ToString(dr["BusinessName"]);
                            obj.PrimaryAddress = Convert.ToString(dr["ClientPrimaryAddress"]);
                            obj.PrimaryPhoneNum = Convert.ToInt64(dr["PrimaryContactNumber"]);
                            obj.AltPhoneNum = Convert.ToInt64(dr["AlternateContactNumber"]);
                            obj.PrimaryMail = Convert.ToString(dr["PrimaryMailId"]);
                            obj.FacebookId = Convert.ToString(dr["FacebookId"]);
                            obj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            obj.IsActive = Convert.ToBoolean(dr["IsActive"]);
                            obj.UserName = Convert.ToString(dr["ClientUserName"]);
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GetClientList(string selectQuery, Dictionary<int, string> clientList)
        {
            if (clientList == null)
                clientList = new Dictionary<int, string>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(selectQuery, con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            clientList.Add(Convert.ToInt32(dr["ClientId"]), Convert.ToString(dr["ClientFirstName"]) + " " + Convert.ToString(dr["ClientLastName"]));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GetUserIDList(string selectQuery, List<String> UserList)
        {
            if (UserList == null)
                UserList = new List<string>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(selectQuery, con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            UserList.Add(Convert.ToString(dr["ClientUserName"]));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GetFullBusinessData(int clientId, string selectQuery, List<ClientBusiness> businessList)
        {
            if (businessList == null)
                businessList = new List<ClientBusiness>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(selectQuery, con);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ClientBusiness cb = new ClientBusiness();
                            cb.BizId = Convert.ToInt32(dr["ClientBusinessDetailId"]);

                            if (businessList.Count > 0 && businessList.Any(x => x.BizId == cb.BizId))
                            {
                                // Add gallery details to the existing element's biz details
                                ClientBusinessGallery cbg = new ClientBusinessGallery();

                                cbg.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                                cbg.ImageName = Convert.ToString(dr["ImageName"]);
                                cbg.UploadDate = Convert.ToDateTime(dr["UploadedDate"]);

                                if (cb.GalleryList == null)
                                    cb.GalleryList = new List<ClientBusinessGallery>();

                                cb.GalleryList.Add(cbg);

                                continue;
                            }

                            cb.BizCategoryId = Convert.ToInt32(dr["BusinessCategoryTypeId"]);
                            cb.BizSubCategoryType = Convert.ToString(dr["BusinessSubCategoryType"]);
                            cb.ClientId = Convert.ToInt32(dr["PayPeriodTypeId"]);
                            //cb. = Convert.ToString(dr["BusinessHours"]);
                            cb.IsPremiumBiz = Convert.ToBoolean(dr["IsPremiumCustomer"]);
                            cb.NegotiatedPrice = Convert.ToDecimal(dr["NegotiatedPrice"]);
                            cb.IsBuldDataReceived = Convert.ToBoolean(dr["IsBulkDataReceived"]);
                            cb.IsPhonePublic = Convert.ToBoolean(dr["IsMobileNumberToBePublicAccess"]);
                            cb.GeoLongitude = Convert.ToDecimal(dr["LocationLongitude"]);
                            cb.GeoLatitude = Convert.ToDecimal(dr["LocationLattitude"]);
                            cb.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                            cb.IsActive = Convert.ToBoolean(dr["IsActive"]);
                            cb.BizDescription = Convert.ToString(dr["BusinessDescription"]);
                            cb.BizGalleryPath = Convert.ToString(dr["BusinessGalleryPath"]);
                            cb.BizWebSite = Convert.ToString(dr["BusinessWebSite"]);
                            cb.BizLogoPath = Convert.ToString(dr["BusinessLogoPath"]);
                            cb.BizSubCategoryNativeType = Convert.ToString(dr["BusinessSubCategoryNativeType"]);

                            ClientBusinessGallery cbg2 = new ClientBusinessGallery();

                            cbg2.GalleryId = Convert.ToInt32(dr["GalleryId"]);
                            cbg2.ImageName = Convert.ToString(dr["ImageName"]);
                            cbg2.UploadDate = Convert.ToDateTime(dr["UploadedDate"]);

                            cb.GalleryList = new List<ClientBusinessGallery>();

                            cb.GalleryList.Add(cbg2);

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
