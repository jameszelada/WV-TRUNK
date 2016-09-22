using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Repository;
using System.Configuration;
using WV.WebApplication.Utils;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace WV.WebApplication.Utils
{
    public abstract class ActionTemplate
    {

        private string _Connection = ConfigurationManager.ConnectionStrings["VISIONMUNDIALEntities"].ConnectionString;

        public string Connection
        {
            get { return _Connection; }
        }

        private const string SESSION_USERNAME = "username";
        private string _systemUsername = "";

        public string SystemUsername
        {
            get {
                if (HttpContext.Current.Session[SESSION_USERNAME] != null)
                {
                    _systemUsername = HttpContext.Current.Session[SESSION_USERNAME].ToString().ToUpper();
                }
                else
                {
                    _systemUsername = "";
                }
 
                return _systemUsername; }
            
        }

        public abstract void InitializeObjects();


        public abstract string GetAllRecords();


        public abstract string GetSingleRecord(HttpContext context);


        public abstract string DeleteRecord(HttpContext context);


        public abstract string AddRecord(HttpContext context);


        public abstract string EditRecord(HttpContext context);
        
        
    }
}