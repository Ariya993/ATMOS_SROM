using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ATMOS_SROM.Domain;
using ATMOS_SROM.Model;

namespace ATMOS_SROM.Configuration
{
    public partial class Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindConfiguration();
            }
        }

        protected void bindConfiguration()
        {
            LOGIN_DA loginDA = new LOGIN_DA();
            tbLock.Text = loginDA.getParam(" where NAME = 'lock'").VALUE;
        }

        protected void btnLockClick(object sender, EventArgs e)
        {
            
        }

        protected bool upload(string source)
        {
            bool ret = true;
            return ret;
        }
    }
}