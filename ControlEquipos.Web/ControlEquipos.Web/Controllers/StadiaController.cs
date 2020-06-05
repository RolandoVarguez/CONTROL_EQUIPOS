using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ControlEquipos.Web.Models;

namespace ControlEquipos.Web.Controllers
{
    public class StadiaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult AllStadiums()
        {
            var estadio = db.Stadiums.Include(o => o.Owner).Include(u => u.Owner.ApplicationUser).ToList();
            return View(estadio);
        }

        // GET: Stadia
        [Authorize]
        public ActionResult Index()
        {
            var stadiums = db.Stadiums.Include(s => s.Owner);
            return View(stadiums.ToList());
        }

        // GET: Stadia/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadia stadia = db.Stadiums.Find(id);
            if (stadia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner = (from t in db.Owners
                            select t).ToList();
            return View(stadia);
        }

        // GET: Stadia/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Owner = (from t in db.Owners
                             select t).ToList();
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId");
            return View();
        }

        // POST: Stadia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StadiumName,InaugurationDate,Capacity,OwnerID,Imagen,About")] Stadia stadia)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase == null)
            {
                imagenActual = db.Teams.SingleOrDefault(t => t.Id == stadia.Id).Imagen;
            }

            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                stadia.Imagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Stadiums.Add(stadia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadia.OwnerID);
            return View(stadia);
        }

        // GET: Stadia/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Owner = (from o in db.Owners
                             select o).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadia stadia = db.Stadiums.Find(id);
            if (stadia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner = (from t in db.Owners
                             select t).ToList();
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadia.OwnerID);
            return View(stadia);
        }

        // POST: Stadia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StadiumName,InaugurationDate,Capacity,OwnerID,Imagen,About")] Stadia stadia)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase == null)
            {
                imagenActual = db.Teams.SingleOrDefault(t => t.Id == stadia.Id).Imagen;
            }

            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                stadia.Imagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Stadiums.Add(stadia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                db.Entry(stadia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadia.OwnerID);
            return View(stadia);
        }

        // GET: Stadia/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadia stadia = db.Stadiums.Find(id);
            if (stadia == null)
            {
                return HttpNotFound();
            }
            return View(stadia);
        }

        // POST: Stadia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stadia stadia = db.Stadiums.Find(id);
            db.Stadiums.Remove(stadia);
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

        public ActionResult getImage(int id)
        {
            Stadia estadios = db.Stadiums.Find(id);
            byte[] byteImage = estadios.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "img/jpg");


        }

        public ActionResult FullStadiums()
        {

            var stadiums = db.Stadiums.Include(s => s.Owner);
            return View(stadiums.ToList());
        }

        // GET: Stadiums/Details/5
    
        public ActionResult FullDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadia stadium = db.Stadiums.Find(id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner = (from o in db.Owners
                             select o).ToList();
            return View(stadium);
        }

    }
}
