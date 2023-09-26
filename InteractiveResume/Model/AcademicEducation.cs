namespace InteractiveResume.Model;

public abstract class AcademicEducation : Education
{
    public virtual string Degree { get; set; }
    public virtual string GPA { get; set; }
    public virtual string AttendenceDates { get; set; }
    public string SchoolName { get; set; }
    public string Location { get; set; }
}