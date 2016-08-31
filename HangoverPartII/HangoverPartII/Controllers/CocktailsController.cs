using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HangoverPartII.Models;
using HangoverPartII.ViewModels;
using System.IO;

namespace HangoverPartII.Controllers
{
    public class CocktailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cocktails
        public ActionResult Index()
        {
            var cocktails = db.Cocktails.Include(c => c.Author);
            return View(cocktails);
        }

        // GET: Cocktails/Details/5
        public ActionResult Details(int? id)
        {
            CocktailViewModel m = new CocktailViewModel();
            m.FirstCocktailId = db.Cocktails.OrderBy(x => x.Id).FirstOrDefault();
            m.LastCocktailId = db.Cocktails.OrderByDescending(x => x.Id).FirstOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m.Cocktail = db.Cocktails.Find(id);
            //m.Cocktail.Author = db.Cocktails.Include(c => c.Author).Where(x );
            if (m.Cocktail == null)
            {
                return HttpNotFound();
            }
            //cocktail.Author = db.Cocktails.Add(a => a.Author.UserName);
            return View(m);
        }

        // GET: Cocktails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cocktails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Image,Title,Body,NetLikeCount")] Cocktail cocktail, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string name = Path.GetFileName(image.FileName);
                string path = Path.Combine(Server.MapPath("~/Images"), name);
                image.SaveAs(path);
                cocktail.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                cocktail.Image = name;
                db.Cocktails.Add(cocktail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Author_Id = new SelectList(db.ApplicationUsers, "Id", "FullName");
            return View(cocktail);
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            string name = Path.GetFileName(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Images"), name);
            file.SaveAs(path);

            return View("Index");
        }

        // GET: Cocktails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cocktail cocktail = db.Cocktails.Find(id);
            if (cocktail == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Author_Id = new SelectList(db.ApplicationUsers, "Id", "FullName");
            return View(cocktail);
        }

        // POST: Cocktails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Image,Title,Body,NetLikeCount")] Cocktail cocktail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cocktail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cocktail);
        }

        // GET: Cocktails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cocktail cocktail = db.Cocktails.Find(id);
            if (cocktail == null)
            {
                return HttpNotFound();
            }
            return View(cocktail);
        }

        // POST: Cocktails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cocktail cocktail = db.Cocktails.Find(id);
            db.Cocktails.Remove(cocktail);
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
