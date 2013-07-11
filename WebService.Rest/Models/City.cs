using System;
using System.Collections.Generic;

namespace WebService.Rest.Models
{
    public partial class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int ProvinceId { get; set; }
        public Nullable<int> CitySort { get; set; }
    }
}
