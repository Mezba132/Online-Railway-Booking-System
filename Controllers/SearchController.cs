using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Railway_OnlineTicket.Models;

namespace Railway_OnlineTicket.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public JsonResult Index(string term)
        {
            RailwayDbContext context = new RailwayDbContext();
            List<string> querye = new List<string>();
            List<Fare_Query> clist = context.Queryes.Where(c => c.journey_date.Contains(term) || c.train_name.Contains(term)).ToList();
            foreach (Fare_Query c in clist)
            {
                querye.Add(c.journey_date);
            }
            foreach (Fare_Query c in clist)
            {
                querye.Add(c.train_name);
            }
            return Json(querye, JsonRequestBehavior.AllowGet);
        }
    }
}