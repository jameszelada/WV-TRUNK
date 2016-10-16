using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WV.WebApplication.Pages.Error
{
    public partial class Error404 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidateSession())
            {
                AddUserTag();
                ValidateOptions();
            }
        }
    }
}