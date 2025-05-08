using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.ApplicationUserFriend;

public class UpdateApplicationUserFriendRequestDto
{
    public bool? Accepted
    {
        get; set;
    }

    public bool? Rejected
    {
        get; set;
    }


    public DateTime? UpdatedAt
    {
        get; set;
    }

    // public bool? IsDeleted
    // {
    //     get; set;
    // }
}
