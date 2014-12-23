using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRS.Sync.Watcher.Linq;
using System.Linq.Expressions;
using CRS.Sync.Watcher.DLL;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace CRS.Sync.Watcher.Service.Hotel
{
    public partial class HotelService
    {

        /// <summary>
        /// 调用WCF返回的通用代码部分
        /// </summary>
        /// <param name="category"> 分类</param>
        /// <returns>  CRS.Sync.Watcher.Service.WCFMobileServer.DcPublicGet </returns>
        public CRS.Sync.Watcher.Service.WCFMobileServer.HotelGet GetCRSHotelInterface(CRS.Sync.Watcher.Service.WCFMobileServer.CRSHotelParamsDTO _CRSHotelParamsDTO)
        {
            CRS.Sync.Watcher.Service.WCFMobileServer.HotelGet _result = null;
            try
            {
                _result = _WCFClient.GetCRSHotelInterface(_CRSHotelParamsDTO);
            }
            catch (Exception)
            {
            }
            return _result;
        }


        /// <summary>
        /// 本机加载通用对象
        /// </summary>
        /// <param name="_fileSavePath"></param>
        /// <returns></returns>
        public List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> GetReadXMLToObject(string _fileSavePath)
        {
            XElement xmlinq = XElement.Load(_fileSavePath);
            var hotelDTOs = xmlinq.Element("hotelDTOs");
            List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> result = hotelDTOs.Descendants("HotelDTO")
                .Select(o => new CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO
                {
                    hotelId = Convert.ToInt32(o.Element("hotelId").Value),
                    code = o.Element("code").Value,
                    CName = o.Element("CName").Value,
                    EName = o.Element("EName").Value,
                    star = o.Element("star").Value,
                    category = o.Element("category").Value,
                    country = o.Element("country").Value,
                    state = o.Element("state").Value,
                    city = o.Element("city").Value,
                    address = o.Element("address").Value,
                    tel = o.Element("tel").Value,
                    fax = o.Element("fax").Value,
                    website = o.Element("website").Value,
                    email = o.Element("email").Value,
                    rooms = Convert.ToInt32(o.Element("rooms").Value),
                    beds = Convert.ToInt32(o.Element("beds").Value),
                    seats = Convert.ToInt32(o.Element("seats").Value),
                    intro = o.Element("intro").Value,
                    EIntro = o.Element("EIntro").Value,
                    //photo1 = o.Element("photo1").Value,
                    //photo2 = o.Element("photo2").Value,
                    //photo3 = o.Element("photo3").Value,
                    policy = o.Element("policy").Value,
                    descript = o.Element("descript").Value,
                    EDescript = o.Element("EDescript").Value,
                    remarks = o.Element("remarks").Value,
                    status = o.Element("status").Value,
                    cityArea = o.Element("cityArea").Value,
                    seqId = Convert.ToInt32(o.Element("seqId").Value),
                    EAddress = o.Element("EAddress").Value,
                    checkInTime = o.Element("checkInTime").Value,
                    checkOutTime = o.Element("checkOutTime").Value,
                    zipCode = o.Element("zipCode").Value,
                    bounsItem = o.Element("bounsItem").Value,
                    fbcoef = Convert.ToInt32(o.Element("fbcoef").Value),
                    etcoef = Convert.ToInt32(o.Element("etcoef").Value),
                    otcoef = Convert.ToInt32(o.Element("otcoef").Value),
                    ttcoef = Convert.ToInt32(o.Element("ttcoef").Value),
                    hotelCard = o.Element("hotelCard").Value,
                    rmcoef = Convert.ToInt32(o.Element("rmcoef").Value)
                    //longitude = o.Element("longitude").Value,
                    //latitude = o.Element("latitude").Value,

                }).ToList();
            return result;
        }

        /// <summary>
        /// 获取单个公寓信息
        /// </summary>
        /// <param name="hotel_id"></param>
        /// <returns> hotel_info</returns>
        public hotel_info GetHotelInfoById(int hotel_id)
        {
            return dc.hotel_info.Where(o => o.hotel_id == hotel_id).FirstOrDefault();
        }

        public hotel_info GetHotelInfoByHid(string h_id)
        {
            return dc.hotel_info.Where(o => o.h_id == h_id).FirstOrDefault();
        }

        /// <summary>
        /// 获取多个hotel_info - 动态
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IEnumerable<hotel_info> GetHoteListInfo(Expression<Func<hotel_info, bool>> expression)
        {
            var hoteList = dc.hotel_info.Where<hotel_info>(expression).OrderBy(o => o.hotel_id);
            if (hoteList != null)
            {
                return hoteList;
            }
            return null;
        }

        /// <summary>
        /// 效验公寓是否存在-  id，name
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public bool CheckIsAny(hotel_info hotel)
        {
            return dc.hotel_info.Where(o => o.h_id == hotel.h_id || o.h_name_cn == hotel.h_name_cn && o.source_id == hotel.source_id).Any();
        }

        /// <summary>
        /// DTO转换为 模型
        /// </summary>
        /// <param name="_hotelGetList"></param>
        /// <returns></returns>
        public IEnumerable<hotel_info> HotelDTOToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.HotelDTO> _hotelGetList)
        {
            IEnumerable<hotel_info> hoteList = _hotelGetList.Select(o => new hotel_info
            {
                h_id = o.hotelId.ToString(), //!   CRS 的hotelId
                u_id = "CRS Sync",
                //!  主题需要匹配
                //x h_t_id 
                h_name_cn = o.CName,
                h_name_en = o.EName,
                h_location_cn = o.address,
                h_location_en = o.EAddress,
                h_description_cn = o.descript,
                h_description_en = o.EDescript,
                h_star = !string.IsNullOrEmpty(o.star) ? Convert.ToInt32(o.star) : 0,
                h_organization = o.code,
                h_tel = o.tel,
                //x h_mobile_phone=
                h_fax = o.fax,
                h_email = o.email,
                //x h_contact=
                //x h_traffic_position
                //x h_longitude
                //x h_latitude
                h_check_in = o.checkInTime,
                h_check_out = o.checkOutTime,
                h_room_count = Convert.ToInt32(o.rooms),
                //! 需要转换
                h_province = _provinceService.GetProvinceInfoByCode(o.state).province_Id,
                //! 需要转换
                h_city = _cityService.GetCityInfoByCode(o.city).city_id,
                //! 需要转换
                //x h_administrative_region = o.cityArea,
                //x h_business_zone=
                h_postcode = o.zipCode,
                h_state = true,
                h_hotel_website = o.website,
                h_sort = Convert.ToInt32(o.seqId),
                source_id = 6,
                availPolicy = o.policy,
                //x hotel_theme_id=
                //! 需要转换  
                CheckState = ConvertStatus(o.status),

                bounsItem = o.bounsItem,
                fbcoef = Convert.ToDecimal(o.fbcoef),
                etcoef = Convert.ToDecimal(o.etcoef),
                otcoef = Convert.ToDecimal(o.otcoef),
                ttcoef = Convert.ToDecimal(o.ttcoef),
                hotelCard = o.hotelCard,
                rmcoef = Convert.ToDecimal(o.rmcoef),
                //photo1 = o.Element("photo1").Value,
                //photo2 = o.Element("photo2").Value,
                //photo3 = o.Element("photo3").Value,
                code = o.code
            }).AsEnumerable();
            return hoteList;
        }


        /// <summary>
        /// 状态转换
        /// </summary>
        /// <param name="status">CRS STATUS</param>
        /// <returns> </returns>
        public int ConvertStatus(string status)
        {
            status = status.Trim();
            int result = status == "1" ? 1 : status != "0" ? 2 : 0;
            return result;
        }

    }
}
