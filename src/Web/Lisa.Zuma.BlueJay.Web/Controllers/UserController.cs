using Lisa.Zuma.BlueJay.Web.Models;
using System;
using System.Collections.Generic;
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
	}
}