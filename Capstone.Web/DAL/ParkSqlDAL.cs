using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private string _connectionString;

        public ParkSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<Park> GetAllParks()
        {
            var list = new List<Park>();

            string sql = "SELECT * FROM park ORDER BY parkName ASC;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader r = cmd.ExecuteReader();

                while (r.Read())
                {
                    Park p = new Park() {

                        ParkCode = Convert.ToString(r["parkCode"]),
                        ParkName = Convert.ToString(r["parkName"]),
                        State = Convert.ToString(r["state"]),
                        Acreage = Convert.ToInt32(r["acreage"]),
                        ElevationInFeet = Convert.ToInt32(r["elevationInFeet"]),
                        MilesOfTrail = Convert.ToDouble(r["milesOfTrail"]),
                        NumberOfCampsites = Convert.ToInt32(r["numberOfCampsites"]),
                        Climate = Convert.ToString(r["climate"]),
                        YearFounded = Convert.ToInt32(r["yearFounded"]),
                        AnnualVisitorCount = Convert.ToInt32(r["annualVisitorCount"]),
                        InspirationalQuote = Convert.ToString(r["inspirationalQuote"]),
                        InspirationalQuoteSource = Convert.ToString(r["inspirationalQuoteSource"]),
                        ParkDescription = Convert.ToString(r["parkDescription"]),
                        EntryFee = Convert.ToInt32(r["entryFee"]),
                        NumberOfAnimalSpecies = Convert.ToInt32(r["numberOfAnimalSpecies"]),

                        //FiveDayForecastValue = Convert.ToInt32(r["fiveDayForecastValue"]),
                        //LowTemp = Convert.ToInt32(r["low"]),
                        //HighTemp = Convert.ToInt32(r["high"]),
                        //Forecast = Convert.ToString(r["forecast"])
                    };

                    list.Add(p);
                }
            }
            return list;
        }

        private string SQL_GetParkDetails = "Select * From park Where parkCode = @ParkCode;";

        public Park GetPark(string parkCode)
        {
            Park parkDetails = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkDetails, conn);
                    cmd.Parameters.AddWithValue("@ParkCode", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        parkDetails = GetParkFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return parkDetails;
        }

        private Park GetParkFromReader(SqlDataReader reader)
        {
            Park convertPark = new Park();
            convertPark.ParkCode = Convert.ToString(reader["parkCode"]);
            convertPark.ParkName = Convert.ToString(reader["parkName"]);
            convertPark.State = Convert.ToString(reader["state"]);
            convertPark.Acreage = Convert.ToInt32(reader["acreage"]);
            convertPark.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
            convertPark.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
            convertPark.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            convertPark.Climate = Convert.ToString(reader["climate"]);
            convertPark.YearFounded = Convert.ToInt32(reader["yearFounded"]);
            convertPark.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            convertPark.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
            convertPark.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            convertPark.ParkDescription = Convert.ToString(reader["parkDescription"]);
            convertPark.EntryFee = Convert.ToInt32(reader["entryFee"]);
            convertPark.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

            return convertPark;
        }
    }
}