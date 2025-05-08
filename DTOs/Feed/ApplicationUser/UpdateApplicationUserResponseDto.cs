using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.ApplicationUser;

public class UpdateApplicationUserResponseDto
{
    [Required]
    public required string Message
    {
        get; set;
    }
}
