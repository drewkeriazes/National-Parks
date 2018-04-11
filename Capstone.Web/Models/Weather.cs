using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public int FiveDayForecastValue { get; set; }
        public double LowTempF { get; set; }
        public double HighTempF { get; set; }
        public double LowTempC
        {
            get
            {
                return (((LowTempF - 32)*5)/9);
            }
        }
        public double HighTempC
        {
            get
            {
                return (((HighTempF - 32) * 5) / 9);
            }
        }
        
        public string Forecast { get; set; }
    }
}