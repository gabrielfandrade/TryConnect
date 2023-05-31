using System.Globalization;
using Microsoft.EntityFrameworkCore;
using TryConnect.Models;
using TryConnect.Repository;

namespace TryConnect.Test;

public class CommentTest
{
    [Theory]
    [MemberData(nameof(ShouldGetCommentByIdData))]
    public void ShouldGetCommentById(TryConnectContext context, int commentId, PostComment commentExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetCommenById(commentId);

        result.Should().BeEquivalentTo(commentExpected);
    }
    public readonly static TheoryData<TryConnectContext, int, PostComment> ShouldGetCommentByIdData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldGetCommentById"),
                1,
                new PostComment{
                    PostCommentId = 1,
                    Comment = "Comentario número 1",
                    StudentId = 3,
                    PostId = 2,
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldGetCommentsByPostIdData))]
    public void ShouldGetCommentsByPostId(TryConnectContext context, int postId, List<PostComment> commentsExpected)
    {
        var repository = new TryConnectRepository(context);

        var result = repository.GetCommentsByPostId(postId);

        result.Should().BeEquivalentTo(commentsExpected);
    }
    public readonly static TheoryData<TryConnectContext, int, List<PostComment>> ShouldGetCommentsByPostIdData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldGetCommentsByPostId"),
                3,
                new List<PostComment> {
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
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldAddCommentData))]
    public void ShouldAddComment(TryConnectContext context, PostComment commentToAdd, PostComment commentExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.CreateComment(commentToAdd);

        var actual = context.Comments.FirstOrDefault(comment => comment.PostCommentId == commentToAdd.PostCommentId);

        actual.Should().BeEquivalentTo(commentExpected);
    }
    public readonly static TheoryData<TryConnectContext, PostComment, PostComment> ShouldAddCommentData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldAddComment"),
                new PostComment{
                    Comment = "Comentario número 4",
                    StudentId = 1,
                    PostId = 1,
                },
                new PostComment{
                    PostCommentId = 4,
                    Comment = "Comentario número 4",
                    StudentId = 1,
                    PostId = 1,
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldUpdateCommentData))]
    public void ShouldUpdateComment(TryConnectContext context, PostComment commentToUpdate)
    {
        var repository = new TryConnectRepository(context);

        repository.UpdateComment(commentToUpdate);

        var actual = context.Comments.FirstOrDefault(comment => comment.PostCommentId == commentToUpdate.PostCommentId);

        actual.Should().BeEquivalentTo(commentToUpdate);
    }
    public readonly static TheoryData<TryConnectContext, PostComment> ShouldUpdateCommentData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldUpdateComment"),
                new PostComment{
                    PostCommentId = 1,
                    Comment = "Novo comentario número 1",
                    StudentId = 3,
                    PostId = 2,
                }
            }
        };

    [Theory]
    [MemberData(nameof(ShouldDeleteCommentData))]
    public void ShouldDeleteComment(TryConnectContext context, PostComment commentToDelete, List<PostComment> commentsExpected)
    {
        var repository = new TryConnectRepository(context);

        repository.DeleteComment(commentToDelete);

        context.Comments.Should().BeEquivalentTo(commentsExpected);
    }
    public readonly static TheoryData<TryConnectContext, PostComment, List<PostComment>> ShouldDeleteCommentData =
        new()
        {
            {
                Helpers.GetTryConnectContextForTest("ShouldDeleteComment"),
                new PostComment{
                    PostCommentId = 1,
                    Comment = "Comentario número 1",
                    StudentId = 3,
                    PostId = 2,
                },
                new List<PostComment>{
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
                }
            }
        };
}