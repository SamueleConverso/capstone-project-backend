using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_Project.DTOs.Feed.Videogame;

public class CreateVideogameRequestDto
{
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

    public string? Picture
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

    public string? ReleaseDate
    {
        get; set;
    }

    public string? Platform
    {
        get; set;
    }

    public string? AgeRating
    {
        get; set;
    }

    public string? Contributors
    {
        get; set;
    }

    public string? Price
    {
        get; set;
    }

    public string? IsHidden
    {
        get; set;
    }

    public string? IsAvailableForPurchase
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

    public bool? IsDeleted
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
