using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.PostLike;

public class ApplicationUser_PostLikeDto
{
    public Guid PostLikeId
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
