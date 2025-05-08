using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Capstone_Project.DTOs.Feed.Post
{
    public class CreatePostRequestDto
    {
        [Required]
        public required string Text
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

        public string? Mood
        {
            get; set;
        }

        public string? IsLookingForGamers
        {
            get; set;
        }

        public string? IsLookingForDevelopers
        {
            get; set;
        }

        public string? IsLookingForEditors
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

        public string? IsInUserFeed
        {
            get; set;
        }

        public string? IsInGameFeed
        {
            get; set;
        }

        public string? IsInCommunityFeed
        {
            get; set;
        }

        public string? IsHidden
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

        public string? VideogameId
        {
            get; set;
        }

        public string? CommunityId { get; set; }

        [JsonIgnore]
        public IFormFile? PictureFile { get; set; }
    }
}
