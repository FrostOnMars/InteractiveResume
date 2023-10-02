namespace InteractiveResume.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

public class MySql : DataSources
{
    //implementation
}


public class AcademicEducation
{
    public int AutoID { get; set; }
    public string SchoolName { get; set; }
    public DateTime StartDateAttended { get; set; }
    public DateTime? EndDateAttended { get; set; }
    public DateTime? EstimatedGraduation { get; set; }
    public string Degree { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public List<ProfessionalEducation> ProfessionalEducations { get; set; }
    public List<Achievement> Achievements { get; set; }
}

public class ProfessionalEducation
{
    public int AutoId { get; set; }
    public string EducationType { get; set; }
    public string TitleOf { get; set; }
    public DateTime DateAwarded { get; set; }
    public string Description { get; set; }

    public int AcademicEducationId { get; set; }
    public AcademicEducation AcademicEducation { get; set; }
}

public class Achievement
{
    public int AutoId { get; set; }
    public string AchievementName { get; set; }
    public string Description { get; set; }

    public int SchoolID { get; set; }
    public AcademicEducation AcademicEducation { get; set; }
}

public class SchoolName
{
    [Key]
    public int SchoolID { get; set; }
    public string Name { get; set; }
}