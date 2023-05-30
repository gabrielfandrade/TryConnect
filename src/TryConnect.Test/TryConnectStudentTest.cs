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

public class TryConnectStudentTest : IClassFixture<TryConnectTestContext<Program>>
{
    private readonly HttpClient _client;

     public TryConnectStudentTest(TryConnectTestContext<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Theory(DisplayName = "GET /student Deve retornar uma lista de estudantes")]
    [MemberData(nameof(ShouldReturnAStudentListData))]
    public async Task ShouldReturnAStudentList(List<Student> studentsExpected)
    {
        var response = await _client.GetAsync("student");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<Student>>(content);

        result.Should().BeEquivalentTo(studentsExpected);
    }
    public static readonly TheoryData<List<Student>> ShouldReturnAStudentListData = new()
    {
        new() {
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
    };

    [Theory(DisplayName = "GET /student/1 Deve retornar um estudante de Id 1")]
    [MemberData(nameof(ShouldReturnAStudentData))]
    public async Task ShouldReturnAStudent(Student studentExpected)
    {
        var response = await _client.GetAsync($"student/{studentExpected.StudentId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<Student>(content);

        result.Should().BeEquivalentTo(studentExpected);
    }
    public static readonly TheoryData<Student> ShouldReturnAStudentData = new()
    { 
        new Student{
            StudentId = 1,
            Name = "Gabriel",
            Password = "123limao",
            Email = "gabriel@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
    };

    [Theory(DisplayName = "POST /student Deve criar um estudante")]
    [MemberData(nameof(ShouldCreateAStudentData))]
    public async Task ShouldCreateAStudent(Student student, Student studentExpected)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("student", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<Student>(content);

        result.Should().BeEquivalentTo(studentExpected);
    }
    public static readonly TheoryData<Student, Student> ShouldCreateAStudentData = new()
    {
        {
            new Student{
            Name = "Teste",
            Password = "123teste",
            Email = "teste@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
            },
            new Student{
                StudentId = 4,
                Name = "Teste",
                Password = "123teste",
                Email = "teste@mail.com",
                Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Privacy = Privacy.Normal,
            }
        }
    };

    [Fact(DisplayName = "POST /student Deve retornar BadRequest quando sem um estudante")]
    public async Task ShouldBadRequestWithoutAStudent()
    {
        var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("student", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "PUT /student Deve atualizar um estudante existente")]
    [MemberData(nameof(ShouldUpdateAStudentData))]
    public async Task ShouldUpdateAStudent(Student student)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"student/{student.StudentId}", stringContent);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
    public static readonly TheoryData<Student> ShouldUpdateAStudentData = new()
    {
        new Student{
            StudentId = 1,
            Name = "Teste",
            Password = "123teste",
            Email = "gabriel@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
    };

    [Fact(DisplayName = "PUT /student Deve retornar BadRequest quando sem um estudante")]
    public async Task ShouldBadRequestWhenUpdateWithoutAStudent()
    {
        var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("student/1", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory(DisplayName = "PUT /student Deve retornar NotFound quando sem um estudante")]
    [MemberData(nameof(ShouldNotFoundUpdateData))]
    public async Task ShouldNotFoundUpdate(Student student)
    {
        var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"student/{student.StudentId}", content);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
    public static readonly TheoryData<Student> ShouldNotFoundUpdateData = new()
    {
        new Student{
            StudentId = 4,
            Name = "Teste",
            Password = "123teste",
            Email = "teste@mail.com",
            Birthday = DateTime.ParseExact("01/01/2000", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            Privacy = Privacy.Normal,
        }
    };

    [Theory(DisplayName = "DELETE /student Deve remover um estudante")]
    [MemberData(nameof(ShouldDeleteAStudentData))]
    public async Task ShouldDeleteAStudent(int id, List<Student> studentsExpected)
    {
        var response = await _client.DeleteAsync($"student/{id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        var dbResponse = await _client.GetAsync("student");

        var content = await dbResponse.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<List<Student>>(content);

        result.Should().BeEquivalentTo(studentsExpected);
    }
    public static readonly TheoryData<int, List<Student>> ShouldDeleteAStudentData = new()
    {
    {
        1,
        new() {
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

    [Fact(DisplayName = "DELETE /student Deve retornar NotFound quando quando sem um estudante")]
    public async Task ShouldNotFoundDeleteAStudent()
    {
        var response = await _client.DeleteAsync("student/4");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}