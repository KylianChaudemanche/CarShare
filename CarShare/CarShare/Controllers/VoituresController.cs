﻿using System;
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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CarShare.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
    public class VoituresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: Voitures
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Index()
        {
            if(User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
            {
                return View(db.Voitures.ToList());
            }
            else
            {
                return View(db.Voitures.Where(v => v.Proprietaire.Id == currentUser.Id).ToList());
            }
        }

        // GET: Voitures/Details/5
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: Voitures/Create
        [Authorize(Roles = "SuperAdmin,Utilisateur")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voitures/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Utilisateur")]
        public ActionResult Create([Bind(Include = "Id,NbPlaces,Immatriculation,Couleur,Marque,Modele")] Voiture voiture)
        {
            var currentUserId = currentUser.Id;
            if (ModelState.IsValid)
            {
                voiture.Proprietaire = db.Users.FirstOrDefault(u => u.Id == currentUserId);
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)).AddToRole(currentUserId, "Conducteur");
                var currentUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);
                currentUser.ListeVoitures.Add(voiture);
                db.Voitures.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voiture);
        }

        // GET: Voitures/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin,Conducteur")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: Voitures/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin,Conducteur")]
        public ActionResult Edit([Bind(Include = "Id,NbPlaces,Immatriculation,Couleur,Marque,Modele")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voiture);
        }

        // GET: Voitures/Delete/5
        [Authorize(Roles = "SuperAdmin,Conducteur")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voiture voiture = db.Voitures.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Conducteur")]
        public ActionResult DeleteConfirmed(int id)
        {
            Voiture voiture = db.Voitures.Find(id);
            db.Voitures.Remove(voiture);
            var currentUser = db.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            if (!currentUser.ListeVoitures.Any())
            {
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db)).RemoveFromRole(User.Identity.GetUserId(), "Conducteur");
            }
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
