using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.Community;

namespace Capstone_Project.DTOs.Feed.CommunityApplicationUser;

public class ApplicationUser_CommunityApplicationUserDto
{
    public Guid CommunityApplicationUserId
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

    public ApplicationUser_CommunityDto Community
    {
        get; set;
    }
}
