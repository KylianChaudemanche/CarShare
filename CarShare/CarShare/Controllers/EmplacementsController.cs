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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CarShare.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
    public class EmplacementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: Emplacements
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Index()
        {
            EmplacementViewModels vm = new EmplacementViewModels();
            if (User.IsInRole("SuperAdmin") || User.IsInRole("Admin") )
            {
                return View(db.Emplacements.ToList());
            }
            else
            {
                return View(db.Users.FirstOrDefault(u => u.Id == currentUser.Id).EmplacementsFavoris);
            }
        }

        // GET: Emplacements/Details/5
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
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
        [Authorize(Roles = "SuperAdmin,Utilisateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emplacements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Utilisateur")]
        public ActionResult Create([Bind(Include = "Id,Intitule,Description,Latitude,Longitude")] Emplacement emplacement)
        {
            if (ModelState.IsValid)
            {
                db.Emplacements.Add(emplacement);
                var user = db.Users.Find(currentUser.Id);
                user.EmplacementsFavoris.Add(emplacement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emplacement);
        }

        // GET: Emplacements/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
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
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Edit([Bind(Include = "Id,Intitule,Description,Latitude,Longitude")] Emplacement emplacement)
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
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
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
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
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
