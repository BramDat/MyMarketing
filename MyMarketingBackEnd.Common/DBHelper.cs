using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyMarketingBackEnd.Common
{
    public class DBHelper
    {
        public SqlConnection GetConnection(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public void DisconnectDB(SqlConnection con)
        {
            con.Close();
        }

        public string GetConnString()
        {
            if (ConfigurationManager.ConnectionStrings["MyMarketDBConn"] != null)
                return ConfigurationManager.ConnectionStrings["MyMarketDBConn"].ToString();
            else
                return null;
        }

        public SqlConnection GetConnection()
        {
            return GetConnection(GetConnString());
        }

        public static object DefaultForDBNullValues(object Val)
        {
            if (Val == null)
                return DBNull.Value;
            else
            {
                Type ty = Val.GetType();
                if (ty == typeof(int))
                {
                    if (Convert.ToInt32(Val) == 0)
                        return DBNull.Value;
                    else
                        return Val;
                }
                else if (ty == typeof(string))
                {
                    if (Val.ToString().Trim() == string.Empty)
                        return DBNull.Value;
                    else
                        return Val;
                }
                else if (ty == typeof(decimal))
                {
                    if (Convert.ToDecimal(Val) == default(decimal) || Convert.ToDecimal(Val) == 0)
                        return DBNull.Value;
                    else
                        return Val;
                }
                return Val;
            }
        }
    }
}
