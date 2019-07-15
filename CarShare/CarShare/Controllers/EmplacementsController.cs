using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarShare.BO;
using CarShare.Models;

namespace CarShare.Controllers
{
    public class EmplacementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emplacements
        public ActionResult Index()
        {
            return View(db.Emplacements.ToList());
        }

        // GET: Emplacements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplacement emplacement = db.Emplacements.Find(id);
            if (emplacement == null)
            {
                return HttpNotFound();
            }
            return View(emplacement);
        }

        // GET: Emplacements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emplacements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ville,CodePostal,Rue,Latitude,Longitude")] Emplacement emplacement)
        {
            if (ModelState.IsValid)
            {
                db.Emplacements.Add(emplacement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emplacement);
        }

        // GET: Emplacements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplacement emplacement = db.Emplacements.Find(id);
            if (emplacement == null)
            {
                return HttpNotFound();
            }
            return View(emplacement);
        }

        // POST: Emplacements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ville,CodePostal,Rue,Latitude,Longitude")] Emplacement emplacement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emplacement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emplacement);
        }

        // GET: Emplacements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emplacement emplacement = db.Emplacements.Find(id);
            if (emplacement == null)
            {
                return HttpNotFound();
            }
            return View(emplacement);
        }

        // POST: Emplacements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emplacement emplacement = db.Emplacements.Find(id);
            db.Emplacements.Remove(emplacement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
