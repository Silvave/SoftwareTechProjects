﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HangoverPartII.Models;
using Microsoft.AspNet.Identity;

namespace HangoverPartII.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Cocktail);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.CocktailId = new SelectList(db.Cocktails, "Id", "Author_Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CocktailId,Date,UserName,Body")] Comment comment)
        {
            //var cocktailId = db.Cocktails.Local;
            //var cocktailObj = db.Cocktails.Where(p => p.Id == cocktailId).Single();
            //comment.Cocktail = cocktailObj;
            //var authorId = User.Identity.GetUserId();
            //var authorObj = db.Users.Where(u => u.Id == authorId).Single();
            //comment.User = authorObj;
            //var date = DateTime.Now;
            //comment.Date = date;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Cocktails", new { /*cocktailId. */});
            }

            return View(comment);
            //if (ModelState.IsValid)
            //{
            //    db.Comments.Add(comment);
            //    db.SaveChanges();
            //    return RedirectToAction("Index"); //TODO: make it that it only updates
            //}

            //ViewBag.CocktailId = new SelectList(db.Cocktails, "Id", "Author_Id", comment.CocktailId);
            //return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CocktailId = new SelectList(db.Cocktails, "Id", "Author_Id", comment.CocktailId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CocktailId,Date,UserName,Body")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CocktailId = new SelectList(db.Cocktails, "Id", "Author_Id", comment.CocktailId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
