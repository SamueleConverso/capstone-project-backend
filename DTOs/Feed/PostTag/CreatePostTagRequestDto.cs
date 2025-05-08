using System;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.PostTag;

public class CreatePostTagRequestDto
{
    public bool? IsDeleted
    {
        get; set;
    }

    [Required]
    public required Guid PostId
    {
        get; set;
    }

    [Required]
    public required Guid TagId
    {
        get; set;
    }
}
