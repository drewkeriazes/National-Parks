using System;
using Capstone.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public interface ISurveyDAL
    {
        List<Survey> GetAllSurveys();
        bool SaveNewSurvey(Survey survey);
    }
}