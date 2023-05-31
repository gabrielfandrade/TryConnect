using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TryConnect.Repository;
using TryConnect.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;

namespace TryConnect.Test;

public class TryConnectPostTest : IClassFixture<TryConnectTestContext<Program>>
{
    private readonly HttpClient _client;

     public TryConnectPostTest(TryConnectTestContext<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory(DisplayName = "GET /post Deve retornar uma lista de posts")]
    [MemberData(nameof(ShouldReturnAPostListData))]
    public async Task ShouldReturnAPostList(List<Post> postsExpected)
    {
        var response = await _client.GetAsync("post");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<Post>>(content);

        result.Should().BeEquivalentTo(postsExpected);
    }
    public static readonly TheoryData<List<Post>> ShouldReturnAPostListData = new()
    {
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
        }
    };

    [Theory(DisplayName = "GET /post/1 Deve retornar um post de Id 1")]
    [MemberData(nameof(ShouldReturnAPostData))]
    public async Task ShouldReturnAPost(Post postExpected)
    {
        var response = await _client.GetAsync($"post/{postExpected.PostId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<Post>(content);

        result.Should().BeEquivalentTo(postExpected);
    }
    public static readonly TheoryData<Post> ShouldReturnAPostData = new()
    { 
        new Post{
            PostId = 1,
            CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            UpdatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
            Message = "C# é o melhor",
            StudentId = 2,
        }
    };

    [Theory(DisplayName = "POST /post Deve criar um post")]
    [MemberData(nameof(ShouldCreateAPostData))]
    public async Task ShouldCreateAPost(Post post, Post postExpected)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("post", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<Post>(content);

        result.Should().BeEquivalentTo(postExpected);
    }
    public static readonly TheoryData<Post, Post> ShouldCreateAPostData = new()
    {
        {
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

    [Fact(DisplayName = "POST /post Deve retornar BadRequest quando sem um post")]
    public async Task ShouldBadRequestWithoutAPost()
    {
        var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("post", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "PUT /post Deve atualizar um post existente")]
    [MemberData(nameof(ShouldUpdateAPostData))]
    public async Task ShouldUpdateAPost(Post post)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"post/{post.PostId}", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    public static readonly TheoryData<Post> ShouldUpdateAPostData = new()
    {
        new Post{
            PostId = 1,
            CreatedAt = DateTime.ParseExact("28/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            UpdatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Image = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Csharp_Logo.png?20180210215736",
            Message = "C# é o melhor de todos",
            StudentId = 2,
        }
    };

    [Fact(DisplayName = "PUT /post Deve retornar BadRequest quando sem um post")]
    public async Task ShouldBadRequestWhenUpdateWithoutAStudent()
    {
        var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("post/1", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "PUT /student Deve retornar NotFound quando sem um post")]
    [MemberData(nameof(ShouldNotFoundUpdateData))]
    public async Task ShouldNotFoundUpdate(Post post)
    {
        var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"post/{post.PostId}", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
    public static readonly TheoryData<Post> ShouldNotFoundUpdateData = new()
    {
        new Post{
            PostId = 4,
            CreatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            UpdatedAt = DateTime.ParseExact("31/05/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Image = "https://uploads-ssl.webflow.com/6357d4aeff33e738c1d2c99a/63582ab932507b5fcdc8afd0_thumb.webp",
            Message = "TMB é a melhor",
            StudentId = 1,
        }
    };

    [Theory(DisplayName = "DELETE /post Deve remover um post")]
    [MemberData(nameof(ShouldDeleteAPostData))]
    public async Task ShouldDeleteAPost(int id, List<Post> studentsExpected)
    {
        var response = await _client.DeleteAsync($"post/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        var dbResponse = await _client.GetAsync("post");

        var content = await dbResponse.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<Post>>(content);

        result.Should().BeEquivalentTo(studentsExpected);
    }
    public static readonly TheoryData<int, List<Post>> ShouldDeleteAPostData = new()
    {
    {
        1,
        new() {
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
        }
    }
    };

    [Fact(DisplayName = "DELETE /post Deve retornar NotFound quando quando sem um post")]
    public async Task ShouldNotFoundDeleteAStudent()
    {
        var response = await _client.DeleteAsync("post/4");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}