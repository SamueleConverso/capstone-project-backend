using System;
using System.ComponentModel.DataAnnotations;
using Capstone_Project.DTOs.Feed.ApplicationUser;

namespace Capstone_Project.DTOs.Feed.Videogame;

public class Review_VideogameDto
{
    public Guid VideogameId
    {
        get; set;
    }

    [Required]
    public required string Title
    {
        get; set;
    }

    public string? Subtitle
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

    [Required]
    public required string Genre
    {
        get; set;
    }

    [Required]
    public required string Picture
    {
        get; set;
    }

    public string? Cover
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

    public DateTime? ReleaseDate
    {
        get; set;
    }

    public string? Platform
    {
        get; set;
    }

    public int? AgeRating
    {
        get; set;
    }

    public string? Contributors
    {
        get; set;
    }

    public decimal? Price
    {
        get; set;
    }

    public bool? IsHidden
    {
        get; set;
    }

    public bool? IsAvailableForPurchase
    {
        get; set;
    }

    public string? Country
    {
        get; set;
    }

    public string? City
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

    public Review_Videogame_ApplicationUserDto? ApplicationUser
    {
        get; set;
    }
}
