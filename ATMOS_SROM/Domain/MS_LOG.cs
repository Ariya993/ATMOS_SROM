using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_LOG
    {
        public Int64 idLog { get; set; }
        public string description { get; set; }
        public string userName { get; set; }
        public string ipAddress { get; set; }
        public DateTime? logDate { get; set; }
    }
}