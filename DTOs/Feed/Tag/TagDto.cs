using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.PostTag;

namespace Capstone_Project.DTOs.Feed.Tag;

public class TagDto
{
    public Guid TagId
    {
        get; set;
    }

    [Required]
    public required string Entry
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

    public ICollection<Tag_PostTagDto> PostTags
    {
        get; set;
    }
}
