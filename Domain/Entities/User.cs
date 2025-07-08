namespace ignite.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier for the user
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the user was created
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
