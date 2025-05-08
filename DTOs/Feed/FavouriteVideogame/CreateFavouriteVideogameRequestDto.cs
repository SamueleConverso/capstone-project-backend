using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.FavouriteVideogame;

public class CreateFavouriteVideogameRequestDto
{
    public bool? IsDeleted
    {
        get; set;
    }

    [Required]
    public required Guid FavouriteListId
    {
        get; set;
    }

    [Required]
    public required Guid VideogameId
    {
        get; set;
    }
}
