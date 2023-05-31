using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Test;

public class PostTest
{
    [Theory]
    [MemberData(nameof(ShouldGetPostByIdData))]
    public void ShouldGetPostById(TryConnectContext context, int postId, Post postExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetPostById(postId);

        result.Should().BeEquivalentTo(postExpected);
    }
    public readonly static TheoryData<TryConnectContext, int, Post> ShouldGetPostByIdData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldGetPostById"),
                1,
                new Post{
                    PostId = 1,
                    CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
                    Message = "C# é o melhor",
                    StudentId = 2,
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldGetPostsByStudentIdData))]
    public void ShouldGetPostsByStudentId(TryConnectContext context, int studentId, List<Post> postsExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetPostsByStudentId(studentId);

        result.Should().BeEquivalentTo(postsExpected);
    }
    public readonly static TheoryData<TryConnectContext, int, List<Post>> ShouldGetPostsByStudentIdData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldGetPostsByStudentId"),
                1,
                new List<Post> {
                    new Post{
                        PostId = 3,
                        CreatedAt = DateTime.ParseExact("30/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        UpdatedAt = DateTime.ParseExact("30/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Image = "https://www.dbacorp.com.br/wp-content/uploads/2017/07/microsoft-sql-server-logo-300x163.png",
                        Message = "SQLServer é o melhor",
                        StudentId = 1,
                    },
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldGetPostsData))]
    public void ShouldGetPosts(TryConnectContext context, List<Post> postsExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetPosts();

        result.Should().BeEquivalentTo(postsExpected);
    }
    public readonly static TheoryData<TryConnectContext, List<Post>> ShouldGetPostsData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldGetPosts"),
                Helpers.GetPostsListForTests()
            }
        };

    [Theory]
    [MemberData(nameof(ShouldAddPostData))]
    public void ShouldAddPost(TryConnectContext context, Post postToAdd, Post postExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.CreatePost(postToAdd);

        var actual = context.Posts.FirstOrDefault(post => post.PostId == postToAdd.PostId);

        actual.Should().BeEquivalentTo(postExpected);
    }
    public readonly static TheoryData<TryConnectContext, Post, Post> ShouldAddPostData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldAddPost"),
                new Post{
                    CreatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://uploads-ssl.webflow.com/6357d4aeff33e738c1d2c99a/63582ab932507b5fcdc8afd0_thumb.webp",
                    Message = "TMB é a melhor",
                    StudentId = 1,
                },
                new Post{
                    PostId = 4,
                    CreatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://uploads-ssl.webflow.com/6357d4aeff33e738c1d2c99a/63582ab932507b5fcdc8afd0_thumb.webp",
                    Message = "TMB é a melhor",
                    StudentId = 1,
                }
            }
        };
    
    [Theory]
    [MemberData(nameof(ShouldUpdatePostData))]
    public void ShouldUpdatePost(TryConnectContext context, Post postToUpdate)
    {
        var repository = new TryConnectRepository(context);

        repository.UpdatePost(postToUpdate);

        var actual = context.Posts.FirstOrDefault(post => post.PostId == postToUpdate.PostId);

        actual.Should().BeEquivalentTo(postToUpdate);
    }
    public readonly static TheoryData<TryConnectContext, Post> ShouldUpdatePostData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldUpdatePost"),
                new Post{
                    PostId = 1,
                    CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
                    Message = "C# é o melhor de todos",
                    StudentId = 2,
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldDeletePostData))]
    public void ShouldDeletePost(TryConnectContext context, Post postToDelete, List<Post> postsExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.DeletePost(postToDelete);

        context.Posts.Should().BeEquivalentTo(postsExpected);
    }
    public readonly static TheoryData<TryConnectContext, Post, List<Post>> ShouldDeletePostData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldDeletePost"),
                new Post{
                    PostId = 1,
                    CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
                    Message = "C# é o melhor de todos",
                    StudentId = 2,
                },
                new List<Post>{
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
                    }
                }
            }
        };
}