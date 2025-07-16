<<<<<<< Updated upstream:Domain/Entities/User.cs
namespace ignite.Domain.Entities;
=======
namespace Domain.Models;
>>>>>>> Stashed changes:Domain/Models/User.cs

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}