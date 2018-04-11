using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherDAL
    {
        private string _connectionString;

        public WeatherSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string SQL_GetWeatherDetails = "Select weather.parkCode, weather.fiveDayForecastValue, weather.low, weather.high, weather.forecast, park.parkName From weather JOIN park On park.parkCode = weather.parkCode Where weather.parkCode = @ParkCode;";

        public List<Weather> GetWeather(string parkCode)
        {
            List<Weather> weatherDetails = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetWeatherDetails, conn);
                    cmd.Parameters.AddWithValue("@ParkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        weatherDetails.Add(GetWeatherFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return weatherDetails;
        }

        private Weather GetWeatherFromReader(SqlDataReader reader)
        {
            Weather convertWeather = new Weather();
            convertWeather.ParkCode = Convert.ToString(reader["parkCode"]);
            convertWeather.ParkName = Convert.ToString(reader["parkName"]);
            convertWeather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            convertWeather.LowTempF = Convert.ToDouble(reader["low"]);
            convertWeather.HighTempF = Convert.ToDouble(reader["high"]);
            convertWeather.Forecast = Convert.ToString(reader["forecast"]);
            
            return convertWeather;
        }

      
    }
}