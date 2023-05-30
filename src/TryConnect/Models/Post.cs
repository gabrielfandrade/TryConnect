using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryConnect.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Image { get; set; }
        public string? Message { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public ICollection<PostComment> Comments { get; set; }
    }
}