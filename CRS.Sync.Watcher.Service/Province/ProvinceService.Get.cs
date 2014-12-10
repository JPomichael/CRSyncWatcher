using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CRS.Sync.Watcher.Service.Province
{
    public partial class ProvinceService
    {

        /// <summary>
        /// 验证省份是否已经存在- id name 
        /// </summary>
        /// <param name="provinceinfo"></param>
        /// <returns></returns>
        public bool CheckIsAny(province_info provinceinfo)
        {
            bool result = false;

            try
            {
                //!  此前已格式化名称
                if (dc.province_info.Where(o => o.code == provinceinfo.code || (o.province_name.Contains(provinceinfo.province_name) || o.province_name == provinceinfo.province_name)).Any())
                    result = true;
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 格式化省份名
        /// </summary>
        /// <param name="province_name"></param>
        /// <returns></returns>
        public string FormatProvinceName(string province_name)
        {
            if (province_name.Contains("省"))
            {
                province_name = province_name.Replace("省", "");
            }
            if (province_name.Contains("市"))
            {
                province_name = province_name.Replace("市", "");
            }
            if (province_name.Contains("自治区"))
            {
                province_name = province_name.Replace("自治区", "");
                if (province_name.Contains("维吾尔"))
                {
                    province_name = province_name.Replace("维吾尔", "");
                }
                if (province_name.Contains("回族"))
                {
                    province_name = province_name.Replace("回族", "");
                }
                if (province_name.Contains("壮族"))
                {
                    province_name = province_name.Replace("壮族", "");
                }
                if (province_name.Contains("壮族"))
                {
                    province_name = province_name.Replace("壮族", "");
                }

            }
            return province_name;
        }


        public IEnumerable<province_info> DcPublicToModel(List<CRS.Sync.Watcher.Service.WCFMobileServer.DcPublic> _dcPublicList)
        {
            IEnumerable<province_info> provinceList = _dcPublicList.Select(o => new province_info
            {
                province_Id = SerialNumber(),
                province_name = FormatProvinceName(o.cname),
                ename = o.ename,
                code = o.code,
                remarks = o.remarks,
                seqid = o.seqid,
                status = o.status == "1" ? true : false,
            }).AsEnumerable();
            return provinceList;
        }

        public province_info GetProvinceInfoByCode(string code)
        {
            return dc.province_info.Where(o => o.code != null ? o.code == code : false).FirstOrDefault();
        }

    }
}
