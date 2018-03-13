using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FreelanceTimeCardTracker.DAL;
using FreelanceTimeCardTracker.Models;
using Microsoft.AspNet.Identity;

namespace FreelanceTimeCardTracker.Controllers
{
    public class HomeController : Controller
    {

        public ITimeCardDAL dal;

        public HomeController(ITimeCardDAL dAL)
        {
            this.dal = dAL;
        }

        public ActionResult Index(TimeCard timeCard)
        {

            TimeCard temp = dal.GetTimeCard(User.Identity.GetUserName());

            return View(temp);
        }

        [HttpPost]
        public ActionResult Index(TimeCard timeCard, string Punch)
        {
            if (Punch == "punchin")
            {
                dal.PunchIn(timeCard);
            }

            if(Punch == "punchout")
            {
                dal.PunchOut(timeCard);
            }

            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}