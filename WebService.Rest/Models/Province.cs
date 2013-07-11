using System;
using System.Collections.Generic;

namespace WebService.Rest.Models
{
    public partial class Province
    {
        public int ProvinceId { get; set; }
        public string ProName { get; set; }
        public Nullable<int> ProSort { get; set; }
        public string ProRemark { get; set; }
    }
}
