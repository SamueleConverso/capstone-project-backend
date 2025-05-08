using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.CommentLike;

public class CreateCommentLikeRequestDto
{
    public bool? IsDeleted
    {
        get; set;
    }

    [Required]
    public required string ApplicationUserId
    {
        get; set;
    }

    [Required]
    public required Guid CommentId
    {
        get; set;
    }
}
