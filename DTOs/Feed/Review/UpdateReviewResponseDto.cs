using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Review;

public class UpdateReviewResponseDto
{
    [Required]
    public required string Message
    {
        get; set;
    }
}
