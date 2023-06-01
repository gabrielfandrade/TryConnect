using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TryConnect.Models
{
    public class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }
        [MaxLength(500)]
        public string? Comment { get; set;}
        [Precision(3)]
        public DateTime CreatedAt { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}