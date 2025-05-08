using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.ApplicationUserFriend;

public class ApplicationUser_FriendList_ApplicationUserFriendDto
{
    public Guid ApplicationUserFriendId
    {
        get; set;
    }

    [Required]
    public required bool Sent
    {
        get; set;
    }

    public bool? Accepted
    {
        get; set;
    }

    public bool? Rejected
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

    public ApplicationUser_ApplicationUserDto ApplicationUser
    {
        get; set;
    }
}
