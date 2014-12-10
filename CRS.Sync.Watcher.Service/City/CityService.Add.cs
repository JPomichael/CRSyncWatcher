using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.City
{
    public partial class CityService
    {
        public string SerialNumber()
        {
            Random rad = new Random();  //实例化随机数产生器rad；
            int value = rad.Next(213, 978); //生成大于等于213，小于等于   978的随机数；
            string idStr = rad.Next(1, 9).ToString() + value.ToString() + System.DateTime.Now.Millisecond.ToString() +
                rad.Next(13, 76).ToString();
            if (dc.city_info.Where(o => o.city_id == idStr).Count() > 1)
            {
                while (true)
                {
                    value = rad.Next(213, 978); //生成大于等于21312，小于等于   947856的随机数；
                    idStr = rad.Next(1, 9).ToString() + value.ToString() + System.DateTime.Now.Millisecond.ToString() +
                        rad.Next(13, 76).ToString();
                    if (dc.city_info.Where(o => o.city_id == idStr).Count() < 1)
                    {
                        continue;
                    }
                }
            }
            return idStr;
        }

        public bool AddList(IEnumerable<city_info> citylist)
        {
            bool result = false;
            try
            {
                dc.city_info.InsertAllOnSubmit(citylist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public bool Add(city_info city)
        {
            bool result = false;
            try
            {

                dc.city_info.InsertOnSubmit(city);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}
