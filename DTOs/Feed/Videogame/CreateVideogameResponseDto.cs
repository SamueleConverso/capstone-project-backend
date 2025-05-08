using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Videogame;

public class CreateVideogameResponseDto
{
    [Required]
    public required string Message
    {
        get; set;
    }
}
