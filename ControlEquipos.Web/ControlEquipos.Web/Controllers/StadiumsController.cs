﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlEquipos.Web.Models;

namespace ControlEquipos.Web.Controllers
{
    public class StadiumsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AllStadiums()
        {
            var pets = db.Stadiums.Include(o => o.Owner).Include(u => u.Owner.ApplicationUser).ToList();
            return View(pets);
        }
        // GET: Stadiums
        public ActionResult Index()
        {
            var stadiums = db.Stadiums.Include(s => s.Owner);
            return View(stadiums.ToList());
        }

        // GET: Stadiums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = db.Stadiums.Find(id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            return View(stadium);
        }

        // GET: Stadiums/Create
        public ActionResult Create()
        {
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId");
            return View();
        }

        // POST: Stadiums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StadiumName,InaugurationDate,Capacity,OwnerID,Imagen,About")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                db.Stadiums.Add(stadium);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadium.OwnerID);
            return View(stadium);
        }

        // GET: Stadiums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = db.Stadiums.Find(id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadium.OwnerID);
            return View(stadium);
        }

        // POST: Stadiums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StadiumName,InaugurationDate,Capacity,OwnerID,Imagen,About")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stadium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owners, "Id", "UserId", stadium.OwnerID);
            return View(stadium);
        }

        // GET: Stadiums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = db.Stadiums.Find(id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            return View(stadium);
        }

        // POST: Stadiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stadium stadium = db.Stadiums.Find(id);
            db.Stadiums.Remove(stadium);
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
            Stadium estadios = db.Stadiums.Find(id);
            byte[] byteImage = estadios.Imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "img/jpg");


        }
    }
}
