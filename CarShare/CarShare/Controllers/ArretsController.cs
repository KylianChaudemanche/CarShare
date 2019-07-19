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
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CarShare.Controllers
{
    public class ArretsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //ArretsViewModels vm = new ArretsViewModels();

          

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
            ArretsViewModels vm = CreateVM();
            return View(vm);
        }

        // GET: Arrets/Create
        public ArretsViewModels CreateVM(int id = 0)
        {
            ArretsViewModels vm = new ArretsViewModels();

            vm.EtatArret = 0;
            vm.selectedLongitude = -1.69F;
            vm.selectedLatitude = 48.38F;
            vm.Emplacement = new Emplacement() { Latitude = vm.selectedLatitude, Longitude = vm.selectedLongitude };
            vm.Trajet = db.Trajets.Find(vm.selectedTrajet);
            vm.Arret = db.Arrets.FirstOrDefault(p => p.Id == id);
            return vm;
        }

        // POST: Arrets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArretsViewModels vm)
        {
            
            
            if (ModelState.IsValid)
            {
                var arret = vm.Arret;
                
                db.Arrets.Add(arret);
                db.SaveChanges();
                Task.Factory.StartNew(() =>
                {

                    var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress("test@example.com", "Example User");
                    var subject = "Sending with Twilio SendGrid is Fun";
                    var to = new EmailAddress("aurelie1032@yahoo.fr", "Example User");
                    var plainTextContent = "and easy to do anywhere, even with C#";
                    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = client.SendEmailAsync(msg);

                });
                   
                return RedirectToAction("Index");
            }

            return View();
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
