using TryConnect.Models;

namespace TryConnect.Repository
{
    public interface ITryConnectRepository
    {
        void CreateStudent(Student user);
        Student? GetStudentById(int id);
        IEnumerable<Student>? GetStudents();
        void UpdateStudent(Student user);
        void DeleteStudent(Student user);
        void CreatePost(Post post);
        Post? GetPostById(int id);
        IEnumerable<Post>? GetPostsByStudentId(int id);
        IEnumerable<Post>? GetPosts();
        void UpdatePost(Post post);
        void DeletePost(Post post);
    }
}