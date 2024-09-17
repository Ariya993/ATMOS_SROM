using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_MEMBER
    {
        public Int64 ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string ALAMAT { get; set; }
        public string BRAND { get; set; }
        public string STATUS_MEMBER { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }
        public string NO_BON { get; set; }

        public string NO_CARD { get; set; }
        public string PIN { get; set; }
        public int WRONG_PIN { get; set; }
        public DateTime TGL_MASA_TENGGANG { get; set; }
        public DateTime TGL_EXPIRED { get; set; }
    }
}