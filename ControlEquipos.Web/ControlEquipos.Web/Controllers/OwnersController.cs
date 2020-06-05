using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlEquipos.Web.Controllers
{
    public class OwnersController : Controller
    {
        // GET: Owners
        public ActionResult Index()
        {
            return View();
        }

        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Owners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Owners/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Owners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
