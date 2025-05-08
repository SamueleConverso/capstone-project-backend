using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Review;

public class ApplicationUser_ReviewDto
{
    public Guid ReviewId
    {
        get; set;
    }

    [Required]
    public required string Title
    {
        get; set;
    }

    [Required]
    public required string Text
    {
        get; set;
    }

    [Required]
    [Range(1, 10)]
    public int Rating
    {
        get; set;
    }

    [Required]
    public required bool Recommend
    {
        get; set;
    }

    public string? Picture
    {
        get; set;
    }

    public string? Video
    {
        get; set;
    }

    public string? Link
    {
        get; set;
    }

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
}
