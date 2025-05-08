using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.CommentLike;

namespace Capstone_Project.DTOs.Feed.Comment;

public class Community_Post_CommentDto
{
    public Guid CommentId
    {
        get; set;
    }

    [Required]
    public required string Text
    {
        get; set;
    }

    public string? Picture
    {
        get; set;
    }

    public int? Likes
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

    public ICollection<Community_Post_Comment_CommentLikeDto> CommentLikes
    {
        get; set;
    }

    public Community_Post_Comment_ApplicationUserDto ApplicationUser
    {
        get; set;
    }
}
