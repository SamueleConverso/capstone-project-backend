using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.CartVideogame;

public class UpdateCartVideogameRequestDto
{
    [Required]
    public required int Quantity
    {
        get; set;
    } = 1;

    public DateTime? UpdatedAt
    {
        get; set;
    }
}
