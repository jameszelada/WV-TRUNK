using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WV.WebApplication.Pages
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidateSession())
            {
                
            }
            
            if (Context.Request.Params["u"] != null)
            {
                Context.Session["username"] = Context.Request.Params["u"];
            }

            AddUserTag();
            ValidateOptions();
        }

    }
}