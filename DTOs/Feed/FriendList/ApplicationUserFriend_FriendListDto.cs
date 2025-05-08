using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.FriendList;

public class ApplicationUserFriend_FriendListDto
{
    public Guid FriendListId
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

    public ApplicationUserFriend_FriendList_ApplicationUserDto ApplicationUser
    {
        get; set;
    }
}
