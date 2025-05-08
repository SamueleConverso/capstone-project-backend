using System.ComponentModel.DataAnnotations;

namespace Capstone_Project.DTOs.Feed.ApplicationUser
{
    public class Post_CommentLike_ApplicationUserDto
    {
        [Required]
        public required string ApplicationUserId
        {
            get; set;
        }

        [Required]
        public required string FirstName
        {
            get; set;
        }

        [Required]
        public required string LastName
        {
            get; set;
        }

        [Required]
        public required DateOnly BirthDate
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

        public string? DisplayName
        {
            get; set;
        }

        public string? Avatar
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

        public bool? IsGamer
        {
            get; set;
        }

        public bool? IsDeveloper
        {
            get; set;
        }

        public bool? IsEditor
        {
            get; set;
        }

        public string? DeveloperRole
        {
            get; set;
        }

        public string? Bio
        {
            get; set;
        }

        public string? Title
        {
            get; set;
        }

        public bool? IsHidden
        {
            get; set;
        }
    }
}
