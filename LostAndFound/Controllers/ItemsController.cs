using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LostAndFound.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LostAndFound.Controllers
{
    public class ItemsController : Controller
    {
        private ItemDBContext db = new ItemDBContext();

        protected ApplicationDbContext ApplicationDbContext { get; set; }

        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ItemsController() 
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        // GET: Items
        public ActionResult Index(bool lost = false)
        {
            var list = new List<Item>();
            var items = db.Items.Where(x => x.Lost == lost).ToList();
            list.AddRange(items);

            return View(list);
        }

        public ActionResult MyItems()
        {
            var userId = User.Identity.GetUserId();

            if (userId != null)
            {
                var foundItemsList = new List<Item>();
                var foundItems = db.Items.Where(x=>x.UserId == userId && x.Lost == false).ToList();
                foundItemsList.AddRange(foundItems);
                
                var lostItemsList = new List<Item>();
                var lostItems = db.Items.Where(x=>x.UserId == userId && x.Lost == true).ToList();
                lostItemsList.AddRange(lostItems);
                
                MyItemsViewModel viewModel = new MyItemsViewModel() 
                {
                    FoundItems = foundItems,
                    LostItems = lostItems
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserId,Name,Description,Category,ImgUrl,Reward,Currency,Claim,Lat,Lon,County,Adress,FoundDate,LostDateFrom,LostDateTo,Lost")] Item item)
        {
            if (ModelState.IsValid)
            {
                //Add user to the model
                var userId = User.Identity.GetUserId();
                item.UserId = userId;

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserId,Name,Description,Category,ImgUrl,Reward,Currency,Claim,Lat,Lon,County,Adress,FoundDate,LostDateFrom,LostDateTo,Lost")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyItems");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
