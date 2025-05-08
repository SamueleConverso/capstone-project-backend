using System;

namespace Capstone_Project.DTOs.Feed.Cart;

public class UpdateCartRequestDto
{
    public bool? IsCheckedOut
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
