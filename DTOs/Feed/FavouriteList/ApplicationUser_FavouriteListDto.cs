using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.FavouriteVideogame;

namespace Capstone_Project.DTOs.Feed.FavouriteList;

public class ApplicationUser_FavouriteListDto
{
    public Guid FavouriteListId
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

    public ICollection<ApplicationUser_FavouriteVideogameDto> FavouriteVideogames
    {
        get; set;
    }
}
