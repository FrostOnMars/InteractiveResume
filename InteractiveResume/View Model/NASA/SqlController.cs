using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using InteractiveResume.Model;
using InteractiveResume.Model.ResumeModel;

public class SqlController
{
    public static List<ResumeEducationModel> GetAllResumeEducationModels()
    {
        var resumeEducationModels = new List<ResumeEducationModel>();

        using var connection = new SQLiteConnection("Data Source=ResumeDataFile.db");
        connection.Open();
        var query = "SELECT * FROM ResumeEducation";

        using var command = new SQLiteCommand(query, connection);
        using var reader = command.ExecuteReader();
        //var test = "";
        
        while (reader.Read())
        {
            //test += $"AutoId: {reader[0]} School Name: {reader[1]} StartDateAttended: {reader[2]} {reader[3]}{reader[4]}{reader[5]}"
            var resumeEducation = new ResumeEducationModel
            {
                AutoId = reader.GetInt32(0),
                SchoolName = reader.IsDBNull(1) ? null : reader.GetString(1),
                StartDateAttended = reader.IsDBNull(2) ? null : reader.GetString(2),
                EndDateAttended = reader.IsDBNull(3) ? null : reader.GetString(3),
                GraduationDate = reader.IsDBNull(4) ? null : reader.GetString(4),
                Degree = reader.IsDBNull(5) ? null : reader.GetString(5),
                EducationType = reader.IsDBNull(6) ? null : reader.GetString(6),
                EducationAchievements = reader.IsDBNull(7) ? null : reader.GetString(7),
                CertificationID = reader.IsDBNull(8) ? null : reader.GetString(8)
            };
            resumeEducationModels.Add(resumeEducation);
        }

        connection.Close();
        return resumeEducationModels;
    }
    public static List<ResumeHeaderModel> GetAllResumeHeaders()
    {
        var resumeHeaders = new List<ResumeHeaderModel>();

        using var connection = new SQLiteConnection("Data Source=ResumeDataFile.db");
        connection.Open();

        var query = "SELECT * FROM ResumeHeader";
        //    }
        using (var command = new SQLiteCommand(query, connection))
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var resumeHeader = new ResumeHeaderModel
                {
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Phone = reader.IsDBNull(4) ? null : reader.GetString(4),
                    Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                    PortfolioLink = reader.IsDBNull(6) ? null : reader.GetString(6),
                    ElevatorPitch = reader.GetString(7)
                };

                resumeHeaders.Add(resumeHeader);
            }
        }

        connection.Close();

        return resumeHeaders;
    }
}

