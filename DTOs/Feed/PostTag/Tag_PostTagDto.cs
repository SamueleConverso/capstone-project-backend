using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.Post;

namespace Capstone_Project.DTOs.Feed.PostTag;

public class Tag_PostTagDto
{
    public Guid PostTagId
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

    public Tag_PostTag_PostDto Post
    {
        get; set;
    }
}
