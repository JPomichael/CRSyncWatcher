using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CRS.Sync.Watcher.Service.Public
{
    public partial class PublicService
    {


        /// <summary>
        /// 调用WCF返回的通用代码部分
        /// </summary>
        /// <param name="category"> 分类</param>
        /// <returns>  CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet </returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet GetPublicInterfaceByCategory(string category)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet dcPublicResult = null;
            try
            {
                dcPublicResult = _WCFClient.GetdcPublicInterfaceByCategory(category);
            }
            catch (Exception ex)
            {

            }
            return dcPublicResult;
        }

        /// <summary>
        /// 本机加载通用对象
        /// </summary>
        /// <param name="_fileSavePath"></param>
        /// <returns></returns>
        public List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> GetReadXMLToObject(string _fileSavePath)
        {
            XElement xmlinq = XElement.Load(_fileSavePath);
            var dcPublics = xmlinq.Element("dcPublics");
            List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> dcPublicList = dcPublics.Descendants("DcPublic").Select(o => new CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic
            {
                remarks = o.Element("remarks").Value,
                cname = o.Element("cname").Value,
                code = o.Element("code").Value,
                ename = o.Element("ename").Value,
                seqid = int.Parse(o.Element("seqid").Value),
                status = o.Element("status").Value,
            }).ToList();

            return dcPublicList;
        }

        /// <summary>
        /// 分类数组
        /// </summary>
        public string[] categoryList = new string[] { "country", "state", "city", "hotelsta", "hotelcat", "hotelstt", "cityarea", "procol", "hotelatt", "code" };


        #region test  WCF result
        public void getdate()
        {
            string _result = _WCFClient.GetUserInfoByTelFormatString("18923759873", WCFMobileServer.EnumUserSourceFrom.All, WCFMobileServer.EnumReturnFormatType.json);
        }
        #endregion
    }
}
