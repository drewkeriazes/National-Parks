using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        

        private string connectionString;

        public HomeController(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IParkDAL _dal;

        public HomeController(IParkDAL dal)
        {
            _dal = dal;
        }

        public ActionResult AllParks()
        {
            IList<Park> parkList = _dal.GetAllParks();

            return View("AllParks", parkList);
        }

        public ActionResult ParkDetail(string parkCode)
        {
            if (parkCode.Equals(null))
            {
                return Index();
            }

            var park = _dal.GetPark(parkCode);
            return View("ParkDetail", park);
        }

        // GET: Home
        public ActionResult Index()
        {
            if (Session["isFahrenheit"] == null)
            {
                Session["isFahrenheit"] = "True";
            }

            return View("Index");
        }

        public ActionResult AllParksWeatherList()
        {
            IList<Park> parkList = _dal.GetAllParks();

            if (Session["isFahrenheit"] == null)
            {
                Session["isFahrenheit"] = "True";
            }

            return View("AllParksWeatherList", parkList);
        }

    }
}