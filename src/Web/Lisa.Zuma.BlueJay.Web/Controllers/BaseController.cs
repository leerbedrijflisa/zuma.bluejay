using Lisa.Zuma.BlueJay.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class BaseController : Controller
    {
        protected WebApiHelper WebApiHelper
        {
            get
            {
                if (webApiHelper == null)
                {
                    webApiHelper = new WebApiHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
                }

                return webApiHelper;
            }
        }

        private WebApiHelper webApiHelper;
	}
}