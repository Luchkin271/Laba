namespace Laba.Controllers
{
    internal class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ProjectResponseDto> Projects { get; set; } = new();
    }

    internal class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}