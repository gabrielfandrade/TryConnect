using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;
using System.Globalization;

namespace TryConnect.Test
{
    public static class Helpers
    {
        public static TryConnectContext GetTryConnectContextForTest(string inMemoryDbName)
        {
            var contextOptions = new DbContextOptionsBuilder<TryConnectContext>()
                .UseInMemoryDatabase(inMemoryDbName)
                .Options;
            var context = new TryConnectContext(contextOptions);
            context.Students.AddRange(
                GetStudentsListForTests()
            );
            context.SaveChanges();
            return context;
        }

        public static List<Student> GetStudentsListForTests() =>
            new() {
                new Student{
                    Id = 1,
                    Name = "Gabriel",
                    Password = "123limao",
                    Email = "gabriel@mail.com",
                    Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Privacy = Privacy.Normal,
                },
                new Student{
                    Id = 2,
                    Name = "Faustino",
                    Password = "123uva",
                    Email = "faustino@mail.com",
                    Birthday = DateTime.ParseExact("05/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Privacy = Privacy.Private,
                },
                new Student{
                    Id = 3,
                    Name = "Andrade",
                    Password = "123laranja",
                    Email = "andrade@mail.com",
                    Birthday = DateTime.ParseExact("10/10/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Privacy = Privacy.Normal,
                },
            };
    }
}