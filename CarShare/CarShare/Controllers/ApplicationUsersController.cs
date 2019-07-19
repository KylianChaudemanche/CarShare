using CarShare.BO;
using CarShare.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CarShare.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            ListApplicationUsersVM appUsersVM = new ListApplicationUsersVM();
            appUsersVM.ListRolesDispo = db.Role.ToList();
            appUsersVM.ListApplicationUser = db.Users.ToList();
            return View(appUsersVM);
        }

        // GET: ApplicationUsers/Details/5
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Edit(string id)
        {
            if (User.Identity.GetUserId() == id || User.IsInRole("SuperAdmin") || User.IsInRole("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicationUsersVM applicationUsersVM = new ApplicationUsersVM()
                {
                    ApplicationUser = db.Users.Find(id),
                    ListEcolesDispo = db.Ecoles.ToList(),
                    ListRolesDispo = db.Role.ToList()
                };

                if (applicationUsersVM.ApplicationUser == null)
                {
                    return HttpNotFound();
                }
                if (applicationUsersVM.ApplicationUser.Ecole != null)
                {
                    applicationUsersVM.IdEcoleSelected = applicationUsersVM.ApplicationUser.Ecole.Id;
                }
                return View(applicationUsersVM);
            }
            return Edit(User.Identity.GetUserId());
        }

        // POST: ApplicationUsers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin,Admin,Utilisateur")]
        public ActionResult Edit(ApplicationUsersVM applicationUsersVM)
        {

            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == applicationUsersVM.ApplicationUser.Id);
            user.Ecole = db.Ecoles.FirstOrDefault(e => e.Id == applicationUsersVM.IdEcoleSelected);
            user.Email = applicationUsersVM.ApplicationUser.Email;
            user.UserName = applicationUsersVM.ApplicationUser.Email;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);

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
