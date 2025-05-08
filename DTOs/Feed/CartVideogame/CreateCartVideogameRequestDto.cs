using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.CartVideogame;

public class CreateCartVideogameRequestDto
{
    [Required]
    public required int Quantity
    {
        get; set;
    } = 1;

    public bool? IsDeleted
    {
        get; set;
    }

    [Required]
    public required Guid CartId
    {
        get; set;
    }

    [Required]
    public required Guid VideogameId
    {
        get; set;
    }
}
