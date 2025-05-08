using Capstone_Project.DTOs.Feed.ApplicationUser;
using Capstone_Project.DTOs.Feed.PostLike;
using Capstone_Project.DTOs.Feed.PostTag;
using Capstone_Project.DTOs.Feed.Videogame;
using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.Post
{
    public class Comment_PostDto
    {
        public Guid PostId
        {
            get; set;
        }

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

        public bool? IsLookingForGamers
        {
            get; set;
        }

        public bool? IsLookingForDevelopers
        {
            get; set;
        }

        public bool? IsLookingForEditors
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

        public bool? IsInUserFeed
        {
            get; set;
        }

        public bool? IsInGameFeed
        {
            get; set;
        }

        public bool? IsInCommunityFeed
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }

        public int? Likes
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

        public ICollection<Comment_Post_PostLikeDto> PostLikes
        {
            get; set;
        }

        public ICollection<Comment_Post_PostTagDto> PostTags
        {
            get; set;
        }

        public Comment_Post_ApplicationUserDto? ApplicationUser
        {
            get; set;
        }

        public Comment_Post_VideogameDto? Videogame
        {
            get; set;
        }
    }
}
