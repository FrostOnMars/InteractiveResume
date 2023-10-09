using System;

namespace InteractiveResume.Model.ResumeModel;

public class ResumeEducationModel
{
    public int AutoId { get; set; }
    public string? SchoolName { get; set; }
    public string? StartDateAttended { get; set; }
    public string? EndDateAttended { get; set; }
    public string? GraduationDate { get; set; }
    public string? Degree { get; set; }
    public string? EducationType { get; set; }
    public string? EducationAchievements { get; set; }
    public string? CertificationID { get; set; }
}