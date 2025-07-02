namespace ignite.DTOs
{
    public class CreateLevelDto
    {
        public required string Name { get; set; }
        public required double Min { get; set; }
        public required double Max { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
