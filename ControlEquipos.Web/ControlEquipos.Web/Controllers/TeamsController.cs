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
using Microsoft.AspNet.Identity;

namespace ControlEquipos.Web.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AllTeams()
        {
            var pets = db.Teams.Include(o => o.Owner).Include(u => u.Owner.ApplicationUser).ToList();
            return View(pets);
        }
        // GET: Teams
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var ow = db.Owners.Where(o => o.UserId == user).FirstOrDefault();
            var equipos = db.Teams.Include(u => u.Owner).Where(p => p.OwnerID == ow.Id).ToList();

            return View(equipos);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner = (from o in db.Owners
                                select o).ToList();
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.Owner = (from o in db.Owners
                             select o).ToList();
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamName,FoundationDate,OwnerID,Location,Championships,Imagen")] Team team)
        {
            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

            team.Imagen = image.GetBytes();

            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", team.OwnerID);
            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", team.OwnerID);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeamName,FoundationDate,OwnerID,Location,Championships,Imagen")] Team team)
        {
            if (ModelState.IsValid)
            {
                byte[] imagenActual = null;
                HttpPostedFileBase FileBase = Request.Files[0];

                if (FileBase == null)
                {
                    imagenActual = db.Teams.SingleOrDefault(t => t.Id == team.Id).Imagen;
                }

                else
                {
                    WebImage image = new WebImage(FileBase.InputStream);

                    team.Imagen = image.GetBytes();
                }

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", team.OwnerID);
            return View(team);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
            Team equipos = db.Teams.Find(id);
            byte[] byteImage = equipos.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "img/jpg");


        }
    }
}
