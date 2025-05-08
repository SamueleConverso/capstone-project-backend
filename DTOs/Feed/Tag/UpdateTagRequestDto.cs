using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Tag;

public class UpdateTagRequestDto
{
    [Required]
    public required string Entry
    {
        get; set;
    }

    public DateTime? UpdatedAt
    {
        get; set;
    }
}
