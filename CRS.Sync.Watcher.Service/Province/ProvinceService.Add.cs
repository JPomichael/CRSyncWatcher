using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Province
{
    public partial class ProvinceService
    {

        #region 省份
        public bool AddList(IEnumerable<province_info> provincelist)
        {
            bool result = false;
            try
            {
                dc.province_info.InsertAllOnSubmit(provincelist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public bool Add(province_info provinceinfo)
        {
            bool result = false;
            try
            {
                provinceinfo.province_Id = "0000";
                dc.province_info.InsertOnSubmit(provinceinfo);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public string SerialNumber()
        {
            Random rad = new Random();  //实例化随机数产生器rad；
            int value = rad.Next(213, 978); //生成大于等于213，小于等于   978的随机数；
            string idStr = rad.Next(1, 9).ToString() + value.ToString() + System.DateTime.Now.Millisecond.ToString() +
                rad.Next(13, 76).ToString();
            if (dc.province_info.Where(o => o.province_Id == idStr).Count() > 1)
            {
                while (true)
                {
                    value = rad.Next(213, 978); //生成大于等于21312，小于等于   947856的随机数；
                    idStr = rad.Next(1, 9).ToString() + value.ToString() + System.DateTime.Now.Millisecond.ToString() +
                        rad.Next(13, 76).ToString();
                    if (dc.province_info.Where(o => o.province_Id == idStr).Count() < 1)
                    {
                        continue;
                    }
                }
            }
            return idStr;
        }
        #endregion

    }
}
