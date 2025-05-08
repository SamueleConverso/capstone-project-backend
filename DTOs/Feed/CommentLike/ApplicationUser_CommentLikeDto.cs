using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.CommentLike;

public class ApplicationUser_CommentLikeDto
{
    public Guid CommentLikeId
    {
        get; set;
    }

    [Required]
    public DateTime CreatedAt
    {
        get; set;
    }

    public DateTime? UpdatedAt
    {
        get; set;
    }

    public bool? IsDeleted
    {
        get; set;
    }

    public DateTime? DeletedAt
    {
        get; set;
    }
}
