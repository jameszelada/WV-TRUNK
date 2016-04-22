using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WV.WebApplication
{
    public partial class WV : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
           
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
        }
    }
}