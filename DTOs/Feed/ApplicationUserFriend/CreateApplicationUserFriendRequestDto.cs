using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.ApplicationUserFriend;

public class CreateApplicationUserFriendRequestDto
{
    [Required]
    public required bool Sent
    {
        get; set;
    }

    public bool? Accepted
    {
        get; set;
    }

    public bool? IsDeleted
    {
        get; set;
    }

    [Required]
    public required Guid FriendListId
    {
        get; set;
    }

    [Required]
    public required string ApplicationUserId
    {
        get; set;
    }
}
