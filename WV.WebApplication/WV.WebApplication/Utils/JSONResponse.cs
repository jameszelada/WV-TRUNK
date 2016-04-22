using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WV.WebApplication.Utils
{
    public class JsonResponse
    {
        private bool _IsSucess = false;

        public bool IsSucess
        {
            get { return _IsSucess; }
            set { _IsSucess = value; }
        }

        private string _Message = string.Empty;

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private object _ResponseData = null;

        public object ResponseData
        {
            get { return _ResponseData; }
            set { _ResponseData = value; }
        }

        private string _CallBack = string.Empty;

        public string CallBack
        {
            get { return _CallBack; }
            set { _CallBack = value; }
        }
    }
}