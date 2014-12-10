using CRS.Sync.Watcher.DLL;
using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CRS.Sync.Watcher.ConsoleApplication.Demo
{
    /// <summary>
    /// 公寓基础
    /// </summary>
    public class Base
    {
        #region 服务引用
        //! 服务引用
        CRS.Sync.Watcher.Service.Public.IPublicService _publicService = new CRS.Sync.Watcher.Service.Public.PublicService();
        CRS.Sync.Watcher.Service.Province.IProvinceService _provinceService = new CRS.Sync.Watcher.Service.Province.ProvinceService();
        CRS.Sync.Watcher.Service.City.ICityService _cityService = new CRS.Sync.Watcher.Service.City.CityService();
        CRS.Sync.Watcher.Service.Star.IStarService _starService = new CRS.Sync.Watcher.Service.Star.StarService();
        #endregion

        //!  通用代码分类名数组
        public readonly string[] publiCodeCategoryList = new string[] { "city" }; //x  "state", "city", "hotelsta", 
        //x "country", "state", "city", "cityarea", "hotelsta", "hotelcat", "hotelstt" 

        public string SyncService(string staticFolderSavePath)
        {
            string messages = "";
            //!   循环数组内分类名
            for (int i = 0; i < publiCodeCategoryList.Length; i++)
            {
                //!  文件名
                string _filename = publiCodeCategoryList[i] + ".xml";
                //!   获取完整路径
                string _fileSavePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory + "\\", staticFolderSavePath + _filename);
                //! 请求通用代码
                CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet dcPublicResult = _publicService.GetPublicInterfaceByCategory(publiCodeCategoryList[i]);
                if (dcPublicResult.result == 0 && dcPublicResult.dcPublics != null)
                {
                    //! 保存/更新本机
                    XmlUtil.SerializeToXml(dcPublicResult, dcPublicResult.GetType(), _fileSavePath, null);
                    //x Tip("已更新本机数据文件。", ConsoleColor.Green);

                    //!  加载到通用类
                    List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList = _publicService.GetReadXMLToObject(_fileSavePath);

                    //?  同步操作!!!
                    switch (publiCodeCategoryList[i])
                    {
                        case "state":
                            messages += ProvinceSync(_dcPublicList);
                            break;
                        case "city":
                            messages += CitySync(_dcPublicList);
                            break;
                        case "hotelsta":
                            messages += StarSync(_dcPublicList);
                            break;
                    }
                    //x Tip("已查询到 " + _dcPublicList.Count + " 条数据", ConsoleColor.White);
                    //x Tip("当前已完成。", ConsoleColor.Green);
                }
                else
                    messages = "CRS接口返回为NULL";
            }
            return messages;
        }


        #region 数据同步
        /// <summary>
        /// 省份数据同步
        /// </summary>
        /// <param name="_publicService"></param>
        /// <param name="_dcPublicList"></param>
        public string ProvinceSync(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            string messages = "";
            IEnumerable<province_info> provinceList = _provinceService.DcPublicToModel(_dcPublicList);

            foreach (var _provinceList in provinceList)
            {
                if (!_provinceService.CheckIsAny(_provinceList))
                {
                    bool result = _provinceService.Add(_provinceList);
                    if (!result)
                    {
                        //x Tip("数据同步时出错!  出错数据 ：" + _provinceList.province_name + "", ConsoleColor.Red);
                        messages += "录入数据失败： " + _provinceList.province_name + " " + _provinceList.code;
                    }
                }
                else
                {
                    _provinceService.Update(_provinceList);
                }
            }

            return messages;
        }

        /// <summary>
        /// 城市数据同步
        /// </summary>
        /// <param name="_dcPublicList"></param>
        public string CitySync(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            string messages = "";
            IEnumerable<city_info> cityList = _cityService.DcPublicToModel(_dcPublicList);

            foreach (var _cityList in cityList)
            {
                if (!_cityService.CheckIsAny(_cityList))
                {
                    bool result = _cityService.Add(_cityList);
                    if (!result)
                    {
                        //x Tip("数据同步时出错!  出错数据 ：" + _cityList.city_name + "", ConsoleColor.Red);
                        messages += "录入数据失败： " + _cityList.city_name + " " + _cityList.code;
                    }
                }
                else
                {
                    _cityService.Update(_cityList);
                }
            }
            return messages;
        }


        /// <summary>
        /// 星级数据同步
        /// </summary>
        /// <param name="_dcPublicList"></param>
        public string StarSync(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            string messages = "";
            IEnumerable<hotel_star_info> starList = _starService.DcPublicToModel(_dcPublicList);

            foreach (var _starList in starList)
            {
                if (!_starService.CheckIsAny(_starList))
                {
                    bool result = _starService.add(_starList);
                    if (!result)
                    {
                        //x Tip("数据同步时出错!  出错数据 ：" + _starList.cname + "", ConsoleColor.Red);
                        messages += "录入数据失败： " + _starList.cname + " " + _starList.id;
                    }
                }
                else
                {
                    _starService.Update(_starList);
                }
            }
            return messages;
        }
        #endregion


    }
}
