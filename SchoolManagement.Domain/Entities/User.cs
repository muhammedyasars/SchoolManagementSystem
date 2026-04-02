namespace SchoolManagement.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public string Role { get; set; }  

    public bool IsActive { get; set; } = true;

    // Duplicates ozhivakkan: Name details users-il mathram mathi
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Navigation properties
    public Student? Student { get; set; }
}
