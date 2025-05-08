using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Tag;

public class CreateTagRequestDto
{
    [Required]
    public required string Entry
    {
        get; set;
    }

    public bool? IsDeleted
    {
        get; set;
    }
}
