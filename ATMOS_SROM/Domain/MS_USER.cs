using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATMOS_SROM.Domain
{
    public class MS_USER
    {
        public Int64 idUser { get; set; }
        public Int64? idStore { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string realName { get; set; }
        public string kodeCust { get; set; }
        public string store { get; set; }
        public string userLevel { get; set; }
        public string email { get; set; }
        public string appraisal { get; set; }
        public string online { get; set; }
        public DateTime? lastLogin { get; set; }
        public string lastIP { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public bool status { get; set; }

        //Addition
        public int lastLongOff { get; set; }
        public string STORE_SHOWROOM { get; set; }
        public string BRAND { get; set; }
        public string STATUS_SHOWROOM { get; set; }
        public DateTime lastChangePass { get; set; }
        public int timeLastChangePass { get; set; }
    }
}