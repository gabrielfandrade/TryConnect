using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;
using System.Globalization;
using TryConnect.Services;

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
            context.Posts.AddRange(
                GetPostsListForTests()
            );
            context.Comments.AddRange(
                GetPostCommentsListForTests()
            );
            context.SaveChanges();
            return context;
        }

        public static List<Student> GetStudentsListForTests() =>
            new() {
                new Student{
                    StudentId = 1,
                    Name = "Gabriel",
                    Password = "123limao",
                    Email = "gabriel@mail.com",
                    Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                },
                new Student{
                    StudentId = 2,
                    Name = "Faustino",
                    Password = "123uva",
                    Email = "faustino@mail.com",
                    Birthday = DateTime.ParseExact("05/05/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                },
                new Student{
                    StudentId = 3,
                    Name = "Andrade",
                    Password = "123laranja",
                    Email = "andrade@mail.com",
                    Birthday = DateTime.ParseExact("10/10/2002", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                },
            };

        public static List<Post> GetPostsListForTests() =>
            new() {
                new Post{
                    PostId = 1,
                    CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
                    Message = "C# é o melhor",
                    StudentId = 2,
                },
                new Post{
                    PostId = 2,
                    CreatedAt = DateTime.ParseExact("29/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("29/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://codeopinion.com/wp-content/uploads/2017/10/Bitmap-MEDIUM_Entity-Framework-Core-Logo_2colors_Square_Boxed_RGB-300x300.png",
                    Message = "Entity Framework é o melhor",
                    StudentId = 3,
                },
                new Post{
                    PostId = 3,
                    CreatedAt = DateTime.ParseExact("30/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("30/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://www.dbacorp.com.br/wp-content/uploads/2017/07/microsoft-sql-server-logo-300x163.png",
                    Message = "SQLServer é o melhor",
                    StudentId = 1,
                },
            };

        public static List<PostComment> GetPostCommentsListForTests() =>
            new() {
                new PostComment{
                    PostCommentId = 1,
                    Comment = "Comentario número 1",
                    StudentId = 3,
                    PostId = 2,
                },
                new PostComment{
                    PostCommentId = 2,
                    Comment = "Comentario número 2",
                    StudentId = 1,
                    PostId = 3,
                },
                new PostComment{
                    PostCommentId = 3,
                    Comment = "Comentario número 3",
                    StudentId = 2,
                    PostId = 3,
                }
            };

        public static string GetTokenForTests()
        {
            var student = new Student{
                StudentId = 1,
                Name = "Gabriel",
                Password = "123limao",
                Email = "gabriel@mail.com",
                Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            };

            var token = new TokenGenerator().Generate(student);

            return token;
        }
    }
}