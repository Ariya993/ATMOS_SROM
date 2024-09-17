using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_MENU
    {
        public Int64 ID { get; set; }
        public Int64 ID_PARENT { get; set; }
        public Int64 ID_MENU { get; set; }
        public string MENU { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_PATH { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public bool STATUS { get; set; }

        //Additional
        public Int64 ID_USER_LEVEL { get; set; }
        public string USER_LEVEL { get; set; }
    }
}