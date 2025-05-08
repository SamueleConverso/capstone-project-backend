using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.CommunityApplicationUser;

public class CreateCommunityApplicationUserRequestDto
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
    public required Guid CommunityId
    {
        get; set;
    }
}
