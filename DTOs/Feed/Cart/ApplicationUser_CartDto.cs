using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.CartVideogame;

namespace Capstone_Project.DTOs.Feed.Cart;

public class ApplicationUser_CartDto
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

    public ICollection<ApplicationUser_CartVideogameDto> CartVideogames
    {
        get; set;
    }
}
