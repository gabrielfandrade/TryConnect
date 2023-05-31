using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TryConnect.Models
{
    public class PostComment
    {
        [Key]
        public int PostCommentId { get; set; }
        public string? Comment { get; set;}
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}