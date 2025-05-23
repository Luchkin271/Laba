using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laba.DTO
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<Project> Projects { get; set; } = new();
    }

    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Гарантируем авто-генерацию на стороне Postgres
        public int Id { get; set; }

        public string Content { get; set; }
        public List<User> Users { get; set; } = new();
    }
    public class ProjectCreateDto
    {
        public string Content { get; set; }
    }

    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<ProjectCreateDto> Projects { get; set; } = new();
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
