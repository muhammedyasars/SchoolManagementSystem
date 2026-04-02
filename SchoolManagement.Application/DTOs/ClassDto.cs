namespace SchoolManagement.Application.DTOs;

public class ClassDto
{
    public int Id { get; set; }
    public string ClassName { get; set; }
    public string? ClassCode { get; set; }

    public int TeacherUserId { get; set; }
    public string? TeacherFirstName { get; set; }
    public string? TeacherLastName { get; set; }
    public string? TeacherEmail { get; set; }

    public int SchoolId { get; set; }
    public string? SchoolName { get; set; }
}
