using DataLayer;
using Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WV.WebApplication.Pages
{
    public class PageBase : System.Web.UI.Page
    {
        string connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;
        AWContext _context;
        IDataRepository<Usuario> _usuario;
        public void Logout()
        {
            HttpContext.Current.Session["isActive"] = null;
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("Login");
        }

        public bool ValidateSession()
        {
            bool isActive = true;
            if (Context.Session["isActive"] != null)
            {
                if ((bool)Context.Session["isActive"] == false)
                {
                    isActive = false;
                    Context.Response.Redirect("Login");
                }
            }
            return isActive;
        }

        public void AddUserTag()
        {
           string username = (string)HttpContext.Current.Session["username"];
           string metatag = @"<meta name='username' content='"+username+"'></meta>";
           this.Page.Header.Controls.Add(new LiteralControl(metatag));   
        }

        public void ValidateOptions()
        {
            string user = (string)HttpContext.Current.Session["username"];
            InitializeObjects();
            var statement = @"
                SELECT RE.Recurso FROM Usuario AS US
                INNER JOIN UsuarioRol AS UR
                ON US.ID_Usuario = UR.ID_Usuario
                INNER JOIN  ROL AS RL
                ON RL.ID_Rol = UR.ID_Rol
                INNER JOIN RolRecurso AS RR 
                ON RL.ID_Rol = RR.ID_Rol
                INNER JOIN Recurso AS RE
                ON RE.ID_Recurso = RR.ID_Recurso
                WHERE NombreUsuario = '" + user+"'";
            
            List<string> options = _context.Database.SqlQuery<string>(statement).ToList();

            var statementAllPages = @"
                SELECT Recurso FROM Recurso";

            List<string> allPages = _context.Database.SqlQuery<string>(statementAllPages).ToList();

            List<string> result = allPages.Where(p=> !options.Any(o=> o == p)).ToList();

            foreach (string option in result)
            {
                Control control = Master.FindControl(option);
                if (control != null)
                {
                    control.Visible = false;
                }
                  
            }


            
        }

        private void InitializeObjects()
        {
            _context = new AWContext(connection);
        }

        public bool hasPermissions(string pagename)
        {
            bool hasPermissions = false;
            string user = (string)HttpContext.Current.Session["username"];
            InitializeObjects();
            var statement = @"
                SELECT RE.Recurso FROM Usuario AS US
                INNER JOIN UsuarioRol AS UR
                ON US.ID_Usuario = UR.ID_Usuario
                INNER JOIN  ROL AS RL
                ON RL.ID_Rol = UR.ID_Rol
                INNER JOIN RolRecurso AS RR 
                ON RL.ID_Rol = RR.ID_Rol
                INNER JOIN Recurso AS RE
                ON RE.ID_Recurso = RR.ID_Recurso
                WHERE NombreUsuario = '" + user + "'";

            List<string> options = _context.Database.SqlQuery<string>(statement).ToList();
            
           

            hasPermissions = options.Any(o=>o==pagename);


            return hasPermissions;
        }
    }
}