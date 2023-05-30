using TryConnect.Models;

namespace TryConnect.Repository
{
    public class TryConnectRepository : ITryConnectRepository
    {
        private readonly TryConnectContext _context;

        public TryConnectRepository(TryConnectContext context)
        {
            _context = context;
        }

        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public Student? GetStudentById(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student>? GetStudents()
        {
            return _context.Students;
        }

        public void UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
    }
}