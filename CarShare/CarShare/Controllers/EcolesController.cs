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
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class EcolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ecoles
        public ActionResult Index()
        {
            return View(db.Ecoles.ToList());
        }

        // GET: Ecoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecole ecole = db.Ecoles.Find(id);
            if (ecole == null)
            {
                return HttpNotFound();
            }
            return View(ecole);
        }

        // GET: Ecoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ecoles/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom")] Ecole ecole)
        {
            if (ModelState.IsValid)
            {
                db.Ecoles.Add(ecole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ecole);
        }

        // GET: Ecoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecole ecole = db.Ecoles.Find(id);
            if (ecole == null)
            {
                return HttpNotFound();
            }
            return View(ecole);
        }

        // POST: Ecoles/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom")] Ecole ecole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ecole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ecole);
        }

        // GET: Ecoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ecole ecole = db.Ecoles.Find(id);
            if (ecole == null)
            {
                return HttpNotFound();
            }
            return View(ecole);
        }

        // POST: Ecoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ecole ecole = db.Ecoles.Find(id);
            db.Ecoles.Remove(ecole);
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
