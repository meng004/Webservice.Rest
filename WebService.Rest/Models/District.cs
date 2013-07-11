using System;
using System.Collections.Generic;

namespace WebService.Rest.Models
{
    public partial class District
    {
        public int DistrictId { get; set; }
        public string DisName { get; set; }
        public int CityId { get; set; }
        public Nullable<int> DisSort { get; set; }
    }
}
