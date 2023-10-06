using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.SQLite;

public class SqlController
{
    public static void InsertResumeHeader()
    {
        string firstName = "Mikayla";
        string lastName = "Martin";
        string address = "215 Twisted Willow, Sunset, TX 76270";
        string phone = "972.762.7070";
        string email = "mikaylamartin@proton.me";
        string portfolioLink = "https://github.com/FrostOnMars/InteractiveResume";
        string elevatorPitch = "Lorem ipsum dolor...";

        string connectionString = "Data Source=ResumeDataFile.db";

        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        var insertSql = @"
            INSERT INTO ResumeHeader (FirstName, LastName, Address, Phone, Email, PortfolioLink, ElevatorPitch)
            VALUES (@FirstName, @LastName, @Address, @Phone, @Email, @PortfolioLink, @ElevatorPitch)";

        using var command = new SQLiteCommand(insertSql, connection);
        command.Parameters.AddWithValue("@FirstName", firstName);
        command.Parameters.AddWithValue("@LastName", lastName);
        command.Parameters.AddWithValue("@Address", address);
        command.Parameters.AddWithValue("@Phone", phone);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@PortfolioLink", portfolioLink);
        command.Parameters.AddWithValue("@ElevatorPitch", elevatorPitch);

        command.ExecuteNonQuery();

        connection.Close();
    }
    public static void CreateTable()
    {
        var tableCreationQuery = @"-- Table 1: ResumeHeader
CREATE TABLE ResumeHeader (
    AutoId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Address VARCHAR(255),
    Phone VARCHAR(15),
    Email VARCHAR(50),
    PortfolioLink VARCHAR(100),
    ElevatorPitch VARCHAR(255) NOT NULL
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
    EducationAchievements VARCHAR(255),
    CertificationID VARCHAR(50)
);
";

        var storedProcedureCreationQuery = @"-- Create a stored procedure to insert data into ResumeHeader table
CREATE PROCEDURE InsertResumeHeader
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Address VARCHAR(255),
    @Phone VARCHAR(15),
    @Email VARCHAR(50),
    @PortfolioLink VARCHAR(100),
    @ElevatorPitch VARCHAR(255)
AS
BEGIN
    -- Insert data into ResumeHeader table
    INSERT INTO ResumeHeader (FirstName, LastName, Address, Phone, Email, PortfolioLink, ElevatorPitch)
    VALUES (@FirstName, @LastName, @Address, @Phone, @Email, @PortfolioLink, @ElevatorPitch);
END;
";



        var connectionString = "Data Source=ResumeDataFile.db";

        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        using (var command = new SQLiteCommand(storedProcedureCreationQuery, connection))
        {
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
        }

        connection.Close();
    }
}

