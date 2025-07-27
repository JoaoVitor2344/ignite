namespace ignite.Application.DTOs.Commands.Category
{
    public class UpdateCategoryCommand
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
