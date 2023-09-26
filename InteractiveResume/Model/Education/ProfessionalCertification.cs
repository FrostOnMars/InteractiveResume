namespace InteractiveResume.Model.Education;

public class ProfessionalCertification : Education
{
    public override string EducationSource { get; set; } = "Professional Certification";

    public string CertificationAuthority { get; set; }
    public string Dates { get; set; }

}