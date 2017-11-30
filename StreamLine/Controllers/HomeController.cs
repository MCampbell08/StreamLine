using StreamLineLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StreamLine.Models;
using StreamLine.Services;
using System.Collections;
using StreamLineLib;

namespace StreamLine.Controllers
{
    public class HomeController : Controller
    {
        //ProfileService is the hard coded Data
        //EFProfileService is the Data located in the Database  
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("ProfilePageView", "Account");
            }
            return RedirectToAction("Register", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page, Could Just play with the hard coded test data on this page";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}