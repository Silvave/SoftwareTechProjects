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

            m.FirstCocktailId = db.Cocktails.OrderBy(x => x.Id).FirstOrDefault().Id;
            m.LastCocktailId = db.Cocktails.OrderByDescending(x => x.Id).FirstOrDefault().Id;

            foreach (var cocktail in db.Cocktails.OrderByDescending(i => i.Id))
            { if (cocktail.Id < id) { m.PreviousCocktailId = cocktail.Id; break; } }

            foreach (var cocktail in db.Cocktails.OrderBy(i => i.Id))
            { if (cocktail.Id > id) { m.NextCocktailId = cocktail.Id; break; } }

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
                string name = UploadImage(image);
                cocktail.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                cocktail.Image = name;
                db.Cocktails.Add(cocktail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Author_Id = new SelectList(db.ApplicationUsers, "Id", "FullName");
            return View(cocktail);
        }

        public ActionResult Like (int id)
        {
            Cocktail update = db.Cocktails.ToList().Find( u => u.Id == id);
            update.NetLikeCount += 1;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public string UploadImage(HttpPostedFileBase file)
        {
            string name = null;
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                else if (file.ContentLength > 0)
                {
                    int maxContentLength = 1024 * 1024 * 3; //3 MB
                    string[] allowedFileExtensions = { ".jpg", ".gif", ".png", ".pdf" };

                    if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", allowedFileExtensions));
                    }

                    else if (file.ContentLength > maxContentLength)
                    {
                        ModelState.AddModelError("File",
                            "Your file is too large, maximum allowed size is: " + maxContentLength + " MB");
                    }
                    else
                    {
                        name = "" + Guid.NewGuid() + file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        string path = Path.Combine(Server.MapPath("~/Images"), name);
                        file.SaveAs(path);
                        ModelState.Clear();
                        ViewBag.Message = "File uploaded successfully";
                    }
                }
            }

            return name;
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
                return RedirectToAction("Index" );
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
