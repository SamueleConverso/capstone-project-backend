using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.Videogame;

namespace Capstone_Project.DTOs.Feed.CartVideogame;

public class Cart_CartVideogameDto
{
    public Guid CartVideogameId
    {
        get; set;
    }

    [Required]
    public required int Quantity
    {
        get; set;
    } = 1;

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

    public Cart_CartVideogame_VideogameDto Videogame
    {
        get; set;
    }
}
