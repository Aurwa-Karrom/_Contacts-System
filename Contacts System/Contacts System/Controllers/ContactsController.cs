using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Contacts_System.Models;


namespace Contacts_System.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contacts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show_All([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Profile> Profile = db.Profiles;
            DataSourceResult result = Profile.ToDataSourceResult(request, profile => new {
                ID = profile.ID,
                firstName = profile.firstName,
                lastName = profile.lastName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address
            });

            return Json(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Profile Profile)
        {
            db.Profiles.Add(Profile);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }


}