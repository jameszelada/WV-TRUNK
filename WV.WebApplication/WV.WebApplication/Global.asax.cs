using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WV.WebApplication
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Login", "Login", "~/Pages/Login.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Default", "Home", "~/Pages/Default.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("About", "About", "~/Pages/About.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Logout", "Logout.aspx", "~/Pages/Logout.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("User", "User", "~/Pages/screen_user.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Role", "Role", "~/Pages/screen_role.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Role Config", "Options", "~/Pages/screen_role_configuration.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Not Authorized", "Unauthorized", "~/Pages/Error/Unauthorized.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Community", "Community", "~/Pages/screen_community.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Program Type", "ProgramType", "~/Pages/screen_programtype.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Staff", "Staff", "~/Pages/screen_staff.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Project", "Project", "~/Pages/screen_project.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("JobType", "JobType", "~/Pages/screen_jobtype.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Registration", "Registration", "~/Pages/Registration.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("LogBook", "LogBook", "~/Pages/LogBook.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("ControlStaff", "ControlStaff", "~/Pages/ControlStaff.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("WeeklyPlan", "WeeklyPlan", "~/Pages/WeeklyPlan.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Job", "Job", "~/Pages/screen_job.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("AssignRRHH", "AssignRRHH", "~/Pages/AssignRRHH.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Program", "Program", "~/Pages/screen_program.aspx");
            System.Web.Routing.RouteTable.Routes.MapPageRoute("Activity", "Activity", "~/Pages/screen_activity.aspx");

           
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}