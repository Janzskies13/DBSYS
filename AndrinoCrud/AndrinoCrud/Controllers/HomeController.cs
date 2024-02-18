using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AndrinoCrud.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var list = new List<User>();
            using (var db = new DBSYSEntities())
            {
                list = db.User.ToList();
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User u)
        {
            using (var db = new DBSYSEntities())
            {
                var newUser = new User();
                newUser.username = u.username;
                newUser.password = u.password;

                db.User.Add(newUser);
                db.SaveChanges();

                TempData["msg"] = $"Added {newUser.username} Successfully!";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var u = new User();
            using (var db = new DBSYSEntities())
            {
                u = db.User.Find(id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Update(User u)
        {
            using (var db = new DBSYSEntities())
            {
                var newUser = db.User.Find(u.id);
                newUser.username = u.username;
                newUser.password = u.password;

                db.SaveChanges();

                TempData["msg"] = $"Updated {newUser.username} Successfully!";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var u = new User();
            using (var db = new DBSYSEntities())
            {
                u = db.User.Find(id);
                db.User.Remove(u);
                db.SaveChanges();

                TempData["msg"] = $"Deleted {u.username} Successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}