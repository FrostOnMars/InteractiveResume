using System;

namespace InteractiveResume.Model;

public class ResumeHeaderModel
{
    public Guid AutoId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PortfolioLink { get; set; }
    public string ElevatorPitch { get; set; }
}