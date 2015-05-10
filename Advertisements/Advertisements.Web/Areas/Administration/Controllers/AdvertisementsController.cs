using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Advertisements.Data;
using Advertisements.Models;
using Advertisements.Web.Infrastructure.DataLoader;

namespace Advertisements.Web.Areas.Administration.Controllers
{
    public class AdvertisementsController : AdminController
    {
        // GET: Administration/Advertisements
        public AdvertisementsController(IAdsData data, IDataLoader dataLoader) 
            : base(data, dataLoader)
        {
        }

        public ActionResult Index()
        {
            var advertisements = this.Data.Advertisements.All().Include(a => a.Category).Include(a => a.Owner).Include(a => a.Town);
            return View(advertisements.ToList());
        }

        // GET: Administration/Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = this.Data.Advertisements.GetById(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Administration/Advertisements/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name");
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "Name");
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name");
            return View();
        }

        // POST: Administration/Advertisements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,ImageDataURL,OwnerId,CategoryId,TownId,Date,Status")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                this.Data.Advertisements.Add(advertisement);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name", advertisement.CategoryId);
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "Name", advertisement.OwnerId);
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name", advertisement.TownId);
            return View(advertisement);
        }

        // GET: Administration/Advertisements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = this.Data.Advertisements.GetById(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name", advertisement.CategoryId);
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "Name", advertisement.OwnerId);
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name", advertisement.TownId);
            return View(advertisement);
        }

        // POST: Administration/Advertisements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,ImageDataURL,OwnerId,CategoryId,TownId,Date,Status")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                this.Data.Advertisements.Update(advertisement);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name", advertisement.CategoryId);
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "Name", advertisement.OwnerId);
            ViewBag.TownId = new SelectList(this.Data.Towns.All(), "Id", "Name", advertisement.TownId);
            return View(advertisement);
        }

        // GET: Administration/Advertisements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = this.Data.Advertisements.GetById(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Administration/Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement advertisement = this.Data.Advertisements.GetById(id);
            this.Data.Advertisements.Delete(advertisement);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Advertisements.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
