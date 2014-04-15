using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Helpers;
using Lisa.Zuma.BlueJay.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class UserController : BaseController
    {        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            return Json(await webApiUserHelper.GetAllAsync(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddToRole(string id, string role)
        {
            var result = await webApiUserHelper.AddToRoleAsync(id, "User");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await WebApiHelper.RegisterUserAsync(userModel);
            if (!result.Success)
            {
                ModelState.Clear();

                foreach (var error in result.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(error.Key, message);
                    }
                }

                return View();
            }

            return RedirectToAction("Index");
        }

        private WebApiUserHelper webApiUserHelper = new WebApiUserHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
	}
}