using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATMOS_SROM.Master
{
    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindGrid();
            }
        }

        protected void bindGrid()
        {

        }

        protected void gvMainRowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvMainPageChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        protected void btnSearchClick(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnMemberClick(object sender, EventArgs e)
        {

        }
    }
}