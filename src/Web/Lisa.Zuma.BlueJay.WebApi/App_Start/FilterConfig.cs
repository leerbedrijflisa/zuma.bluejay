using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
