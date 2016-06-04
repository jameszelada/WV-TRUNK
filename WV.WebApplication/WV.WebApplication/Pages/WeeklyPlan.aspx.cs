using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WV.WebApplication.Pages
{
    public partial class WeeklyPlan : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidateSession())
            {
                AddUserTag();
                ValidateOptions();
                if (!hasPermissions(pagename.InnerText))
                {
                    Context.Response.Redirect("Unauthorized");
                }
                else
                {
                    string id = Request.QueryString["id"];
                    idperson.InnerText = id;
                }


            }
        }
    }
}