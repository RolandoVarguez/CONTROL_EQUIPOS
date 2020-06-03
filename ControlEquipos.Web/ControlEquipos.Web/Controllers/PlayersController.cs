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
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Players
        [Authorize]
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = (from t in db.Teams
                             select t).ToList();
            return View(player);
        }

        // GET: Players/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Team = (from t in db.Teams
                            select t).ToList();
            ViewBag.TeamID = new SelectList(db.Teams, "Id", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlayerName,PlayerLastName,Nationality,BornDate,TeamID,Imagen,About")] Player player)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase == null)
            {
                imagenActual = db.Teams.SingleOrDefault(t => t.Id == player.Id).Imagen;
            }

            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                player.Imagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamID = new SelectList(db.Teams, "Id", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = (from t in db.Teams
                            select t).ToList();
            ViewBag.TeamID = new SelectList(db.Teams, "Id", "TeamName", player.TeamID);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlayerName,PlayerLastName,Nationality,BornDate,TeamID,Imagen,About")] Player player)
        {
            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase == null)
            {
                imagenActual = db.Teams.SingleOrDefault(t => t.Id == player.Id).Imagen;
            }

            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                player.Imagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamID = new SelectList(db.Teams, "Id", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
            Player jugadores = db.Players.Find(id);
            byte[] byteImage = jugadores.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "img/jpg");

        }
        [Authorize]
        public ActionResult PlayersByTeam(int teamId)
        {
            var players = db.Players.Where(p => p.TeamID == teamId).ToList();
            return View(players);
        }
        public ActionResult FullPlayersByTeam(int teamId)
        {
            var players = db.Players.Where(p => p.TeamID == teamId).ToList();
            return View(players);
        }

        public ActionResult FullPlayers()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }

        public ActionResult FullDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.Team = (from t in db.Owners
                            select t).ToList();
            return View(player);
        }


    }
}
