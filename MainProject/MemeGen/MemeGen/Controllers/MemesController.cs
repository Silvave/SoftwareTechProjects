using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemeGen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MemeGen.Controllers
{
    public class MemesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Memes
        public ActionResult Index()
        {
            //string pic = System.IO.Path.GetFileName(meme.Path);
            //string path = System.IO.Path.Combine(Server.MapPath("~/Images"), pic);

            return View(db.Memes.ToList());
        }

        // GET: Memes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meme meme = db.Memes.Find(id);
            if (meme == null)
            {
                return HttpNotFound();
            }
            return View(meme);
        }

        // GET: Memes/Create
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddImage(HttpPostedFileBase ImagePath)
        //{
        //    string pic = System.IO.Path.GetFileName(ImagePath.FileName);
        //    string path = System.IO.Path.Combine(Server.MapPath("~/Content/CreatedMemes"), pic);
        //    ImagePath.SaveAs(path);
        //    
        //    Meme meme = new Meme { Path = "~/Content/CreatedMemes" + pic };
        //    db.Memes.Add(meme);
        //    db.SaveChanges();
        //    
        //    return RedirectToAction("Index");
        //}


        // POST: Memes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Path,TopText,BottomText")] Meme meme)
        {
            if (ModelState.IsValid)
            {
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>
                    (new UserStore<ApplicationUser>(db));
                ApplicationUser user = UserManager.FindByName(this.User.Identity.GetUserName());
                meme.Author = user;

                db.Memes.Add(meme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meme);
        }

        // GET: Memes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meme meme = db.Memes.Find(id);
            if (meme == null)
            {
                return HttpNotFound();
            }
            return View(meme);
        }

        // POST: Memes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Path,TopText,BottomText")] Meme meme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meme);
        }

        // GET: Memes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meme meme = db.Memes.Find(id);
            if (meme == null)
            {
                return HttpNotFound();
            }
            return View(meme);
        }

        // POST: Memes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meme meme = db.Memes.Find(id);
            db.Memes.Remove(meme);
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
