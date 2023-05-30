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
    }
}