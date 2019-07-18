using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CarShare.BO;
using CarShare.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CarShare.Controllers
{
    public class TrajetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: Trajets
        public ActionResult Index()
        {
            return View(db.Trajets.Include(t => t.Conducteur).Include(t => t.Passagers).Include(t => t.Depart).Include(t => t.Arrive).Include(t => t.Arrets).ToList());
        }

        // GET: Trajets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trajet trajet = db.Trajets.Where(t => t.Id == id).Include(t => t.Conducteur).Include(t => t.Passagers).Include(t => t.Depart).Include(t => t.Arrive).Include(t => t.Arrets).FirstOrDefault();
            if (trajet == null)
            {
                return HttpNotFound();
            }
            
            return View(trajet);
        }

        // GET: Trajets/Create
        public ActionResult Create()
        {
            var vm = new TrajetsViewModels();
            vm.Conducteur = db.Users.Where(u => u.Id == currentUser.Id).Include(u => u.Ecole.Emplacement).Include(u => u.EmplacementsFavoris).Include(u => u.Voiture).FirstOrDefault();
            vm.listeEmplacements = vm.Conducteur.EmplacementsFavoris.ToList();
            vm.listeEmplacements.Add(vm.Conducteur.Ecole.Emplacement);
            vm.selectedDepart = vm.Conducteur.EmplacementsFavoris[0].Id;
            vm.selectedArrivee = vm.Conducteur.Ecole.Emplacement.Id;
            vm.selectedHeureDepart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 30, 0);
            vm.selectedDateDebut = DateTime.Now;

            return View(vm);
        }

        // POST: Trajets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrajetsViewModels vm)
        {
            vm.Conducteur = db.Users.Where(u => u.Id == currentUser.Id).Include(u => u.Ecole.Emplacement).Include(u => u.EmplacementsFavoris).Include(u => u.Voiture).FirstOrDefault();
            vm.listeEmplacements = vm.Conducteur.EmplacementsFavoris.ToList();
            vm.listeEmplacements = vm.Conducteur.EmplacementsFavoris.ToList();
            vm.listeEmplacements.Add(vm.Conducteur.Ecole.Emplacement);

            if (ModelState.IsValid)
            {

                if (vm.selectedDepart == vm.selectedArrivee)
                {
                    ModelState.AddModelError("", $"Départ et Arrivée ne peuvent pas être identiques");
                    return View(vm);
                }

                if (vm.selectedDateDebut >= vm.selectedDateFin)
                {
                    ModelState.AddModelError("", $"La date de Début doit être avant la date de Fin");
                    return View(vm);
                }

                if(vm.selectedDateDebut.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError("", $"Impossible de choisir une date passé pour la date de début");
                    return View(vm);
                }


                // Create listDate
                vm.selectedDateDebut.AddHours(vm.selectedHeureDepart.Hour).AddMinutes(vm.selectedHeureDepart.Minute);
                vm.selectedDateFin.AddHours(vm.selectedHeureDepart.Hour).AddMinutes(vm.selectedHeureDepart.Minute);
                var listeDates = Enumerable.Range(0, 1 + vm.selectedDateFin.Subtract(vm.selectedDateDebut).Days)
                  .Select(offset => vm.selectedDateDebut.AddDays(offset))
                  .ToArray();
                
                // Verifier que l'utilisateur courant n'ai pas déjà des trajets sur cette date
                if (db.Trajets.Where(t => t.Conducteur.Id == currentUser.Id).Where(t => listeDates.Contains(t.Date)).Any() )
                {
                    ModelState.AddModelError("", $"Vous avez déja des Trajets de prévu sur cette période");
                    return View(vm);
                }
                // Remove Holidays and week-ends
                // https://stackoverflow.com/questions/3709584/business-holiday-date-handling/38366429

                // Create list of Trajets
                foreach (DateTime date in listeDates)
                {
                    vm.Trajet = new Trajet() { Depart = db.Emplacements.SingleOrDefault(e => e.Id == vm.selectedDepart), Arrive = db.Emplacements.SingleOrDefault(e => e.Id == vm.selectedArrivee), Conducteur = db.Users.SingleOrDefault(u => u.Id == currentUser.Id), Date = date.AddHours(vm.selectedHeureDepart.Hour).AddMinutes(vm.selectedHeureDepart.Minute) };
                    db.Trajets.Add(vm.Trajet);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Trajets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new TrajetsViewModels();
            //vm.Trajet = db.Trajets.Where(t => t.Id == id).Include(t => t.Arrets).Include(t => t.Depart).Include(t => t.Arrive).Include(t => t.Conducteur.Ecole.Emplacement).Include(t => t.Passagers).FirstOrDefault();
            vm.Trajet = db.Trajets.Find(id);
            vm.listeEmplacements = vm.Trajet.Conducteur.EmplacementsFavoris.ToList();
            vm.listeEmplacements.Add(vm.Trajet.Conducteur.Ecole.Emplacement);
            vm.selectedHeureDepart = vm.Trajet.Date;

            if (vm.Trajet == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Trajets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trajet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trajet);
        }

        // GET: Trajets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trajet trajet = db.Trajets.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            return View(trajet);
        }

        // POST: Trajets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trajet trajet = db.Trajets.Find(id);
            db.Trajets.Remove(trajet);
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
