using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Cart;

public class CartVideogame_CartDto
{
    public Guid CartId
    {
        get; set;
    }

    public bool? IsCheckedOut
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
