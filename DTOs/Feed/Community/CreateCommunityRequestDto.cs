using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.Community;

public class CreateCommunityRequestDto
{
    [Required]
    public required string Type
    {
        get; set;
    }

    [Required]
    public required string Name
    {
        get; set;
    }

    public string? ExtraName
    {
        get; set;
    }

    [Required]
    public required string Description
    {
        get; set;
    }

    public string? ExtraDescription
    {
        get; set;
    }

    public string? Picture
    {
        get; set;
    }

    public string? Cover
    {
        get; set;
    }

    public string? Link
    {
        get; set;
    }

    public string? IsPrivate
    {
        get; set;
    }

    public string? IsHidden
    {
        get; set;
    }

    [Required]
    public required string MaxMembers
    {
        get; set;
    }

    [Required]
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

    public string? ApplicationUserId
    {
        get; set;
    }

    [JsonIgnore]
    public IFormFile? PictureFile { get; set; }

    [JsonIgnore]
    public IFormFile? CoverFile { get; set; }
}
