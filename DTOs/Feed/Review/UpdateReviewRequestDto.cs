using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_Project.DTOs.Feed.Review;

public class UpdateReviewRequestDto
{
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
    //[Range(1, 10)]
    public required string Rating
    {
        get; set;
    }

    [Required]
    public required string Recommend
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

    public DateTime? UpdatedAt
    {
        get; set;
    }

    // public bool? IsDeleted
    // {
    //     get; set;
    // }

    [JsonIgnore]
    public IFormFile? PictureFile { get; set; }
}
