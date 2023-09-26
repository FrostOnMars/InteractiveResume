namespace InteractiveResume.Model.Education;

public class College : AcademicEducation
{
    public override string EducationSource { get; set; } = "College";
    public override string Date { get; set; } 
    public override string AttendenceDates { get; set; }
    public override string Degree { get; set; }
    public override string GPA { get; set; }
    public string Honors { get; set; }
}