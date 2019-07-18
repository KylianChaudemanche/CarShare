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
    [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
    public class ArretsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Arrets
        public ActionResult Index()
        {
            return View(db.Arrets.ToList());
        }

        // GET: Arrets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arret arret = db.Arrets.Find(id);
            if (arret == null)
            {
                return HttpNotFound();
            }
            return View(arret);
        }

        // GET: Arrets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arrets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Horaire,EtatArret")] Arret arret)
        {
            if (ModelState.IsValid)
            {
                db.Arrets.Add(arret);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arret);
        }

        // GET: Arrets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arret arret = db.Arrets.Find(id);
            if (arret == null)
            {
                return HttpNotFound();
            }
            return View(arret);
        }

        // POST: Arrets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Horaire,EtatArret")] Arret arret)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arret).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arret);
        }

        // GET: Arrets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arret arret = db.Arrets.Find(id);
            if (arret == null)
            {
                return HttpNotFound();
            }
            return View(arret);
        }

        // POST: Arrets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arret arret = db.Arrets.Find(id);
            db.Arrets.Remove(arret);
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
