using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Community;

public class ApplicationUser_CommunityDto
{
    public Guid CommunityId
    {
        get; set;
    }

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

    public bool? IsPrivate
    {
        get; set;
    }

    public bool? IsHidden
    {
        get; set;
    }

    [Required]
    public required int MaxMembers
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
}
