using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.CartVideogame;

namespace Capstone_Project.DTOs.Feed.Cart;

public class CartDto
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

    public Cart_ApplicationUserDto ApplicationUser
    {
        get; set;
    }

    public ICollection<Cart_CartVideogameDto> CartVideogames
    {
        get; set;
    }
}
