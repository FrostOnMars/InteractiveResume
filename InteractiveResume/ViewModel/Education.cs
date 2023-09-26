using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using InteractiveResume.Model;
using Newtonsoft.Json;

namespace InteractiveResume.ViewModel
{
    internal class Education
    {
        public void PrintEducation()
        {
            var myEducation = new College();

            myEducation.SchoolName = "Texas Woman's University";
            myEducation.Degree = "Bachelor of Science in Computer Science";
            myEducation.AttendenceDates = "August 2017 - May 2021";
            myEducation.GPA = "3.5";
            myEducation.Honors = "Cum Laude";
            myEducation.Location = "Denton, TX";
            myEducation.Date = "May 2021";

            var jsonString = JsonConvert.SerializeObject


        }
    }
}
