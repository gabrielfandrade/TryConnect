using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using TryConnect.Models;
using TryConnect.Services;
using TryConnect.ViewModels;

namespace TryConnect.Test;

public class TryConnectAuthorizeTest : IClassFixture<TryConnectTestContext<Program>>
{
    private readonly HttpClient _client;

     public TryConnectAuthorizeTest(TryConnectTestContext<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [MemberData(nameof(ShouldAuthorizeLoginData))]
    public async void ShouldAuthorizeLogin(Student student)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("login", stringContent);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<StudentViewModel>(content);

        result.Token.Should().NotBeNull();
    }
    public static readonly TheoryData<Student> ShouldAuthorizeLoginData = new()
    {
        new Student(){
            Password = "123limao",
            Email = "gabriel@mail.com",
        }
    };

    [Theory]
    [MemberData(nameof(ShouldReturnNotFoundLoginData))]
    public async void ShouldReturnNotFoundLogin(Student student)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("login", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
    public static readonly TheoryData<Student> ShouldReturnNotFoundLoginData = new()
    {
        new Student(){
            Password = "123teste",
            Email = "teste@mail.com",
        }
    };

    [Theory]
    [MemberData(nameof(ShouldReturnBadRequestLoginData))]
    public async void ShouldReturnBadRequestLogin(Student student)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("login", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
    public static readonly TheoryData<Student> ShouldReturnBadRequestLoginData = new()
    {
        new Student(){
            Password = "",
            Email = "teste@mail.com",
        }
    };
}