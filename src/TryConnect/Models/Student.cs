using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TryConnect.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [Precision(3)]
        public DateTime? Birthday { get; set; }
        public Privacy Privacy { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<PostComment>? Comments { get; set; }
    }

    public enum Privacy
    {
        Normal,
        Private,
    }
}