using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Railway_OnlineTicket.Models;
using System.Web.Security;

namespace Railway_OnlineTicket.Controllers
{
    public class UserController : Controller
    {
        //private object Route;
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserInfo log)
        {
            using (RailwayDbContext db = new RailwayDbContext())
            {
                var user = db.usreinfoes.Where(u => u.UserName.Equals(log.UserName) && u.Password.Equals(log.Password)).FirstOrDefault();
                var admin = db.usreinfoes.Where(u => u.UserName.Equals(log.UserName) && u.Password.Equals(log.Password) && u.role.Equals("admin")).SingleOrDefault();
                if (admin != null)
                {
                    ViewBag.Message = "Admin Login Successfull";
                    Session["Id"] = user.Id.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    string username = Session["UserName"].ToString();
                    int id = Convert.ToInt32(Session["Id"].ToString());
                    return RedirectToAction("Index", "Admin", new { username = username });
                }
                else if(user !=null)
                {
                    ViewBag.Message = "User Login Successfull";
                    Session["Id"] = user.Id.ToString();
                    Session["UserName"] = user.UserName.ToString();
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.Message = "Login UnSuccessfull";
                    ModelState.AddModelError(" ", "Username and Password is wrong");
                    return View(log);
                }
            }

        }
        [HttpGet]
        public ActionResult FPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FPassword(UserInfo fp)
        {
            using (RailwayDbContext db = new RailwayDbContext())
            {
                var forpass = db.usreinfoes.Where(u => u.UserName.Equals(fp.UserName)).FirstOrDefault();
                if (forpass != null)
                {
                    Session["Id"] = forpass.Id.ToString();
                    int id = Convert.ToInt32(Session["Id"].ToString());
                    return RedirectToAction("UPassword", "User", new { userid = id });
                }
                else
                {
                    ViewBag.Message = "UserName Doesn't Match";
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult UPassword()
        {
            int id = Convert.ToInt32(Session["Id"]);
            RailwayDbContext db = new RailwayDbContext();
            UserInfo fpass = db.usreinfoes.SingleOrDefault(p => p.Id == id);
            ViewBag.Message = "ID FOUND";

            return View();

        }

        [HttpPost]
        public ActionResult UPassword(UserInfo up)
        {
            int id = Convert.ToInt32(Session["Id"]);
            using (RailwayDbContext db = new RailwayDbContext())
            {
                UserInfo newpass = db.usreinfoes.SingleOrDefault(p => p.Id == id);

                if (newpass != null)
                {
                    newpass.Password = up.Password;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = "Db Not Found";
                }

            }
            return View();
        }


        public ActionResult LogOut()
        {
            Session.Abandon();
            //Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Register(UserInfo info)
         {
            RailwayDbContext db = new RailwayDbContext();
            UserInfo unamecheak = db.usreinfoes.Where(x=>x.UserName == info.UserName).SingleOrDefault();
            if(unamecheak !=null)
            {
                ViewBag.Message ="UserName Is Already Exist.Try Another Username";
                return View();
            }
            else
            {
                db.usreinfoes.Add(info);
                db.SaveChanges();
                return View();
            }

         }
         [HttpGet]
         public ActionResult Index()
         {
            if (Session["UserName"] != null)
            {
                var username = Session["UserName"].ToString();
                var userid = Convert.ToInt32(Session["Id"].ToString());
                RailwayDbContext db = new RailwayDbContext();
                UserInfo userinfo = db.usreinfoes.Where(x => x.UserName == username && x.Id == userid).SingleOrDefault();
                if (userinfo != null)
                {
                    return View(userinfo);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

         public ActionResult SearchRoute()
         {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
         }

         [HttpPost]
         public ActionResult SearchRoute(Fare_Query Route)
         {
            using (RailwayDbContext db = new RailwayDbContext())
            {
                var user = db.Queryes.Where(u => u.journey_date.Equals(Route.journey_date) && u.station_from.Equals(Route.station_from) && u.station_to.Equals(Route.station_to)).SingleOrDefault();
                if (user != null)
                {
                    Session["ticketid"] = user.ticketid.ToString();
                    int id = Convert.ToInt32(Session["ticketid"].ToString());
                    return RedirectToAction("RouteDetails", "User", new { ticketid = id });
                }
                else
                {
                    ViewBag.Message = "DB NOT FOUND";
                }
            }
            return RedirectToAction("Index");
         }

         [HttpGet]
         public ActionResult RouteDetails()
         {
            if(Session["UserName"] !=null)
            {
                int id = Convert.ToInt32(Session["ticketid"].ToString());
                RailwayDbContext db = new RailwayDbContext();
                Fare_Query users = db.Queryes.SingleOrDefault(p => p.ticketid == id);
                
                return View(users);
            }
            else
            {
                return RedirectToAction("Login");
            }

         }

        public ActionResult ShowPrice()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult ShowPrice(Fare_Query query)
        {
            using (RailwayDbContext db = new RailwayDbContext())
            {
                var user = db.Queryes.Where(u => u.journey_date.Equals(query.journey_date) && u.station_from.Equals(query.station_from) && u.station_to.Equals(query.station_to) && u.train_name.Equals(query.train_name) && u.Class.Equals(query.Class)).SingleOrDefault();
                if (user != null)
                {
                    Session["journey_date"] = user.journey_date.ToString();
                    Session["station_from"] = user.station_from.ToString();
                    Session["station_to"] = user.station_to.ToString();
                    Session["train_name"] = user.train_name.ToString();
                    Session["Class"] = user.Class.ToString();
                    Session["ticketid"] = user.ticketid.ToString();
                    int id = Convert.ToInt32(Session["ticketid"].ToString());

                    DateTime today = DateTime.Now;
                    var dbdate = Convert.ToDateTime((from Fare_Query in db.Queryes
                                                     where Fare_Query.ticketid == id
                                                     select Fare_Query).Select(x => x.journey_time).Single());
                    if(today > dbdate)
                    {
                        ViewBag.Message = "Date old";
                    }
                    else
                    {
                        int aseat = Convert.ToInt32(Request.Form["Available_seat"].ToString());
                        Session["Available_seat"] = aseat;
                        var seat = (from Fare_Query in db.Queryes
                                    where Fare_Query.ticketid == id
                                    select Fare_Query).Select(x => x.Available_seat).Single();
                        if (seat >= aseat)
                        {
                            var price = (from Fare_Query in db.Queryes
                                         where Fare_Query.ticketid == id
                                         select Fare_Query).Select(x => x.unique_price).Single();

                            int total_price = price * aseat;
                            Session["total_price"] = total_price;

                            return RedirectToAction("DetailPrice", "User", new { ticketid = id });

                        }
                        else
                        {
                            ViewBag.Message = "Seat Unavailable";
                        }

                    }

                }
                else
                {
                    ViewBag.Message = "Not Found";
                }
            }
            return View();
        }
        public ActionResult DetailPrice()
        {
            if(Session["UserName"] !=null)
            {
                if (Session["ticketid"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("ShowPrice");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [HttpPost]
        public ActionResult DetailPrice(Purchase_Ticket pt)
        {

            int id = Convert.ToInt32(Session["ticketid"].ToString());
            int seats = Convert.ToInt32(Session["Available_seat"].ToString());
            int tp    = Convert.ToInt32(Session["total_price"].ToString());
            using (RailwayDbContext db = new RailwayDbContext())
            {
                if(Session["ticketid"] != null)
                {
                    pt.UserName = Session["UserName"].ToString();
                    pt.Journey_date = Session["journey_date"].ToString();
                    pt.Station_from = Session["station_from"].ToString();
                    pt.Station_To = Session["station_from"].ToString();
                    pt.train_name = Session["train_name"].ToString();
                    pt.Class = Session["Class"].ToString();
                    pt.seats = seats;
                    pt.price = tp;
                    pt.date = DateTime.Now;

                    db.PurchaseTickets.Add(pt);
                    db.SaveChanges();
                    ViewBag.Message = "Working";

                    var availableseat = (from Fare_Query in db.Queryes
                                         where Fare_Query.ticketid == id
                                         select Fare_Query).Select(x => x.Available_seat).Single();
                    availableseat -= seats;

                    Fare_Query fareTable = db.Queryes.SingleOrDefault(p => p.ticketid == id);
                    if(fareTable !=null)
                    {
                        fareTable.Available_seat = availableseat;
                        //fareTable.sell_seats = seats.ToString();
                        int totalsell_seat = Int32.Parse(fareTable.sell_seats)+seats;
                        fareTable.sell_seats = totalsell_seat.ToString();
                        db.SaveChanges();
                        ViewBag.Message = "UPDATED";
                    }
                    ViewBag.Message = availableseat.ToString();
                }
                else
                {
                    ViewBag.Message = "Not Working";
                }

            }
            return View();
        }



        [HttpGet]
        public ActionResult History()
        {
            if(Session["UserName"] !=null)
            {
                RailwayDbContext db = new RailwayDbContext();
                var username = Session["UserName"].ToString();
                Purchase_Ticket useinfo = db.PurchaseTickets.Where(x=>x.UserName == username).SingleOrDefault();
                if(useinfo !=null)
                {
                    return View(useinfo);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}