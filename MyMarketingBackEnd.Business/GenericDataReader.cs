using MyMarketingBackEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarketingBackEnd.Business
{
    /// <summary>
    /// Singleton class to read table lists (static) from DB
    /// This doesn't need to initialize objects everytime and data retreival happens only during object initialization
    /// </summary>
    public class GenericDataReader
    {
        private static GenericDataReader _genericDataReader;
        private static readonly object padlock = new object();

        public static GenericDataReader GetInstance()
        {
            if (_genericDataReader == null)
            {
                lock (padlock)
                {
                    if (_genericDataReader == null)
                    {
                        _genericDataReader = new GenericDataReader();
                    }
                }
            }
            return _genericDataReader;
        }

        #region MEMBERS - Private & Public

        private Dictionary<int, string> _businessCategoryList;
        private Dictionary<int, string> _payPeriodList;
        private ClientDataAccess clientDA = new ClientDataAccess();

        public Dictionary<int, string> BusinessCategoryList
        {
            get
            {
                return _businessCategoryList;
            }
        }
        public Dictionary<int, string> PayPeriodList
        {
            get
            {
                return _payPeriodList;
            }
        }

        #endregion

        GenericDataReader()
        {
            // Load Business Category List
            _businessCategoryList = new Dictionary<int, string>();
            clientDA.GetBusinessCategoryList(ref _businessCategoryList);

            // Load Pay Period List
            _payPeriodList = new Dictionary<int, string>();
            clientDA.GetPayPeriods(ref _payPeriodList);
        }

    }
}
