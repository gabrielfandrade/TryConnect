namespace TryConnect.Models
{
    public class PostComment
    {
        public int PostCommentId { get; set; }
        public string? Comment { get; set;}
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}