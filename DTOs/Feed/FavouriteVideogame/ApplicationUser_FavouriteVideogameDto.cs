using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.Videogame;

namespace Capstone_Project.DTOs.Feed.FavouriteVideogame;

public class ApplicationUser_FavouriteVideogameDto
{
    public Guid FavouriteVideogameId
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
    } = false;

    public DateTime? DeletedAt
    {
        get; set;
    }

    public ApplicationUser_VideogameDto Videogame
    {
        get; set;
    }
}
