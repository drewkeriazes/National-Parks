using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveyDAL
    {
        private string _connectionString;

        public SurveySqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Survey> GetAllSurveys()
        {
            List<Survey> surveys = new List<Survey>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string survey = @"SELECT park.parkName, survey_result.parkCode, COUNT(survey_result.parkCode) AS parkVotes FROM survey_result
                                JOIN park on park.parkCode = survey_result.parkCode GROUP BY survey_result.parkCode, park.parkName ORDER BY parkVotes DESC, parkName ASC;";
                   
                SqlCommand cmd = new SqlCommand(survey, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    surveys.Add(GetSurveyFromReader(reader));
                }

            }
            return surveys;
        }
        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            Survey survey = new Survey()
            {

                
                ParkCode = Convert.ToString(reader["parkCode"]),
                ParkName = Convert.ToString(reader["parkName"]),
                ParkVotes = Convert.ToInt32(reader["parkVotes"])
                

            };
            return survey;
        }

        public bool SaveNewSurvey(Survey newSurvey)
        {
            Survey sPost = new Survey();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string survey = @"INSERT INTO survey_result(parkCode, emailAddress, state, activityLevel)
                                VALUES(@parkCode, @emailAddress, @state, @activityLevel) SELECT CAST(SCOPE_IDENTITY() AS INT)";

                SqlCommand cmd = new SqlCommand(survey, conn);
                
                cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", newSurvey.State);
                cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                int newId = (int)cmd.ExecuteScalar();
                sPost.SurveyId = newId;

            }
            return true;
        }
    }
}