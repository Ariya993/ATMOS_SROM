using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_PARAMETER
    {
        public Int64 ID { get; set; }
        public string NAME { get; set; }
        public string VALUE { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
    }
}