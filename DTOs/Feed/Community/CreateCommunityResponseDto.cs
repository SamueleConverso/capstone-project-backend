using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Community;

public class CreateCommunityResponseDto
{
    [Required]
    public required string Message
    {
        get; set;
    }
}
