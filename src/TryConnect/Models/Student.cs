namespace TryConnect.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Privacy Privacy { get; set; }
    }

    public enum Privacy
    {
        Normal,
        Private,
    }
}