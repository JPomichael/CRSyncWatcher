using CRS.Sync.Watcher.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Service.Country
{
    public interface ICountryService
    {
        bool AddList(IEnumerable<country_info> countrylist);

        bool Add(country_info countryinfo);
    }
}
