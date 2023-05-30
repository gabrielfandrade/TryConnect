namespace TryConnect.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? Image { get; set; }
        public string? Message { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public ICollection<PostComment> Comments { get; set; }
    }
}