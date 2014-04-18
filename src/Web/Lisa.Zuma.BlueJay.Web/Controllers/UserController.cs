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
        public async Task<ActionResult> Index()
        {
            var users = await webApiUserHelper.GetAllAsync();

            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            var user = await webApiUserHelper.GetAsync(id);
            return View(user);
        }

        public async Task<ActionResult> Edit(string id)
        {
            var user = await webApiUserHelper.GetAsync(id);
            ViewBag.RoleSelect = await GetRoleSelectListAsync();

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            await webApiUserHelper.UpdateUserAsync(user);
            return RedirectToAction("Details", new { id = user.Id });
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

            if (userModel.IsParent)
            {
                var dossier = new Dossier 
                {
                    Name = userModel.DossierName
                };

                var dossierResult = await webApiDossierHelper.CreateAsync(result.User.Id, dossier);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> BlankRoleRow()
        {
            ViewBag.RoleSelect = await GetRoleSelectListAsync();
            return PartialView("_BlankRoleRow", new UserRole());
        }

        private async Task<SelectList> GetRoleSelectListAsync()
        {
            var roles = await webApiRoleHelper.GetRolesAsync();
            return new SelectList(roles, "Name", "Name");
        }

        private WebApiUserHelper webApiUserHelper = new WebApiUserHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
        private WebApiRoleHelper webApiRoleHelper = new WebApiRoleHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
        private WebApiDossierHelper webApiDossierHelper = new WebApiDossierHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
	}
}