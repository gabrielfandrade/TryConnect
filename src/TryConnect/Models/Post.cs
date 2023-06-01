using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TryConnect.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Precision(3)]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [MaxLength(500)]
        public string? Image { get; set; }
        [MaxLength(500)]
        public string? Message { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public ICollection<PostComment>? Comments { get; set; }
    }
}