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

        public ActionResult Update(int id)
        {
            var Profile = db.Profiles.SingleOrDefault(p => p.ID == id);

            var Model = new Profile
            {
                ID = Profile.ID,
                firstName = Profile.firstName,
                lastName = Profile.lastName,
                Address = Profile.Address,
                Email = Profile.Email,
                Phone = Profile.Phone
            };
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Profile Profile)
        {
            if (Profile.ID==0)
            {
                db.Profiles.Add(Profile);
            }
            else
            {
                var Temp_Profile = db.Profiles.SingleOrDefault(p => p.ID == Profile.ID);
                Temp_Profile.ID = Profile.ID;
                Temp_Profile.firstName = Profile.firstName;
                Temp_Profile.lastName = Profile.lastName;
                Temp_Profile.Address = Profile.Address;
                Temp_Profile.Phone = Profile.Phone;
                Temp_Profile.Email = Profile.Email;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }


}