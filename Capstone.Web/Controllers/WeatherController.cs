using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherDAL _dal;
        public WeatherController(IWeatherDAL dal)
        {
            _dal = dal;
        }

        public ActionResult WeatherDetail(string parkCode)
        {

            var weather = _dal.GetWeather(parkCode);
            return View("WeatherDetail", weather);
        }

        public ActionResult isFahrenheit(string isFahrenheit, string parkCode)
        {
   
            Session["isFahrenheit"] = isFahrenheit;

            var weather = _dal.GetWeather(parkCode);
            return View("WeatherDetail", weather);
        }



    }
}