using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Controllers
{
    public class UserProfileController : AdminController
    {
        public UserProfileController()
            : this (new AdsData(), new EfDataLoader())
        {
            
        }

        public UserProfileController(IAdsData data, IDataLoader dataLoader)
            : base(data, dataLoader)
        {

        }
        
        // GET: UserProfile
        public ActionResult Index()
        {
            var applicationUsers = Data.Users.All().Include(a => a.Town);
            return View(applicationUsers.ToList());
        }

        // GET: UserProfile/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = Data.Users.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: UserProfile/Create
        public ActionResult Create()
        {
            ViewBag.TownId = new SelectList(Data.Towns.All(), "Id", "Name");
            return View();
        }

        // POST: UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,TownId,Email,PhoneNumber,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                Data.Users.Add(applicationUser);
                Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TownId = new SelectList(Data.Towns.All(), "Id", "Name", applicationUser.TownId);
            return View(applicationUser);
        }

        // GET: UserProfile/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = Data.Users.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.TownId = new SelectList(Data.Towns.All(), "Id", "Name", applicationUser.TownId);
            return View(applicationUser);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,TownId,Email,PhoneNumber,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                Data.Users.Update(applicationUser);
                Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TownId = new SelectList(Data.Towns.All(), "Id", "Name", applicationUser.TownId);
            return View(applicationUser);
        }

        // GET: UserProfile/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = Data.Users.GetById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = Data.Users.GetById(id);
            Data.Users.Delete(applicationUser);
            Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Data.Users.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
