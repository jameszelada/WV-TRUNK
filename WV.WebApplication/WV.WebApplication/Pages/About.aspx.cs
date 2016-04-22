using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WV.WebApplication.Pages
{
    public partial class About : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidateSession())
            {
                AddUserTag();
                ValidateOptions();
                if (!hasPermissions(pagename.InnerHtml))
                {
                    Context.Response.Redirect("Unauthorized");
                }
            }
           
        }
    }
}