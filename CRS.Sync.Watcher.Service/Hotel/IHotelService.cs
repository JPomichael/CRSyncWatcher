using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public interface IHotelService
    {
        /// <summary>
        /// 请求CRS接口
        /// </summary>
        /// <param name="_CRSHotelParamsDTO"></param>
        /// <returns></returns>
        CRS.Sync.Watcher.Service.WCFMobileServer.HotelGet GetCRSHotelInterface(CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO);

        /// <summary>
        /// 本机加载XML
        /// </summary>
        /// <param name="_fileSavePath"></param>
        /// <returns></returns>
        List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> GetReadXMLToObject(string _fileSavePath);

        /// <summary>
        /// 添加酒店
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        bool Add(hotel_info hotel);

        /// <summary>
        /// 添加酒店List
        /// </summary>
        /// <param name="hotelist"></param>
        /// <returns></returns>
        bool AddList(IEnumerable<hotel_info> hotelist);

        /// <summary>
        /// 删除公寓-修改状态
        /// </summary>
        /// <param name="hotel_id"></param>
        /// <returns></returns>
        bool Delete(int hotel_id);

        /// <summary>
        /// 删除-修改多个公寓状态
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        bool Delete(int[] idList);

        /// <summary>
        /// 物理删除多个公寓
        /// </summary>
        /// <param name="hotelist"></param>
        /// <returns></returns>
        bool DeleteTrue(IEnumerable<hotel_info> hotelist);

        /// <summary>
        /// 物理删除公寓
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        bool DleteTrue(hotel_info hotel);

        /// <summary>
        /// 更新公寓
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        bool Update(hotel_info hotel);

        /// <summary>
        /// 检测公寓是否存在
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        bool CheckIsAny(hotel_info hotel);

        /// <summary>
        /// 获取公寓信息-据ID
        /// </summary>
        /// <param name="hotel_id"></param>
        /// <returns></returns>
        hotel_info GetHotelInfoById(int hotel_id);

        /// <summary>
        /// 获取多个公寓信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEnumerable<hotel_info> GetHoteListInfo(Expression<Func<hotel_info, bool>> expression);

        /// <summary>
        /// 多个公寓DTO转换至Model
        /// </summary>
        /// <param name="_hotelGetList"></param>
        /// <returns></returns>
        IEnumerable<hotel_info> HotelDTOToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> _hotelGetList);

        /// <summary>
        /// 公寓审核状态转换
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        int ConvertStatus(string status);
    }
}
