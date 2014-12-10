using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CRS.Sync.Watcher.Service
{
    public class WCFRefServieceFactory
    {
        public static WCFMobileServer.MobileContractClient GetProductServiceRef()
        {
            WCFMobileServer.MobileContractClient _client = new WCFMobileServer.MobileContractClient();
            _client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["connectionWCF_userName"].ToString();
            _client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["connectionWCF_pass"].ToString();
            return _client;
        }
    }
}