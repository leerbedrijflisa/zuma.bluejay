using Lisa.Zuma.BlueJay.Models;
using Lisa.Zuma.BlueJay.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Lisa.Zuma.BlueJay.Web.Controllers
{
    public class DossierController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var dossiers = await webApiDossierHelper.GetAllAsync();
            
            return View(dossiers);
        }

        public async Task<ActionResult> Details(int id)
        {
            var dossier = await webApiDossierHelper.GetAsync(id);

            return View(dossier);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var dossier = await webApiDossierHelper.GetAsync(id);

            return View(dossier);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Dossier dossier)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "One or more fields are not valid");
                return View(dossier);
            }

            await webApiDossierHelper.UpdateAsync(dossier);

            return RedirectToAction("Details", new { id = dossier.Id });
        }

        public async Task<ActionResult> BlankWatcherRow()
        {
            ViewBag.WatcherSelect = await GetUserSelectList();

            return PartialView("_BlankWatcherRow", new User());
        }

        public async Task<SelectList> GetUserSelectList()
        {
            var items = new List<SelectListItem>();
            var users = await webApiUserHelper.GetAllAsync();

            foreach (var user in users)
            {
                items.Add(new SelectListItem()
                {
                    Text = user.UserName,
                    Value = user.ToString()
                });
            }

            return new SelectList(items, "Value", "Text");
        }

        private WebApiDossierHelper webApiDossierHelper = new WebApiDossierHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
        private WebApiUserHelper webApiUserHelper = new WebApiUserHelper(ConfigurationManager.AppSettings["WebApiBaseUrl"]);
	}
}