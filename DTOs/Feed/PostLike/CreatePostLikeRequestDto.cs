using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.PostLike;

public class CreatePostLikeRequestDto
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
    public required Guid PostId
    {
        get; set;
    }
}
