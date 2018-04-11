using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IWeatherDAL
    {
        List<Weather> GetWeather(string parkCode);
    }
}
