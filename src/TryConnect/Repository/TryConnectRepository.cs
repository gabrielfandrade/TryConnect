using TryConnect.Models;
using System.Security.Cryptography;
using BCrypt.Net;

namespace TryConnect.Repository
{
    public class TryConnectRepository : ITryConnectRepository
    {
        private readonly TryConnectContext _context;

        public TryConnectRepository(TryConnectContext context)
        {
            _context = context;
        }

        public void CreateComment(PostComment comment)
        {
            comment.CreatedAt = DateTime.Now;

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void CreatePost(Post post)
        {
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;

            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void CreateStudent(Student student)
        {
            student.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(student.Password, hashType: HashType.SHA384);

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteComment(PostComment comment)
        {
            var entity = _context.Comments.First(c => c.PostCommentId == comment.PostCommentId);

            _context.Comments.Remove(entity);

            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            var entity = _context.Posts.First(p => p.PostId == post.PostId);

            _context.Posts.Remove(entity);

            _context.SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            var entity = _context.Students.First(s => s.StudentId == student.StudentId);

            _context.Students.Remove(entity);

            _context.SaveChanges();
        }

        public PostComment? GetCommenById(int id)
        {
            return _context.Comments.Find(id);
        }

        public IEnumerable<PostComment>? GetCommentsByPostId(int id)
        {
            return _context.Comments.Where(p => p.PostId == id).ToList();
        }

        public Post? GetPostById(int id)
        {
            return _context.Posts.Find(id);
        }

        public IEnumerable<Post>? GetPosts()
        {
            return _context.Posts;
        }

        public IEnumerable<Post>? GetPostsByStudentId(int id)
        {
            return _context.Posts.Where(post => post.StudentId == id).ToList();
        }

        public Student? GetStudent(Login login)
        {   
            var entity = _context.Students.Where(s => s.Email == login.Email).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }

            var verify = BCrypt.Net.BCrypt.EnhancedVerify(login.Password, entity.Password, hashType:HashType.SHA384);

            if (!verify)
            {
                throw new UnauthorizedAccessException();
            }

            return entity;
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student>? GetStudents()
        {
            return _context.Students;
        }

        public void UpdateComment(PostComment comment)
        {
            var entity = _context.Comments.First(c => c.PostCommentId == comment.PostCommentId);

            entity.Comment = comment.Comment;

            _context.SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            var entity = _context.Posts.First(p => p.PostId == post.PostId);

            entity.Message = post.Message;
            entity.Image = post.Image;
            entity.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            var entity = _context.Students.First(s => s.StudentId == student.StudentId);

            entity.Name = student.Name;
            entity.Birthday = student.Birthday;
            entity.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(student.Password, hashType: HashType.SHA384);

            _context.SaveChanges();
        }
    }
}