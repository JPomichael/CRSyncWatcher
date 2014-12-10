using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Country
{
    public partial class CountryService
    {

        public bool AddList(IEnumerable<country_info> countrylist)
        {
            bool result = false;
            try
            {
                dc.country_info.InsertAllOnSubmit(countrylist);
                dc.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public bool Add(country_info countryinfo)
        {
            bool result = false;
            try
            {

                dc.country_info.InsertOnSubmit(countryinfo);
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