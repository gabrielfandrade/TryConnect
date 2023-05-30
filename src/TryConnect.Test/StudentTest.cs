using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Test;

public class StudentTest
{
    [Theory]
    [MemberData(nameof(ShouldGetStudentByIdData))]
    public void ShouldGetStudentById(TryConnectContext context, int studentId, Student studentExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetStudentById(studentId);

        result.Should().BeEquivalentTo(studentExpected);
    }
    public readonly static TheoryData<TryConnectContext, int, Student> ShouldGetStudentByIdData =
      new()
      {
      {
        Helpers.GetTryConnectContextForTest("ShouldGetStudentById"),
        1,
        new Student{
            StudentId = 1,
            Name = "Gabriel",
            Password = "123limao",
            Email = "gabriel@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
      },
      };

    [Theory]
    [MemberData(nameof(ShouldGetStudentsData))]
    public void ShouldGetStudents(TryConnectContext context, List<Student> studentsExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetStudents();

        result.Should().BeEquivalentTo(studentsExpected);
    }
    public readonly static TheoryData<TryConnectContext, List<Student>> ShouldGetStudentsData =
      new()
      {
      {
        Helpers.GetTryConnectContextForTest("ShouldGetStudents"),
        new List<Student> {
            new Student{
                StudentId = 1,
                Name = "Gabriel",
                Password = "123limao",
                Email = "gabriel@mail.com",
                Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Normal,
            },
            new Student{
                StudentId = 2,
                Name = "Faustino",
                Password = "123uva",
                Email = "faustino@mail.com",
                Birthday = DateTime.ParseExact("05/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Private,
            },
            new Student{
                StudentId = 3,
                Name = "Andrade",
                Password = "123laranja",
                Email = "andrade@mail.com",
                Birthday = DateTime.ParseExact("10/10/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Normal,
            },
        }
      }
      };

    [Theory]
    [MemberData(nameof(ShouldAddStudentData))]
    public void ShouldAddStudent(TryConnectContext context, Student studentToAdd, Student studentExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.CreateStudent(studentToAdd);

        var actual = context.Students.FirstOrDefault(student => student.StudentId == studentToAdd.StudentId);

        actual.Should().BeEquivalentTo(studentExpected);
    }
    public readonly static TheoryData<TryConnectContext, Student, Student> ShouldAddStudentData =
      new()
      {
      {
        Helpers.GetTryConnectContextForTest("ShouldAddStudent"),
        new Student{
            Name = "Estudante",
            Password = "123banana",
            Email = "estudante@mail.com",
            Birthday = DateTime.ParseExact("01/01/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        },
        new Student{
            StudentId = 4,
            Name = "Estudante",
            Password = "123banana",
            Email = "estudante@mail.com",
            Birthday = DateTime.ParseExact("01/01/2003", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
      },
      };

    [Theory]
    [MemberData(nameof(ShouldUpdateStudentData))]
    public void ShouldUpdateStudent(TryConnectContext context, Student studentToUpdate)
    {
        var repository = new TryConnectRepository(context);

        repository.UpdateStudent(studentToUpdate);

        var actual = context.Students.Find(studentToUpdate.StudentId);

        actual.Should().BeEquivalentTo(studentToUpdate);
    }
    public readonly static TheoryData<TryConnectContext, Student> ShouldUpdateStudentData =
      new()
      {
      {
        Helpers.GetTryConnectContextForTest("ShouldUpdateStudent"),
        new Student{
            StudentId = 1,
            Name = "Gabriel Gabriel",
            Password = "123limao",
            Email = "gabriel@mail.com",
            Birthday = DateTime.ParseExact("10/10/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
      },
      };

    [Theory]
    [MemberData(nameof(ShouldDeleteStudentData))]
    public void ShouldDeleteStudent(TryConnectContext context, Student studentToDelete, List<Student> studentsExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.DeleteStudent(studentToDelete);

        context.Students.Should().BeEquivalentTo(studentsExpected);
    }
    public readonly static TheoryData<TryConnectContext, Student, List<Student>> ShouldDeleteStudentData =
      new()
      {
      {
        Helpers.GetTryConnectContextForTest("ShouldDeleteStudent"),
        new Student{
            StudentId = 1,
            Name = "Gabriel",
            Password = "123limao",
            Email = "gabriel@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        },
        new List<Student> {
            new Student{
                StudentId = 2,
                Name = "Faustino",
                Password = "123uva",
                Email = "faustino@mail.com",
                Birthday = DateTime.ParseExact("05/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Private,
            },
            new Student{
                StudentId = 3,
                Name = "Andrade",
                Password = "123laranja",
                Email = "andrade@mail.com",
                Birthday = DateTime.ParseExact("10/10/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Normal,
            },
        }
      },
      };
}