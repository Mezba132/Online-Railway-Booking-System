using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Railway_OnlineTicket.Models;

namespace Railway_OnlineTicket.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index(string username)
        {
             if (username != null)
             {
                var user = Session["UserName"].ToString();
                RailwayDbContext db = new RailwayDbContext();
                 return View(db.Queryes.ToList());
             }
             else
             {
                 return RedirectToAction("Login","User");
             }

        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(Fare_Query fq)
        {
            RailwayDbContext db = new RailwayDbContext();
            Fare_Query search = db.Queryes.Where(x => x.journey_date == fq.journey_date).SingleOrDefault();
            if (search != null)
            {
                Session["ticketid"] = search.ticketid.ToString();
                var id = Convert.ToInt32(Session["ticketid"].ToString());
                return RedirectToAction("DateShow", "Admin", new { ticketid = id });
            }
            else
            {
                ViewBag.Message = "Not Found";
                return View();
            }
        }

        public ActionResult Farequery()
        {
            if(Session["UserName"] !=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public ActionResult Farequery(Fare_Query fare)
        {
            if(Session["UserName"] != null)
            {
                RailwayDbContext db = new RailwayDbContext();

                db.Queryes.Add(fare);
                db.SaveChanges();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public ActionResult report()
        {
            return View();
        }
        /*[HttpPost]
        public ActionResult report(Fare_Query fq)
        {
            RailwayDbContext db = new RailwayDbContext();
            var today = DateTime.Today;
            var week = today.AddDays(-3);
            var date = db.Queryes.Where ()
        }*/

     /* [HttpPost]
      public ActionResult ShowDate(Fare_Query ds)
      {
        using (RailwayDbContext db = new RailwayDbContext())
        {
            var date = db.Queryes.Where(x => x.journey_date == ds.journey_date && x.ticketid == ds.ticketid).SingleOrDefault();
            if (date != null)
            {
                Session["journey_date"] = date.journey_date.ToString();
                Session["ticketid"] = date.ticketid.ToString();
                int id = Convert.ToInt32(Session["ticketid"].ToString());
                return RedirectToAction("DateShow", "Admin", new { ticketid = id });
            }

        }
        return View();
    }*/

    [HttpGet]
    public ActionResult DateShow()
    {
        int id = Convert.ToInt32(Session["ticketid"].ToString());
        RailwayDbContext db = new RailwayDbContext();
        Fare_Query users = db.Queryes.SingleOrDefault(p => p.ticketid == id);
        return View(users);
    }
    }
}