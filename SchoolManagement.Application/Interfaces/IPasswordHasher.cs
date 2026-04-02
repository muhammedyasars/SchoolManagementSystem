namespace SchoolManagement.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
}