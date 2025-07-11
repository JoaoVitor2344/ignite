using ignite.Services.Interfaces;

namespace ignite.Services.Implementations;

public class PasswordHasherService : IPasswordHasherService
{
    private static readonly int WorkFactor = 12;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}