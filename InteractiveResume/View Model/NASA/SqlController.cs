using System.Data;
using SQLitePCL;
using Microsoft.Data.Sqlite;

namespace InteractiveResume.View_Model.NASA;



public class SqlController
{
    public static void CreateTable()
    {
        SQLitePCL.raw.SetProvider(new SQLite3Provider_e_sqlite3());

        var tableCreationQuery = @"-- Table 1: ResumeHeader
CREATE TABLE ResumeHeader (
    AutoId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Address VARCHAR(MAX),
    Phone VARCHAR(15),
    Email VARCHAR(50),
    PortfolioLink VARCHAR(100),
    ElevatorPitch VARCHAR(MAX) NOT NULL
);

-- Table 2: ResumeEducation
CREATE TABLE ResumeEducation (
    AutoId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    SchoolName VARCHAR(50) NOT NULL,
    StartDateAttended DATE,
    EndDateAttended DATE,
    GraduationDate DATE,
    Degree VARCHAR(50),
    EducationType VARCHAR(50) NOT NULL,
    EducationAchievements VARCHAR(MAX),
    CertificationID VARCHAR(50)
);
";
        string connectionString = "Data Source=ResumeDataFile.db";
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = new SqliteCommand(tableCreationQuery, connection)
        {
            CommandType = CommandType.Text
        };

        connection.Close();
    }
}