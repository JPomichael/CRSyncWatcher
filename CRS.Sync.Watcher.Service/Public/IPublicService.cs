using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Public
{
    public interface IPublicService
    {

        #region CRS 通用代码服务
        /// <summary>
        /// 请求通用代码接口
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet GetPublicInterfaceByCategory(string category);

        /// <summary>
        /// 加载本机XML只通用模型
        /// </summary>
        /// <param name="_fileSavePath"></param>
        /// <returns></returns>
        List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> GetReadXMLToObject(string _fileSavePath);
        #endregion


    }
}
