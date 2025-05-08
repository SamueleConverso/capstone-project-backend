using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUserFriend;

namespace Capstone_Project.DTOs.Feed.FriendList;

public class ApplicationUser_FriendListDto
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

    public ICollection<ApplicationUser_FriendList_ApplicationUserFriendDto> ApplicationUserFriends
    {
        get; set;
    }
}
